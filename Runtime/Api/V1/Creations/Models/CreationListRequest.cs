using Newtonsoft.Json;

public class CreationListRequest
{
    public CreationListQueryParams Params { get; set; } = new CreationListQueryParams();
}

public class CreationListQueryParams : PaginationQueryParams
{
    [JsonProperty("projectId")]
    public string ProjectId { get; set; }

    [JsonProperty("playerId")]
    public string PlayerId { get; set; }

    [JsonProperty("assetId")]
    public string AssetId { get; set; }

    [JsonProperty("status")]
    public string[] Status { get; set; }
}
