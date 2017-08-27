using System;
using System.Net;

namespace RestDotNet
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