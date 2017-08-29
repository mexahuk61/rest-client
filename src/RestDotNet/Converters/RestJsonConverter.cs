using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace RestDotNet.Converters
{
    public class RestJsonConverter : IJsonConverter
    {
        public TResult Deserialize<TResult>(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DeserializationException("Failed to deserialize. Value is empty.");
            try
            {
                return JsonConvert.DeserializeObject<TResult>(value);
            }
            catch (JsonException exception)
            {
                throw new DeserializationException(exception.Message);
            }
        }

        public string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}