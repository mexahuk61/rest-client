using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using RESTfulClient.Converters;

namespace RESTfulClient
{
    internal class RestHandler<TResponse> : IRestHandler<TResponse>
    {
        private readonly Func<Task<HttpResponseMessage>> _request;
        private readonly Dictionary<HttpStatusCode, Action<string>> _сallbacks;
        private readonly IJsonConverter _jsonConverter;

        public RestHandler(Func<Task<HttpResponseMessage>> request,
            IJsonConverter jsonConverter)
        {
            _request = request;
            _jsonConverter = jsonConverter;
            _сallbacks = new Dictionary<HttpStatusCode, Action<string>>();
        }
        
        public void RegisterCallback(HttpStatusCode code, Action action)
        {
            _сallbacks.Add(code, content => action());
        }

        public void RegisterCallback<TReponse>(HttpStatusCode code, Action<TReponse> action)
        {
            _сallbacks.Add(code, content => action(_jsonConverter.Deserialize<TReponse>(content)));
        }

        public async Task SuccessAsync(Action action)
        {
            RestResponseMessage message = await HandleAsync();
            if (message.IsSuccessStatusCode)
            {
                action();
            }
        }

        public async Task SuccessAsync(Action<TResponse> action)
        {
            RestResponseMessage message = await HandleAsync();
            if (message.IsSuccessStatusCode)
            {
                TResponse response = _jsonConverter.Deserialize<TResponse>(message.Content);
                action(response);
            }
        }

        async Task IRestHandler.ExecuteAsync()
        {
            await HandleAsync();
        }

        public async Task<TResponse> ExecuteAsync()
        {
            RestResponseMessage message = await HandleAsync();
            if (!message.IsSuccessStatusCode)
                return default(TResponse);
            
            TResponse response = _jsonConverter.Deserialize<TResponse>(message.Content);
            return response;
        }
        
        private async Task<RestResponseMessage> HandleAsync()
        {
            HttpResponseMessage response = await _request();
            string content = response.Content != null 
                ? await response.Content.ReadAsStringAsync() 
                : string.Empty;
            HttpStatusCode code = response.StatusCode;

            if (code == HttpStatusCode.OK)
                return new RestResponseMessage(true, content);

            if (!_сallbacks.ContainsKey(code)) throw new UnhandledResponseException(code, content);
            _сallbacks[code](content);
            return new RestResponseMessage(false, content);
        }
    }
}