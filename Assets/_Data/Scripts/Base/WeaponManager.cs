using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] protected Weapon[] weaponsList;

    protected Weapon currentWeapon;

    protected virtual void Start()
    {
        foreach (Weapon weapon in weaponsList)
        {
            weapon.ActivateWeapon(false);
        }
    }

    public virtual void EquipWeapon(Weapon weapon)
    {
        UnequipWeapon();

        currentWeapon = weapon;
        currentWeapon?.ActivateWeapon(true);
    }

    public virtual void UnequipWeapon()
    {
        currentWeapon?.ActivateWeapon(false);
        currentWeapon = null;
    }

    public bool CollectAmmo(AmmoCollectable ammoCollectable)
    {
        RangeWeapon currentWeapon = null;

        foreach (RangeWeapon weapon in weaponsList)
        {
            if (weapon.GetWeaponObject == ammoCollectable.GetWeaponObject)
            {
                currentWeapon = weapon;
                break;
            }
        }

        return currentWeapon.AddAmmo(ammoCollectable.GetAmmoValue);
    }
}
