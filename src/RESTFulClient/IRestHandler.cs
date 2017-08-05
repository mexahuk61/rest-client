using System;
using System.Net;
using System.Threading.Tasks;

namespace RESTfulClient
{
    public interface IRestHandler
    {
        void RegisterCallback(HttpStatusCode code, Action action);

        void RegisterCallback<TReponse>(HttpStatusCode code, Action<TReponse> action);

        Task SuccessAsync(Action action);

        Task ExecuteAsync();
    }

    public interface IRestHandler<TReponse> : IRestHandler
    {
        Task SuccessAsync(Action<TReponse> action);

        new Task<TReponse> ExecuteAsync();
    }
}