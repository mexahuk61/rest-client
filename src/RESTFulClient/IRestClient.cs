namespace RESTFulClient
{
    public interface IRestClient
    {
        IRestHandler<TResponse> Get<TResponse>(string url);

        IRestHandler<TResponse> Post<TResponse>(string url, object data);

        IRestHandler Post(string url, object data);

        IRestHandler<TResponse> Put<TResponse>(string url, object data);

        IRestHandler Put(string url, object data);

        IRestHandler<TResponse> Delete<TResponse>(string url);

        IRestHandler Delete(string url);
    }
}