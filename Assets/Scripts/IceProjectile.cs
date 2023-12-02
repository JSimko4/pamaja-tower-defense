using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProjectile : Projectile
{
    private List<Monster> allMonsters;
    public override void Init(Tower parent)
    {
        base.Init(parent);

        //rescale based on parent range scale
        transform.localScale = new Vector3(parent.Range, parent.Range);

        allMonsters = new List<Monster>(parent.MonstersInTowerRange);

        // hit all monsters in the moment projectile spawns
        HitMonsters();

        // disappear after a short delay
        Invoke("Destruct", 0.5f);
    }

    // do not move to target
    protected override void MoveToTarget()
    {

    }

    // do not collide with other monsters because it would cause anothor Hit and Destruct
    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void HitMonsters()
    {
        foreach (Monster monster in allMonsters)
        {
            monster.TakeDamage(parentTower);
        }
    }
}
