using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RestDotNet
{
    public class RestRequest : IRestRequest
    {
        private readonly IRestHandler _handler;

        public RestRequest(IRestHandler handler)
        {
            _handler = handler;
        }

        public void RegisterCallback(HttpStatusCode code, Action action)
            => _handler.RegisterCallback(code, action);

        public void RegisterCallback<TResult>(HttpStatusCode code, Action<TResult> action)
            => _handler.RegisterCallback(code, action);

        public Task ExecuteAsync() 
            => ExecuteAsync(CancellationToken.None);

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _handler.RegisterCallback(HttpStatusCode.OK, () => { });
            return _handler.HandleAsync(cancellationToken);
        }
    }

    public class RestRequest<TResponse> : RestRequest, IRestRequest<TResponse>
    {
        private readonly IRestHandler _handler;

        public RestRequest(IRestHandler handler)
            : base(handler)
        {
            _handler = handler;
        }

        public new Task<TResponse> ExecuteAsync()
            => ExecuteAsync(CancellationToken.None);

        public new async Task<TResponse> ExecuteAsync(CancellationToken cancellationToken)
        {
            TResponse result = default(TResponse);
            _handler.RegisterCallback(HttpStatusCode.OK, (TResponse content) => result = content);
            await _handler.HandleAsync(cancellationToken);
            return result;
        }
    }
}