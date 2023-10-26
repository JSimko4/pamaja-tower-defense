using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tile1;

    [SerializeField]
    private GameObject tile2;

    [SerializeField]
    private Camera localCamera;


    private float tileSize;

    private Vector3 worldStart;

    // Start is called before the first frame update
    void Start()
    {
        tileSize = tile1.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        worldStart = localCamera.ScreenToWorldPoint(new Vector3(0, Screen.height));


        Vector3 first =  worldStart;
        first.x = first.x + tileSize /2;
        first.y = first.y - tileSize /2;
        first.z = 0;

        Instantiate(tile1, first, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
