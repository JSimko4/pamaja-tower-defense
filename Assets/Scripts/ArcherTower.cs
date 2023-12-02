using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ArcherTower : Tower
{
    [SerializeField]
    private int arrowTargetsCount;

    public List<Monster> Targets { get => MonstersInRange.Count > 0 ? MonstersInRange.GetRange(0, Mathf.Min(MonstersInRange.Count, arrowTargetsCount)) : null; }

    protected override void Attack()
    {
        // attack speed mechanic
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= AttackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }

        if (canAttack && Targets != null)
        {
            foreach (var target in Targets)
            {
                if (target.IsAlive)
                {
                    SpawnProjectile(target);
                    canAttack = false;
                }
            }
        }
    }
}
