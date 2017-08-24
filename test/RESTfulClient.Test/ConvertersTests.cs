using RESTfulClient.Converters;
using Xunit;

namespace RESTfulClient.Test
{
    public class ConvertersTests : BaseTest
    {
        private readonly RestQueryConverter _queryConverter;

        public ConvertersTests()
        {
            _queryConverter = new RestQueryConverter();
        }

        [Fact]
        public void Query_Serialization_Success()
        {
            string expected =
                "?string=Value" +
                "&bool=true" +
                "&int=2" +
                "&decimal=3%2C2" +
                "&enum=live" +
                "&ienumerable=val" +
                "&ienumerable=5" +
                "&ienumerable=3%2C2" +
                "&ienumerable=false" +
                "&ienumerable=test";

            var assert = new
            {
                String = "Value",
                Bool = true,
                Int = 2,
                Decimal = 3.2,
                Enum = TestEnum.Live,
                IEnumerable = new object[] { "val", 5, 3.2, false, TestEnum.Test }
            };
            string act = _queryConverter.Serialize(assert);
            
            Assert.Equal(expected, act);
        }

        private enum TestEnum
        {
            Live = 1,
            Test = 2
        }
    }
}