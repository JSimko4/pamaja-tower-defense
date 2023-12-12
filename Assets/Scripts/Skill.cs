using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField]
    private int manaCost;

    [SerializeField]
    private string skillName;
    [SerializeField]
    private float radius;
    public string SkillName { get => skillName; }
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
