using System;
using Newtonsoft.Json;

namespace PlayerMake.Api
{
    public class DeveloperValidation
    {
        [Serializable]
        public class User
        {
            [JsonProperty("id")]
            public string Id;

            [JsonProperty("tier")]
            public string Tier;
        }
    }
}
