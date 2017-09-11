using System;
using System.Net;
using Moq;
using Xunit;

namespace RestDotNet.Tests.Fluent
{
    public class OkTests : FluentBaseTests
    {
        [Fact]
        public override void Typed_Response_With_Content_Register_Callback()
        {
            TypedMock.Object.Ok((object res) => { });
            TypedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action<object>>()), Times.Once);
        }

        [Fact]
        public override void Typed_Response_With_Content_Return_The_Same()
        {
            IRestRequest<object> act = TypedMock.Object.Ok((object res) => { });
            Assert.Equal(TypedMock.Object, act);
        }

        [Fact]
        public override void Typed_Response_Without_Content_Register_Callback()
        {
            TypedMock.Object.Ok(() => { });
            TypedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action>()), Times.Once);
        }

        [Fact]
        public override void Typed_Response_Without_Content_Return_The_Same()
        {
            IRestRequest<object> act = TypedMock.Object.Ok(() => { });
            Assert.Equal(TypedMock.Object, act);
        }

        [Fact]
        public override void Untyped_Response_With_Content_Register_Callback()
        {
            UntypedMock.Object.Ok((object res) => { });
            UntypedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action<object>>()), Times.Once);
        }

        [Fact]
        public override void Untyped_Response_With_Content_Return_The_Same()
        {
            IRestRequest act = UntypedMock.Object.Ok((object res) => { });
            Assert.Equal(UntypedMock.Object, act);
        }

        [Fact]
        public override void Untyped_Response_Without_Content_Register_Callback()
        {
            UntypedMock.Object.Ok(() => { });
            UntypedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.OK, It.IsAny<Action>()), Times.Once);
        }

        [Fact]
        public override void Untyped_Response_Without_Content_Return_The_Same()
        {
            IRestRequest act = UntypedMock.Object.Ok(() => { });
            Assert.Equal(UntypedMock.Object, act);
        }
    }
}