using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Ally : Unit
{
    [SerializeField]
    private int price;

    public int Price { get => price; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // CHECK IF GATHER POINT TILE CHANGED
        
    }
}
