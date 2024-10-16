using System.Collections.Generic;

namespace PlayerMake.Api
{
    public class Request
    {
        public string Url { get; set; }

        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }

    public class RequestWithBody<T> : Request
    {
        public T Payload { get; set; }
    }
}