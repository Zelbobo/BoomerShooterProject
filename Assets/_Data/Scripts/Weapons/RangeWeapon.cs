using TMPro;
using UnityEngine;

public abstract class RangeWeapon : Weapon
{
    [Header("RangeStats")]
    [SerializeField] protected int maxAmmo;
    [SerializeField] protected int maxAmmoInClip;
    [SerializeField] protected float reloadTime;

    [Header("RangeUI")]
    [SerializeField] private TextMeshProUGUI textAmmo;

    #region [PublicVars]

    public int CurrentAmmo
    {
        get => currentAmmo;
        set
        {
            currentAmmo = Mathf.Clamp(value, 0, maxAmmo);

            if (textAmmo != null)
            {
                textAmmo.text = $"{currentAmmoInClip}/{currentAmmo}";
            }
        }
    }
    public int CurrentAmmoInClip
    {
        get => currentAmmoInClip;
        set
        {
            currentAmmoInClip = value;

            if (textAmmo != null)
            {
                textAmmo.text = $"{currentAmmoInClip}/{currentAmmo}";
            }
        }
    }

    #endregion

    #region [PrivateVars]

    protected int currentAmmo;
    protected int currentAmmoInClip;

    private bool isReloading = false;

    #endregion

    public override void UseWeapon()
    {
        if ((Time.time >= nextTime2Fire) && (CurrentAmmoInClip > 0))
        {
            nextTime2Fire = Time.time + 1f / fireRate;
            Shoot();
            CurrentAmmoInClip--;
        }
        else if (CurrentAmmoInClip <= 0)
        {
            Reload();
        }
    }

    protected abstract void Shoot();

    #region [Utils]

    public bool AddAmmo(int _value)
    {
        if (CurrentAmmo >= maxAmmo)
        {
            return false;
        }

        CurrentAmmo += _value;
        return true;
    }

    public override void SetWeaponObject(WeaponObject weaponObject)
    {
        RangeWeaponObject rangeWeaponObject = (RangeWeaponObject)weaponObject;

        base.SetWeaponObject(rangeWeaponObject);
        maxAmmo = rangeWeaponObject.GetMaxAmmo;
        maxAmmoInClip = rangeWeaponObject.GetMaxAmmoInClip;
        reloadTime = rangeWeaponObject.GetReloadTime;
        CurrentAmmo = maxAmmo;
        CurrentAmmoInClip = maxAmmoInClip;
    }

    #endregion

    #region [Reload]

    public void Reload()
    {
        if ((currentAmmo <= 0) || (isReloading) || (CurrentAmmoInClip >= maxAmmoInClip))
        {
            return;
        }

        isReloading = true;

        //Ќачать производить анимацию перезар€дки

        Invoke(nameof(FinishReloading), reloadTime);
    }

    protected void FinishReloading()
    {
        int needAmmo = maxAmmoInClip - currentAmmoInClip;
        int addAmmo = CurrentAmmo >= needAmmo ? needAmmo : CurrentAmmo;

        CurrentAmmoInClip += addAmmo;
        CurrentAmmo -= addAmmo;

        isReloading = false;
    }

    #endregion
}
