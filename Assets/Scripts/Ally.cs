using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Ally : Unit
{
    [SerializeField]
    private int price;

    public static Tile GatherTile;
    private Tile previousGatherTile;
    private Tile lastTile;

    public int Price { get => price; }

    // Start is called before the first frame update
    void Start()
    {
        path = PathFinding.Instance.GetPath(startTile, destinationTile);
        nextTile = startTile;
        lastTile = startTile;
        previousGatherTile = LevelManager.Instance.End;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    public void TakeDamage(Monster monster)
    {
        health -= monster.Damage;

        if (health <= 0)
        {
            isAlive = false;
            Destroy(gameObject);
        }
    }

    // GET NEW PATH AND START RUNNING TO IT IF GATHER POINT CHANGED
    private void getPathToGatherPoint()
    {
        path = PathFinding.Instance.GetPath(lastTile, GatherTile);
        previousGatherTile = GatherTile;
    }

    private void Move()
    {
        if (nextTile != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextTile.transform.position, Speed * Time.deltaTime);
            // Get next tile after we already moved to the tile we were heading to
            if (transform.position == nextTile.transform.position)
            {
                lastTile = nextTile;

                if (GatherTile != previousGatherTile) getPathToGatherPoint();
                nextTile = path.GetNextTile();
            }
        }
        else if (GatherTile != previousGatherTile)
        {
            getPathToGatherPoint();
            nextTile = path.GetNextTile();
        }
    }
}
