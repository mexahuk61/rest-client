using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Xunit;

namespace RestDotNet.Tests.Client
{
    public class RequestMethodTypeTests
    {
        private readonly Uri _uri;
        private readonly string _path;

        public RequestMethodTypeTests()
        {
            _uri = new Uri("http://test/");
            _path = string.Empty;
        }

        [Fact]
        public async Task Get_Executed()
        {
            var mock = new Mock<HttpMessageHandler>();
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.OK)))
                .Callback<HttpRequestMessage, CancellationToken>((request, token) => Assert.Equal(HttpMethod.Get, request.Method));

            var client = new RestClient(_uri, mock.Object);
            IRestRequest<object> restHandler = client.Get<object>(_path);
            await restHandler.ExecuteAsync();
        }

        [Fact]
        public async Task Put_Executed_When_Untyped_Requested()
        {
            var mock = new Mock<HttpMessageHandler>();
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.OK)))
                .Callback<HttpRequestMessage, CancellationToken>((request, token) => Assert.Equal(HttpMethod.Put, request.Method));

            var client = new RestClient(_uri, mock.Object);
            IRestRequest restHandler = client.Put(_path, 0);
            await restHandler.ExecuteAsync();
        }

        [Fact]
        public async Task Put_Executed_When_Typed_Requested()
        {
            var mock = new Mock<HttpMessageHandler>();
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.OK)))
                .Callback<HttpRequestMessage, CancellationToken>((request, token) => Assert.Equal(HttpMethod.Put, request.Method));

            var client = new RestClient(_uri, mock.Object);
            IRestRequest<object> restHandler = client.Put<object>(_path, 0);
            await restHandler.ExecuteAsync();
        }

        [Fact]
        public async Task Post_Executed_When_Untyped_Requested()
        {
            var mock = new Mock<HttpMessageHandler>();
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.OK)))
                .Callback<HttpRequestMessage, CancellationToken>((request, token) => Assert.Equal(HttpMethod.Post, request.Method));

            var client = new RestClient(_uri, mock.Object);
            IRestRequest restHandler = client.Post(_path, 0);
            await restHandler.ExecuteAsync();
        }

        [Fact]
        public async Task Post_Executed_When_Typed_Requested()
        {
            var mock = new Mock<HttpMessageHandler>();
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.OK)))
                .Callback<HttpRequestMessage, CancellationToken>((request, token) => Assert.Equal(HttpMethod.Post, request.Method));

            var client = new RestClient(_uri, mock.Object);
            IRestRequest<object> restHandler = client.Post<object>(_path, 0);
            await restHandler.ExecuteAsync();
        }

        [Fact]
        public async Task Delete_Executed_When_Untyped_Requested()
        {
            var mock = new Mock<HttpMessageHandler>();
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.OK)))
                .Callback<HttpRequestMessage, CancellationToken>((request, token) => Assert.Equal(HttpMethod.Delete, request.Method));

            var client = new RestClient(_uri, mock.Object);
            IRestRequest restHandler = client.Delete(_path);
            await restHandler.ExecuteAsync();
        }

        [Fact]
        public async Task Delete_Executed_When_Typed_Requested()
        {
            var mock = new Mock<HttpMessageHandler>();
            mock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.OK)))
                .Callback<HttpRequestMessage, CancellationToken>((request, token) => Assert.Equal(HttpMethod.Delete, request.Method));

            var client = new RestClient(_uri, mock.Object);
            IRestRequest<object> restHandler = client.Delete<object>(_path);
            await restHandler.ExecuteAsync();
        }

        private HttpMessageHandler CreateHandler(HttpStatusCode code, object expectedResponse = null)
        {
            var message = new HttpResponseMessage(code);
            if (expectedResponse != null)
                message.Content = new StringContent(JsonConvert.SerializeObject(expectedResponse));

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(message));
            return handler.Object;
        }
    }
}