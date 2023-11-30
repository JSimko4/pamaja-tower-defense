using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private bool isAlive;
    public bool IsAlive { get => isAlive; }

    [SerializeField]
    private float speed;
    [SerializeField]
    private int health;
    [SerializeField]
    private int damage;
    [SerializeField]
    private int attackCooldown;


    public float Speed { get => speed; }
    public int Health { get => health; }
    public int Damage { get => damage; }
    public int AttackCooldown { get => attackCooldown; }

    public int MaxHealth { get; private set; }
    public float MaxSpeed { get; private set; }

    public Path path;
    public Tile DestinationTile { get => LevelManager.Instance.End; }
    public Tile startTile;
    public Tile nextTile;

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = health;
        MaxSpeed = speed;
        isAlive = true;
        path = PathFinding.Instance.GetPath(startTile, DestinationTile);
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
}
