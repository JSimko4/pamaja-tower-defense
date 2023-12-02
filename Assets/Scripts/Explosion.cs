using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private int damage;
    private int heal;
    private float stunDuration;

    private bool initialized = false;

    public void Init(Vector3 position, float radius ,int damage=0, float stunDuration =0, int heal =0,  float disappearAfter = 0.3f)
    {
        transform.position = position;
        transform.localScale = new Vector3(radius, radius);

        this.damage = damage;
        this.stunDuration = stunDuration;
        this.heal = heal;

        initialized = true;

        Debug.Log("Explosion INIT");

        Invoke("Destruct", disappearAfter);
    }

    private void Destruct()
    {
        Debug.Log("Explosion destroy");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!initialized)
            return;

        if(other.tag == "Monster")
        {
            var monster = other.GetComponent<Monster>();
            if (damage > 0)
            {
                monster.TakeDamage(damage);
            }
            if(stunDuration > 0)
            {
                monster.ApplyStun(stunDuration);
            }
            

        } else if(other.tag == "Ally")
        {
            if(heal>0)
                other.GetComponent<Ally>().ApplyHeal(heal);
        }
    }

}
