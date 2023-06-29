using System.Collections;
using UnityEngine;

public class HitscanWeapon : Weapon
{
    [SerializeField] private LayerMask hitLayer;

    [Header("Bullet")]
    [SerializeField] private HitscanBullet hitscanBullet;
    [SerializeField] private Transform bulletSpawnPoint;

    [SerializeField] private bool isBulletSpread;
    [SerializeField] private Vector3 bulletSpread;

    #region [PrivateVars]

    private float nextTime2Fire;

    #endregion

    #region [Components]

    private CreatureStats creatureStats;
    private Camera _camera;

    #endregion

    private void Start()
    {
        _camera = Camera.main;
        creatureStats = GetComponentInParent<CreatureStats>();
    }

    public override void UseWeapon()
    {
        if (Time.time >= nextTime2Fire)
        {
            nextTime2Fire = Time.time + 1f / weaponObject.GetFireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        Ray ray = new Ray(_camera.transform.position, _camera.transform.forward);
        RaycastHit hit;

        HitscanBullet bullet = Instantiate(hitscanBullet, bulletSpawnPoint.position, Quaternion.identity);

        if (Physics.Raycast(ray, out hit, float.MaxValue, hitLayer))
        {
            StartCoroutine(SpawnHitscanBullet(bullet, hit.point));

            if (hit.transform.TryGetComponent(out CreatureStats creatureStats))
            {
                if (creatureStats.GetTeamID == this.creatureStats.GetTeamID)
                {
                    return;
                }

                ActivateHitMarker();
                creatureStats.TakeDamage(GetWeaponObject.GetDamage);
            }

            if ((hit.transform.TryGetComponent(out Rigidbody rigidbody)) && (creatureStats == null))
            {
                rigidbody.AddForce(hit.point * 10f, ForceMode.Impulse);
            }
        }
        else
        {
            StartCoroutine(SpawnHitscanBullet(bullet, bulletSpawnPoint.position + GetDirection() * 1000));
        }
    }

    private IEnumerator SpawnHitscanBullet(HitscanBullet bullet, Vector3 hitPoint)
    {
        Vector3 startPosition = bullet.transform.position;
        float distance = Vector3.Distance(bullet.transform.position, hitPoint);
        float remainingDistance = distance;

        while (remainingDistance > 0)
        {
            bullet.transform.position = Vector3.Lerp(startPosition, hitPoint, 1 - (remainingDistance / distance));
            remainingDistance -= bullet.GetSpeed * Time.deltaTime;

            yield return null;
        }

        bullet.transform.position = hitPoint;
        //Instantiate impact
        Destroy(bullet.gameObject, bullet.GetTrail.time);
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = bulletSpawnPoint.forward;

        if (isBulletSpread)
        {
            direction += new Vector3(
                Random.Range(-bulletSpread.x, bulletSpread.x),
                Random.Range(-bulletSpread.y, bulletSpread.y),
                Random.Range(-bulletSpread.z, bulletSpread.z)
                );

            direction.Normalize();
        }

        return direction;
    }
}