using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace RestDotNet
{
    internal class HeadersProvider
    {
        private readonly List<Action<HttpRequestHeaders>> _headersAccessors;

        public HeadersProvider()
        {
            _headersAccessors = new List<Action<HttpRequestHeaders>>();
        }

        public void UseHeaders(Action<HttpRequestHeaders> headersAccessor)
        {
            _headersAccessors.Add(headersAccessor);
        }

        public void Build(HttpRequestHeaders headers)
        {
            headers.Clear();
            _headersAccessors.ForEach(accessor => accessor(headers));
        }
    }
}