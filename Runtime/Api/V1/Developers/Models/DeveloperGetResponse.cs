using Newtonsoft.Json;
using PlayerMake.Api;

public class DeveloperGetResponse : Response
{
    [JsonProperty("data")]
    public Developer Data { get; set; }
}