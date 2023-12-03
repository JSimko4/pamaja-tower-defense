using Newtonsoft.Json;

[System.Serializable]
public class Spawn
{
    [JsonProperty("monsterIndex")]
    public int monsterIndex;

    [JsonProperty("startTilePositionIndex")]
    public int startTilePositionIndex;

    [JsonProperty("delayBeforeSpawnInSec")]
    public float delayBeforeSpawnInSec;
}