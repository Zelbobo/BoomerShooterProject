using System.Collections;
using UnityEngine;

public class EnemyController : AIController
{
    [Header("Obstacles")]
    [SerializeField] private LayerMask obstaclesLayer;

    [Header("Rotation")]
    [SerializeField] private float speedRotation;
    [SerializeField] private Transform body;

    #region [PrivateVars]

    private PlayerStats currentPlayer;

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
        checkingPlayer = StartCoroutine(CheckPlayer(player));
    }

    public void OnPlayerExit(PlayerStats player)
    {
        currentPlayer = null;

        if (checkingPlayer != null)
        {
            StopCoroutine(checkingPlayer);
        }
    }

    private IEnumerator CheckPlayer(PlayerStats player)
    {
        while (currentPlayer == null)
        {
            CastRay(player);

            yield return null;
        }
    }

    private void CastRay(PlayerStats player)
    {
        Ray ray = new Ray(transform.position, player.transform.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, obstaclesLayer))
        {
            if (hit.transform.TryGetComponent(out PlayerStats m_player))
            {               
                StartCoroutine(AttackPlayer(m_player));
            }
        }
    }

    private IEnumerator AttackPlayer(PlayerStats player)
    {
        currentPlayer = player;

        if (aiState == AIState.Patrol)
        {
            StopPatrol();
        }

        while (currentPlayer != null)
        {
            if (Vector3.Distance(currentPlayer.transform.position, transform.position) <= aIAttack.GetAttackRange)
            {
                aIAttack.AttackPlayer(currentPlayer);
                Rotate2Player(currentPlayer.transform);
            }
            else
            {
                Move2Point(currentPlayer.transform);
            }

            yield return null;
        }

        CheckPlayer(currentPlayer);

        if (aiState == AIState.Patrol)
        {
            StartPatrol();
        }
    }

    private void Rotate2Player(Transform point)
    {
        viewDirection = new Vector3(point.position.x, transform.position.y, point.position.z) - transform.position;
        body.forward = Vector3.Slerp(body.forward, viewDirection.normalized, Time.deltaTime * speedRotation);
    }
}
