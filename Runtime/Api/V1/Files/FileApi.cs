using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace PlayerMake.Api
{
    public static class FileApi
    {
        public static async Task<Texture2D> DownloadImageAsync(string url, string apiKey)
        {
            var webRequest = UnityWebRequestTexture.GetTexture(url);
            webRequest.SetRequestHeader("x-api-key", apiKey);
            var asyncOperation = webRequest.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (webRequest.error != null)
            {
                Debug.Log(apiKey);
                Debug.Log(url);
            }

            return DownloadHandlerTexture.GetContent(webRequest);
        }
    }
}
