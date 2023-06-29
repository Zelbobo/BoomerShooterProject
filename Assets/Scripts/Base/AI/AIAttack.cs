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

    private float nextTime2Fire;

    #endregion

    public virtual void AttackPlayer(PlayerStats player)
    {
        if (Time.time >= nextTime2Fire)
        {
            nextTime2Fire = Time.time + 1f / attackSpeed;
            player.TakeDamage(damage);
        }   
    }
}
