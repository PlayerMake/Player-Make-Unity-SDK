using GLTFast;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public static class FileApi
{
    public static async Task<Texture2D> DownloadImageAsync(string url)
    {
        var webRequest = UnityWebRequestTexture.GetTexture(url);
        var asyncOperation = webRequest.SendWebRequest();

        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        return DownloadHandlerTexture.GetContent(webRequest);
    }
}
