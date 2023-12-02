using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    // Start is called before the first frame update
    void Start()
    {
        destinationTile = LevelManager.Instance.End;
        path = PathFinding.Instance.GetPath(startTile, destinationTile);
        nextTile = startTile;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
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

    public void ApplySlow(int percentage)
    {
        speed = MaxSpeed * (1 - percentage/100f);
    }

    public void RemoveSlow()
    {
        speed = MaxSpeed;
    }
}
