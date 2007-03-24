using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Converters
{
	[TestFixture]
	public class DecimalNumbers
	{
		FileHelperEngine engine;

		[Test]
		public void Decimals1()
		{
			engine = new FileHelperEngine(typeof (DecimalType));

			DecimalType[] res;
			res = (DecimalType[]) Common.ReadTest(engine, @"Good\NumberFormat.txt");

			Assert.AreEqual(10, res.Length);

			CheckDecimal((decimal) 32.38, res[0]);
			CheckDecimal((decimal) 11.61, res[1]);
			CheckDecimal((decimal) 65.83, res[2]);
			CheckDecimal((decimal) 41.34, res[3]);
			CheckDecimal((decimal) 51.3, res[4]);
			CheckDecimal((decimal) 58.17, res[5]);
			CheckDecimal((decimal) 22.98, res[6]);
			CheckDecimal((decimal) 148.33, res[7]);
			CheckDecimal((decimal) 13.97, res[8]);
			CheckDecimal((decimal) 81.91, res[9]);

		}

		private static void CheckDecimal(decimal dec, DecimalType res)
		{
			Assert.AreEqual((decimal) dec, res.DecimalField);
			Assert.AreEqual((double) dec, res.DoubleField);
			Assert.AreEqual((float) dec, res.FloatField);
		}


		[Test]
		public void NegativeNumbers()
		{
			engine = new FileHelperEngine(typeof (DecimalType));

			DecimalType[] res;
			res = (DecimalType[]) Common.ReadTest(engine, @"Good\NumberNegative.txt");

			Assert.AreEqual(10, res.Length);

			CheckDecimal((decimal) 32.38, res[0]);
			CheckDecimal((decimal) -11.61, res[1]);
			CheckDecimal((decimal) -65.83, res[2]);
			CheckDecimal((decimal) 41.34, res[3]);
			CheckDecimal((decimal) 51.3, res[4]);
			CheckDecimal((decimal) -58.17, res[5]);
			CheckDecimal((decimal) 22.98, res[6]);
			CheckDecimal((decimal) -148.33, res[7]);
			CheckDecimal((decimal) 13.97, res[8]);
			CheckDecimal((decimal) 81.91, res[9]);

		}


		[DelimitedRecord("|")]
		public class DecimalType
		{
			public int IntField;
			public float FloatField;
			public double DoubleField;
			public decimal DecimalField;

		}
	}
}