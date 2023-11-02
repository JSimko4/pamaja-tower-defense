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

    private Vector3 destination { get => LevelManager.Instance.End.transform.position; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, Speed * Time.deltaTime);
    }
}
