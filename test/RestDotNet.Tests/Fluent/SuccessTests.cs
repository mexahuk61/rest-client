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
        private readonly Mock<IRestRequest<object>> _typedMock;
        private readonly Mock<IRestRequest> _untypedMock;

        public SuccessTests()
        {
            _typedMock = new Mock<IRestRequest<object>>();
            _untypedMock = new Mock<IRestRequest>();
        }

        [Fact]
        public async Task Typed_Response_With_Content_Register_Callback()
        {
            await _typedMock.Object.SuccessAsync(res => { });
            _typedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action<object>>()), Times.Once);
        }

        [Fact]
        public async Task Typed_Response_With_Content_Invoke_Handler()
        {
            await _typedMock.Object.SuccessAsync(res => { });
            _typedMock.Verify(handler => handler.ExecuteAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Untyped_Response_Without_Content_Register_Callback()
        {
            await _untypedMock.Object.SuccessAsync(() => { });
            _untypedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action>()), Times.Once);
        }

        [Fact]
        public async Task Untyped_Response_Without_Content_Invoke_Handler()
        {
            await _untypedMock.Object.SuccessAsync(() => { });
            _untypedMock.Verify(handler => handler.ExecuteAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}