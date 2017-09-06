using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace RestDotNet
{
    public class RestClient : IRestClient, IDisposable
    {
        private readonly HttpClient _client;
        private readonly RequestBuilder _requestBuilder;
        private Action<HttpRequestHeaders> _headersModifyer;

        public RestClient(Uri baseAddress)
            : this(baseAddress, options => { })
        {
        }

        public RestClient(Uri baseAddress, HttpMessageHandler messageHandler)
            : this(baseAddress, messageHandler, options => { })
        {
        }

        public RestClient(Uri baseAddress, Action<RestClientOptions> optionsAccessor)
            : this(baseAddress, new HttpClientHandler(), optionsAccessor)
        {
        }

        public RestClient(Uri baseAddress, 
            HttpMessageHandler messageHandler,
            Action<RestClientOptions> optionsAccessor)
        {
            _client = new HttpClient(messageHandler) { BaseAddress = baseAddress };
            //_client.DefaultRequestHeaders.Accept.Clear();
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));
            _headersModifyer = headers => { };

            var options = new RestClientOptions();
            optionsAccessor(options);
            _requestBuilder = new RequestBuilder(_client, options.DefaultSerializer, options.DeserializerFactory, _headersModifyer);
        }

        //public IResponse<TResponse> Get<TResponse, TRequest>(string uri, TRequest request)
        //    => Get<TResponse>(uri + _queryConverter.Serialize(request));

        public IRestRequest<TResponse> Get<TResponse>(string uri) 
            => _requestBuilder.CreateRequest<TResponse>(uri, HttpMethod.Get);

        public IRestRequest Post(string uri, object request) 
            => _requestBuilder.CreateRequest(uri, HttpMethod.Post, request);

        public IRestRequest<TResponse> Post<TResponse>(string uri, object request) 
            => _requestBuilder.CreateRequest<TResponse>(uri, HttpMethod.Post, request);

        public IRestRequest Put(string uri, object request) 
            => _requestBuilder.CreateRequest(uri, HttpMethod.Put, request);

        public IRestRequest<TResponse> Put<TResponse>(string uri, object request) 
            => _requestBuilder.CreateRequest<TResponse>(uri, HttpMethod.Put, request);

        public IRestRequest Delete(string uri) 
            => _requestBuilder.CreateRequest(uri, HttpMethod.Delete);

        public IRestRequest<TResponse> Delete<TResponse>(string uri) 
            => _requestBuilder.CreateRequest<TResponse>(uri, HttpMethod.Delete);

        public RestClient UseHeaders(Action<HttpRequestHeaders> headersAccessor)
        {
            _headersModifyer = headersAccessor;
            return this;
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}