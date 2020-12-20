using FileHelpers.Converters;
using NUnit.Framework;

namespace FileHelpers.Tests.Converters
{
    [TestFixture]
    public class DefaultCultureInfo
    {
        [DelimitedRecord("|")]
        public class RecordWithoutSpecifiedCulture
        {
            public decimal DecimalField;
        }

        [DelimitedRecord("|", "fr-FR")]
        public class RecordWithDefaultCulture
        {
            public decimal DecimalFieldWithoutCulture;


            [FieldConverter(ConverterKind.Decimal, "en-US")]
            public decimal DecimalFieldWithEnglishCulture;
        }

        [DelimitedRecord("|")]
        public class RecordWithFieldCulture
        {
            [FieldConverter(ConverterKind.Decimal, "fr-FR")]
            public decimal DecimalFieldWithFrenchCulture;

            public decimal DecimalFieldWithoutCulture;
        }


        [Test]
        public void RecordWithoutCultureHasDefaultSeparator()
        {
            var engine = new FileHelperEngine<RecordWithoutSpecifiedCulture>();
            Assert.AreEqual(1, engine.Options.Fields.Count);
            var decimalConverter = engine.Options.Fields[0].Converter;
            AssertCanConvertEnglishNumbers(decimalConverter);
        }

        [Test]
        public void RecordWithSpecifiedCultureInRecordAttributeUsesThatCultureByDefault()
        {
            var engine = new FileHelperEngine<RecordWithDefaultCulture>();
            Assert.AreEqual(2, engine.Options.Fields.Count);
            var decimalConverterWithoutCulture = engine.Options.Fields[0].Converter;
            AssertCanConvertFrenchNumbers(decimalConverterWithoutCulture);

            var decimalConverterWithEnglishCulture = engine.Options.Fields[1].Converter;
            AssertCanConvertEnglishNumbers(decimalConverterWithEnglishCulture);
        }

        [Test]
        public void FieldWithSpecifiedCultureInFieldConverterAttributeUsesThatCulture()
        {
            var engine = new FileHelperEngine<RecordWithFieldCulture>();
            Assert.AreEqual(2, engine.Options.Fields.Count);
            var decimalConverterWithFrenchCulture = engine.Options.Fields[0].Converter;
            AssertCanConvertFrenchNumbers(decimalConverterWithFrenchCulture);

            var decimalConverterWithoutCulture = engine.Options.Fields[1].Converter;
            AssertCanConvertEnglishNumbers(decimalConverterWithoutCulture);
        }

        #region Helpers
        private static void AssertCanConvertEnglishNumbers(IConverter decimalConverter)
        {
            Assert.AreEqual(123.12,
                decimalConverter.StringToField("123.12"),
                "If no culture is specified, the decimal separator should be a dot");
            Assert.AreEqual(1234.12,
                decimalConverter.StringToField("1,234.12"),
                "If no culture is specified, the group separator should be a comma");
        }

        private static void AssertCanConvertFrenchNumbers(IConverter decimalConverterWithoutCulture)
        {
            Assert.AreEqual(1.23, decimalConverterWithoutCulture.StringToField("1,23"), "If a culture is specified, the decimal separator should be the specified culture decimal separator");
            Assert.AreEqual(1234.12, decimalConverterWithoutCulture.StringToField("1 234,12"), "If a culture is specified, the group separator should be the specified culture group separator");
            Assert.Catch(() => { decimalConverterWithoutCulture.StringToField("1.23"); }, "The dot is not a valid french separator");
        }
        #endregion

        [DelimitedRecord("|", defaultCultureName: "fr-FR")]
        public class DecimalTypeWithFrenchConversionAsAWhole
        {
            public int IntField;
            public float FloatField;
            public double DoubleField;
            public decimal DecimalField;
        }

        [DelimitedRecord("|")]
        public class DecimalTypeWithFrenchConversion
        {
            [FieldConverter(ConverterKind.Int32, "fr-FR")]
            public int IntField;
            [FieldConverter(ConverterKind.Single, "fr-FR")]
            public float FloatField;
            [FieldConverter(ConverterKind.Double, "fr-FR")]
            public double DoubleField;
            [FieldConverter(ConverterKind.Decimal, "fr-FR")]
            public decimal DecimalField;
        }

        [Test]
        public void DecimalsWithFrenchCulture()
        {
            var engine = new FileHelperEngine<DecimalTypeWithFrenchConversion>();
            var res = TestCommon.ReadTest<DecimalTypeWithFrenchConversion>(engine, "Good", "NumberFormatFrench.txt");

            Assert.AreEqual(3, res.Length);

            Assert.AreEqual(10248, res[0].IntField);
            CheckDecimal((decimal)32.38, res[0]);

            Assert.AreEqual(10249, res[1].IntField);
            CheckDecimal((decimal)1011.61, res[1]);

            Assert.AreEqual(10250, res[2].IntField);
            CheckDecimal((decimal)1165.83, res[2]);
        }

        [Test]
        public void DecimalsWithFrenchCulture2()
        {
            var engine = new FileHelperEngine<DecimalTypeWithFrenchConversionAsAWhole>();
            var res = TestCommon.ReadTest<DecimalTypeWithFrenchConversionAsAWhole>(engine, "Good", "NumberFormatFrench.txt");

            Assert.AreEqual(3, res.Length);

            Assert.AreEqual(10248, res[0].IntField);
            CheckDecimal((decimal)32.38, res[0]);

            Assert.AreEqual(10249, res[1].IntField);
            CheckDecimal((decimal)1011.61, res[1]);

            Assert.AreEqual(10250, res[2].IntField);
            CheckDecimal((decimal)1165.83, res[2]);
        }

        private static void CheckDecimal(decimal dec, DecimalTypeWithFrenchConversion res)
        {
            Assert.AreEqual((decimal)dec, res.DecimalField);
            Assert.AreEqual((double)dec, res.DoubleField);
            Assert.AreEqual((float)dec, res.FloatField);
        }

        private static void CheckDecimal(decimal dec, DecimalTypeWithFrenchConversionAsAWhole res)
        {
            Assert.AreEqual((decimal)dec, res.DecimalField);
            Assert.AreEqual((double)dec, res.DoubleField);
            Assert.AreEqual((float)dec, res.FloatField);
        }
    }
}