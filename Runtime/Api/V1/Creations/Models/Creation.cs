using Newtonsoft.Json;

namespace PlayerMake.Api
{
    public class Creation : IDownloadableIcon, IDownloadableModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("iconUrl")]
        public string IconUrl { get; set; }

        [JsonProperty("creatorPlayerName")]
        public string CreatorPlayerName { get; set; }

        [JsonProperty("creatorPlayerId")]
        public string CreatorPlayerId { get; set; }

        [JsonProperty("asset")]
        public CreationAssetSummary Asset { get; set; }
    }

    public class CreationAssetSummary
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}