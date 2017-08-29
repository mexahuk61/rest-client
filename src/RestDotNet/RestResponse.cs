using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RestDotNet
{
    public class RestResponse : IResponse
    {
        public RestResponse(IRestHandler handler)
        {
            Handler = handler;
        }

        public IRestHandler Handler { get; }

        public Task ExecuteAsync() 
            => ExecuteAsync(CancellationToken.None);

        public Task ExecuteAsync(CancellationToken cancellationToken)
        {
            Handler.RegisterCallback(HttpStatusCode.OK, () => { });
            return Handler.HandleAsync(cancellationToken);
        }
    }

    public class RestResponse<TResponse> : RestResponse, IResponse<TResponse>
    {
        public RestResponse(IRestHandler handler)
            : base(handler)
        {
        }

        public new Task<TResponse> ExecuteAsync()
            => ExecuteAsync(CancellationToken.None);

        public new async Task<TResponse> ExecuteAsync(CancellationToken cancellationToken)
        {
            TResponse result = default(TResponse);
            Handler.RegisterCallback(HttpStatusCode.OK, (TResponse content) => result = content);
            await Handler.HandleAsync(cancellationToken);
            return result;
        }
    }
}