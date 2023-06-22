using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected GameObject weaponMesh;

    [Space, SerializeField] protected WeaponObject weaponObject;

    [Header("UI")]
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private float diactivateDelay;

    #region [PublicVars]

    public WeaponObject GetWeaponObject => weaponObject;

    #endregion

    #region [PrivateVars]

    private bool isHitMarkerActivated = false;

    #endregion

    public abstract void UseWeapon();

    public virtual void ActivateWeapon(bool isActivate)
    {
        weaponMesh.SetActive(isActivate);
    }

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
}
