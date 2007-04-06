using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
    

    // NUNIT TESTS
    [TestFixture]
    public class CustomConverterBad
    {
        FileHelperEngine engine;

        [Test]
        [ExpectedException(typeof(ConvertException))]
        public void PriceConverterTest()
        {
            engine = new FileHelperEngine(typeof(GodRecord));

            try
            {
                GodRecord[] res = (GodRecord[])Common.ReadTest(engine, @"Good\PriceConverter.txt");
            }
            catch (ConvertException ex)
            {
                Assert.AreEqual(1, ex.LineNumber);
                Assert.AreEqual("PriceList", ex.FieldName);
                Assert.AreEqual(typeof(decimal), ex.FieldType);
                Assert.AreEqual(null, ex.FieldStringValue);

                throw;
            }

        
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
