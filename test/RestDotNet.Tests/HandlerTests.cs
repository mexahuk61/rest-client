using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using RestDotNet.Deserializers;
using Xunit;

namespace RestDotNet.Tests
{
    public class HandlerTests
    {
        private readonly IDeserializerFactory _deserializerFactory;

        public HandlerTests()
        {
            var deserializerMock = new Mock<IDeserializer>();
            deserializerMock.Setup(deserializer => deserializer.ContentType).Returns("application/json");
            deserializerMock.Setup(converter => converter.Deserialize<object>(string.Empty))
                .Throws<DeserializationException>();

            var deserializerFactoryMock = new Mock<IDeserializerFactory>();
            deserializerFactoryMock.Setup(factory => factory.GetDeserializer(It.IsAny<string>()))
                .Returns(deserializerMock.Object);

            _deserializerFactory = deserializerFactoryMock.Object;
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public Task Throws_UnhandledResponseException_If_Code_Not_Handled(HttpStatusCode code)
        {
            IRestHandler handler = new RestHandler(CreateRequest(code), _deserializerFactory);

            return Assert.ThrowsAsync<UnhandledResponseException>(() => handler.HandleAsync());
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public Task Does_Not_Throws_UnhandledResponseException_If_Code_Handled(HttpStatusCode code)
        {
            IRestHandler handler = new RestHandler(CreateRequest(code), _deserializerFactory);
            handler.RegisterCallback(code, () => { });
            return handler.HandleAsync();
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public void Not_Invoke_Callback_Without_Handler_Execution(HttpStatusCode code)
        {
            bool act = false;

            IRestHandler handler = new RestHandler(CreateRequest(code), _deserializerFactory);
            handler.RegisterCallback(code, (object content) => act = true);

            Assert.False(act);
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public async Task Invoke_Typed_Callback_When_Content_Presented(HttpStatusCode code)
        {
            bool act = false;
            
            IRestHandler handler = new RestHandler(CreateRequest(code, true), _deserializerFactory);
            handler.RegisterCallback(code, (object content) => act = true);
            await handler.HandleAsync();

            Assert.True(act);
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public async Task Invoke_Untyped_Callback_When_Content_Presented(HttpStatusCode code)
        {
            bool act = false;

            IRestHandler handler = new RestHandler(CreateRequest(code, true), _deserializerFactory);
            handler.RegisterCallback(code, () => act = true);
            await handler.HandleAsync();

            Assert.True(act);
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public async Task Invoke_Untyped_Callback_When_Content_Not_Presented(HttpStatusCode code)
        {
            bool act = false;

            IRestHandler handler = new RestHandler(CreateRequest(code), _deserializerFactory);
            handler.RegisterCallback(code, () => act = true);
            await handler.HandleAsync();

            Assert.True(act);
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public Task Throws_DeserializationException_When_Typed_Callback_Invoked_But_Content_Not_Presented(HttpStatusCode code)
        {
            IRestHandler handler = new RestHandler(CreateRequest(code), _deserializerFactory);
            handler.RegisterCallback(code, (object list) => { });

            return Assert.ThrowsAsync<DeserializationException>(() => handler.HandleAsync());
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public async Task Only_Single_Callback_Invoked(HttpStatusCode code)
        {
            List<HttpStatusCode> expected = new List<HttpStatusCode> { code };
            List<HttpStatusCode> act = new List<HttpStatusCode>();
            
            IRestHandler handler = new RestHandler(CreateRequest(code), _deserializerFactory);
            handler.RegisterCallback(code, () => act.Add(code));
            GetStatusCodes()
                .Where(statusCode => statusCode != code)
                .ToList()
                .ForEach(statusCode => handler.RegisterCallback(statusCode, () => act.Add(statusCode)));
            await handler.HandleAsync();

            Assert.Equal(expected, act);
        }

        private Func<CancellationToken, Task<HttpResponseMessage>> CreateRequest(HttpStatusCode code,
            bool includeContent = false)
        {
            return message => Task.FromResult(new HttpResponseMessage(code) { Content = includeContent ? new StringContent("content") : null });
        }

        private static IEnumerable<object[]> GetMemberData()
        {
            return GetStatusCodes()
                .Select(code => new object[] { code });
        }
        
        private static IEnumerable<HttpStatusCode> GetStatusCodes()
        {
            return Enum.GetValues(typeof(HttpStatusCode))
                .Cast<HttpStatusCode>()
                .Distinct();
        }
    }
}
