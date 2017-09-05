namespace RestDotNet.Deserializers
{
    public interface IDeserializerFactory
    {
        IDeserializer GetDeserializer(string contentType);

        void AddDeserializer(IDeserializer deserializer);
    }
}
