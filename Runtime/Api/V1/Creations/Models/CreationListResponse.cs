using Newtonsoft.Json;
using PlayerMake.Api;
using System.Collections.Generic;

public class CreationListResponse : Response
{
    [JsonProperty("data")]
    public List<Creation> Data { get; set; } = new List<Creation>();
}
