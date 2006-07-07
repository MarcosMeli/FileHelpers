using System;
using System.Collections;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class DiffRecords
	{
		FileDiffEngine engine;

		[Test]
		public void DiffCustomers()
		{
			engine = new FileDiffEngine(typeof (CustomersVerticalBar));
			CustomersVerticalBar[] res = engine.OnlyNewRecords(Common.TestPath(@"good\CustomersVerticalBarOlds.txt"), Common.TestPath(@"good\CustomersVerticalBar.txt")) as CustomersVerticalBar[];
			Assert.AreEqual(10, res.Length);
			Assert.AreEqual("BLAUS", res[0].CustomerID);
			Assert.AreEqual("BLONP", res[1].CustomerID);
		}


		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void DiffTypeError()
		{
			engine = new FileDiffEngine(typeof (CustomersFixed));
		}

		[Test]
		public void SmallDiff()
		{
			engine = new FileDiffEngine(typeof (DiffOrdersFixed));
			DiffOrdersFixed[] res = engine.OnlyNewRecords(Common.TestPath(@"good\CustomersVerticalBarOlds.txt"), Common.TestPath(@"good\CustomersVerticalBar.txt")) as DiffOrdersFixed[];

			Assert.AreEqual(10, res.Length);
			Assert.AreEqual("BLAUS", res[0].CustomerID);
			Assert.AreEqual("BLONP", res[1].CustomerID);

		}

		[FixedLengthRecord]
		private class DiffOrdersFixed: IComparableRecord
		{
			[FieldFixedLength(7)] public int OrderID;

			[FieldFixedLength(12)] public string CustomerID;

			[FieldFixedLength(3)] public int EmployeeID;

			[FieldFixedLength(10)] public DateTime OrderDate;

			[FieldFixedLength(10)] public DateTime RequiredDate;

			[FieldFixedLength(10)]
			[FieldNullValue(typeof (DateTime), "2005-1-1")] public DateTime ShippedDate;

			[FieldFixedLength(3)] public int ShipVia;

			[FieldFixedLength(10)] public decimal Freight;

			
			public bool IsEqualRecord(object record)
			{
				DiffOrdersFixed rec = (DiffOrdersFixed) record;
				return this.OrderID == rec.OrderID;
			}
		}


	}
}