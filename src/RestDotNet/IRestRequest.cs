using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RestDotNet
{
    public interface IRestRequest
    {
        void RegisterCallback(HttpStatusCode code, Action action);

        void RegisterCallback<TReponse>(HttpStatusCode code, Action<TReponse> action);

        Task ExecuteAsync();

        Task ExecuteAsync(CancellationToken cancellationToken);
    }

    public interface IRestRequest<TResponse>
    {
        void RegisterCallback(HttpStatusCode code, Action action);

        void RegisterCallback<TReponse>(HttpStatusCode code, Action<TReponse> action);

        Task<TResponse> ExecuteAsync();

        Task<TResponse> ExecuteAsync(CancellationToken cancellationToken);
    }
}