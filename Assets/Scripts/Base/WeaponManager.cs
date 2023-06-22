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
}
