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

        [global::FileHelpers.Converters.DoubleConverter(",")]
        public double DoubleField2;

        [global::FileHelpers.Converters.SingleConverter(",")]
        public float FloatField2;

        [global::FileHelpers.Converters.DecimalConverter(",")]
        public decimal DecimalField2;
    }

    // TEST CLASS
    [DelimitedRecord("|")]
    public sealed class AllConvertersType
    {
        [global::FileHelpers.Converters.DateTimeConverter]
        public DateTime Field2;

        [global::FileHelpers.Converters.ByteConverter]
        public byte Field3;

        [global::FileHelpers.Converters.SByteConverter]
        public sbyte Field4;

        [global::FileHelpers.Converters.Int16Converter]
        public short Field5;

        [global::FileHelpers.Converters.Int32Converter]
        public int Field6;

        [global::FileHelpers.Converters.Int64Converter]
        public long Field7;

        [global::FileHelpers.Converters.UInt16Converter]
        public ushort Field8;

        [global::FileHelpers.Converters.UInt32Converter]
        public uint Field9;

        [global::FileHelpers.Converters.UInt64Converter]
        public ulong Field10;

        [global::FileHelpers.Converters.DecimalConverter]
        public decimal Field11;

        [global::FileHelpers.Converters.DecimalConverter]
        public double Field12;

        [global::FileHelpers.Converters.SingleConverter]
        public float Field13;

        [global::FileHelpers.Converters.BooleanConverter]
        public bool Field14;
    }

    // NUNIT TESTS
    [TestFixture]
    public class ConvertersStuff
    {
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
            [global::FileHelpers.Converters.DoubleConverter(".")]
            [FieldNullValue(double.NaN)]
            public double DoubleField1;

            [global::FileHelpers.Converters.DoubleConverter(",")]
            [FieldNullValue(double.NaN)]
            public double DoubleField2;
        }
    }
}