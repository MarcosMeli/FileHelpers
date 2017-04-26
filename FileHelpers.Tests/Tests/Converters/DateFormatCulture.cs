using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using NFluent;
using NUnit.Framework;

namespace FileHelpers.Tests.Converters
{
    [TestFixture]
    public class DateFormatCulture
    {

        [DelimitedRecord(",")]
        private class OnlyOneDateFormatEnglish1
        {
            [FieldConverter(ConverterKind.Date, "dd/MMMM/yy", "en")]
            public DateTime Fecha;
        }

        [DelimitedRecord(",")]
        private class OnlyOneDateFormatEnglish2
        {
            [FieldConverter(ConverterKind.Date, "dd/MMMM/yy", "en-US")]
            public DateTime Fecha;
        }


        [DelimitedRecord(",")]
        private class OnlyOneDateFormatEnglishBadCulture
        {
            [FieldConverter(ConverterKind.Date, "dd/MMMM/yy", "es")]
            public DateTime Fecha;
        }

        [DelimitedRecord(",")]
        private class OnlyOneDateFormatSpanish1
        {
            [FieldConverter(ConverterKind.Date, "dd/MMMM/yy", "es")]
            public DateTime Fecha;
        }

        [DelimitedRecord(",")]
        private class OnlyOneDateFormatSpanish2
        {
            [FieldConverter(ConverterKind.Date, "dd/MMMM/yy", "es-AR")]
            public DateTime Fecha;
        }

        private void CheckDates<T>(string[] dates) where T : class
        {
            var dateString = string.Join(Environment.NewLine, dates) + Environment.NewLine;
            var engine = new FileHelperEngine<T>();
            T[] res = engine.ReadString(dateString);

            dynamic record = res[0];

            Check.That((DateTime) record.Fecha).IsEqualTo(new DateTime(1996, 7, 16));
            record = res[5];
            Check.That((DateTime) record.Fecha).IsEqualTo(new DateTime(1996, 8, 16));

            var writtenString = engine.WriteString(res);
            var expected = dateString.ToLower();
            Check.That(writtenString.ToLower()).IsEqualTo(expected);
        }




        [Test]
        public void SpanishMonthFormatParsing1()
        {
            var dates = new[]
                {"16/Julio/96", "10/Diciembre/96", "12/Julio/96", "15/Enero/96", "11/Julio/96", "16/Agosto/96"};

            CheckDates<OnlyOneDateFormatSpanish1>(dates);
        }

        [Test]
        public void SpanishMonthFormatParsing2()
        {
            var dates = new[]
                {"16/Julio/96", "10/Diciembre/96", "12/Julio/96", "15/Enero/96", "11/Julio/96", "16/Agosto/96"};

            CheckDates<OnlyOneDateFormatSpanish2>(dates);
        }

        [Test]
        public void EnglishMonthFormatParsing1()
        {
            var dates = new[]
                {"16/July/96", "10/December/96", "12/July/96", "15/January/96", "11/July/96", "16/August/96"};

            CheckDates<OnlyOneDateFormatEnglish1>(dates);
        }

        [Test]
        public void EnglishMonthFormatParsing2()
        {
            var dates = new[]
                {"16/July/96", "10/December/96", "12/July/96", "15/January/96", "11/July/96", "16/August/96"};

            CheckDates<OnlyOneDateFormatEnglish2>(dates);
        }
        


        [Test]
        public void EnglishBadCulture()
        {
            var dates = new[]
                {"16/July/96", "10/December/96", "12/July/96", "15/January/96", "11/July/96", "16/August/96"};

            Check.ThatCode(() =>
                CheckDates<OnlyOneDateFormatEnglishBadCulture>(dates)).Throws<ConvertException>();

        }
    }
}