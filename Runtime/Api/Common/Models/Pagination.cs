using Newtonsoft.Json;

namespace PlayerMake.Api
{
    public class Pagination
    {
        [JsonProperty("skip")]
        public int Skip { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}