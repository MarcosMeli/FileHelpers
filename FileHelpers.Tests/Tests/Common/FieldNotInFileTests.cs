using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class FieldNotInFileTests
	{
		[Test]
		public void IgnoreFirst()
		{
			var engine = new FileHelperEngine<CustomersTabIgnored3>();
			
			var res = TestCommon.ReadTest<CustomersTabIgnored3>(engine, "Good", "CustomersTabIgnoreFirst.txt");

			Assert.AreEqual(10, res.Length);
			foreach (CustomersTabIgnored3 record in res)
			{
				Assert.AreEqual(null, record.CustomerID);	
			}
		}


		[Test]
		public void IgnoreMiddle()
		{

			var engine = new FileHelperEngine<CustomersTabIgnored2>();

            var  res = TestCommon.ReadTest<CustomersTabIgnored2>(engine, "Good", "CustomersTabIgnoreMiddle.txt");

			Assert.AreEqual(10, res.Length);
			foreach (CustomersTabIgnored2 record in res)
			{
				Assert.AreEqual(null, record.ContactName);	
			}
		}

		[Test]
		public void IgnoreLast()
		{
			var engine = new FileHelperEngine<CustomersTabIgnored>();

            CustomersTabIgnored[] res = TestCommon.ReadTest<CustomersTabIgnored>(engine, "Good", "CustomersTabIgnoreLast.txt");

			Assert.AreEqual(10, res.Length);
			foreach (CustomersTabIgnored record in res)
			{
				Assert.AreEqual(null, record.Country);	
			}
		}

		[Test]
		public void IgnoreMiddle2()
		{

			var engine = new FileHelperEngine<OrdersFixedIgnore>();

            OrdersFixedIgnore[] res = TestCommon.ReadTest<OrdersFixedIgnore>(engine, "Good", "OrdersFixedIgnoreMiddle.txt");

			Assert.AreEqual(10, res.Length);
			foreach (OrdersFixedIgnore record in res)
			{
				Assert.AreEqual(0, record.EmployeeID);	
			}
		}




		[DelimitedRecord("\t")]
		public class CustomersTabIgnored3
		{
			[FieldNotInFile()]
			public string CustomerID;
			public string CompanyName;
			public string ContactName;
			public string ContactTitle;
			public string Address;
			public string City;
			public string Country;
		}

		[DelimitedRecord("\t")]
		public class CustomersTabIgnored
		{
			public string CustomerID;
			public string CompanyName;
			public string ContactName;
			public string ContactTitle;
			public string Address;
			public string City;

			[FieldNotInFile()]
			public string Country;
		}

		[DelimitedRecord("\t")]
		public class CustomersTabIgnored2
		{
			public string CustomerID;
			public string CompanyName;
			[FieldNotInFile()]
			public string ContactName;
			public string ContactTitle;
			public string Address;
			public string City;
			public string Country;
		}



		[FixedLengthRecord]
		public class OrdersFixedIgnore
		{
			[FieldFixedLength(7)] public int OrderID;

			[FieldFixedLength(12)] public string CustomerID;

			[FieldNotInFile()] public int EmployeeID;

			[FieldFixedLength(10)] public DateTime OrderDate;

			[FieldFixedLength(10)] public DateTime RequiredDate;

			[FieldFixedLength(10)]
			[FieldNullValue(typeof (DateTime), "2005-1-1")] public DateTime ShippedDate;

			[FieldFixedLength(3)] public int ShipVia;

			[FieldFixedLength(10)] public decimal Freight;
		}



	}


}