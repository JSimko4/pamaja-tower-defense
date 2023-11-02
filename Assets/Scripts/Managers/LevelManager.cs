using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject tile1;

    [SerializeField]
    private GameObject tile2;

    [SerializeField]
    private Camera localCamera;

    public Dictionary<Vector2Int, Tile> Tiles { get; set; }

    private Vector3 worldStart;
    private Vector3 first;

    private string mapFileName = "Level1";
    private float tileSize ;

    private List<Tile> starts;
    private Tile end;

    public Tile End { get { return end; } }

    public Tile getStartTile(int index)
    {
        return starts[index];
    }

    // Start is called before the first frame update
    void Start()
    {
        tileSize = tile1.GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        worldStart = localCamera.ScreenToWorldPoint(new Vector3(0, Screen.height));
        first = worldStart;
        first.x = first.x + tileSize / 2;
        first.y = first.y - tileSize / 2;
        first.z = 0;
        Tiles = new Dictionary<Vector2Int, Tile>();
        LoadMap();
    }

    void LoadMap()
    {
        TextAsset mapText = Resources.Load<TextAsset>(mapFileName);
        string[] mapLines = mapText.text.Split('\n', System.StringSplitOptions.RemoveEmptyEntries);

        for (int i = 0; i < mapLines.Length; i++)
        {
            for (int j = 0; j < mapLines[i].Length; j++)
            {
                char tileChar = mapLines[i][j];
                Vector2 spawnPosition = new Vector2(first.x + j * tileSize, first.y -i * tileSize); // Negative on 'i' to start from top

                Tile tile;

                if (tileChar == '1')
                {
                    tile = Instantiate(tile1, spawnPosition, Quaternion.identity).GetComponent<Tile>();
                    tile.TileType = 1;
                    Tiles.Add(new Vector2Int(i, j), tile);
                }
                else if(tileChar == '0') {
                    tile = Instantiate(tile2, spawnPosition, Quaternion.identity).GetComponent<Tile>();
                    tile.TileType = 0;
                    Tiles.Add(new Vector2Int(i, j), tile);
                }

                
            }
        }

        //TODO programatically set
        starts = new List<Tile>();
        starts.Add(Tiles.GetValueOrDefault(new Vector2Int(0, 1)));
        end = Tiles.GetValueOrDefault(new Vector2Int(9, 6));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
