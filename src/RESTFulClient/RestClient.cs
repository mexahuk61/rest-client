using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RESTfulClient
{
    public class RestClient : IRestClient, IDisposable
    {
        private readonly string _mediaType;
        private readonly HttpClient _httpClient;

        public RestClient(Uri baseAddress)
        {
            _mediaType = "application/json";
            _httpClient = new HttpClient { BaseAddress = baseAddress };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));
        }

        public Action<HttpClient> BeforeExecuted;

        public IRestHandler<TResponse> Get<TResponse>(string url)
        {
            return new RestHandler<TResponse>(Request(() => _httpClient.GetAsync(url)));
        }

        public IRestHandler<TResponse> Post<TResponse>(string url, object request)
        {
            StringContent content = GetContent(request);
            return new RestHandler<TResponse>(Request(() => _httpClient.PostAsync(url, content)));
        }

        public IRestHandler Post(string url, object request)
        {
            return Post<string>(url, request);
        }

        public IRestHandler<TResponse> Put<TResponse>(string url, object request)
        {
            StringContent content = GetContent(request);
            return new RestHandler<TResponse>(Request(() => _httpClient.PutAsync(url, content)));
        }

        public IRestHandler Put(string url, object request)
        {
            return Put<string>(url, request);
        }

        private StringContent GetContent(object request)
        {
            string json = JsonConvert.SerializeObject(request);
            return new StringContent(json, Encoding.UTF8, _mediaType);
        }

        public IRestHandler<TResponse> Delete<TResponse>(string url)
        {
            return new RestHandler<TResponse>(Request(() => _httpClient.DeleteAsync(url)));
        }

        private Func<Task<HttpResponseMessage>> Request(Func<Task<HttpResponseMessage>> request)
        {
            BeforeExecuted?.Invoke(_httpClient);
            return request;
        }

        public IRestHandler Delete(string url)
        {
            return Delete<string>(url);
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
