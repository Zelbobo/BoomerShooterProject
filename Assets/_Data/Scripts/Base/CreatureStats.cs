using UnityEngine;

public abstract class CreatureStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float health;
    [SerializeField] private int teamID;

    #region [PublicVars]

    public int GetTeamID => teamID;

    #endregion

    public virtual void TakeDamage(float _ammount)
    {
        health -= _ammount;

        if (health > 0)
        {
            return;
        }

        Death();
    }

    protected virtual void Death()
    {
        Debug.Log($"{transform.name} is died");
    }
}
