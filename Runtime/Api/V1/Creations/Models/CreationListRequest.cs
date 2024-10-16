using Newtonsoft.Json;

public class CreationListRequest
{
    public CreationListQueryParams Params { get; set; } = new CreationListQueryParams();
}

public class CreationListQueryParams : PaginationQueryParams
{
    [JsonProperty("projectId")]
    public string ProjectId { get; set; }

    [JsonProperty("userId")]
    public string UserId { get; set; }
}
