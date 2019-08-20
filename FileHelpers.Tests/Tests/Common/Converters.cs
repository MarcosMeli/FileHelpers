using System;
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
        public byte Field3;

        [FieldConverter(ConverterKind.SByte)]
        public sbyte Field4;

        [FieldConverter(ConverterKind.Int16)]
        public short Field5;

        [FieldConverter(ConverterKind.Int32)]
        public int Field6;

        [FieldConverter(ConverterKind.Int64)]
        public long Field7;

        [FieldConverter(ConverterKind.UInt16)]
        public ushort Field8;

        [FieldConverter(ConverterKind.UInt32)]
        public uint Field9;

        [FieldConverter(ConverterKind.UInt64)]
        public ulong Field10;

        [FieldConverter(ConverterKind.Decimal)]
        public decimal Field11;

        [FieldConverter(ConverterKind.Double)]
        public double Field12;

        [FieldConverter(ConverterKind.Single)]
        public float Field13;

        [FieldConverter(ConverterKind.Boolean)]
        public bool Field14;
    }

    // NUNIT TESTS
    [TestFixture]
    public class ConvertersStuff
    {
        [Test]
        public void BadConverterOver()
        {
            Assert.Throws<BadUsageException>(
                () => new FileHelperEngine<BadConverter>());
        }

        [Test]
        public void AllConverters()
        {
            var engine = new FileHelperEngine<AllConvertersType>();
        }

        [Test]
        public void NameConverterTest()
        {
            var engine = new FileHelperEngine<DecimalConvType>();

            DecimalConvType[] res = TestCommon.ReadTest<DecimalConvType>(engine, "Good", "ConverterDecimals1.txt");

            Assert.AreEqual(5, res.Length);

            for (int i = 0; i < 5; i++) {
                Assert.AreEqual(res[i].DecimalField, res[i].DecimalField2);
                Assert.AreEqual(res[i].DoubleField, res[i].DoubleField2);
                Assert.AreEqual(res[i].FloatField, res[i].FloatField2);
            }
        }

        [Test]
        public void NameConverterTest2()
        {
            var engine = new FileHelperEngine<DecimalConvType2>();

            DecimalConvType2[] res = TestCommon.ReadTest<DecimalConvType2>(engine, "Good", "ConverterDecimals2.txt");

            Assert.AreEqual(5, res.Length);

            for (int i = 0; i < 5; i++)
                Assert.AreEqual(res[i].DoubleField1, res[i].DoubleField2);
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