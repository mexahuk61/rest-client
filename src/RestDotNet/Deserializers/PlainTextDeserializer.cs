namespace RestDotNet.Deserializers
{
    public class PlainTextDeserializer : IDeserializer
    {
        public string ContentType => "text/plain";

        public T Deserialize<T>(string content)
        {
            if (typeof(T) != typeof(string) && typeof(T) != typeof(object))
                throw new DeserializationException();

            return (T) (content as object);
        }
    }
}