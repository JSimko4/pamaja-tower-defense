using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageProjectile : Projectile
{
    [SerializeField]
    private GameObject explosionPrefab;

    private void Explode()
    {
        var explosion = Instantiate(explosionPrefab).GetComponent<Explosion>();

        explosion.Init(transform.position, ((MageTower)parentTower).SplashRange, parentTower.Damage);

        Destruct();
    }

    // collide with any monster not necessarily your target
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Monster")
        {
            Explode();
        }
    }
}
