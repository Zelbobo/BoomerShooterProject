using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject weaponMesh;

    [Space, SerializeField] protected WeaponObject weaponObject;

    [Header("Stats")]
    [SerializeField] protected float damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected float impactForce;

    [Header("UI")]
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private float diactivateDelay;

    [Header("Anim")]
    [SerializeField] protected Animator weaponAnimator;

    #region [PublicVars]

    public WeaponObject GetWeaponObject => weaponObject;

    #endregion

    #region [PrivateVars]

    private bool isHitMarkerActivated = false;

    protected float nextTime2Fire;

    protected readonly string ANIM_USEWEAPON = "UseWeapon";

    #endregion

    protected virtual void Start()
    {
        if (weaponObject != null)
        {
            SetWeaponObject(weaponObject);
        }
    }

    public abstract void UseWeapon();

    #region [Utils]

    public virtual void ActivateWeapon(bool isActivate)
    {
        weaponMesh.SetActive(isActivate);
    }

    public virtual void SetWeaponObject(WeaponObject weaponObject)
    {
        damage = weaponObject.GetDamage;
        fireRate = weaponObject.GetFireRate;
        impactForce = weaponObject.GetImpactForce;
    }

    #endregion

    #region [HitMarker]

    protected void ActivateHitMarker()
    {
        if ((isHitMarkerActivated) || (hitMarker == null))
        {
            return;
        }

        isHitMarkerActivated = true;
        hitMarker.SetActive(true);
        Invoke(nameof(DiactivateHitMarker), diactivateDelay);
    }

    private void DiactivateHitMarker()
    {
        isHitMarkerActivated = false;
        hitMarker.SetActive(false);
    }

    #endregion
}
