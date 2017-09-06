using System.Collections.Generic;
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
            DeserializerFactory.AddDeserializer(new JsonDeserializer());
        }

        internal IDeserializerFactory DeserializerFactory { get; }

        public ISerializer DefaultSerializer { get; }

        public IEnumerable<IDeserializer> Deserializers => DeserializerFactory.GetDeserializers();
    }
}