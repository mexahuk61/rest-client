namespace RestDotNet
{
    public interface IRestClient
    {
        IRestRequest<TResponse> Get<TResponse>(string url);

        //IResponse<TResponse> Get<TResponse, TRequest>(string url, TRequest request)
        //    where TRequest : class;

        IRestRequest<TResponse> Post<TResponse>(string url, object data);

        IRestRequest Post(string url, object data);

        IRestRequest<TResponse> Put<TResponse>(string url, object data);

        IRestRequest Put(string url, object data);

        IRestRequest<TResponse> Delete<TResponse>(string url);

        IRestRequest Delete(string url);
    }
}