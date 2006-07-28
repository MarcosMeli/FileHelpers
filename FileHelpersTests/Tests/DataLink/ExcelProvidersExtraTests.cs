#if ! MINI

using System;
using System.IO;
using FileHelpers;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpersTests.DataLink
{
	[TestFixture]
	public class ExcelDataProviderExtraTests
	{
		[Test]
		public void OneColumn()
		{
			ExcelStorage provider = new ExcelStorage(typeof (OneColumnType), 1, 1);

			provider.FileName = @"..\data\Excel\OneColumn.xls";

			object[] res = provider.ExtractRecords();

			Assert.AreEqual(50, res.Length);
		}

		[DelimitedRecord("|")]
		internal class OneColumnType
		{
			public string CustomerCode;
		}

		[Test]
		public void OrdersRead()
		{
			ExcelStorage provider = new ExcelStorage(typeof (OrdersExcelType), 1, 1);

			provider.FileName = @"..\data\Excel\Orders.xls";

			object[] res = provider.ExtractRecords();

			Assert.AreEqual(830, res.Length);
		}


		[Test]
		public void OrdersWrite()
		{
			FileHelperEngine engine = new FileHelperEngine(typeof(OrdersExcelType));

			OrdersExcelType[] resFile = (OrdersExcelType[]) Common.ReadTest(engine, @"Good\OrdersWithOutDates.txt");

			ExcelStorage provider = new ExcelStorage(typeof (OrdersExcelType));
			provider.StartRow = 1;
			provider.StartColumn = 1;
			provider.FileName = @"c:\tempex.xls";
			provider.OverrideFile = true;

			provider.InsertRecords(resFile);

			OrdersExcelType[] res = (OrdersExcelType[]) provider.ExtractRecords();
			
			if (File.Exists(@"c:\tempex.xls")) File.Delete(@"c:\tempex.xls");

			Assert.AreEqual(resFile.Length, res.Length);

			for(int i =0; i < res.Length; i++)
			{
				Assert.AreEqual(resFile[i].CustomerID, res[i].CustomerID);
				Assert.AreEqual(resFile[i].EmployeeID, res[i].EmployeeID);
				Assert.AreEqual(resFile[i].Freight, res[i].Freight);
				Assert.AreEqual(resFile[i].OrderID, res[i].OrderID);
				Assert.AreEqual(resFile[i].ShipVia, res[i].ShipVia);
			}

		}


		[DelimitedRecord("\t")]
		public class OrdersExcelType
		{
			public int OrderID;

			public string CustomerID;

			public int EmployeeID;

			public int ShipVia;

			public decimal Freight;
		}

		[DelimitedRecord("\t")]
		private class SmallEnumType
		{
			public NetVisibility Visibility;

			public SmallEnumType()
			{}

			public SmallEnumType(NetVisibility v)
			{
				Visibility = v;
			}
		}

		[Test]
		public void OrdersReadWithErrors()
		{
			ExcelStorage provider = new ExcelStorage(typeof (OrdersExcelType), 1, 1);
			provider.FileName = @"..\data\Excel\Orders.xls";
			provider.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

			object[] res = provider.ExtractRecords();

			Assert.AreEqual(830, res.Length);
		}


		[Test]
		[ExpectedException(typeof(NotImplementedException))]
		public void OrdersWithDate()
		{
			ExcelStorage provider = new ExcelStorage(typeof (OrdersExcelWithDate), 1, 1);

			provider.FileName = @"..\data\Excel\Orders.xls";
		
			object[] res = provider.ExtractRecords();

			Assert.AreEqual(830, res.Length);
		}

		[Test]
		[ExpectedException(typeof(ExcelBadUsageException))]
		public void NoTemplate()
		{
			ExcelStorage provider = new ExcelStorage(typeof (CustomersVerticalBar), 1, 1);
			provider.TemplateFile = @"..\the template is not there.xls";
			provider.FileName = @"output.xls";
		
			provider.InsertRecords(new object[] {new CustomersVerticalBar()});
		}

		[Test]
		public void OrdersWriteWithTemplate()
		{
			FileHelperEngine engine = new FileHelperEngine(typeof(OrdersExcelType));

			OrdersExcelType[] resFile = (OrdersExcelType[]) Common.ReadTest(engine, @"Good\OrdersWithOutDates.txt");

			ExcelStorage provider = new ExcelStorage(typeof (OrdersExcelType));
			provider.StartRow = 4;
			provider.StartColumn = 1;
			provider.FileName = @"c:\tempex.xls";
			provider.TemplateFile = Common.TestPath(@"Excel\Template.xls");
			provider.OverrideFile = true;

			provider.InsertRecords(resFile);

			OrdersExcelType[] res = (OrdersExcelType[]) provider.ExtractRecords();
			
			if (File.Exists(@"c:\tempex.xls")) File.Delete(@"c:\tempex.xls");

			Assert.AreEqual(resFile.Length, res.Length);

			for(int i =0; i < res.Length; i++)
			{
				Assert.AreEqual(resFile[i].CustomerID, res[i].CustomerID);
				Assert.AreEqual(resFile[i].EmployeeID, res[i].EmployeeID);
				Assert.AreEqual(resFile[i].Freight, res[i].Freight);
				Assert.AreEqual(resFile[i].OrderID, res[i].OrderID);
				Assert.AreEqual(resFile[i].ShipVia, res[i].ShipVia);
			}

		}


		[DelimitedRecord("\t")]
		public class OrdersExcelWithDate
		{
			public int OrderID;

			public string CustomerID;

			public DateTime WhyNotAllowMe;

		}

		[Test]
		public void EnumConverter()
		{
			ExcelStorage provider = new ExcelStorage(typeof(SmallEnumType), 1, 1);

			provider.FileName = @"..\data\Excel\OneColumnEnum.xls";

			SmallEnumType[] res = (SmallEnumType[]) provider.ExtractRecords();

			Assert.AreEqual(10, res.Length);
			Assert.AreEqual(NetVisibility.Public, res[0].Visibility);
			Assert.AreEqual(NetVisibility.Private, res[1].Visibility);
			Assert.AreEqual(NetVisibility.Protected, res[9].Visibility);
		}

		[Test]
		public void EnumConverterBad()
		{
			ExcelStorage provider = new ExcelStorage(typeof(SmallEnumType), 4, 2);
			provider.FileName = @"..\data\Excel\OneColumnEnumBad.xls";
			provider.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            
			SmallEnumType[] res = (SmallEnumType[]) provider.ExtractRecords();

			Assert.AreEqual(9, res.Length);
			Assert.AreEqual(NetVisibility.Public, res[0].Visibility);
			Assert.AreEqual(NetVisibility.Private, res[1].Visibility);
			Assert.AreEqual(NetVisibility.Protected, res[8].Visibility);

			Assert.AreEqual(1, provider.ErrorManager.ErrorCount);
			Assert.AreEqual(8, provider.ErrorManager.Errors[0].LineNumber);
			Assert.AreEqual("BadValue", provider.ErrorManager.Errors[0].RecordString);

		}


	}

}

#endif