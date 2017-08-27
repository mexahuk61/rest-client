using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestDotNet.Converters;

namespace RestDotNet
{
    public class RestClient : IRestClient, IDisposable
    {
        private readonly string _mediaType;
        private readonly HttpClient _httpClient;
        private readonly IJsonConverter _jsonConverter;
        private readonly IQueryConverter _queryConverter;

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
            _mediaType = "application/json";
            
            _httpClient = new HttpClient(messageHandler) { BaseAddress = baseAddress };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));

            var options = new RestClientOptions();
            optionsAccessor(options);
            _jsonConverter = options.JsonConverter;
            _queryConverter = options.QueryConverter;
        }

        //public Action<HttpClient> BeforeExecuted;

        public IRestHandler<TResponse> Get<TResponse>(string url)
        {
            Func<CancellationToken, Task<HttpResponseMessage>> func = Request(token => _httpClient.GetAsync(url, token));
            return CreateHandler<TResponse>(func);
        }

        public IRestHandler<TResponse> Get<TResponse, TRequest>(string url, TRequest request)
            where TRequest : class 
            => Get<TResponse>(url + _queryConverter.Serialize(request));

        public IRestHandler<TResponse> Post<TResponse>(string url, object request)
        {
            StringContent content = GetContent(request);
            Func<CancellationToken, Task<HttpResponseMessage>> func = Request(token => _httpClient.PostAsync(url, content, token));
            return CreateHandler<TResponse>(func);
        }

        public IRestHandler Post(string url, object request) 
            => Post<string>(url, request);

        public IRestHandler<TResponse> Put<TResponse>(string url, object request)
        {
            StringContent content = GetContent(request);
            Func<CancellationToken, Task<HttpResponseMessage>> func = Request(token => _httpClient.PutAsync(url, content, token));
            return CreateHandler<TResponse>(func);
        }

        public IRestHandler Put(string url, object request) 
            => Put<string>(url, request);

        public IRestHandler<TResponse> Delete<TResponse>(string url)
        {
            Func<CancellationToken, Task<HttpResponseMessage>> func = Request(token => _httpClient.DeleteAsync(url, token));
            return CreateHandler<TResponse>(func);
        }

        public IRestHandler Delete(string url) 
            => Delete<string>(url);

        private StringContent GetContent(object request)
        {
            string json = _jsonConverter.Serialize(request);
            return new StringContent(json, Encoding.UTF8, _mediaType);
        }

        private Func<CancellationToken, Task<HttpResponseMessage>> Request(Func<CancellationToken, Task<HttpResponseMessage>> request)
        {
            //BeforeExecuted?.Invoke(_httpClient);
            return request;
        }

        private IRestHandler<TResponse> CreateHandler<TResponse>(Func<CancellationToken, Task<HttpResponseMessage>> func)
        {
            return new RestHandler<TResponse>(func, _jsonConverter);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}