using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutManager : MonoBehaviour
{
    [SerializeField]
    private GameObject knightPrefab;
    [SerializeField]
    private GameObject elfPrefab;

    [SerializeField]
    private GameObject meteorPrefab;
    [SerializeField]
    private GameObject freezePrefab;
    [SerializeField]
    private GameObject healPrefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SkillButton.SelectSpell(meteorPrefab);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SkillButton.SelectSpell(freezePrefab);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SkillButton.SelectSpell(healPrefab);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            AllyButton.spawnAlly(knightPrefab);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            AllyButton.spawnAlly(elfPrefab);
        }
        // Reset skill hover when right clicked 
        else if (Input.GetMouseButtonDown(1))
        {
            SkillButton.ResetHoverPrefab();
        }
    }
}
