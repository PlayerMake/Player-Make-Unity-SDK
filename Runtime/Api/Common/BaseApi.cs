using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace PlayerMake.Api
{
    public abstract class BaseApi
    {

        protected virtual async Task<TResponse> PostAsync<TResponse, TPayload>(RequestWithBody<TPayload> request) where TResponse : Response, new()
        {
            var serializedPayload = JsonConvert.SerializeObject(request.Payload, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            return await SendAsync<TResponse>(new BaseRequest
            {
                Headers = request.Headers,
                Method = "POST",
                SerializedPayload = serializedPayload,
                Url = request.Url
            });
        }

        protected virtual async Task<TResponse> GetAsync<TResponse>(Request request) where TResponse : Response, new()
        {
            return await SendAsync<TResponse>(new BaseRequest
            {
                Headers = request.Headers,
                Method = "GET",
                Url = request.Url
            });
        }

        private async Task<TResponse> SendAsync<TResponse>(BaseRequest request) where TResponse : Response, new()
        {
            using var webRequest = new UnityWebRequest();
            webRequest.url = request.Url;
            webRequest.method = request.Method;
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            if (request.Headers != null)
            {
                foreach (var header in request.Headers)
                {
                    webRequest.SetRequestHeader(header.Key, header.Value);
                }
            }

            if (!string.IsNullOrEmpty(request.SerializedPayload))
            {
                var payloadBytes = Encoding.UTF8.GetBytes(request.SerializedPayload);
                webRequest.uploadHandler = new UploadHandlerRaw(payloadBytes);
            }

            var asyncOperation = webRequest.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (webRequest.result == UnityWebRequest.Result.Success)
                return JsonConvert.DeserializeObject<TResponse>(webRequest.downloadHandler.text);

            Debug.Log(webRequest.url);
            Debug.Log(webRequest.error);

            return new TResponse()
            {
                IsSuccess = false,
                Status = webRequest.responseCode,
                Error = webRequest.error,
            };
        }

        private class BaseRequest
        {
            public string Url { get; set; }

            public string Method { get; set; }

            public string SerializedPayload { get; set; }

            public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        }

    }
}
