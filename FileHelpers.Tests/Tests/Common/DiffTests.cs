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
		public void DiffCustomersAllEquals()
		{
			engine = new FileDiffEngine(typeof (CustomersVerticalBar));
			CustomersVerticalBar[] res = engine.OnlyNewRecords(Common.TestPath(@"good\CustomersVerticalBarOlds.txt"),Common.TestPath(@"good\CustomersVerticalBarOlds.txt")) as CustomersVerticalBar[];
			Assert.AreEqual(0, res.Length);
		}

		[Test]
		public void DiffEmptyOld()
		{
			engine = new FileDiffEngine(typeof (CustomersVerticalBar));
			CustomersVerticalBar[] res = engine.OnlyNewRecords(Common.TestPath(@"good\EmptyFile.txt"),Common.TestPath(@"good\CustomersVerticalBarOlds.txt")) as CustomersVerticalBar[];
			Assert.AreEqual(81, res.Length);
		}

		[Test]
		public void DiffEmptyNews()
		{
			engine = new FileDiffEngine(typeof (CustomersVerticalBar));
			CustomersVerticalBar[] res = engine.OnlyNewRecords(Common.TestPath(@"good\CustomersVerticalBarOlds.txt"), Common.TestPath(@"good\EmptyFile.txt")) as CustomersVerticalBar[];
			Assert.AreEqual(0, res.Length);
		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void DiffTypeError()
		{
			engine = new FileDiffEngine(typeof (CustomersFixed));
		}

		[Test]
		public void OnlyNewRecords()
		{
			engine = new FileDiffEngine(typeof (DiffOrdersFixed));
			DiffOrdersFixed[] res = engine.OnlyNewRecords(Common.TestPath(@"good\DiffOrdersOld.txt"), Common.TestPath(@"good\DiffOrdersNew.txt")) as DiffOrdersFixed[];
			Assert.AreEqual(5, res.Length);
			
			res = engine.OnlyMissingRecords(Common.TestPath(@"good\DiffOrdersNew.txt"), Common.TestPath(@"good\DiffOrdersOld.txt")) as DiffOrdersFixed[];
			Assert.AreEqual(5, res.Length);
		}

		[Test]
		public void MissingRecords()
		{
			engine = new FileDiffEngine(typeof (DiffOrdersFixed));
			DiffOrdersFixed[] res = engine.OnlyMissingRecords(Common.TestPath(@"good\DiffOrdersOld.txt"), Common.TestPath(@"good\DiffOrdersNew.txt")) as DiffOrdersFixed[];
			Assert.AreEqual(2, res.Length);
		}

		[Test]
		public void OnlyNoDuplicatedRecords()
		{
			engine = new FileDiffEngine(typeof (DiffOrdersFixed));
			DiffOrdersFixed[] res = engine.OnlyNoDuplicatedRecords(Common.TestPath(@"good\DiffOrdersOld.txt"), Common.TestPath(@"good\DiffOrdersNew.txt")) as DiffOrdersFixed[];
			Assert.AreEqual(7, res.Length);
		}

		[Test]
		public void OnlyDuplicatedRecords()
		{
			engine = new FileDiffEngine(typeof (DiffOrdersFixed));
			DiffOrdersFixed[] res = engine.OnlyDuplicatedRecords(Common.TestPath(@"good\DiffOrdersOld.txt"), Common.TestPath(@"good\DiffOrdersNew.txt")) as DiffOrdersFixed[];
			Assert.AreEqual(10, res.Length);
		}

		[Test]
		public void RemoveDuplicatedRecords()
		{
			FileHelperEngine eng = new FileHelperEngine(typeof (DiffOrdersFixed));
			
			DiffOrdersFixed[] res = (DiffOrdersFixed[]) Common.ReadTest(eng, @"good\DiffOrdersDup.txt");

			Assert.AreEqual(14, res.Length);
			res = (DiffOrdersFixed[]) CommonEngine.RemoveDuplicateRecords(res);
			Assert.AreEqual(4, res.Length);
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