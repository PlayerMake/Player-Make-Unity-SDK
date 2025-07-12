using UnityEngine;

namespace PlayerMake.Api
{

    public class PlayerMakeSettings : ScriptableObject
    {
        [SerializeField]
        private string _apiBaseUrl = "https://api.playermake.com";

        public string ProjectId = "";

        public string ApiKey = "";

        public string ApiBaseUrl => _apiBaseUrl;
    }
}
