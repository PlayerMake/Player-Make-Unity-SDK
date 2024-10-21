using Newtonsoft.Json;

public class PlayerLoginRequest
{
    [JsonProperty("code")]
    public string Code { get; set; }
}
