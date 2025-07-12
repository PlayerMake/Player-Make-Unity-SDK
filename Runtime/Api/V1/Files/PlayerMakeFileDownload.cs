using System;
using System.Threading.Tasks;
using GLTFast.Loading;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMakeFileDownload : IDownload
{
    private readonly Uri uri;
    private readonly string authToken;
    private UnityWebRequest request;

    public PlayerMakeFileDownload(Uri uri, string authToken)
    {
        this.uri = uri;
        this.authToken = authToken;
    }

    public async Task<bool> WaitAsync()
    {
        request = UnityWebRequest.Get(uri);
        request.SetRequestHeader("Authorization", $"Bearer {authToken}");
        request.downloadHandler = new DownloadHandlerBuffer();

#if UNITY_2023_1_OR_NEWER
        await request.SendWebRequest();
#else
        var op = request.SendWebRequest();
        while (!op.isDone)
            await Task.Yield();
#endif

        return !request.isHttpError && !request.isNetworkError;
    }

    public byte[]? Data => request.downloadHandler?.data;

    public string? Error => request.error;

    public string? Text => request.downloadHandler?.text;

    public bool Success => request.downloadHandler.isDone

    public bool? IsBinary => throw new NotImplementedException();

    public void Dispose()
    {
        request?.Dispose();
    }
}