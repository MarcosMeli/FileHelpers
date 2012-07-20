using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.Converters
{
	[TestFixture]
	public class DateFormat
	{
		[Test]
		public void DifferentSpanishFormat()
		{
			var engine = new FileHelperEngine<DateFormatType1>();

			var res = TestCommon.ReadTest<DateFormatType1>(engine, "Good", "DateFormat1.txt");

			Assert.AreEqual(6, res.Length);

			Assert.AreEqual(new DateTime(1996, 7, 4), res[0].OrderDate);
			Assert.AreEqual(new DateTime(1996, 7, 5), res[1].OrderDate);
			Assert.AreEqual(new DateTime(1996, 7, 8), res[2].OrderDate);
			Assert.AreEqual(new DateTime(1996, 7, 8), res[3].OrderDate);

			Assert.AreEqual(new DateTime(1996, 8, 1), res[0].RequiredDate);
			Assert.AreEqual(new DateTime(1996, 8, 16), res[1].RequiredDate);
			Assert.AreEqual(new DateTime(1996, 8, 5), res[2].RequiredDate);
			Assert.AreEqual(new DateTime(1996, 8, 5), res[3].RequiredDate);

			Assert.AreEqual(new DateTime(1996, 7, 16), res[0].ShippedDate);
			Assert.AreEqual(new DateTime(1996, 7, 10), res[1].ShippedDate);
			Assert.AreEqual(new DateTime(1996, 7, 12), res[2].ShippedDate);
			Assert.AreEqual(new DateTime(1996, 7, 15), res[3].ShippedDate);

		}

		[Test]
		public void DifferentEnglishFormat()
		{
			var engine = new FileHelperEngine<DateFormatType2>();

            var res = TestCommon.ReadTest<DateFormatType2>(engine, "Good", "DateFormat2.txt");

			Assert.AreEqual(6, res.Length);

			Assert.AreEqual(new DateTime(1996, 7, 4), res[0].OrderDate);
			Assert.AreEqual(new DateTime(1996, 7, 5), res[1].OrderDate);
			Assert.AreEqual(new DateTime(1996, 7, 8), res[2].OrderDate);
			Assert.AreEqual(new DateTime(1996, 7, 8), res[3].OrderDate);

			Assert.AreEqual(new DateTime(1996, 8, 1), res[0].RequiredDate);
			Assert.AreEqual(new DateTime(1996, 8, 16), res[1].RequiredDate);
			Assert.AreEqual(new DateTime(1996, 8, 5), res[2].RequiredDate);
			Assert.AreEqual(new DateTime(1996, 8, 5), res[3].RequiredDate);

			Assert.AreEqual(new DateTime(1996, 7, 16), res[0].ShippedDate);
			Assert.AreEqual(new DateTime(1996, 7, 10), res[1].ShippedDate);
			Assert.AreEqual(new DateTime(1996, 7, 12), res[2].ShippedDate);
			Assert.AreEqual(new DateTime(1996, 7, 15), res[3].ShippedDate);

		}


        [Test]
        public void NullDateFormat()
        {
            String data = "23/11/2010,24/11/2010\n,\n          ,          \n";

            var engine = new FileHelperEngine<NullDateFormat>();
            NullDateFormat[] result = engine.ReadString(data);
            Assert.AreEqual(new DateTime(2010, 11, 23), result[0].OrderDate, "Order date should be 23/11/2010 from first line" );
            Assert.AreEqual(new DateTime(2010, 11, 24), result[0].ShipDate, "Ship date should be 24/11/2010 from first line");
            Assert.AreEqual(null, result[1].OrderDate, "Order date should be null on second line");
            Assert.AreEqual(null, result[1].ShipDate, "Ship date should be null on second line");
            Assert.AreEqual(null, result[2].OrderDate, "Order date should be null on third line with blanks");
            Assert.AreEqual(null, result[2].ShipDate, "Ship date should be null on third line with blanks");
        }

	}


    [DelimitedRecord(",")]
    public class NullDateFormat
    {
        [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
        public DateTime? OrderDate;

        [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
        public Nullable<DateTime> ShipDate;
    }

    [DelimitedRecord(",")]
	public class DateFormatType1
	{
		public int OrderID;
		public int EmployeeID;
		[FieldConverter(ConverterKind.Date, "d-M-yyyy")] public DateTime OrderDate;
		public DateTime RequiredDate;
		[FieldConverter(ConverterKind.Date, "d/M/yy")] public DateTime ShippedDate;
	}

	[DelimitedRecord(",")]
	public class DateFormatType2
	{
		public int OrderID;
		public int EmployeeID;
		[FieldConverter(ConverterKind.Date, "M-d-yyyy")] public DateTime OrderDate;
		[FieldConverter(ConverterKind.Date, "MMddyyyy")] public DateTime RequiredDate;
		[FieldConverter(ConverterKind.Date, "M/d/yy")] public DateTime ShippedDate;
	}
}