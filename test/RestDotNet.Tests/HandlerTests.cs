using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using RestDotNet.Converters;
using Xunit;

namespace RestDotNet.Tests
{
    public class HandlerTests : BaseTest
    {
        private readonly RestJsonConverter _jsonConverter;

        public HandlerTests()
        {
            _jsonConverter = new RestJsonConverter();
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public Task Throws_UnhandledResponseException_If_Not_Handled(HttpStatusCode code)
        {
            IRestHandler handler = new RestHandler(
                token => Task.FromResult(new HttpResponseMessage(code)),
                _jsonConverter);

            return Assert.ThrowsAsync<UnhandledResponseException>(() => handler.HandleAsync());
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public Task Does_Not_Throws_UnhandledResponseException_If_Handled(HttpStatusCode code)
        {
            IRestHandler handler = new RestHandler(
                token => Task.FromResult(new HttpResponseMessage(code)),
                _jsonConverter);
            handler.RegisterCallback(code, () => {});

            return handler.HandleAsync();
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public async Task Invoke_Typed_Callback_With_Content(HttpStatusCode code)
        {
            int expected = 1;
            int act = default(int);
            IRestHandler handler = new RestHandler(
                token => Task.FromResult(new HttpResponseMessage(code){ Content = new StringContent(_jsonConverter.Serialize(expected)) }),
                _jsonConverter);
            handler.RegisterCallback(code, (int content) => act = expected);

            await handler.HandleAsync();
            Assert.Equal(expected, act);
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public async Task Invoke_Untyped_Callback_With_Content(HttpStatusCode code)
        {
            int expected = 1;
            int act = default(int);
            IRestHandler handler = new RestHandler(
                token => Task.FromResult(new HttpResponseMessage(code) { Content = new StringContent(_jsonConverter.Serialize(expected)) }),
                _jsonConverter);
            handler.RegisterCallback(code, () => act = expected);

            await handler.HandleAsync();
            Assert.Equal(expected, act);
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public async Task Invoke_Untyped_Callback_Without_Content(HttpStatusCode code)
        {
            int expected = 1;
            int act = default(int);
            IRestHandler handler = new RestHandler(
                token => Task.FromResult(new HttpResponseMessage(code)),
                _jsonConverter);
            handler.RegisterCallback(code, () => act = expected);

            await handler.HandleAsync();
            Assert.Equal(expected, act);
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public Task Invoke_Typed_Callback_Without_Content_Throw_DeserializationException(HttpStatusCode code)
        {
            IRestHandler handler = new RestHandler(
                token => Task.FromResult(new HttpResponseMessage(code)),
                _jsonConverter);
            handler.RegisterCallback(code, (IList<int> list) => {});

            return Assert.ThrowsAsync<DeserializationException>(() => handler.HandleAsync());
        }

        [Theory]
        [MemberData(nameof(GetMemberData))]
        public async Task Invoke_Only_One_Callback(HttpStatusCode code)
        {
            List<HttpStatusCode> expected = new List<HttpStatusCode> { code };
            List<HttpStatusCode> act = new List<HttpStatusCode>();
            IRestHandler handler = new RestHandler(
                token => Task.FromResult(new HttpResponseMessage(code)),
                _jsonConverter);
            handler.RegisterCallback(code, () => act.Add(code));
            GetStatusCodes()
                 .Where(statusCode => statusCode != code)
                .ToList()
                .ForEach(statusCode => handler.RegisterCallback(statusCode, () => act.Add(statusCode)));

            await handler.HandleAsync();
            Assert.Equal(expected, act);
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
