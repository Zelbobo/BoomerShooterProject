using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/New weapon")]
public class WeaponObject : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float fireRate;
    [SerializeField] private float impactForce;

    public float GetFireRate => fireRate;
    public float GetDamage => damage;
    public float GetImpactForce => impactForce;
}
