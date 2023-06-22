using UnityEngine;

public class PlayerWeapon : WeaponManager
{
    [SerializeField] private Weapon startWeapon = null;

    public bool IsShoot { get; set; } = false;

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
}
