using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower
{

    [SerializeField]
    private int slowAmountPercentage;

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.tag == "Monster")
        {
            Monster target = other.GetComponent<Monster>();
            target.ApplySlow(slowAmountPercentage);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        if (other.tag == "Monster")
        {
            other.GetComponent<Monster>().RemoveSlow();
        }
    }
}
