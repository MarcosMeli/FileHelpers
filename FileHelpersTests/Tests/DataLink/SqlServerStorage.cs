using System;
using System.IO;
using FileHelpers;
using FileHelpers.DataLink;
using NUnit.Framework;

namespace FileHelpersTests.DataLink
{
	[TestFixture]
	public class SqlServer
	{
		[Test]
		public void OrdersExtract()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar));
			
			storage.ServerName = "NEON-64";
			storage.DatabaseName = "Northwind";

			storage.SelectSql = "SELECT * FROM Orders";
			storage.FillRecordCallback = new FillRecordHandler(FillRecordOrder);

			OrdersVerticalBar[] res = storage.ExtractRecords() as OrdersVerticalBar[];

			Assert.AreEqual(830, res.Length);

			Assert.AreEqual("VINET", res[0].CustomerID);
			Assert.AreEqual("TOMSP", res[1].CustomerID);
			Assert.AreEqual("HANAR", res[2].CustomerID);
		}

		[Test]
		public void OrdersExtractToFile()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar));
			
			storage.ServerName = "NEON-64";
			storage.DatabaseName = "Northwind";

			storage.SelectSql = "SELECT * FROM Orders";
			storage.FillRecordCallback = new FillRecordHandler(FillRecordOrder);

			FileDataLink.EasyExtractToFile(storage, "tempord.txt");


			FileDataLink link = new FileDataLink(storage);
			link.ExtractToFile("tempord.txt");

			OrdersVerticalBar[] res = CommonEngine.ReadFile(typeof(OrdersVerticalBar), "tempord.txt") as OrdersVerticalBar[];

			if (File.Exists("tempord.txt")) File.Delete("tempord.txt");

			Assert.AreEqual(830, res.Length);

			Assert.AreEqual("VINET", res[0].CustomerID);
			Assert.AreEqual("TOMSP", res[1].CustomerID);
			Assert.AreEqual("HANAR", res[2].CustomerID);
		}


		[Test]
		public void OrdersExtract2()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar), "NEON-64", "Northwind");
			
			storage.SelectSql = "SELECT TOP 100 * FROM Orders";
			storage.FillRecordCallback = new FillRecordHandler(FillRecordOrder);

			OrdersVerticalBar[] res = (OrdersVerticalBar[]) storage.ExtractRecords();

			Assert.AreEqual(100, res.Length);

			Assert.AreEqual("VINET", res[0].CustomerID);
			Assert.AreEqual("TOMSP", res[1].CustomerID);
			Assert.AreEqual("HANAR", res[2].CustomerID);
		}


		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void OrdersExtractBad1()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar), "NEON-64", "Northwind");
			
			storage.FillRecordCallback = new FillRecordHandler(FillRecordOrder);
			storage.ExtractRecords();
		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void OrdersExtractBad2()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar));
			
			storage.SelectSql = "SELECT TOP 100 * FROM Orders";
			storage.FillRecordCallback = new FillRecordHandler(FillRecordOrder);

			storage.ExtractRecords();
		}


		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void OrdersExtractBad3()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar), "NEON-64", "Northwind");
			
			storage.SelectSql = "SELECT TOP 100 * FROM Orders";

			storage.ExtractRecords();
		}

		[Test]
		[ExpectedException(typeof(System.Data.SqlClient.SqlException))]
		public void OrdersExtractBad4()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar), "NEON-64", "SureThatThisDontExist");
			
			storage.SelectSql = "SELECT TOP 100 * FROM Orders";

			storage.ExtractRecords();
		}

		[Test]
		[ExpectedException(typeof(System.Data.SqlClient.SqlException))]
		public void OrdersExtractBad5()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar), "WhereIsThisServer", "Northwind");
			
			storage.SelectSql = "SELECT TOP 100 * FROM Orders";

			storage.ExtractRecords();
		}

		protected object FillRecordOrder(object[] fields)
		{
			OrdersVerticalBar record = new OrdersVerticalBar();

			record.OrderID = (int) fields[0];
			record.CustomerID = (string) fields[1];
			record.EmployeeID = (int) fields[2];
			record.OrderDate = (DateTime) fields[3];
			record.RequiredDate = (DateTime) fields[4];
			if (fields[5] != DBNull.Value)
				record.ShippedDate = (DateTime) fields[5];
			else
				record.ShippedDate = DateTime.MinValue;
			record.ShipVia = (int) fields[6];
			record.Freight = (decimal) fields[7];

			return record;
		}

	}

}

