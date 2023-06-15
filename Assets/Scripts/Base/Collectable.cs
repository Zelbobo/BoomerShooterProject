using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStats playerStats))
        {
            if (Collect(playerStats))
            {
                Destroy(gameObject);
            }
        }
    }

    protected abstract bool Collect(PlayerStats playerStats);
}
