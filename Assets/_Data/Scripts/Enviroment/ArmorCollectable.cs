using UnityEngine;

public class ArmorCollectable : Collectable
{
    [SerializeField] private float armorDeal;

    protected override bool Collect(PlayerStats playerStats)
    {
        return playerStats.AddArmor(armorDeal);
    }
}
