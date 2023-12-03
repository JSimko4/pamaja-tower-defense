using Newtonsoft.Json;
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

    private string mapFileName = "level1";
    private float tileSize;

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
        // Load level data from JSON
        string jsonFilePath = $"Assets/Resources/{mapFileName}.json";
        string jsonText = System.IO.File.ReadAllText(jsonFilePath);
        LevelData levelData = JsonConvert.DeserializeObject<LevelData>(jsonText);

        // Load waves to GameManager
        GameManager.Instance.waves = levelData.Waves;
        
        // Update Waves UI
        UIManager.Instance.SetWave(0, levelData.Waves.Count);

        string[] mapLines = levelData.grid.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
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
                    tile.mapCoordinates = new Vector2Int(i, j);
                    Tiles.Add(new Vector2Int(i, j), tile);
                }
                else if(tileChar == '0') {
                    tile = Instantiate(tile2, spawnPosition, Quaternion.identity).GetComponent<Tile>();
                    tile.TileType = 0;
                    tile.mapCoordinates = new Vector2Int(i, j);
                    Tiles.Add(new Vector2Int(i, j), tile);
                }
            }
        }

        // Assign start tiles
        starts = new List<Tile>();
        for (int i = 0; i < levelData.StartTilePositions.Count; i++)
        {
            starts.Add(Tiles.GetValueOrDefault(new Vector2Int(levelData.StartTilePositions[i][0], levelData.StartTilePositions[i][1])));
        }


        // Assign end tile (base location)
        end = Tiles.GetValueOrDefault(new Vector2Int(levelData.EndTilePosition[0], levelData.EndTilePosition[1]));
        Ally.GatherTile = end;
    }
}
