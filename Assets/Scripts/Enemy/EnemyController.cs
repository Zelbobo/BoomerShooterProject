using System.Collections;
using UnityEngine;

public class EnemyController : AIController
{
    [Header("FOV")]
    [SerializeField] private float radius;
    [SerializeField] private float fovAngle;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask obstaclesMask;

    [Header("Rotation")]
    [SerializeField] private float speedRotation;
    [SerializeField] private Transform body;

    #region [PublicVars]

    public float GetRaduis => radius;
    public float GetFOVAnge => fovAngle;
    public CreatureStats GetCurrentPlayer => currentPlayer;

    #endregion

    #region [PrivateVars]

    private CreatureStats currentPlayer;

    private Coroutine checkingPlayer;

    private AIAttack aIAttack;

    #endregion

    protected override void Start()
    {
        base.Start();

        aIAttack = GetComponent<AIAttack>();
    }

    public void OnPlayerEnter(PlayerStats player)
    {
        if (checkingPlayer != null)
        {
            StopCoroutine(checkingPlayer);
        }

        checkingPlayer = StartCoroutine(CheckPlayer());
    }

    public void OnPlayerExit(PlayerStats player)
    {
        currentPlayer = null;

        if (checkingPlayer != null)
        {
            StopCoroutine(checkingPlayer);
        }
    }

    private IEnumerator CheckPlayer()
    {
        while (currentPlayer == null)
        {
            CastRay();

            yield return new WaitForFixedUpdate();
        }
    }

    private void CastRay()
    {
        Collider[] obstacles = Physics.OverlapSphere(transform.position, radius, playerMask);

        if (obstacles.Length > 0)
        {
            Transform target = obstacles[0].transform;
            Vector3 direction2Target = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, direction2Target) < fovAngle / 2)
            {
                float distance2Target = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, direction2Target, distance2Target, obstaclesMask))
                {
                    if (target.TryGetComponent(out PlayerStats m_player))
                    {
                        StartCoroutine(AttackPlayer(m_player));
                        StopCoroutine(checkingPlayer);
                    }
                }
            }
        }
    }

    public void HitFromPlayer(CreatureStats player)
    {
        StartCoroutine(AttackPlayer(player));
    }

    private IEnumerator AttackPlayer(CreatureStats player)
    {
        navMeshAgent.isStopped = false;
        currentPlayer = player;

        if (aiState == AIState.Patrol)
        {
            StopPatrol();
        }

        while (currentPlayer != null)
        {
            if (aIAttack.IsCanAttack(currentPlayer.transform))
            {
                navMeshAgent.isStopped = true;
                Stopped();
                Rotate2Player(currentPlayer.transform);
                aIAttack.AttackPlayer(currentPlayer);
            }
            else
            {
                navMeshAgent.isStopped = false;
                Move2Point(currentPlayer.transform);
            }

            yield return null;
        }

        switch (aiState)
        {
            case AIState.Idle:
                {
                    navMeshAgent.isStopped = true;
                    Stopped();
                }
                break;
            case AIState.Patrol:
                {
                    StartPatrol();
                }
                break;
        }
    }

    private void Rotate2Player(Transform point)
    {
        viewDirection = new Vector3(point.position.x, transform.position.y, point.position.z) - transform.position;
        body.forward = Vector3.Slerp(body.forward, viewDirection.normalized, Time.deltaTime * speedRotation);
    }
}
