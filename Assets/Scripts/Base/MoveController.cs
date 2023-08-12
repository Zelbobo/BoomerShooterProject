using UnityEngine;

public abstract class MoveController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float defaultSpeed;

    protected bool isClimbing;

    public virtual void SetClimbing(bool _value) => isClimbing = _value;
}
