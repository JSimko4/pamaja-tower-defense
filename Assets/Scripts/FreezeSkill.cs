using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeSkill : Skill
{
    [SerializeField]
    private int damage;

    [SerializeField]
    private float stunDuration;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Explosion>().Init(transform.position, Radius, damage, stunDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
