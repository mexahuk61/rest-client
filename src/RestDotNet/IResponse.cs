using System.Threading;
using System.Threading.Tasks;

namespace RestDotNet
{
    public interface IResponse
    {
        IRestHandler Handler { get; }

        Task ExecuteAsync();

        Task ExecuteAsync(CancellationToken cancellationToken);
    }

    public interface IResponse<TResponse>
    {
        IRestHandler Handler { get; }

        Task<TResponse> ExecuteAsync();

        Task<TResponse> ExecuteAsync(CancellationToken cancellationToken);
    }
}