using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public string grid;
    public List<int> startTilePositions; // Can not use 2D array because Jsonutility is not able to parse 2D array
    public List<int> endTilePosition;
}