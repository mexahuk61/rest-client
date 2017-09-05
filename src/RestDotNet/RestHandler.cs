using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RestDotNet.Deserializers;

namespace RestDotNet
{
    public class RestHandler : IRestHandler
    {
        private readonly Func<CancellationToken, Task<HttpResponseMessage>> _request;
        private readonly IDeserializerFactory _deserializerFactory;
        private readonly Dictionary<HttpStatusCode, Action<IDeserializer, string>> _сallbacks;

        public RestHandler(Func<CancellationToken, Task<HttpResponseMessage>> request,
            IDeserializerFactory deserializerFactory)
        {
            _request = request;
            _deserializerFactory = deserializerFactory;
            _сallbacks = new Dictionary<HttpStatusCode, Action<IDeserializer, string>>();
        }
        
        public void RegisterCallback(HttpStatusCode code, Action action) 
            => _сallbacks.Add(code, (deserializer, content) => action());

        public void RegisterCallback<TReponse>(HttpStatusCode code, Action<TReponse> action) 
            => _сallbacks.Add(code, (deserializer, content) => action(deserializer.Deserialize<TReponse>(content)));

        public Task HandleAsync()
            => HandleAsync(CancellationToken.None);

        public async Task HandleAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage message = await _request(cancellationToken);
            string content = message.Content != null
                ? await message.Content.ReadAsStringAsync()
                : string.Empty;
            HttpStatusCode code = message.StatusCode;

            if (!_сallbacks.ContainsKey(code)) throw new UnhandledResponseException(code, content);
            IDeserializer deserializer = _deserializerFactory.GetDeserializer(message.Content?.Headers.ContentType.MediaType);
            _сallbacks[code](deserializer, content);
        }
    }
}