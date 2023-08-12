using UnityEngine;

public class HitscanBullet : Bullet
{
    [SerializeField] private TrailRenderer trail;

    public TrailRenderer GetTrail => trail;
}