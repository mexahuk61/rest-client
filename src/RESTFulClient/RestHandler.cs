using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RESTfulClient.Converters;

namespace RESTfulClient
{
    internal class RestHandler<TResponse> : IRestHandler<TResponse>
    {
        private readonly Func<CancellationToken, Task<HttpResponseMessage>> _request;
        private readonly Dictionary<HttpStatusCode, Action<string>> _сallbacks;
        private readonly IJsonConverter _jsonConverter;

        public RestHandler(Func<CancellationToken, Task<HttpResponseMessage>> request,
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

        public Task SuccessAsync(Action action)
            => SuccessAsync(action, CancellationToken.None);

        public async Task SuccessAsync(Action action, CancellationToken cancellationToken)
        {
            RestResponseMessage message = await HandleAsync(cancellationToken);
            if (!message.IsSuccessStatusCode) return;

            action();
        }

        public Task SuccessAsync(Action<TResponse> action)
            => SuccessAsync(action, CancellationToken.None);

        public async Task SuccessAsync(Action<TResponse> action, CancellationToken cancellationToken)
        {
            RestResponseMessage message = await HandleAsync(cancellationToken);
            if (!message.IsSuccessStatusCode) return;

            TResponse response = _jsonConverter.Deserialize<TResponse>(message.Content);
            action(response);
        }

        Task IRestHandler.ExecuteAsync()
            => ExecuteAsync(CancellationToken.None);

        async Task IRestHandler.ExecuteAsync(CancellationToken cancellationToken)
        {
            await HandleAsync(cancellationToken);
        }

        public Task<TResponse> ExecuteAsync()
            => ExecuteAsync(CancellationToken.None);

        public async Task<TResponse> ExecuteAsync(CancellationToken cancellationToken)
        {
            RestResponseMessage message = await HandleAsync(cancellationToken);
            if (!message.IsSuccessStatusCode) return default(TResponse);
            
            TResponse response = _jsonConverter.Deserialize<TResponse>(message.Content);
            return response;
        }

        private async Task<RestResponseMessage> HandleAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _request(cancellationToken);
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