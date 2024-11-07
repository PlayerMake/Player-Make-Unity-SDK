using Newtonsoft.Json;

namespace PlayerMake.Api
{
    public class PlayerLoginResponse : Response
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
