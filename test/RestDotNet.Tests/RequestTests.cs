using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace RestDotNet.Tests
{
    public class RequestTests
    {
        private readonly Mock<IRestHandler> _mock;

        public RequestTests()
        {
            _mock = new Mock<IRestHandler>();
            _mock.Setup(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action<object>>()))
                .Callback<HttpStatusCode, Action<object>>((code, action) => action(new object()));
        }

        [Fact]
        public async Task Execution_Register_Success_Callbak()
        {
            IRestRequest restRequest = new RestRequest(_mock.Object);
            await restRequest.ExecuteAsync();

            _mock.Verify(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action>()), Times.AtLeast(1));
        }

        [Fact]
        public async Task Execution_Invoke_Handle()
        {
            IRestRequest restRequest = new RestRequest(_mock.Object);
            await restRequest.ExecuteAsync();

            _mock.Verify(handler => handler.HandleAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Typed_Execution_Register_Success_Callbak()
        {
            IRestRequest<object> restRequest = new RestRequest<object>(_mock.Object);
            await restRequest.ExecuteAsync();

            _mock.Verify(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action<object>>()), Times.AtLeast(1));
        }

        [Fact]
        public async Task Typed_Execution_Invoke_Handle()
        {
            IRestRequest<object> restRequest = new RestRequest<object>(_mock.Object);
            await restRequest.ExecuteAsync();

            _mock.Verify(handler => handler.HandleAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Typed_Execution_Provide_Value()
        {
            IRestRequest<object> restRequest = new RestRequest<object>(_mock.Object);
            object act = await restRequest.ExecuteAsync();

            Assert.NotNull(act);
        }
    }
}