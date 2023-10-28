using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    private float range;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float attackCooldown;

    public float Range { get => range; }

    public int Damage { get => damage; }

    public float AttackCooldown { get => attackCooldown; }

    private bool canAttack = false;
    private float attackTimer;


    private List<Monster> MonstersInRange;

    public Monster Target { get => MonstersInRange[0]; }

    public void RangeRescale()
    {
        transform.localScale = new Vector3(Range, Range);
    }
    // Start is called before the first frame update
    void Start()
    {
        MonstersInRange = new List<Monster>();
        RangeRescale();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
    private void Attack()
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

        if (canAttack && Target != null && Target.IsAlive)
        {
            SpawnProjectile();
            canAttack = false;
        }
    }

    private void SpawnProjectile()
    {
        //TODO
    }


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Monster")
        {
            MonstersInRange.Add(other.GetComponent<Monster>());
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {    //exit 2D also handles when monster dies and is inactive

        if (other.tag == "Monster")
        {
            MonstersInRange.Remove(other.GetComponent<Monster>());
        }
    }
}
