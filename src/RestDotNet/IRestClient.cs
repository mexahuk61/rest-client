namespace RestDotNet
{
    public interface IRestClient
    {
        IRestRequest<TResponse> Get<TResponse>(string uri);

        IRestRequest<TResponse> Post<TResponse>(string uri, object data);

        IRestRequest Post(string uri, object data);

        IRestRequest<TResponse> Put<TResponse>(string uri, object data);

        IRestRequest Put(string uri, object data);

        IRestRequest<TResponse> Delete<TResponse>(string uri);

        IRestRequest Delete(string uri);
    }
}