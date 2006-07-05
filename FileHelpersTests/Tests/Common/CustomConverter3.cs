using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	// SPECIAL FIELD
    [FixedLengthRecord]
	public class PriceRecord
	{
        [FieldFixedLength(6)]
        public int ProductId;

        [FieldFixedLength(8)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PriceList;

        [FieldFixedLength(8)]
        [FieldConverter(typeof(MoneyConverter))]
        public decimal PriceOnePay;
	}

	// CUSTOM CONVERTER
	public class MoneyConverter : ConverterBase
	{
		public override object StringToField(string from)
		{
            return decimal.Parse(from) / 100;
		}
	}


	// NUNIT TESTS
	[TestFixture]
	public class CustomConvertPrice
	{
		FileHelperEngine engine;

		[Test]
		public void PriceConverterTest()
		{
			engine = new FileHelperEngine(typeof (PriceRecord));

			PriceRecord[] res = (PriceRecord[]) Common.ReadTest(engine, @"Good\PriceConverter.txt");

			Assert.AreEqual(4, res.Length);


			Assert.AreEqual(125,          res[0].ProductId);
			Assert.AreEqual((decimal)145.88,   res[0].PriceList);
			Assert.AreEqual((decimal)130.25,   res[0].PriceOnePay);

            Assert.AreEqual(126, res[1].ProductId);
            Assert.AreEqual((decimal)1234.56, res[1].PriceList);
            Assert.AreEqual((decimal)2345.67, res[1].PriceOnePay);

            Assert.AreEqual(127, res[2].ProductId);
            Assert.AreEqual((decimal)12345.67, res[2].PriceList);
            Assert.AreEqual((decimal)23456.78, res[2].PriceOnePay);

            Assert.AreEqual(128, res[3].ProductId);
            Assert.AreEqual((decimal)8765.43, res[3].PriceList);
            Assert.AreEqual((decimal)1234.56, res[3].PriceOnePay);

		}

	}
}
