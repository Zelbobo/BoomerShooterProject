using UnityEngine;

public class RangeEnemyAnimations : EnemyAnimations
{
    #region [AnimNames]

    private readonly string PREPARE_ATTACK = "IsPrepareAttack";

    #endregion

    public void SetPrepareAttack(bool _value)
    {
        _animator.SetBool(PREPARE_ATTACK, _value);
    }
}
