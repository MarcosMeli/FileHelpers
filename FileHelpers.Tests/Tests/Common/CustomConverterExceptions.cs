using FileHelpers;
using System;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	// SPECIAL FIELD
    [DelimitedRecord("|")]
    public class CustomConvType
	{
        [FieldConverter(typeof(VeryBadConverter))]
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
		FileHelperEngine engine;


        string testTo = "sad23\nverybad\n";

		[Test]
        [ExpectedException(typeof(ConvertException))]
		public void ExceptionsTestsPriceConverterTest()
		{
			engine = new FileHelperEngine(typeof (CustomConvType));
            PriceRecord[] res = (PriceRecord[]) engine.ReadString(testTo);
		}

        [Test]
        public void ExceptionsTestsPriceConverterTest2()
        {
            try
            {
                engine = new FileHelperEngine(typeof(CustomConvType));
                PriceRecord[] res = (PriceRecord[])engine.ReadString(testTo);
            }
            catch (ConvertException ex)
            {
                Assert.IsTrue(ex.Message.IndexOf("VeryBadConverter") >= 0);
                Assert.IsTrue(ex.Message.IndexOf("custom converter") >= 0);
                Assert.IsTrue(ex.Message.IndexOf("Line: 1") >= 0);
                Assert.IsTrue(ex.Message.IndexOf("Column: 1") >= 0);
            }
        }

	}
}
