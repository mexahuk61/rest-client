using System;
using System.Net;
using Moq;
using Xunit;

namespace RestDotNet.Tests.FluentTests
{
    public class BadRequestTests : FluentBaseTests
    {
        [Fact]
        public override void Typed_Response_With_Content_Register_Callback()
        {
            TypedMock.Object.BadRequest((object res) => { });
            TypedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.BadRequest, It.IsAny<Action<object>>()), Times.Once);
        }

        [Fact]
        public override void Typed_Response_With_Content_Return_The_Same()
        {
            IRestRequest<object> act = TypedMock.Object.BadRequest((object res) => { });
            Assert.Equal(TypedMock.Object, act);
        }

        [Fact]
        public override void Typed_Response_Without_Content_Register_Callback()
        {
            TypedMock.Object.BadRequest(() => { });
            TypedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.BadRequest, It.IsAny<Action>()), Times.Once);
        }
        
        [Fact]
        public override void Typed_Response_Without_Content_Return_The_Same()
        {
            IRestRequest<object> act = TypedMock.Object.BadRequest(() => { });
            Assert.Equal(TypedMock.Object, act);
        }
        
        [Fact]
        public override void Untyped_Response_With_Content_Register_Callback()
        {
            UntypedMock.Object.BadRequest((object res) => { });
            UntypedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.BadRequest, It.IsAny<Action<object>>()), Times.Once);
        }

        [Fact]
        public override void Untyped_Response_With_Content_Return_The_Same()
        {
            IRestRequest act = UntypedMock.Object.BadRequest((object res) => { });
            Assert.Equal(UntypedMock.Object, act);
        }

        [Fact]
        public override void Untyped_Response_Without_Content_Register_Callback()
        {
            UntypedMock.Object.BadRequest(() => { });
            UntypedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.BadRequest, It.IsAny<Action>()), Times.Once);
        }

        [Fact]
        public override void Untyped_Response_Without_Content_Return_The_Same()
        {
            IRestRequest act = UntypedMock.Object.BadRequest(() => { });
            Assert.Equal(UntypedMock.Object, act);
        }
    }
}