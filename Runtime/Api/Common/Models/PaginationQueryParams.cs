using Newtonsoft.Json;

namespace PlayerMake.Api
{
    public class PaginationQueryParams
    {
        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("skip")]
        public int Skip { get; set; }
    }
}