using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/New range weapon")]
public class RangeWeaponObject : WeaponObject
{
    [SerializeField] private int maxAmmo;
    [SerializeField] private int maxAmmoInClip;
    [SerializeField] private float reloadTime;

    public int GetMaxAmmo => maxAmmo;
    public float GetReloadTime => reloadTime;
    public int GetMaxAmmoInClip => maxAmmoInClip;
}
