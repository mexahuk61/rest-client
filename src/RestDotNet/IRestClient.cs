namespace RestDotNet
{
    public interface IRestClient
    {
        IResponse<TResponse> Get<TResponse>(string url);

        //IResponse<TResponse> Get<TResponse, TRequest>(string url, TRequest request)
        //    where TRequest : class;

        IResponse<TResponse> Post<TResponse>(string url, object data);

        IResponse Post(string url, object data);

        IResponse<TResponse> Put<TResponse>(string url, object data);

        IResponse Put(string url, object data);

        IResponse<TResponse> Delete<TResponse>(string url);

        IResponse Delete(string url);
    }
}