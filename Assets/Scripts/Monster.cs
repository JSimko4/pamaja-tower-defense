using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : Unit
{
    private float stunnedDuration;
    public Ally fightingAlly = null;

    private bool canAttack = false;
    private float attackTimer;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isStunned { get => stunnedDuration > 0; }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        destinationTile = LevelManager.Instance.End;
        path = PathFinding.Instance.GetPath(startTile, destinationTile);
        nextTile = startTile;
    }

    // Update is called once per frame
    void Update()
    {
        // do not move when stunned
        if (isStunned)
        {
            animator.SetBool("IsMoving", false);
            stunnedDuration -= Time.deltaTime;
            return;
        }

        if (fightingAlly != null && fightingAlly.IsAtGatherTile)
        {
            animator.SetBool("IsMoving", false);
            Attack();
        }
        else
        {
            animator.SetBool("IsMoving", true);
            Move();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            isAlive = false;
            Destroy(gameObject);
        }
    }


    public void TakeDamage(Tower tower)
    {
        health -= tower.Damage;

        if(health <= 0)
        {
            isAlive = false;
            Destroy(gameObject);
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

        if (canAttack && fightingAlly != null && fightingAlly.IsAlive)
        {
            fightingAlly.TakeDamage(damage);
            canAttack = false;
        }
    }

    private void Move()
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
            Tile potentionalNextTile = path.GetNextTile();
            if (potentionalNextTile != null)
            {
                nextTile = potentionalNextTile;
            }
            // Infiltrate base 
            else
            {
                int lives = 1;
                GameManager.Instance.DecreaseLives(lives);
                Destroy(gameObject);
            }
        }
    }

    public void ApplyStun(float duration)
    {
        stunnedDuration = duration;
    }

    public void ApplySlow(int percentage)
    {
        speed = MaxSpeed * (1 - percentage/100f);
    }

    public void RemoveSlow()
    {
        speed = MaxSpeed;
    }
}
