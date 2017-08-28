//using System.Collections.Generic;
//using RestDotNet.Converters;
//using Xunit;

//namespace RestDotNet.Tests
//{
//    public class ConvertersTests : BaseTest
//    {
//        private readonly RestQueryConverter _queryConverter;

//        public ConvertersTests()
//        {
//            _queryConverter = new RestQueryConverter();
//        }

//        [Fact]
//        public void Query_Serialization_Success()
//        {
//            string expected =
//                "?string=Value" +
//                "&bool=true" +
//                "&int=2" +
//                "&double=3%2C2" +
//                "&dec=3%2C2" +
//                "&enum=live" +
//                "&ienumerable=val" +
//                "&ienumerable=5" +
//                "&ienumerable=3%2C2" +
//                "&ienumerable=false" +
//                "&ienumerable=test";

//            var arrange = new TestClass
//            {
//                String = "Value",
//                Bool = true,
//                Int = 2,
//                Double = 3.2,
//                Dec = 3.2m,
//                Enum = TestEnum.Live,
//                Ienumerable = new object[] { "val", 5, 3.2, false, TestEnum.Test },
//                PrivateRead = true,
//                NullField = null,
//                NullableField = true
//            };
//            string act = _queryConverter.Serialize(arrange);
            
//            Assert.Equal(expected, act);
//        }

//        private class TestClass
//        {
//            public TestClass()
//            {
//                PrivateField = true;
//            }

//            private bool PrivateField { get; set; }

//            public string String { get; set; }
//            public bool Bool { get; set; }
//            public int Int { get; set; }
//            public double Double { get; set; }
//            public decimal Dec { get; set; }
//            public TestEnum Enum { get; set; }
//            public IEnumerable<object> Ienumerable { get; set; }

//            public bool PrivateRead { private get; set; }
//            public bool? NullField { get; set; }
//            public bool? NullableField { get; set; }
//        }

//        private enum TestEnum
//        {
//            Live = 1,
//            Test = 2
//        }
//    }
//}