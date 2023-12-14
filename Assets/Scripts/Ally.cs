using System.Collections.Generic;
using UnityEngine;

public class Ally : Unit
{
    [SerializeField]
    private int price;

    [SerializeField]
    private string allyName;
    [SerializeField]
    private int maxFightCapacity;

    public List<Monster> MonstersInRange = new List<Monster>();
    public List<Monster> FightingMonsters = new List<Monster>();

    public Tile lastTile;

    public static Tile GatherTile;
    public Tile previousGatherTile;

    private bool canAttack = false;
    private float attackTimer;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public int Price { get => price; }
    public string AllyName { get => allyName; }
    public bool IsAtGatherTile { get => lastTile == GatherTile; }
    public Monster Target { get => FightingMonsters.Count > 0 ? FightingMonsters[0] : null; }

    public bool CanFightMoreMonsters { get => FightingMonsters.Count < maxFightCapacity; }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        path = PathFinding.Instance.GetPath(startTile, destinationTile);
        nextTile = path.GetNextTile();
        lastTile = startTile;
        previousGatherTile = GatherTile;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.GameLost)
        {
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsFighting", false);
            return;
        }

        // Add more monsters to fight if can fight aditional monsters
        // This is useful in cases when one of the enemies the ally was fighting with died
        // In this case we look for enemies in the box collider and start fighting them
        if (IsAtGatherTile && CanFightMoreMonsters && FightingMonsters.Count < MonstersInRange.Count)
        {
            foreach (Monster monster in MonstersInRange.GetRange(0, Mathf.Min(MonstersInRange.Count, maxFightCapacity)))
            {
                // Check if someone isnt fighting this monster already
                if (monster.fightingAlly == null)
                {
                    FightingMonsters.Add(monster);
                    monster.fightingAlly = this;
                }
            }
        }

        if (IsAtGatherTile && Target != null)
        {
            animator.SetBool("IsMoving", false);
            animator.SetBool("IsFighting", true);
            Attack();
        }
        else
        {
            animator.SetBool("IsFighting", false);
            Move();
        }
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
            Target.TakeDamage(damage);
            canAttack = false;
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        unitCanvas.RedrawHealthBar();

        if (health <= 0)
        {
            isAlive = false;
            Destroy(gameObject);
        }
    }

    private void getPathToGatherPoint()
    {
        path = PathFinding.Instance.GetPath(lastTile, GatherTile);
        previousGatherTile = GatherTile;
    }

    private void Move()
    {
        if (nextTile != null)
        {
            Vector2 currentPosition = transform.position;
            Vector2 targetPosition = nextTile.transform.position;

            // Flip sprite based on direction
            if (targetPosition.x > currentPosition.x)
                spriteRenderer.flipX = false; // moving right
            else if (targetPosition.x < currentPosition.x)
                spriteRenderer.flipX = true; // moving left

            bool isMoving = (currentPosition != targetPosition);
            animator.SetBool("IsMoving", isMoving);

            // Existing movement code
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, Speed * Time.deltaTime);
            // Get next tile after we already moved to the tile we were heading to
            if (transform.position == nextTile.transform.position)
            {
                lastTile = nextTile;

                // Get new path if gather tile changed
                if (GatherTile != previousGatherTile) getPathToGatherPoint();

                nextTile = path.GetNextTile();
            }
        }
        // Get new path if gather tile changed
        else if (GatherTile != previousGatherTile)
        {
            getPathToGatherPoint();
            nextTile = path.GetNextTile();
        }
        else { 
            animator.SetBool("IsMoving", false); 
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Monster")
        {
            Monster monster = other.GetComponent<Monster>();

            if (CanFightMoreMonsters && IsAtGatherTile && monster.fightingAlly == null)
            {
                FightingMonsters.Add(monster);
                monster.fightingAlly = this;
            }

            MonstersInRange.Add(monster);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {    
        if (other.tag == "Monster")
        {
            Monster monster = other.GetComponent<Monster>();
            monster.fightingAlly = null;

            MonstersInRange.Remove(monster);
            FightingMonsters.Remove(monster);
        }
    }

    public void ApplyHeal(int amount)
    {
        var newHealth = health + amount;
        if(newHealth > MaxHealth)
            newHealth = MaxHealth;
        health = newHealth;

        unitCanvas.RedrawHealthBar();
    }
}
