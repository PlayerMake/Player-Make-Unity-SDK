using Newtonsoft.Json;

public class Developer
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("shortId")]
    public string ShortId { get; set; }
}
