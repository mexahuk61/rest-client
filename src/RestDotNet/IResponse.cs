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

    public interface IResponse<TResponse> : IResponse
    {
        new Task<TResponse> ExecuteAsync();

        new Task<TResponse> ExecuteAsync(CancellationToken cancellationToken);
    }
}