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



 

    private Vector3 worldStart;

    public GameObject roadTile; 
    public GameObject otherTile; 

    private string mapFileName = "Level1";
    private float tileSize ;
    // Start is called before the first frame update
    void Start()
    {
        

        tileSize = tile1.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        LoadMap();
        worldStart = localCamera.ScreenToWorldPoint(new Vector3(0, Screen.height));


        Vector3 first =  worldStart;
        first.x = first.x + tileSize /2;
        first.y = first.y - tileSize /2;
        first.z = 0;

        Instantiate(tile1, first, Quaternion.identity);
    }




    void LoadMap()
    {
        TextAsset mapText = Resources.Load<TextAsset>(mapFileName);
        string[] mapLines = mapText.text.Split('\n');

        for (int i = 0; i < mapLines.Length; i++)
        {
            for (int j = 0; j < mapLines[i].Length; j++)
            {
                char tileChar = mapLines[i][j];
                Vector2 spawnPosition = new Vector2(j * tileSize, -i * tileSize); // Negative on 'i' to start from top

                if (tileChar == '1')
                {
                    Instantiate(tile1, spawnPosition, Quaternion.identity);
                }
                else if (tileChar == '0')
                {
                    Instantiate(tile2, spawnPosition, Quaternion.identity);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
