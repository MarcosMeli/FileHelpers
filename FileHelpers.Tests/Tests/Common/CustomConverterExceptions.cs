using System;
using FileHelpers.Converters;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    // SPECIAL FIELD
    [DelimitedRecord("|")]
    public class CustomConvType
    {
        [VeryBadConverter]
        public decimal PriceList;
    }

    // CUSTOM CONVERTER
    internal class VeryBadConverter : ConverterBase
    {
        public override object StringToField(string from)
        {
            throw new Exception("Not god format.");
        }
    }

    // NUNIT TESTS
    [TestFixture]
    public class CustomConverterExceptions
    {
        private string testTo = "sad23\nverybad\n";

        [Test]
        public void ExceptionsTestsPriceConverterTest()
        {
            var engine = new FileHelperEngine<CustomConvType>();

            Assert.Throws<ConvertException>(
                () => engine.ReadString(testTo));
        }

        [Test]
        public void ExceptionsTestsPriceConverterTest2()
        {
            try {
                var engine = new FileHelperEngine<CustomConvType>();
                object[] res = engine.ReadString(testTo);
            }
            catch (ConvertException ex) {
                Assert.IsTrue(ex.Message.IndexOf("VeryBadConverter") >= 0);
                Assert.IsTrue(ex.Message.IndexOf("custom converter") >= 0);
                Assert.IsTrue(ex.Message.IndexOf("Line: 1") >= 0);
                Assert.IsTrue(ex.Message.IndexOf("Column: 1") >= 0);
            }
        }
    }
}