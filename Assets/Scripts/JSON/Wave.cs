using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class Wave
{
    [JsonProperty("manaReward")]
    public int manaReward;

    [JsonProperty("spawns")]
    public List<Spawn> Spawns;
}