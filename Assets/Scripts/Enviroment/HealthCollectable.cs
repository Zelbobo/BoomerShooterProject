using UnityEngine;

public class HealthCollectable : Collectable
{
    [SerializeField] private float healthDeal;

    protected override bool Collect(PlayerStats playerStats)
    {
        return playerStats.AddHealth(healthDeal);
    }
}
