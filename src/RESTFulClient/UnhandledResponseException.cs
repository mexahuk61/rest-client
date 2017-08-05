using System;
using System.Net;

namespace RESTFulClient
{
    public class UnhandledResponseException : Exception
    {
        public UnhandledResponseException(HttpStatusCode code, string content)
            : base($"Unhandled response with HTTP status code: {code}.")
        {
            Code = code;
            Content = content;
        }

        public HttpStatusCode Code { get; }
        
        public string Content { get; }
    }
}