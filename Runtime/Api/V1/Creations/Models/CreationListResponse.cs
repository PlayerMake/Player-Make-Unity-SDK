using Newtonsoft.Json;
using System.Collections.Generic;

namespace PlayerMake.Api
{
    public class CreationListResponse : Response
    {
        [JsonProperty("data")]
        public List<Creation> Data { get; set; } = new List<Creation>();

        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; } = new Pagination();
    }
}