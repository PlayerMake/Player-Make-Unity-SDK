using Newtonsoft.Json;

public class AssetListRequest
{
    public AssetListQueryParams Params { get; set; } = new AssetListQueryParams();
}

public class AssetListQueryParams : PaginationQueryParams
{
    [JsonProperty("projectId")]
    public string ProjectId { get; set; }
}