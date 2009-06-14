using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Converters
{
	[TestFixture]
	public class DateFormat
	{
		FileHelperEngine engine;

		[Test]
		public void DiferentSpanishFormat()
		{
			engine = new FileHelperEngine(typeof (DateFormatType1));

			DateFormatType1[] res = (DateFormatType1[]) TestCommon.ReadTest(engine, @"Good\DateFormat1.txt");

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
		public void DiferentEnglishFormat()
		{
			engine = new FileHelperEngine(typeof (DateFormatType2));

			DateFormatType2[] res = (DateFormatType2[]) TestCommon.ReadTest(engine, @"Good\DateFormat2.txt");

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