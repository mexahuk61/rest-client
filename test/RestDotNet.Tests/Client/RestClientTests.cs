using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using RestDotNet.Deserializers;
using Xunit;

namespace RestDotNet.Tests.Client
{
    public class RestClientTests
    {
        private readonly Uri _uri;
        private readonly string _path;

        public RestClientTests()
        {
            _uri = new Uri("http://test/");
            _path = string.Empty;
        }

        [Fact]
        public async Task Response_Typed_WithResponse_Success()
        {
            int expected = 1;
            HttpMessageHandler handler = CreateHandler(HttpStatusCode.OK, expected);
            var client = new RestClient(_uri, handler);
            IRestRequest<int> restHandler = client.Put<int>(_path, 0);

            int act = await restHandler.ExecuteAsync();
            
            Assert.Equal(expected, act);
        }

        [Fact]
        public async Task Response_Typed_WithoutResponse_Fail()
        {
            HttpMessageHandler handler = CreateHandler(HttpStatusCode.OK);
            var client = new RestClient(_uri, handler);
            IRestRequest<int> restHandler = client.Put<int>(_path, 0);

            await Assert.ThrowsAsync<DeserializationException>(async () => await restHandler.ExecuteAsync());
        }

        [Fact]
        public async Task Response_Untyped_WithResponse_Success()
        {
            HttpMessageHandler handler = CreateHandler(HttpStatusCode.OK, 1);
            var client = new RestClient(_uri, handler);
            IRestRequest restHandler = client.Put(_path, 0);

            await restHandler.ExecuteAsync();
        }

        [Fact]
        public async Task Response_Untyped_WithoutResponse_Success()
        {
            HttpMessageHandler handler = CreateHandler(HttpStatusCode.OK);
            var client = new RestClient(_uri, handler);
            IRestRequest restHandler = client.Put(_path, 0);
            
            await restHandler.ExecuteAsync();
        }

        private HttpMessageHandler CreateHandler(HttpStatusCode code, object expectedResponse = null)
        {
            var message = new HttpResponseMessage(code);
            if (expectedResponse != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(expectedResponse), Encoding.UTF8, "application/json");
            }

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(message));
            return handler.Object;
        }
    }
}