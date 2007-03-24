using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{


	// TEST CLASS
	[DelimitedRecord("|")]
	public sealed class DecimalConvType
	{
		public double DoubleField;
		public float FloatField;
		public decimal DecimalField;
		[FieldConverter(ConverterKind.Double, ",")]
		public double DoubleField2;
		[FieldConverter(ConverterKind.Single, ",")]
		public float FloatField2;
		[FieldConverter(ConverterKind.Decimal, ",")]
		public decimal DecimalField2;
	}


	// NUNIT TESTS
	[TestFixture]
	public class ConvertersStuff
	{
		FileHelperEngine engine;

		[Test]
		public void NameConverterTest()
		{
			engine = new FileHelperEngine(typeof (DecimalConvType));

			DecimalConvType[] res = (DecimalConvType[]) Common.ReadTest(engine, @"Good\ConverterDecimals1.txt");

			Assert.AreEqual(5, res.Length);

			for (int i = 0; i < 5; i++)
			{
				Assert.AreEqual(res[i].DecimalField, res[i].DecimalField2);
				Assert.AreEqual(res[i].DoubleField, res[i].DoubleField2);
				Assert.AreEqual(res[i].FloatField, res[i].FloatField2);
			}
			
		}

	}
}