using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected bool isAlive;
    public bool IsAlive { get => isAlive; }

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected int attackCooldown;


    public float Speed { get => speed;  }
    public int Health { get => health; }
    public int Damage { get => damage; }
    public int AttackCooldown { get => attackCooldown; }

    public int MaxHealth { get; private set; }
    public float MaxSpeed { get; private set; }

    // SET DESTINATION AND START TILES WHEN INSTANTIATE-ING THE CHILDREN
    public Tile destinationTile;
    public Tile startTile;
    ///////////////////////////////////////////////////////////////////
    public Tile nextTile;
    public Path path;

    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = health;
        MaxSpeed = speed;
        isAlive = true;
        nextTile = startTile;
    }
}