using RestDotNet.Deserializers;
using RestDotNet.Serializers;

namespace RestDotNet
{
    public class RestClientOptions
    {
        public RestClientOptions()
        {
            DefaultSerializer = new JsonSerializer();
            DeserializerFactory = new DeserializerFactory(new PlainTextDeserializer());
        }

        public ISerializer DefaultSerializer { get; }

        public IDeserializerFactory DeserializerFactory { get; }
    }
}