using Newtonsoft.Json;
using PlayerMake.Api;
using System.Collections.Generic;

public class AssetListResponse : Response
{
    [JsonProperty("data")]
    public List<Asset> Data { get; set; } = new List<Asset>();

    [JsonProperty("pagination")]
    public Pagination Pagination { get; set; } = new Pagination();
}

