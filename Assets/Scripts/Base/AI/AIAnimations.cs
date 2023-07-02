using UnityEngine;

public class AIAnimations : MonoBehaviour
{
    [SerializeField] protected Animator _animator;

    #region [AnimNames]

    private readonly string IS_RUN = "IsRun";

    #endregion

    public void SetIsRun(bool _value)
    {
        _animator.SetBool(IS_RUN, _value);
    }
}
