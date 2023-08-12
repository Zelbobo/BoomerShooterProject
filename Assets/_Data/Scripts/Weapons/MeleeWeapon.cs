using UnityEngine;

public class MeleeWeapon : Weapon
{
    [SerializeField] private LayerMask hitLayer;

    [Header("MeleeStats")]
    [SerializeField] private float attackTiming;

    #region [PrivateVars]

    private bool isAttacking;

    #endregion

    #region [Components]

    private CreatureStats creatureStats;
    private Camera _camera;

    #endregion

    protected override void Start()
    {
        base.Start();

        _camera = Camera.main;
        creatureStats = GetComponentInParent<CreatureStats>();
    }

    public override void UseWeapon()
    {
        if (Time.time >= nextTime2Fire)
        {
            nextTime2Fire = Time.time + 1f / fireRate;
            Attack();
        }
    }

    private void Attack()
    {
        if (isAttacking)
        {
            return;
        }

        isAttacking = true;
        weaponAnimator.SetTrigger(ANIM_USEWEAPON);

        Invoke(nameof(CastRay), attackTiming);
    }

    private void CastRay()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, float.MaxValue, hitLayer))
        {
            if (hit.transform.TryGetComponent(out CreatureStats creatureStats))
            {
                if (creatureStats.GetTeamID == this.creatureStats.GetTeamID)
                {
                    return;
                }

                ActivateHitMarker();
                creatureStats.TakeDamage(damage);

                if (creatureStats.TryGetComponent(out EnemyController enemyController))
                {
                    enemyController.HitFromPlayer(this.creatureStats);
                }
            }

            if ((hit.transform.TryGetComponent(out Rigidbody rigidbody)) && (creatureStats == null))
            {
                rigidbody.AddForce(hit.point * impactForce, ForceMode.Impulse);
            }
        }

        isAttacking = false;
    }

    public override void SetWeaponObject(WeaponObject weaponObject)
    {
        MeleeWeaponObject meleeWeaponObject = (MeleeWeaponObject)weaponObject;

        base.SetWeaponObject(meleeWeaponObject);
        attackTiming = meleeWeaponObject.GetAttackTiming;
    }
}
