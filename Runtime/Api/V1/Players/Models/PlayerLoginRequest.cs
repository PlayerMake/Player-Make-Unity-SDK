using Newtonsoft.Json;

namespace PlayerMake.Api
{
    public class PlayerLoginRequest
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("projectId")]
        public string ProjectId { get; set; }
    }
}