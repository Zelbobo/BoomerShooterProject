using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class AIController : MoveController
{
    [Header("AI state")]
    [SerializeField] protected AIState aiState;

    [Header("Patrol")]
    [SerializeField] private Transform[] patrolPoints;

    #region [PublicVars]

    public UnityAction OnPathCompleted { private get; set; }

    public enum AIState
    {
        Idle,
        Patrol
    }

    #endregion

    #region [PrivateVars]

    private NavMeshAgent navMeshAgent;
    private AIAnimations aIAnimations;

    private int currentPatrolPoint = 0;

    private Coroutine checkingDestination;

    protected Vector3 viewDirection;

    #endregion

    protected virtual void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        aIAnimations = GetComponent<AIAnimations>();

        navMeshAgent.speed = defaultSpeed;

        SetState(aiState);
    }

    public void SetState(AIState state)
    {
        switch (state)
        {
            case AIState.Idle:
                {
                    Debug.Log("State idle");
                }
                break;
            case AIState.Patrol:  
                {
                    StartPatrol();
                }
                break;
            default:
                {
                    Debug.LogWarning("None state");
                }
                break;
        }
    }

    public void StartPatrol()
    {
        if (patrolPoints.Length <= 0)
        {
            Debug.LogWarning("Add patrol points");
            return;
        }

        OnPathCompleted = () =>
        {
            currentPatrolPoint++;

            Transform nextPoint = patrolPoints[(currentPatrolPoint + patrolPoints.Length) % patrolPoints.Length];

            Move2Point(nextPoint);
        };

        Transform point = patrolPoints[(currentPatrolPoint + patrolPoints.Length) % patrolPoints.Length];

        Move2Point(point);
    }

    public void StopPatrol()
    {
        OnPathCompleted = null;
    }

    public void Move2Point(Transform point)
    {
        if (point == null)
        {
            Debug.LogWarning("Point is null");
            return;
        }

        aIAnimations.SetIsRun(true);

        if (checkingDestination != null)
        {
            StopCoroutine(checkingDestination);
        }

        navMeshAgent.SetDestination(point.position);

        checkingDestination = StartCoroutine(CheckIsStopped(point));
    }

    private IEnumerator CheckIsStopped(Transform point)
    {
        float distance;

        do
        {
            distance = Vector3.Distance(point.position, transform.position);

            yield return null;
        } while (distance > navMeshAgent.stoppingDistance + 0.2f);

        aIAnimations.SetIsRun(false);
        OnPathCompleted?.Invoke();
    }
}
