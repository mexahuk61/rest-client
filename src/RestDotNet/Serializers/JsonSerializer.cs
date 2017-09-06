using Newtonsoft.Json;

namespace RestDotNet.Serializers
{
    public class JsonSerializer : ISerializer
    {
        public string MediaType => "application/json";

        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}