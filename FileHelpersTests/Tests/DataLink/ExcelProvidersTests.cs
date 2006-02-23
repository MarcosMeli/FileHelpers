#if ! MINI

using System;
using System.IO;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpersTests
{
	[TestFixture]
	public class ExcelDataProviderTest
	{
		[Test]
		public void CustomersRead()
		{
			ExcelStorage provider = new ExcelStorage(typeof (CustomersVerticalBar));

			provider.StartRow = 3;
			provider.StartColumn = 2;

			provider.FileName = @"..\data\Excel\Customers.xls";

			long start = DateTime.Now.Ticks;
			object[] res = provider.ExtractRecords();

			TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - start);
			Console.WriteLine(ts.TotalSeconds);

			Assert.AreEqual(91, res.Length);
		}

		[Test]
		public void CustomersWithSheetName()
		{
			ExcelStorage provider = new ExcelStorage(typeof (CustomersVerticalBar));

			provider.SheetName = "WhiteSheet";
			provider.StartRow = 3;
			provider.StartColumn = 2;

			provider.FileName = @"..\data\Excel\Customers.xls";

			object[] res = provider.ExtractRecords();

			Assert.AreEqual(0, res.Length);
		}

		[Test]
		public void CustomersWithSheetName2()
		{
			ExcelStorage provider = new ExcelStorage(typeof (CustomersVerticalBar));

			provider.SheetName = "SheetWith10";
			provider.StartRow = 1;
			provider.StartColumn = 1;

			provider.FileName = @"..\data\Excel\Customers.xls";

			object[] res = provider.ExtractRecords();

			Assert.AreEqual(10, res.Length);
		}


		[Test]
		[ExpectedException(typeof(ExcelBadUsageException))]
		public void CustomersWithSheetNameError()
		{
			ExcelStorage provider = new ExcelStorage(typeof (CustomersVerticalBar));

			provider.SheetName = "SheetNotExists";
			provider.FileName = @"..\data\Excel\Customers.xls";

			provider.ExtractRecords();
		}

		[Test]
		[ExpectedException(typeof(FileNotFoundException))]
		public void CustomersFileError()
		{
			ExcelStorage provider = new ExcelStorage(typeof (CustomersVerticalBar));

			provider.FileName = @"t:\SureThatThisCanExistInAnyMachine.xls";

			provider.ExtractRecords();
		}


		[Test]
		public void CustomersWrite()
		{
			ExcelStorage provider = new ExcelStorage(typeof (CustomersVerticalBar));
			provider.StartRow = 3;
			provider.StartColumn = 2;
			provider.FileName = @"..\data\Excel\Customers.xls";

			object[] res = provider.ExtractRecords();

			long start = DateTime.Now.Ticks;

			provider.FileName = @"salida.xls";

			provider.StartRow = 10;
			provider.StartColumn = 5;
			provider.InsertRecords(res);

			TimeSpan ts = new TimeSpan(DateTime.Now.Ticks - start);
			Console.WriteLine(ts.TotalSeconds);

			if (File.Exists(@"salida.xls")) File.Delete(@"salida.xls");
		}

		[Test]
		public void CustomersRead2()
		{
			ExcelStorage provider = new ExcelStorage(typeof (CustomersVerticalBar));

			provider.StartRow = 3;
			provider.StartColumn = 2;

			provider.FileName = @"..\data\Excel\Customers2.xls";

			object[] res = provider.ExtractRecords();

			Assert.AreEqual(91, res.Length);
		}

	}

}

#endif