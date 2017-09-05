using Newtonsoft.Json;

namespace RestDotNet.Deserializers
{
    public class JsonDeserializer : IDeserializer
    {
        public string ContentType => "application/json";

        public T Deserialize<T>(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new DeserializationException("Failed to deserialize. Value is empty.");
            try
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (JsonException exception)
            {
                throw new DeserializationException(exception.Message);
            }
        }
    }
}