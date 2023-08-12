using UnityEngine;

public class AmmoCollectable : Collectable
{
    [SerializeField] private WeaponObject weaponObject;
    [SerializeField] private int ammoValue;

    public WeaponObject GetWeaponObject => weaponObject;
    public int GetAmmoValue => ammoValue;

    protected override bool Collect(PlayerStats playerStats)
    {
        playerStats.TryGetComponent(out WeaponManager weaponManager);
        return weaponManager.CollectAmmo(this);
    }
}
