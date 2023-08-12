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

    protected float nextTime2Fire;

    #endregion

    public virtual void AttackPlayer(CreatureStats player)
    {
        if (Time.time >= nextTime2Fire)
        {
            nextTime2Fire = Time.time + 1f / attackSpeed;
            player.TakeDamage(damage);
        }   
    }

    public virtual bool IsCanAttack(Transform target)
    {
        return Vector3.Distance(target.position, transform.position) <= attackRange;
    }
}
