using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/New weapon")]
public class WeaponObject : ScriptableObject
{
    [SerializeField] protected float damage;
    [SerializeField] protected float fireRate;

    public float GetFireRate => fireRate;
    public float GetDamage => damage;
}
