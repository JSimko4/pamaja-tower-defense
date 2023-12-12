using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField]
    private float range;

    [SerializeField]
    private string towerName;

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

    // UPGRADE
    [SerializeField]
    private int upgradePrice;

    [SerializeField]
    private float upgradedRange;

    [SerializeField]
    private int upgradedDamage;

    private bool isUpgraded;

    public float ProjectileSpeed { get => projectileSpeed; }
    public int Price { get => price; }
    public float Range { get => range; }
    public string TowerName { get => towerName; }
    public int Damage { get => damage; }

    public bool IsUpgraded { get => isUpgraded; }
    public int UpgradePrice { get => upgradePrice; }
    public float UpgradedRange { get => upgradedRange; }
    public int UpgradedDamage { get => upgradedDamage; }

    public float AttackCooldown { get => attackCooldown; }

    protected bool canAttack = false;
    protected float attackTimer;


    protected List<Monster> MonstersInRange;

    public Monster Target { get => MonstersInRange.Count > 0 ? MonstersInRange[0] : null; }


    public List<Monster> MonstersInTowerRange { get => MonstersInRange; }

    private bool isPlaced = false;

    private SpriteRenderer spriteRenderer;
    private GameObject upgradeStar;

    // Returns false if player does not have enough gold to upgrade or tower is upgraded already
    public virtual bool Upgrade()
    {
        if (GameManager.Instance.Gold < upgradePrice || isUpgraded) return false;

        GameManager.Instance.Gold -= upgradePrice;

        range = upgradedRange;
        damage = upgradedDamage;
        price += upgradePrice;
        isUpgraded = true;

        upgradeStar.SetActive(true);
        RangeRescale();

        return true;
    }

    public void Sell()
    {
        float sellPenalty = 0.3f;
        int sellingPrice = (int)(Price * (1- sellPenalty));
        GameManager.Instance.Gold += sellingPrice;

        // Destroy parent object which contains the tower range script (tower logic) and its sprite
        Destroy(transform.parent.gameObject);
    }
    public void ToggleRangeVisible(bool value)
    {
        if(!spriteRenderer)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        spriteRenderer.enabled = value;
    }

    public void Place()
    {
        Tile.ClickedTile.PlaceTower(this);
        isPlaced = true;
        ToggleRangeVisible(false);
    }

    public void RangeRescale()
    {
        transform.localScale = new Vector3(Range, Range);
    }
    // Start is called before the first frame update
    void Start()
    {
        MonstersInRange = new List<Monster>();
        isPlaced = false;
        isUpgraded = false;
        RangeRescale();

        Transform findUpgradeStar = transform.parent.Find("upgradeStar");
        upgradeStar = findUpgradeStar.gameObject;
        upgradeStar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaced)
            Attack();
    }
    protected virtual void Attack()
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
        var projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
        projectile.Init(this);
    }

    protected void SpawnProjectile(Monster monster)
    {
        var projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
        projectile.Init(monster, this);
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
