using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : Skill
{
    [SerializeField]
    private int healAmount;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Explosion>().Init(transform.position, Radius/10, 0, 0, healAmount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
