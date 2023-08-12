using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed;

    public float GetSpeed => speed;
}
