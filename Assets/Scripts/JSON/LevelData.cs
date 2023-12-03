using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    public List<string> grid;

    [JsonProperty("startTilePositions")]
    public List<List<int>> StartTilePositions;

    [JsonProperty("endTilePosition")]
    public List<int> EndTilePosition;

    [JsonProperty("waves")]
    public List<Wave> Waves;
}