using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/New melee weapon")]
public class MeleeWeaponObject : WeaponObject
{
    [SerializeField] private float attackTiming;

    public float GetAttackTiming => attackTiming;
}
