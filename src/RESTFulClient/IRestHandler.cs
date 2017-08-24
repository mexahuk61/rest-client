using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RESTfulClient
{
    public interface IRestHandler
    {
        void RegisterCallback(HttpStatusCode code, Action action);

        void RegisterCallback<TReponse>(HttpStatusCode code, Action<TReponse> action);

        Task SuccessAsync(Action action);

        Task SuccessAsync(Action action, CancellationToken cancellationToken);

        Task ExecuteAsync();

        Task ExecuteAsync(CancellationToken cancellationToken);
    }

    public interface IRestHandler<TReponse> : IRestHandler
    {
        Task SuccessAsync(Action<TReponse> action);

        Task SuccessAsync(Action<TReponse> action, CancellationToken cancellationToken);

        new Task<TReponse> ExecuteAsync();

        new Task<TReponse> ExecuteAsync(CancellationToken cancellationToken);
    }
}