using RestDotNet.Deserializers;
using RestDotNet.Serializers;

namespace RestDotNet
{
    public class RestClientOptions
    {
        public RestClientOptions()
        {
            JsonConverter = new JsonSerializer();
            
            DeserializerFactory = new DeserializerFactory();
            DeserializerFactory.AddDeserializer(new JsonDeserializer());
        }

        public ISerializer JsonConverter { get; set; }

        public IDeserializerFactory DeserializerFactory { get; }
    }
}