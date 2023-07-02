using UnityEngine;

public abstract class AIAttack : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float damage;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed;

    #region [PublicVars]

    public float GetAttackRange => attackRange;

    #endregion

    #region [PrivateVars]

    protected EnemyAnimations enemyAnimations;

    protected float nextTime2Fire;

    #endregion

    protected virtual void Start()
    {
        enemyAnimations = GetComponent<EnemyAnimations>();
    }

    public virtual void AttackPlayer(PlayerStats player)
    {
        if (Time.time >= nextTime2Fire)
        {
            nextTime2Fire = Time.time + 1f / attackSpeed;
            player.TakeDamage(damage);
            enemyAnimations.AttackTrigger();
        }   
    }
}
