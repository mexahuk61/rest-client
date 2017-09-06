using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using RestDotNet.Deserializers;
using RestDotNet.Tests.Deserializers;
using Xunit;
using Xunit.Sdk;

namespace RestDotNet.Tests.Client
{
    public class ClientOptionsTests
    {
        private readonly RestClientOptions _options;

        public ClientOptionsTests()
        {
            _options = new RestClientOptions();
        }

        [Fact]
        public void Default_Serializer_Is_text_plain()
        {
            string expected = "application/json";
            string act = _options.DefaultSerializer.MediaType;
            Assert.Equal(expected, act);
        }

        [Theory]
        [InlineData("text/plain")]
        [InlineData("application/json")]
        public void Contains_Deserializer(string expected)
        {
            IEnumerable<IDeserializer> act = _options.Deserializers;

            Assert.Contains(act, deserializer => deserializer.ContentType == expected);
        }
    }
}