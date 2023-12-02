using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    private int manaCost;

    [SerializeField]
    private float radius;

    [SerializeField]
    private GameObject precastPrefab;

    public GameObject PrecastPrefab { get => precastPrefab; }

    public int ManaCost { get => manaCost; }
    public float Radius { get => radius; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
