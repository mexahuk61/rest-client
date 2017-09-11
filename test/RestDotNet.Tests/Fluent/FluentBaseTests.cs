using Moq;

namespace RestDotNet.Tests.FluentTests
{
    public abstract class FluentBaseTests
    {
        protected FluentBaseTests()
        {
            UntypedMock = new Mock<IRestRequest>();
            TypedMock = new Mock<IRestRequest<object>>();
        }

        public Mock<IRestRequest> UntypedMock { get; }

        public Mock<IRestRequest<object>> TypedMock { get; }

        public abstract void Typed_Response_With_Content_Register_Callback();

        public abstract void Typed_Response_With_Content_Return_The_Same();

        public abstract void Typed_Response_Without_Content_Register_Callback();

        public abstract void Typed_Response_Without_Content_Return_The_Same();

        public abstract void Untyped_Response_With_Content_Register_Callback();

        public abstract void Untyped_Response_With_Content_Return_The_Same();

        public abstract void Untyped_Response_Without_Content_Register_Callback();

        public abstract void Untyped_Response_Without_Content_Return_The_Same();
    }
}