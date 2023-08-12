using UnityEngine;

public class DamageZone : MonoBehaviour
{
    [SerializeField] private float damageDeal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerStats playerStats))
        {
            playerStats.TakeDamage(damageDeal);
        }
    }
}
