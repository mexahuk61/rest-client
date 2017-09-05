using Moq;
using RestDotNet.Deserializers;
using Xunit;

namespace RestDotNet.Tests.Deserializers
{
    public class DeserializerFactoryTests
    {
        private readonly Mock<IDeserializer> _mock;

        public DeserializerFactoryTests()
        {
            _mock = new Mock<IDeserializer>();
            _mock.Setup(deserializer => deserializer.ContentType)
                .Returns("json");
        }

        [Theory]
        [InlineData("json")]
        [InlineData("unknown")]
        public void Return_Default_Desirializer_If_Content_Type_Is_Not_Registred(string contenType)
        {
            var factory = new DeserializerFactory(_mock.Object);
            IDeserializer act = factory.GetDeserializer(contenType);

            Assert.Equal(_mock.Object, act);
        }

        [Fact]
        public void Return_Excepted_Desirializer_If_Only_Default_Provided()
        {
            var factory = new DeserializerFactory(_mock.Object);
            IDeserializer act = factory.GetDeserializer("json");

            Assert.Equal(_mock.Object, act);
        }

        [Fact]
        public void Return_Excepted_Custom_Desirializer_If_Custom_Registred()
        {
            var mock = new Mock<IDeserializer>();
            mock.Setup(deserializer => deserializer.ContentType)
                .Returns("xml");

            var factory = new DeserializerFactory(_mock.Object);
            factory.AddDeserializer(mock.Object);
            IDeserializer act = factory.GetDeserializer("xml");

            Assert.Equal(mock.Object, act);
        }

        [Fact]
        public void Return_Default_Desirializer_If_Custom_Registred_And_Unknown_Requested()
        {
            var mock = new Mock<IDeserializer>();
            mock.Setup(deserializer => deserializer.ContentType)
                .Returns("xml");

            var factory = new DeserializerFactory(_mock.Object);
            factory.AddDeserializer(mock.Object);
            IDeserializer act = factory.GetDeserializer("unknown");

            Assert.Equal(_mock.Object, act);
        }

        [Fact]
        public void Return_Excepted_Desirializer_If_Custom_Registred()
        {
            var mock = new Mock<IDeserializer>();
            mock.Setup(deserializer => deserializer.ContentType)
                .Returns("xml");

            var factory = new DeserializerFactory(_mock.Object);
            factory.AddDeserializer(mock.Object);
            IDeserializer act = factory.GetDeserializer("json");

            Assert.Equal(_mock.Object, act);
        }
    }
}