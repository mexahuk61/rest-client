using System.Threading;
using System.Threading.Tasks;

namespace RestDotNet
{
    public interface IRestRequest
    {
        IRestHandler Handler { get; }

        Task ExecuteAsync();

        Task ExecuteAsync(CancellationToken cancellationToken);
    }

    public interface IRestRequest<TResponse>
    {
        IRestHandler Handler { get; }

        Task<TResponse> ExecuteAsync();

        Task<TResponse> ExecuteAsync(CancellationToken cancellationToken);
    }
}