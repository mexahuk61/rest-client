namespace RestDotNet.Deserializers
{
    public interface IDeserializer
    {
        string ContentType { get; }

        T Deserialize<T>(string content);
    }
}