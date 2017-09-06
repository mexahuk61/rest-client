using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestDotNet.Deserializers;
using RestDotNet.Serializers;

namespace RestDotNet
{
    internal class RequestBuilder
    {
        private readonly HttpClient _client;
        private readonly ISerializer _serializer;
        private readonly IDeserializerFactory _deserializerFactory;
        private readonly Action<HttpRequestHeaders> _headersModifyer;

        public RequestBuilder(HttpClient client,
            ISerializer serializer,
            IDeserializerFactory deserializerFactory, 
            Action<HttpRequestHeaders> headersModifyer)
        {
            _client = client;
            _serializer = serializer;
            _deserializerFactory = deserializerFactory;
            _headersModifyer = headersModifyer;
        }

        public IRestRequest CreateRequest(string uri, HttpMethod httpMethod)
        {
            return CreateRequest(token => _client.SendAsync(new HttpRequestMessage(httpMethod, uri), token));
        }

        public IRestRequest CreateRequest(string uri, HttpMethod httpMethod, object content)
        {
            StringContent stringContent = GetContent(content);
            return CreateRequest(token => _client.SendAsync(new HttpRequestMessage(httpMethod, uri) {Content = stringContent}, token));
        }

        public IRestRequest<TResponse> CreateRequest<TResponse>(string uri, HttpMethod httpMethod)
        {
            return CreateRequest<TResponse>(token => _client.SendAsync(new HttpRequestMessage(httpMethod, uri), token));
        }

        public IRestRequest<TResponse> CreateRequest<TResponse>(string uri, HttpMethod httpMethod, object content)
        {
            StringContent stringContent = GetContent(content);
            return CreateRequest<TResponse>(token => _client.SendAsync(new HttpRequestMessage(httpMethod, uri) { Content = stringContent }, token));
        }

        private StringContent GetContent(object request)
        {
            string mediaType = _serializer.MediaType;
            string json = _serializer.Serialize(request);
            return new StringContent(json, Encoding.UTF8, mediaType);
        }

        private IRestRequest CreateRequest(Func<CancellationToken, Task<HttpResponseMessage>> request)
        {
            Func<CancellationToken, Task<HttpResponseMessage>> wrapper = WrapRequest(request);
            return new RestRequest(new RestHandler(wrapper, _deserializerFactory));
        }

        private IRestRequest<TResponse> CreateRequest<TResponse>(Func<CancellationToken, Task<HttpResponseMessage>> request)
        {
            Func<CancellationToken, Task<HttpResponseMessage>> wrapper = WrapRequest(request);
            return new RestRequest<TResponse>(new RestHandler(wrapper, _deserializerFactory));
        }

        private Func<CancellationToken, Task<HttpResponseMessage>> WrapRequest(Func<CancellationToken, Task<HttpResponseMessage>> request)
        {
            return token =>
            {
                _headersModifyer?.Invoke(_client.DefaultRequestHeaders);
                return request(token);
            };
        }
    }
}
