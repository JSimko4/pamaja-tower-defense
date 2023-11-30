using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Ally : MonoBehaviour
{
    [SerializeField]
    private GameObject unitPrefab;

    private Tile spawnLocation;

    // Start is called before the first frame update
    void Start()
    {
      // spawnLocation = LevelManager.Instance.End;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
