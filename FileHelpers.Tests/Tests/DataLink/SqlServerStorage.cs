using System;
using System.IO;
using FileHelpers;
using FileHelpers.DataLink;
using NUnit.Framework;
using System.Data.SqlClient;

namespace FileHelpersTests.DataLink
{
	[TestFixture]
    [Ignore]
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

			OrdersVerticalBar[] res = null;

			try
			{
				res = storage.ExtractRecords() as OrdersVerticalBar[];
			}
			catch(SqlException ex)
			{
				if (ex.Number == 208)
					Assert.Ignore("You dont have this tables in your SqlServer");

				if (ex.Number == 6)
					Assert.Ignore("SqlServer not found, skipping this test.");

				Assert.Ignore(ex.ToString());
			}

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

			try
			{
				FileDataLink.EasyExtractToFile(storage, "tempord.txt");
			}
			catch(SqlException ex)
			{
				if (ex.Number == 208)
					Assert.Ignore("You dont have this tables in your SqlServer");

				if (ex.Number == 6)
					Assert.Ignore("SqlServer not found, skipping this test.");

				Assert.Ignore(ex.ToString());
			}


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

			OrdersVerticalBar[] res = null;
			try
			{
				 res = (OrdersVerticalBar[]) storage.ExtractRecords();
			}
			catch(SqlException ex)
			{
				if (ex.Number == 208)
					Assert.Ignore("You dont have this tables in your SqlServer");

				if (ex.Number == 6)
					Assert.Ignore("SqlServer not found, skipping this test.");

				Assert.Ignore(ex.ToString());
			}

			Assert.AreEqual(100, res.Length);

			Assert.AreEqual("VINET", res[0].CustomerID);
			Assert.AreEqual("TOMSP", res[1].CustomerID);
			Assert.AreEqual("HANAR", res[2].CustomerID);
		}


		[Test]
		public void OrdersExtractBad1()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar), "NEON-64", "Northwind");
			
			storage.FillRecordCallback = new FillRecordHandler(FillRecordOrder);
			
            Assert.Throws<BadUsageException>(()
                 => storage.ExtractRecords());
		}

		[Test]
		public void OrdersExtractBad2()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar));
			
			storage.SelectSql = "SELECT TOP 100 * FROM Orders";
			storage.FillRecordCallback = new FillRecordHandler(FillRecordOrder);

			Assert.Throws<BadUsageException>(()
                 => storage.ExtractRecords());
		}


		[Test]
		[Ignore]
		public void OrdersExtractBad3()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar), "NEON-64", "Northwind");
			
			storage.SelectSql = "SELECT TOP 100 * FROM Orders";

            //try
            //{
            //    storage.ExtractRecords();
            //}
            //catch(SqlException ex)
            //{
            //    if (ex.Number == 208)
            //        Assert.Ignore("You dont have this tables in your SqlServer");

            //    if (ex.Number == 6)
            //        Assert.Ignore("SqlServer not found, skipping this test.");

            //    Assert.Ignore(ex.ToString());
            //}
		}

		[Test]
		public void OrdersExtractBad4()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar), "NEON-64", "SureThatThisDontExist");
			
			storage.SelectSql = "SELECT TOP 100 * FROM Orders";


            Assert.Throws<SqlException>(()
                => storage.ExtractRecords());
		}

		[Test]
		public void OrdersExtractBad5()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar), "WhereIsThisServer", "Northwind");
			
			storage.SelectSql = "SELECT TOP 100 * FROM Orders";

			Assert.Throws<SqlException>(()
                => storage.ExtractRecords());
		}

		protected void FillRecordOrder(object rec, object[] fields)
		{
			OrdersVerticalBar record = (OrdersVerticalBar) rec;

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
		}

		[Test]
		public void CustomerInsert()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(CustomersVerticalBar));
			
			storage.ServerName = "NEON-64";
			storage.DatabaseName = "Northwind";

			storage.InsertSqlCallback = new InsertSqlHandler(GetInsertSqlCust);

			try
			{
				storage.InsertRecords(CommonEngine.ReadFile(typeof(CustomersVerticalBar), TestCommon.GetPath("Good", "CustomersVerticalBar.txt")));
			}
			catch(SqlException ex)
			{
				if (ex.Number == 208)
					Assert.Ignore("You dont have this tables in your SqlServer");

				if (ex.Number == 6)
					Assert.Ignore("SqlServer not found, skipping this test.");

				Assert.Ignore(ex.ToString());
			}


		}

		[Test]
		public void CustomersInsertEasy()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(CustomersVerticalBar));
			
			storage.ServerName = "NEON-64";
			storage.DatabaseName = "Northwind";

			storage.InsertSqlCallback = new InsertSqlHandler(GetInsertSqlCust);

			try
			{
				FileDataLink.EasyInsertFromFile(storage, TestCommon.GetPath("Good", "CustomersVerticalBar.txt"));
			}
			catch(SqlException ex)
			{
				if (ex.Number == 208)
					Assert.Ignore("You dont have this tables in your SqlServer");

				if (ex.Number == 6)
					Assert.Ignore("SqlServer not found, skipping this test.");

				Assert.Ignore(ex.ToString());
			}
		}

		[Test]
		public void OrdersInsertBad()
		{
			SqlServerStorage storage = new SqlServerStorage(typeof(OrdersVerticalBar));
			
			storage.ServerName = "NEON-64";
			storage.DatabaseName = "Northwind";

			storage.InsertSqlCallback = new InsertSqlHandler(GetInsertSqlOrder);
			OrdersVerticalBar[] res = (OrdersVerticalBar[]) CommonEngine.ReadFile(typeof(OrdersVerticalBar), TestCommon.GetPath("Good", "OrdersVerticalBar.txt"));
			OrdersVerticalBar[] res2 = new OrdersVerticalBar[1];
			res2[0] = res[0];

			//storage.ExecuteInBatchSize
			//res2[0].OrderDate = new DateTime(1750, 10, 10);
			try
			{
				storage.InsertRecords(res2);
			}
			catch(SqlException ex)
			{
				if (ex.Number == 208)
					Assert.Ignore("You dont have this tables in your SqlServer");

				if (ex.Number == 6)
					Assert.Ignore("SqlServer not found, skipping this test.");

				Assert.Ignore(ex.ToString());
			}

		}


		#region "  GetInsertSql  "

		protected string GetInsertSqlCust(object record)
		{
			CustomersVerticalBar obj = (CustomersVerticalBar) record;

			return String.Format("INSERT INTO CustomersTemp (Address, City, CompanyName, ContactName, ContactTitle, Country, CustomerID) " +
				" VALUES ( '{0}' , '{1}' , '{2}' , '{3}' , '{4}' , '{5}' , '{6}'  ); ",
				obj.Address.Replace("'", "\""),
				obj.City.Replace("'", "\""),
				obj.CompanyName.Replace("'", "\""),
				obj.ContactName.Replace("'", "\""),
				obj.ContactTitle.Replace("'", "\""),
				obj.Country.Replace("'", "\""),
				obj.CustomerID
				);

		}

		
		protected string GetInsertSqlOrder(object record)
		{
			OrdersVerticalBar obj = (OrdersVerticalBar) record;

			return String.Format("INSERT INTO OrdersTemp (CustomerId, OrderDate) " +
				" VALUES ('{0}' , '{1}'); ",
				obj.CustomerID,
				obj.OrderDate.ToShortDateString()
				);

		}
		#endregion


	}

}

