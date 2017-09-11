using System;
using System.Net;
using Moq;
using Xunit;

namespace RestDotNet.Tests.Fluent
{
    public class ForbiddenTests : FluentBaseTests
    {
        [Fact]
        public override void Typed_Response_With_Content_Register_Callback()
        {
            TypedMock.Object.Forbidden((object res) => { });
            TypedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.Forbidden, It.IsAny<Action<object>>()), Times.Once);
        }

        [Fact]
        public override void Typed_Response_With_Content_Return_The_Same()
        {
            IRestRequest<object> act = TypedMock.Object.Forbidden((object res) => { });
            Assert.Equal(TypedMock.Object, act);
        }

        [Fact]
        public override void Typed_Response_Without_Content_Register_Callback()
        {
            TypedMock.Object.Forbidden(() => { });
            TypedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.Forbidden, It.IsAny<Action>()), Times.Once);
        }
        
        [Fact]
        public override void Typed_Response_Without_Content_Return_The_Same()
        {
            IRestRequest<object> act = TypedMock.Object.Forbidden(() => { });
            Assert.Equal(TypedMock.Object, act);
        }

        [Fact]
        public override void Untyped_Response_With_Content_Register_Callback()
        {
            UntypedMock.Object.Forbidden((object res) => { });
            UntypedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.Forbidden, It.IsAny<Action<object>>()), Times.Once);
        }

        [Fact]
        public override void Untyped_Response_With_Content_Return_The_Same()
        {
            IRestRequest act = UntypedMock.Object.Forbidden((object res) => { });
            Assert.Equal(UntypedMock.Object, act);
        }
        
        [Fact]
        public override void Untyped_Response_Without_Content_Register_Callback()
        {
            UntypedMock.Object.Forbidden(() => { });
            UntypedMock.Verify(handler => handler.RegisterCallback(HttpStatusCode.Forbidden, It.IsAny<Action>()), Times.Once);
        }

        [Fact]
        public override void Untyped_Response_Without_Content_Return_The_Same()
        {
            IRestRequest act = UntypedMock.Object.Forbidden(() => { });
            Assert.Equal(UntypedMock.Object, act);
        }
    }
}