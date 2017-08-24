using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace RESTfulClient.Test
{
    public class TestHandler : BaseTest
    {
        private readonly Uri _uri;
        private readonly string _path;

        public TestHandler()
        {
            _uri = new Uri("http://test/");
            _path = string.Empty;
        }

        [Fact]
        public async Task Execute_Success()
        {
            int expected = 1;
            HttpMessageHandler handler = CreateHandler(HttpStatusCode.OK, expected);
            var client = new RestClient(_uri, handler);
            IRestHandler<int> restHandler = client.Put<int>(_path, new { });
            
            int act = await restHandler.ExecuteAsync();
            
            Assert.Equal(expected, act);
        }

        [Fact]
        public async Task Execute_UnhandledResponseException()
        {
            HttpMessageHandler handler = CreateHandler(HttpStatusCode.BadRequest, 1);
            var client = new RestClient(_uri, handler);
            IRestHandler<int> restHandler = client.Put<int>(_path, new { });
            
            await Assert.ThrowsAsync<UnhandledResponseException>(async () => await restHandler.ExecuteAsync());
        }

        [Fact]
        public async Task Fluent_Success()
        {
            int expected = 1;
            HttpMessageHandler handler = CreateHandler(HttpStatusCode.OK, expected);
            var client = new RestClient(_uri, handler);
            IRestHandler<int> restHandler = client.Put<int>(_path, new { });
            
            int act = 0;
            await restHandler.SuccessAsync(result => act = result);
            
            Assert.Equal(expected, act);
        }

        [Fact]
        public async Task Fluent_BadRequest()
        {
            int expected = 1;
            HttpMessageHandler handler = CreateHandler(HttpStatusCode.BadRequest, expected);
            var client = new RestClient(_uri, handler);
            IRestHandler restHandler = client.Put(_path, new { });
            
            int act = 0;
            restHandler.BadRequest<int>(result => act = result);
            await restHandler.SuccessAsync(() => { });
            
            Assert.Equal(expected, act);
        }

        [Fact]
        public async Task Fluent_NotSuccess()
        {
            int expected = 1;
            HttpMessageHandler handler = CreateHandler(HttpStatusCode.BadRequest, expected);
            var client = new RestClient(_uri, handler);
            IRestHandler restHandler = client.Put(_path, new { });

            int act = 0;
            restHandler.BadRequest<int>(result => act = result);
            await restHandler.SuccessAsync(() => act = 2);

            Assert.Equal(expected, act);
        }

        [Fact]
        public async Task Fluent_UnhandledResponseException()
        {
            HttpMessageHandler handler = CreateHandler(HttpStatusCode.BadRequest, 1);
            var client = new RestClient(_uri, handler);
            IRestHandler restHandler = client.Put(_path, new { });

            await Assert.ThrowsAsync<UnhandledResponseException>(async () => await restHandler.SuccessAsync(() => { }));
        }
    }
}
