using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Monster target;

    protected Tower parentTower;

    protected float speed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveToTarget();
    }

    public virtual void Init(Tower parent)
    {
        parentTower = parent;
        target = parent.Target;
        speed = parent.ProjectileSpeed;

        transform.position = parent.transform.position;
    }

    public virtual void Init(Monster monster, Tower parent)
    {
        parentTower = parent;
        speed = parent.ProjectileSpeed;
        target = monster;

        transform.position = parent.transform.position;
    }

    protected virtual void MoveToTarget()
    {
        if (target != null && target.IsAlive)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);

            Vector2 direction = target.transform.position - transform.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            Destruct();
        }
    }

    protected virtual void HitTarget()
    {
        Debug.Log("Hit monster");
        target.TakeDamage(parentTower);
    }

    protected virtual void Destruct()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Monster" && other.gameObject == target.gameObject) //check for correct target hit
        { 
            HitTarget();
            //some animator trigger
            Destruct();
        }
    }
}
