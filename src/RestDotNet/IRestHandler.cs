using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RestDotNet
{
    public interface IRestHandler
    {
        void RegisterCallback(HttpStatusCode code, Action action);

        void RegisterCallback<TReponse>(HttpStatusCode code, Action<TReponse> action);

        Task ExecuteAsync();

        Task ExecuteAsync(CancellationToken cancellationToken);
    }

    public interface IRestHandler<TReponse> : IRestHandler
    {
        new Task<TReponse> ExecuteAsync();

        new Task<TReponse> ExecuteAsync(CancellationToken cancellationToken);
    }
}