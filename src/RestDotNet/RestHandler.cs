using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RestDotNet.Converters;

namespace RestDotNet
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
            => _сallbacks.Add(code, content => action());

        public void RegisterCallback<TReponse>(HttpStatusCode code, Action<TReponse> action) 
            => _сallbacks.Add(code, content => action(_jsonConverter.Deserialize<TReponse>(content)));

        Task IRestHandler.ExecuteAsync()
            => ExecuteAsync(CancellationToken.None);

        Task IRestHandler.ExecuteAsync(CancellationToken cancellationToken) 
            => ExecuteAsync(cancellationToken);

        public Task<TResponse> ExecuteAsync()
            => ExecuteAsync(CancellationToken.None);

        public async Task<TResponse> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _request(cancellationToken);
            string content = response.Content != null
                ? await response.Content.ReadAsStringAsync()
                : string.Empty;
            HttpStatusCode code = response.StatusCode;

            TResponse result = default(TResponse);
            if (code == HttpStatusCode.OK && !_сallbacks.ContainsKey(code))
                RegisterCallback(HttpStatusCode.OK, (TResponse res) => result = res);

            if (!_сallbacks.ContainsKey(code)) throw new UnhandledResponseException(code, content);
            _сallbacks[code](content);
            
            return result;
        }
    }
}