using UnityEngine;

public abstract class MoveController : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float defaultSpeed;

    protected abstract void Move();
}
