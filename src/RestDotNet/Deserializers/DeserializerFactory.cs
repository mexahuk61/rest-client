using System;
using System.Collections.Generic;
using System.Linq;

namespace RestDotNet.Deserializers
{
    public class DeserializerFactory : IDeserializerFactory
    {
        private const string DefaultDeserializer = "application/json";

        private readonly IList<IDeserializer> _deserializers;

        public DeserializerFactory()
        {
            _deserializers = new List<IDeserializer>();
        }

        public IDeserializer GetDeserializer(string contentType)
        {
            if (Enumerable.All<IDeserializer>(_deserializers, d => d.ContentType != contentType))
                return Enumerable.First<IDeserializer>(_deserializers, d => d.ContentType == DefaultDeserializer);
            return Enumerable.Last<IDeserializer>(_deserializers, d => d.ContentType == contentType);
        }

        public void AddDeserializer(IDeserializer deserializer)
        {
            if (deserializer == null)
                throw new ArgumentNullException(nameof(deserializer));
            _deserializers.Add(deserializer);
        }
    }
}