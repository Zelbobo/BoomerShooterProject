using UnityEngine;

public class PlayerWeapon : WeaponManager
{
    [SerializeField] private Weapon startWeapon = null;

    #region [PublicVars]

    public bool IsShoot { get; set; } = false;

    #endregion

    #region [PrivateVars]

    private int currentWeaponID;

    #endregion

    protected override void Start()
    {
        base.Start();

        EquipWeapon(startWeapon);
    }

    private void Update()
    {
        if (IsShoot)
        {
            UseWeapon();
        }
    }

    private void UseWeapon()
    {
        if (currentWeapon == null)
        {
            return;
        }

        currentWeapon.UseWeapon();
    }

    public void ReloadWeapon()
    {
        if (currentWeapon == null)
        {
            return;
        }

        if (currentWeapon.TryGetComponent(out RangeWeapon weapon))
        {
            weapon.Reload();
        }
    }

    public void ChangeWeapon()
    {
        int weaponID = (weaponsList.Length + currentWeaponID) % weaponsList.Length;
        EquipWeapon(weaponsList[weaponID]);

        currentWeaponID++;
    }
}
