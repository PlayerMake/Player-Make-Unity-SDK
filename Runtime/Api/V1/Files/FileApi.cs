using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace PlayerMake.Api
{
    public static class FileApi
    {
        public static async Task<byte[]> DownloadFileAsync(string url, string apiKey)
        {
            using var webRequest = UnityWebRequest.Get(url);
            webRequest.SetRequestHeader("x-api-key", apiKey);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
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

            Debug.Log(webRequest.downloadHandler.data.Length);

            return webRequest.downloadHandler.data;
        }

        public static async Task<Texture2D> DownloadImageAsync(string url, string apiKey)
        {
            using var webRequest = UnityWebRequestTexture.GetTexture(url);
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

        public static Texture2D TextureFromBytes(byte[] bytes)
        {
            var texture = new Texture2D(1, 1);
            texture.LoadImage(bytes);
            return texture;
        }
    }
}
