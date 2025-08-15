using UnityEngine;

namespace PlayerMake.Api
{

    public class PlayerMakeSettings : ScriptableObject
    {
        [SerializeField]
        private string _apiBaseUrl = "https://api.playermake.com";

        public string ProjectId = "";

        public string ApiKey = "";

        public int IconCacheFileCountLimit = 100;

        public int IconCacheTotalFileSizeLimitMb = 25;

        public int ModelCacheFileCountLimit = 100;

        public int ModelCacheTotalFileSizeLimitMb = 25;

        public string ApiBaseUrl => _apiBaseUrl;
    }
}
