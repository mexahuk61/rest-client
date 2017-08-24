namespace RESTfulClient.Converters
{
    public interface IQueryConverter
    {
        string Serialize(object request);
    }
}