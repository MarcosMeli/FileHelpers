using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
    [TestFixture]
    public class CustomConverterBad
    {
        FileHelperEngine engine;

        [Test]
        public void PriceConverterTest()
        {
            engine = new FileHelperEngine(typeof (GodRecord));

            ConvertException ex = 
                Assert.Throws<ConvertException>(
                () => TestCommon.ReadTest(engine, @"Good\PriceConverter.txt"));

            Assert.AreEqual(1, ex.LineNumber);
            Assert.AreEqual("PriceList", ex.FieldName);
            Assert.AreEqual(typeof(decimal), ex.FieldType);
            Assert.AreEqual(null, ex.FieldStringValue);

        
        }

        // SPECIAL FIELD
        [FixedLengthRecord]
        public class GodRecord
        {
            [FieldFixedLength(6)]
            public int ProductId;

            [FieldFixedLength(8)]
            [FieldConverter(typeof(BadConverter))]
            public decimal PriceList;

            [FieldFixedLength(8)]
            [FieldConverter(typeof(BadConverter))]
            public decimal PriceOnePay;
        }

        // CUSTOM CONVERTER
        public class BadConverter : ConverterBase
        {
            public override object StringToField(string from)
            {
                return (int) 12;
            }
        }
    }
}
