using Newtonsoft.Json;

namespace RestDotNet.Converters
{
    internal class RestJsonConverter : IJsonConverter
    {
        public T Deserialize<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}