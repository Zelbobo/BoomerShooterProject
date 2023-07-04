using UnityEngine;

public class EnemyAnimations : AIAnimations
{
    #region [AnimNames]

    private readonly string ATTACK_TRIGGER = "Attack";

    #endregion

    public void AttackTrigger()
    {
        _animator.SetTrigger(ATTACK_TRIGGER);
    }
}
