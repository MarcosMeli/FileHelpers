using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class QuotedStringEntities
	{
		[Test]
		public void OrdersQuoted()
		{
			var engine = new FileHelperEngine<OrdersQuotedType>();

			OrdersQuotedType[] res = TestCommon.ReadTest<OrdersQuotedType>(engine, "Good", "QuotedOrders.txt");

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
			var engine = new FileHelperEngine<OrdersQuoted2Type>();

            var res = TestCommon.ReadTest<OrdersQuoted2Type>(engine, "Good", "QuotedOrders2.txt");

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
			var engine = new FileHelperEngine<CustomersQuotedType>();

            var res = TestCommon.ReadTest<CustomersQuotedType>(engine, "Good", "QuotedCustomers.txt");

			Assert.AreEqual(10, res.Length);

			Assert.AreEqual("Alfreds Futterkiste", res[0].CompanyName);
			Assert.AreEqual("Ana Trujillo Emparedados y helados", res[1].CompanyName);
			Assert.AreEqual(@"Berglunds snabbkp", res[4].CompanyName);
		}

		[Test]
		public void CustomersQuoted2()
		{
			var engine = new FileHelperEngine<CustomersQuotedType2>();

            var res = TestCommon.ReadTest<CustomersQuotedType2>(engine, "Good", "QuotedCustomers2.txt");

			Assert.AreEqual(10, res.Length);

			Assert.AreEqual("Alfreds Futterkiste", res[0].CompanyName);
			Assert.AreEqual("Ana Trujillo Emparedados y helados", res[1].CompanyName);
			Assert.AreEqual(@"Berglunds snabbkp", res[4].CompanyName);
		}


		[Test]
		public void OrdersQuotedWrite()
		{
			var engine = new FileHelperEngine<OrdersQuotedType>();

            OrdersQuotedType[] res = TestCommon.ReadTest<OrdersQuotedType>(engine, "Good", "QuotedOrders.txt");
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