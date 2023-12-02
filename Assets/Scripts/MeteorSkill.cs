using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSkill : Skill
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float startHeight;

    [SerializeField]
    private float speed;

    private Vector3 destination;

    [SerializeField]
    private GameObject explosionPrefab;


    // Start is called before the first frame update
    void Start()
    {
        //transform.localScale = new Vector3(Radius, Radius);
        destination = new Vector3(transform.position.x, transform.position.y, -9);
        transform.position = new Vector3(transform.position.x, transform.position.y + startHeight, -9);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        Debug.Log("Meteor move " + transform.position + " - " + destination);
        if(transform.position == destination)
        {
            Explode();
        }
    }

    void Explode()
    {
        var explosion = Instantiate(explosionPrefab).GetComponent<Explosion>();

        explosion.Init(transform.position, Radius, damage);

        Destroy(gameObject);
    }
}
