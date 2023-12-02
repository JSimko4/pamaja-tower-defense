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

    [SerializeField]
    private int price;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private GameObject projectilePrefab;

    public float ProjectileSpeed { get => projectileSpeed; }
    public int Price { get => price; }
    public float Range { get => range; }

    public int Damage { get => damage; }

    public float AttackCooldown { get => attackCooldown; }

    private bool canAttack = false;
    private float attackTimer;


    private List<Monster> MonstersInRange;

    public Monster Target { get => MonstersInRange.Count > 0 ? MonstersInRange[0] : null; }


    public List<Monster> MonstersInTowerRange { get => MonstersInRange; }

    private bool isPlaced = false;

    public void Place()
    {
        Tile.ClickedTile.PlaceTower(this);
        isPlaced = true;
    }

    public void RangeRescale()
    {
        var initialScaleRange = 4;
        transform.localScale = new Vector3(transform.localScale.x / initialScaleRange * Range, transform.localScale.y /initialScaleRange * Range);
    }
    // Start is called before the first frame update
    void Start()
    {
        MonstersInRange = new List<Monster>();
        isPlaced = false;
        RangeRescale();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaced)
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
        Debug.Log("Spawn projectil");
        var projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
        projectile.Init(this);
    }


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("enter " + other.tag);
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
