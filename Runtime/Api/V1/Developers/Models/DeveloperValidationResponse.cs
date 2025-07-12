using Newtonsoft.Json;
using PlayerMake.Api;

public class DeveloperValidationResponse : Response
{
    [JsonProperty("data")]
    public DeveloperValidation Data { get; set; }
}
