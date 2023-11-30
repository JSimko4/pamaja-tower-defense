using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Ally : MonoBehaviour
{
    [SerializeField]
    private GameObject unitPrefab;

    [SerializeField]
    private int health;

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
        
    }
}
