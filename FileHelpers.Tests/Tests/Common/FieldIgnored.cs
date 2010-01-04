using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class FieldIgnored
	{
		FileHelperEngine engine;

		[Test]
		public void IgnoreFirst()
		{
			engine = new FileHelperEngine(typeof (CustomersTabIgnored3));
			
			CustomersTabIgnored3[] res = (CustomersTabIgnored3[]) TestCommon.ReadTest(engine, "Good", "CustomersTabIgnoreFirst.txt");

			Assert.AreEqual(10, res.Length);
			foreach (CustomersTabIgnored3 record in res)
			{
				Assert.AreEqual(null, record.CustomerID);	
			}
		}


		[Test]
		public void IgnoreMiddle()
		{

			engine = new FileHelperEngine(typeof (CustomersTabIgnored2));
			
			CustomersTabIgnored2[] res = (CustomersTabIgnored2[]) TestCommon.ReadTest(engine, "Good", "CustomersTabIgnoreMiddle.txt");

			Assert.AreEqual(10, res.Length);
			foreach (CustomersTabIgnored2 record in res)
			{
				Assert.AreEqual(null, record.ContactName);	
			}
		}

		[Test]
		public void IgnoreLast()
		{
			engine = new FileHelperEngine(typeof (CustomersTabIgnored));
			
			CustomersTabIgnored[] res = (CustomersTabIgnored[]) TestCommon.ReadTest(engine, "Good", "CustomersTabIgnoreLast.txt");

			Assert.AreEqual(10, res.Length);
			foreach (CustomersTabIgnored record in res)
			{
				Assert.AreEqual(null, record.Country);	
			}
		}

		[Test]
		public void IgnoreMiddle2()
		{

			engine = new FileHelperEngine(typeof (OrdersFixedIgnore));
			
			OrdersFixedIgnore[] res = (OrdersFixedIgnore[]) TestCommon.ReadTest(engine, "Good", "OrdersFixedIgnoreMiddle.txt");

			Assert.AreEqual(10, res.Length);
			foreach (OrdersFixedIgnore record in res)
			{
				Assert.AreEqual(0, record.EmployeeID);	
			}
		}




		[DelimitedRecord("\t")]
		public class CustomersTabIgnored3
		{
			[FieldIgnored()]
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

			[FieldIgnored()]
			public string Country;
		}

		[DelimitedRecord("\t")]
		public class CustomersTabIgnored2
		{
			public string CustomerID;
			public string CompanyName;
			[FieldIgnored()]
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

			[FieldIgnored()] public int EmployeeID;

			[FieldFixedLength(10)] public DateTime OrderDate;

			[FieldFixedLength(10)] public DateTime RequiredDate;

			[FieldFixedLength(10)]
			[FieldNullValue(typeof (DateTime), "2005-1-1")] public DateTime ShippedDate;

			[FieldFixedLength(3)] public int ShipVia;

			[FieldFixedLength(10)] public decimal Freight;
		}



	}


}