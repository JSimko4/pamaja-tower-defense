using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    private float stunnedDuration;

    private bool isStunned { get => stunnedDuration > 0; }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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
            stunnedDuration -= Time.deltaTime;
            return;
        }
        Move();
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

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextTile.transform.position, Speed * Time.deltaTime);
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
        Debug.Log("Slow" + speed + "-" + MaxSpeed);
    }

    public void RemoveSlow()
    {
        speed = MaxSpeed;
    }
}
