using Newtonsoft.Json;
using PlayerMake.Api;

public class DeveloperQuotaResponse : Response
{
    [JsonProperty("data")]
    public DeveloperQuota Data { get; set; }
}
