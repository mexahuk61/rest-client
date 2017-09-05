using System;
using System.Collections.Generic;
using System.Linq;

namespace RestDotNet.Deserializers
{
    public class DeserializerFactory : IDeserializerFactory
    {
        private readonly IList<IDeserializer> _deserializers;
        private readonly IDeserializer _defaultDeserializer;

        public DeserializerFactory(IDeserializer defaultDeserializer)
        {
            _deserializers = new List<IDeserializer> { defaultDeserializer };
            _defaultDeserializer = defaultDeserializer;
        }

        public IDeserializer GetDeserializer(string contentType)
        {
            return _deserializers.All(d => d.ContentType != contentType) 
                ? _defaultDeserializer
                : _deserializers.Last(d => d.ContentType == contentType);
        }

        public void AddDeserializer(IDeserializer deserializer)
        {
            if (deserializer == null)
                throw new ArgumentNullException(nameof(deserializer));
            _deserializers.Add(deserializer);
        }
    }
}