namespace RestDotNet.Converters
{
    public interface IQueryConverter
    {
        string Serialize(object request);
    }
}