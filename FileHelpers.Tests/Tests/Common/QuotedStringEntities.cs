using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class QuotedStringEntities
	{
		FileHelperEngine engine;

		[Test]
		public void OrdersQuoted()
		{
			engine = new FileHelperEngine(typeof (OrdersQuotedType));

			OrdersQuotedType[] res = (OrdersQuotedType[]) TestCommon.ReadTest(engine, "Good", "QuotedOrders.txt");

			Assert.AreEqual(6, res.Length);

			Assert.AreEqual("VINET", res[0].CustomerID);
			Assert.AreEqual("TO,SP", res[1].CustomerID);
			Assert.AreEqual("HA\"AR", res[2].CustomerID);
			Assert.AreEqual("VICTE", res[3].CustomerID);
			Assert.AreEqual("S\"U\"P\"\"", res[4].CustomerID);
			Assert.AreEqual("HANAR", res[5].CustomerID);
		}

		[Test]
		public void OrdersQuoted2()
		{
			engine = new FileHelperEngine(typeof (OrdersQuoted2Type));

			OrdersQuoted2Type[] res = (OrdersQuoted2Type[]) TestCommon.ReadTest(engine, "Good", "QuotedOrders2.txt");

			Assert.AreEqual(6, res.Length);

			Assert.AreEqual("VINET", res[0].CustomerID);
			Assert.AreEqual("T,O", res[1].CustomerID);
			Assert.AreEqual("HA,,AR", res[2].CustomerID);
			Assert.AreEqual("VICTE", res[3].CustomerID);
			Assert.AreEqual("S,\",\"", res[4].CustomerID);
			Assert.AreEqual("HA,NAR", res[5].CustomerID);
		}

		[Test]
		public void CustomersQuoted()
		{
			engine = new FileHelperEngine(typeof (CustomersQuotedType));

			var res = (CustomersQuotedType[]) TestCommon.ReadTest(engine, "Good", "QuotedCustomers.txt");

			Assert.AreEqual(10, res.Length);

			Assert.AreEqual("Alfreds Futterkiste", res[0].CompanyName);
			Assert.AreEqual("Ana Trujillo Emparedados y helados", res[1].CompanyName);
			Assert.AreEqual(@"Berglunds snabbkp", res[4].CompanyName);
		}

		[Test]
		public void CustomersQuoted2()
		{
			engine = new FileHelperEngine(typeof (CustomersQuotedType2));

			var res = (CustomersQuotedType2[]) TestCommon.ReadTest(engine, "Good", "QuotedCustomers2.txt");

			Assert.AreEqual(10, res.Length);

			Assert.AreEqual("Alfreds Futterkiste", res[0].CompanyName);
			Assert.AreEqual("Ana Trujillo Emparedados y helados", res[1].CompanyName);
			Assert.AreEqual(@"Berglunds snabbkp", res[4].CompanyName);
		}


		[Test]
		public void OrdersQuotedWrite()
		{
			engine = new FileHelperEngine(typeof (OrdersQuotedType));

			OrdersQuotedType[] res = (OrdersQuotedType[]) TestCommon.ReadTest(engine, "Good", "QuotedOrders.txt");
			engine.WriteFile("temp2.txt", res);
			res = (OrdersQuotedType[]) engine.ReadFile("temp2.txt");

			Assert.AreEqual(6, res.Length);

			Assert.AreEqual("VINET", res[0].CustomerID);
			Assert.AreEqual("TO,SP", res[1].CustomerID);
			Assert.AreEqual("HA\"AR", res[2].CustomerID);
			Assert.AreEqual("VICTE", res[3].CustomerID);
			Assert.AreEqual("S\"U\"P\"\"", res[4].CustomerID);
			Assert.AreEqual("HANAR", res[5].CustomerID);
		}


	}

	[DelimitedRecord("|")]
	public class OrdersQuotedType
	{
		public int OrderID;
		[FieldQuoted()]
		public string CustomerID;
		public int EmployeeID;
		public DateTime OrderDate;
		public DateTime RequiredDate;
		[FieldNullValue(typeof (DateTime), "2005-1-1")] 
		public DateTime ShippedDate;
		public int ShipVia;
		public decimal Freight;
	}

	[DelimitedRecord(",")]
	public class OrdersQuoted2Type
	{
		public int OrderID;
		[FieldQuoted()] public string CustomerID;
		public int EmployeeID;
		public DateTime OrderDate;
		public DateTime RequiredDate;
		[FieldNullValue(typeof (DateTime), "2005-1-1")]
		public DateTime ShippedDate;
		public int ShipVia;
		public decimal Freight;
	}

	[DelimitedRecord("|")]
	public class CustomersQuotedType
	{
		public string CustomerID;
		[FieldQuoted(QuoteMode.AlwaysQuoted, MultilineMode.NotAllow)]
		public string CompanyName;
		public string ContactName;
		public string ContactTitle;
		public string Address;
		public string City;
		public string Country;
	}

	[DelimitedRecord("|")]
	public class CustomersQuotedType2
	{
		public string CustomerID;

		[FieldQuoted()]
		[FieldTrim(TrimMode.Both)]
        public string CompanyName;

		public string ContactName;
		public string ContactTitle;
		public string Address;
		public string City;
		public string Country;
	}

}