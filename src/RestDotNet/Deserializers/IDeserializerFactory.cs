using System.Collections.Generic;

namespace RestDotNet.Deserializers
{
    public interface IDeserializerFactory
    {
        IEnumerable<IDeserializer> GetDeserializers();

        IDeserializer GetDeserializer(string contentType);

        void AddDeserializer(IDeserializer deserializer);
    }
}
