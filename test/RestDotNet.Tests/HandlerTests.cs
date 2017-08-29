using System;
using System.Collections;
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
        [MemberData(nameof(GetStatusCodes))]
        public Task Throws_UnhandledResponseException_If_Not_Handled(HttpStatusCode code)
        {
            IRestHandler handler = new RestHandler<object>(
                token => Task.FromResult(new HttpResponseMessage(code)),
                _jsonConverter);

            return Assert.ThrowsAsync<UnhandledResponseException>(() => handler.HandleAsync());
        }

        [Theory]
        [MemberData(nameof(GetStatusCodes))]
        public Task Does_Not_Throws_UnhandledResponseException_If_Handled(HttpStatusCode code)
        {
            IRestHandler handler = new RestHandler<object>(
                token => Task.FromResult(new HttpResponseMessage(code)),
                _jsonConverter);
            handler.RegisterCallback(code, () => {});

            return handler.HandleAsync();
        }

        [Theory]
        [MemberData(nameof(GetStatusCodes))]
        public async Task Invoke_Typed_Callback_With_Content(HttpStatusCode code)
        {
            int[] expected = {1, 2, 3};
            IList<int> act = default(IList<int>);
            IRestHandler handler = new RestHandler<string>(
                token => Task.FromResult(new HttpResponseMessage(code){ Content = new StringContent(_jsonConverter.Serialize(expected)) }),
                _jsonConverter);
            handler.RegisterCallback(code, (IList<int> list) => act = list);

            await handler.HandleAsync();
            Assert.Equal(expected, act);
        }

        [Theory]
        [MemberData(nameof(GetStatusCodes))]
        public async Task Invoke_Untyped_Callback_With_Content(HttpStatusCode code)
        {
            int expected = 1;
            int act = default(int);
            IRestHandler handler = new RestHandler<string>(
                token => Task.FromResult(new HttpResponseMessage(code) { Content = new StringContent(_jsonConverter.Serialize(expected)) }),
                _jsonConverter);
            handler.RegisterCallback(code, () => act = expected);

            await handler.HandleAsync();
            Assert.Equal(expected, act);
        }

        [Theory]
        [MemberData(nameof(GetStatusCodes))]
        public Task Invoke_Typed_Callback_Without_Content_Throw_DeserializationException(HttpStatusCode code)
        {
            IRestHandler handler = new RestHandler<string>(
                token => Task.FromResult(new HttpResponseMessage(code)),
                _jsonConverter);
            handler.RegisterCallback(code, (IList<int> list) => {});

            return Assert.ThrowsAsync<DeserializationException>(() => handler.HandleAsync());
        }

        [Theory]
        [MemberData(nameof(GetStatusCodes))]
        public async Task Invoke_Untyped_Callback_Without_Content(HttpStatusCode code)
        {
            int expected = 1;
            int act = default(int);
            IRestHandler handler = new RestHandler<string>(
                token => Task.FromResult(new HttpResponseMessage(code)),
                _jsonConverter);
            handler.RegisterCallback(code, () => act = expected);

            await handler.HandleAsync();
            Assert.Equal(expected, act);
        }

        private static IEnumerable<object[]> GetStatusCodes()
        {
            return Enum.GetValues(typeof(HttpStatusCode))
                .Cast<HttpStatusCode>()
                .Select(code => new object[] { code });
        }
    }
}
