using System;
using System.Net;
using Moq;
using Xunit;

namespace RestDotNet.Tests.FluentTests
{
    public class InternalServerErrorTests : FluentBaseTests
    {
        [Fact]
        public override void Typed_Response_With_Content_Register_Callback()
        {
            TypedMock.Object.InternalServerError((object res) => { });
            HandlerMock.Verify(response => response.RegisterCallback(HttpStatusCode.InternalServerError, It.IsAny<Action<object>>()), Times.Once);
        }

        [Fact]
        public override void Typed_Response_With_Content_Return_The_Same()
        {
            IResponse<object> act = TypedMock.Object.InternalServerError((object res) => { });
            Assert.Equal(TypedMock.Object, act);
        }

        [Fact]
        public override void Typed_Response_Without_Content_Register_Callback()
        {
            TypedMock.Object.InternalServerError(() => { });
            HandlerMock.Verify(response => response.RegisterCallback(HttpStatusCode.InternalServerError, It.IsAny<Action>()), Times.Once);
        }

        [Fact]
        public override void Typed_Response_Without_Content_Return_The_Same()
        {
            IResponse<object> act = TypedMock.Object.InternalServerError(() => { });
            Assert.Equal(TypedMock.Object, act);
        }

        [Fact]
        public override void Untyped_Response_With_Content_Register_Callback()
        {
            UntypedMock.Object.InternalServerError((object res) => { });
            HandlerMock.Verify(response => response.RegisterCallback(HttpStatusCode.InternalServerError, It.IsAny<Action<object>>()), Times.Once);
        }

        [Fact]
        public override void Untyped_Response_With_Content_Return_The_Same()
        {
            IResponse act = UntypedMock.Object.InternalServerError((object res) => { });
            Assert.Equal(UntypedMock.Object, act);
        }

        [Fact]
        public override void Untyped_Response_Without_Content_Register_Callback()
        {
            UntypedMock.Object.InternalServerError(() => { });
            HandlerMock.Verify(response => response.RegisterCallback(HttpStatusCode.InternalServerError, It.IsAny<Action>()), Times.Once);
        }

        [Fact]
        public override void Untyped_Response_Without_Content_Return_The_Same()
        {
            IResponse act = UntypedMock.Object.InternalServerError(() => { });
            Assert.Equal(UntypedMock.Object, act);
        }
    }
}