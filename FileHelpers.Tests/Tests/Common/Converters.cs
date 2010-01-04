using System.IO;
using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
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

    // TEST CLASS
    [DelimitedRecord("|")]
    public sealed class BadConverter
    {
        [FieldConverter(ConverterKind.Double)]
        public decimal DecimalField;
    }

    // TEST CLASS
    [DelimitedRecord("|")]
    public sealed class AllConvertersType
    {
        [FieldConverter(ConverterKind.Date)]
        public DateTime Field2;

        [FieldConverter(ConverterKind.Byte)]
        public Byte Field3;
        [FieldConverter(ConverterKind.SByte)]
        public SByte Field4;
        [FieldConverter(ConverterKind.Int16)]
        public Int16 Field5;
        [FieldConverter(ConverterKind.Int32)]
        public Int32 Field6;
        [FieldConverter(ConverterKind.Int64)]
        public Int64 Field7;
        [FieldConverter(ConverterKind.UInt16)]
        public UInt16 Field8;
        [FieldConverter(ConverterKind.UInt32)]
        public UInt32 Field9;
        [FieldConverter(ConverterKind.UInt64)]
        public UInt64 Field10;
        [FieldConverter(ConverterKind.Decimal)]
        public Decimal Field11;
        [FieldConverter(ConverterKind.Double)]
        public Double Field12;
        [FieldConverter(ConverterKind.Single)]
        public Single Field13;
        [FieldConverter(ConverterKind.Boolean)]
        public Boolean Field14;
    }

	// NUNIT TESTS
	[TestFixture]
	public class ConvertersStuff
	{
		FileHelperEngine engine;

        [Test]
        public void BadConverterOver()
        {
            Assert.Throws<BadUsageException>(
                () => new FileHelperEngine(typeof(BadConverter)));
        }

        [Test]
        public void AllConverters()
        {
            engine = new FileHelperEngine(typeof(AllConvertersType));
        }

		[Test]
		public void NameConverterTest()
		{
			engine = new FileHelperEngine(typeof (DecimalConvType));

			DecimalConvType[] res = (DecimalConvType[]) TestCommon.ReadTest(engine, "Good", "ConverterDecimals1.txt");

			Assert.AreEqual(5, res.Length);

			for (int i = 0; i < 5; i++)
			{
				Assert.AreEqual(res[i].DecimalField, res[i].DecimalField2);
				Assert.AreEqual(res[i].DoubleField, res[i].DoubleField2);
				Assert.AreEqual(res[i].FloatField, res[i].FloatField2);
			}
			
		}

		[Test]
		public void NameConverterTest2()
		{
			engine = new FileHelperEngine(typeof (DecimalConvType2));

			DecimalConvType2[] res = (DecimalConvType2[]) TestCommon.ReadTest(engine, "Good", "ConverterDecimals2.txt");

			Assert.AreEqual(5, res.Length);

			for (int i = 0; i < 5; i++)
			{
				Assert.AreEqual(res[i].DoubleField1, res[i].DoubleField2);
			}

		}
		// TEST CLASS
		[DelimitedRecord("|")]
			public sealed class DecimalConvType2
		{
			[FieldConverter(ConverterKind.Double, ".")]
			[FieldNullValue(double.NaN)] 
			public double DoubleField1;

			[FieldConverter(ConverterKind.Double, ",")]
			[FieldNullValue(double.NaN)] 
			public double DoubleField2;
			
		}

	}
}