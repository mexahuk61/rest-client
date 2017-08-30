using System;
using System.Net;
using Moq;
using Xunit;

namespace RestDotNet.Tests.FluentTests
{
    public class ConflictTests : FluentBaseTests
    {
        [Fact]
        public override void Typed_Response_With_Content_Register_Callback()
        {
            TypedMock.Object.Conflict((object res) => { });
            HandlerMock.Verify(response => response.RegisterCallback(HttpStatusCode.Conflict, It.IsAny<Action<object>>()), Times.Once);
        }

        [Fact]
        public override void Typed_Response_With_Content_Return_The_Same()
        {
            IResponse<object> act = TypedMock.Object.Conflict((object res) => { });
            Assert.Equal(TypedMock.Object, act);
        }

        [Fact]
        public override void Typed_Response_Without_Content_Register_Callback()
        {
            TypedMock.Object.Conflict(() => { });
            HandlerMock.Verify(response => response.RegisterCallback(HttpStatusCode.Conflict, It.IsAny<Action>()), Times.Once);
        }

        [Fact]
        public override void Typed_Response_Without_Content_Return_The_Same()
        {
            IResponse<object> act = TypedMock.Object.Conflict(() => { });
            Assert.Equal(TypedMock.Object, act);
        }

        [Fact]
        public override void Untyped_Response_With_Content_Register_Callback()
        {
            UntypedMock.Object.Conflict((object res) => { });
            HandlerMock.Verify(response => response.RegisterCallback(HttpStatusCode.Conflict, It.IsAny<Action<object>>()), Times.Once);
        }

        [Fact]
        public override void Untyped_Response_With_Content_Return_The_Same()
        {
            IResponse act = UntypedMock.Object.Conflict((object res) => { });
            Assert.Equal(UntypedMock.Object, act);
        }

        [Fact]
        public override void Untyped_Response_Without_Content_Register_Callback()
        {
            UntypedMock.Object.Conflict(() => { });
            HandlerMock.Verify(response => response.RegisterCallback(HttpStatusCode.Conflict, It.IsAny<Action>()), Times.Once);
        }
        
        [Fact]
        public override void Untyped_Response_Without_Content_Return_The_Same()
        {
            IResponse act = UntypedMock.Object.Conflict(() => { });
            Assert.Equal(UntypedMock.Object, act);
        }
    }
}