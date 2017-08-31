using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace RestDotNet.Tests.FluentTests
{
    public class SuccessTests
    {
        private readonly Mock<IRestHandler> _handlerMock;
        private readonly Mock<IRestRequest> _untypedMock;
        private readonly Mock<IRestRequest<object>> _typedMock;

        public SuccessTests()
        {
            _handlerMock = new Mock<IRestHandler>();
            _untypedMock = new Mock<IRestRequest>();
            _untypedMock.Setup(request => request.Handler)
                .Returns(_handlerMock.Object);

            _typedMock = new Mock<IRestRequest<object>>();
            _typedMock.Setup(request => request.Handler)
                .Returns(_handlerMock.Object);
        }

        [Fact]
        public async Task Typed_Response_With_Content_Register_Callback()
        {
            await _typedMock.Object.SuccessAsync(res => { });
            _handlerMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action<object>>()), Times.Once);
        }

        [Fact]
        public async Task Typed_Response_With_Content_Invoke_Handler()
        {
            await _typedMock.Object.SuccessAsync(res => { });
            _handlerMock.Verify(handler => handler.HandleAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Untyped_Response_Without_Content_Register_Callback()
        {
            await _untypedMock.Object.SuccessAsync(() => { });
            _handlerMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action>()), Times.Once);
        }

        [Fact]
        public async Task Untyped_Response_Without_Content_Invoke_Handler()
        {
            await _untypedMock.Object.SuccessAsync(() => { });
            _handlerMock.Verify(handler => handler.HandleAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}