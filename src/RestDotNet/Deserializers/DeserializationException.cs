using System;

namespace RestDotNet.Deserializers
{
    public class DeserializationException : Exception
    {
        public DeserializationException() : base("Failed to deserialize.")
        {
        }

        public DeserializationException(string message) : base(message)
        {
        }
    }
}