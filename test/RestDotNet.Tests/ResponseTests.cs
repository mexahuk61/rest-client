using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace RestDotNet.Tests
{
    public class ResponseTests
    {
        private readonly Mock<IRestHandler> _mock;

        public ResponseTests()
        {
            _mock = new Mock<IRestHandler>();
            _mock.Setup(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action<object>>()))
                .Callback<HttpStatusCode, Action<object>>((code, action) => action(new object()));
        }

        [Fact]
        public async Task Execution_Register_Success_Callbak()
        {
            IResponse response = new RestResponse(_mock.Object);
            await response.ExecuteAsync();

            _mock.Verify(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action>()), Times.AtLeast(1));
        }

        [Fact]
        public async Task Execution_Invoke_Handle()
        {
            IResponse response = new RestResponse(_mock.Object);
            await response.ExecuteAsync();

            _mock.Verify(handler => handler.HandleAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Typed_Execution_Register_Success_Callbak()
        {
            IResponse<object> response = new RestResponse<object>(_mock.Object);
            await response.ExecuteAsync();

            _mock.Verify(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action<object>>()), Times.AtLeast(1));
        }

        [Fact]
        public async Task Typed_Execution_Invoke_Handle()
        {
            IResponse<object> response = new RestResponse<object>(_mock.Object);
            await response.ExecuteAsync();

            _mock.Verify(handler => handler.HandleAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Typed_Execution_Provide_Value()
        {
            IResponse<object> response = new RestResponse<object>(_mock.Object);
            object act = await response.ExecuteAsync();

            Assert.NotNull(act);
        }
    }
}