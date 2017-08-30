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
        private readonly Mock<IResponse> _untypedMock;
        private readonly Mock<IResponse<object>> _typedMock;

        public SuccessTests()
        {
            _handlerMock = new Mock<IRestHandler>();
            _untypedMock = new Mock<IResponse>();
            _untypedMock.Setup(response => response.Handler)
                .Returns(_handlerMock.Object);

            _typedMock = new Mock<IResponse<object>>();
            _typedMock.Setup(response => response.Handler)
                .Returns(_handlerMock.Object);
        }

        [Fact]
        public async Task Typed_Response_With_Content_Register_Callback()
        {
            await _typedMock.Object.SuccessAsync(res => { });
            _handlerMock.Verify(response => response.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action<object>>()), Times.Once);
        }

        [Fact]
        public async Task Typed_Response_With_Content_Invoke_Handler()
        {
            await _typedMock.Object.SuccessAsync(res => { });
            _handlerMock.Verify(response => response.HandleAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Untyped_Response_Without_Content_Register_Callback()
        {
            await _untypedMock.Object.SuccessAsync(() => { });
            _handlerMock.Verify(response => response.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action>()), Times.Once);
        }

        [Fact]
        public async Task Untyped_Response_Without_Content_Invoke_Handler()
        {
            await _untypedMock.Object.SuccessAsync(() => { });
            _handlerMock.Verify(response => response.HandleAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}