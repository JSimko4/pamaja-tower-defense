using UnityEngine;

public class Ally : Unit
{
    [SerializeField]
    private int price;

    public static Tile GatherTile;
    private Tile previousGatherTile;
    private Tile lastTile;

    public int Price { get => price; }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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

    private void getPathToGatherPoint()
    {
        path = PathFinding.Instance.GetPath(lastTile, GatherTile);
        GatherTile.PresentAllies.Remove(this);
        Debug.Log("removing ally from gather tile");
        previousGatherTile = GatherTile;
    }

    public void addAllyToGatherTile()
    {
        Debug.Log("reached gather");
        GatherTile.PresentAllies.Add(this);
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

                // Reached gather tile
                if (GatherTile == lastTile) addAllyToGatherTile();

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
    }

    public void ApplyHeal(int amount)
    {
        var newHealth = health + amount;
        if(newHealth > MaxHealth)
            newHealth = MaxHealth;
        health = newHealth;
    }
}
