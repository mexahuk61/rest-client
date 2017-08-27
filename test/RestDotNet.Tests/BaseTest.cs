using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace RestDotNet.Tests
{
    public abstract class BaseTest
    {
        internal HttpMessageHandler CreateHandler(HttpStatusCode code, object expectedResponse = null)
        {
            var response = new HttpResponseMessage(code);
            if (expectedResponse != null)
                response.Content = new StringContent(JsonConvert.SerializeObject(expectedResponse));

            var handler = new Mock<HttpMessageHandler>();
            handler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .Returns(Task.FromResult(response));
            return handler.Object;
        }
    }
}