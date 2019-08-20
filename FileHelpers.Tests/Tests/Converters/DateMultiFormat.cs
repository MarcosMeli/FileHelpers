using System;
using NUnit.Framework;

namespace FileHelpers.Tests.Converters
{
    [TestFixture]
    public class DateMultiFormat
    {
        [Test]
        public void NullDateFormat()
        {
            string data = string.Format("23/11/2010,24/11/2010{0},{0},{0}", Environment.NewLine);

            var engine = new FileHelperEngine<NullDateFormatMulti>();
            NullDateFormatMulti[] result = engine.ReadString(data);
            Assert.AreEqual(new DateTime(2010, 11, 23),
                result[0].OrderDate,
                "Order date should be 23/11/2010 from first line");
            Assert.AreEqual(new DateTime(2010, 11, 24),
                result[0].ShipDate,
                "Ship date should be 24/11/2010 from first line");
            Assert.AreEqual(null, result[1].OrderDate, "Order date should be null on second line");
            Assert.AreEqual(null, result[1].ShipDate, "Ship date should be null on second line");
            Assert.AreEqual(null, result[2].OrderDate, "Order date should be null on third line with blanks");
            Assert.AreEqual(null, result[2].ShipDate, "Ship date should be null on third line with blanks");

            string newData = engine.WriteString(result);
            Assert.AreEqual(data, newData, "Round trip should match");
        }
    }

    [DelimitedRecord(",")]
    public class NullDateFormatMulti
    {
        [FieldConverter(ConverterKind.DateMultiFormat, "dd/MM/yyyy", "d/M/yyyy")]
        public DateTime? OrderDate;

        [FieldConverter(ConverterKind.DateMultiFormat, "dd/MM/yyyy", "d/M/yyyy")]
        public Nullable<DateTime> ShipDate;
    }
}