using System;
using Newtonsoft.Json;

[Serializable]
public class DeveloperQuota
{
    [JsonProperty("creationQuota")]
    public int GenerationQuota;

    [JsonProperty("downloadQuota")]
    public int DownloadQuota;

    [JsonProperty("currentGenerationCount")]
    public int GenerationCount;

    [JsonProperty("currentDownloadCount")]
    public int RequestCount;

    [JsonProperty("tier")]
    public string Tier;
}
