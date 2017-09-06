using RestDotNet.Deserializers;
using Xunit;

namespace RestDotNet.Tests.Deserializers
{
    public class TextPlainDeserializerTests
    {
        private readonly IDeserializer _deserializer;

        public TextPlainDeserializerTests()
        {
            _deserializer = new PlainTextDeserializer();
        }

        [Fact]
        public void Executed_Without_Exception_If_String_Deserialize_Invoked()
        {
            _deserializer.Deserialize<string>(string.Empty);
        }

        [Fact]
        public void Throw_Deserialization_Exception_If_Int_Deserialize_Invoked()
        {
            Assert.Throws<DeserializationException>(() => _deserializer.Deserialize<int>("0"));
        }

        [Fact]
        public void Return_The_Some_If_String_Deserialize_Invoked()
        {
            string expected = "text";
            string act = _deserializer.Deserialize<string>(expected);
            Assert.Equal(act, expected);
        }

        [Fact]
        public void Return_The_Some_If_Object_Deserialize_Invoked()
        {
            string expected = "text";
            object act = _deserializer.Deserialize<object>(expected);
            Assert.Equal(act, expected);
        }
    }
}