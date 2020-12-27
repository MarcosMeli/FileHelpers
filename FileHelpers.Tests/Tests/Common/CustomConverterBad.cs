using FileHelpers.Converters;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class CustomConverterBad
    {
        [Test]
        public void PriceConverterTest()
        {
            var engine = new FileHelperEngine<GodRecord>();

            var ex =
                Assert.Throws<ConvertException>(
                    () => TestCommon.ReadTest<GodRecord>(engine, "Good", "PriceConverter.txt"));

            Assert.AreEqual(1, ex.LineNumber);
            Assert.AreEqual("PriceList", ex.FieldName);
            Assert.AreEqual(typeof (decimal), ex.FieldType);
            Assert.AreEqual(null, ex.FieldStringValue);
        }

        // SPECIAL FIELD
        [FixedLengthRecord]
        public class GodRecord
        {
            [FieldFixedLength(6)]
            public int ProductId;

            [FieldFixedLength(8)]
            [BadConverter]
            public decimal PriceList;

            [FieldFixedLength(8)]
            [BadConverter]
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