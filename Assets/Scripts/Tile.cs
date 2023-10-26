using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int TileType { get; set; }

    public static int PathTileType { get => 1; }
    public static int EmptyTileType { get => 0; }

    //get center of tile
    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2),
                               transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y / 2));
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
