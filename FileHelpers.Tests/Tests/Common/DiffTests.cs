using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class DiffRecords
	{
		

		[Test]
		public void DiffCustomers()
		{
            var engine = new FileDiffEngine<CustomersVerticalBar>();
			CustomersVerticalBar[] res = engine.OnlyNewRecords(Common.TestPath(@"good\CustomersVerticalBarOlds.txt"), Common.TestPath(@"good\CustomersVerticalBar.txt"));
			Assert.AreEqual(10, res.Length);
			Assert.AreEqual("BLAUS", res[0].CustomerID);
			Assert.AreEqual("BLONP", res[1].CustomerID);
		}

		[Test]
		public void DiffCustomersAllEquals()
		{
            var engine = new FileDiffEngine<CustomersVerticalBar>();
			CustomersVerticalBar[] res = engine.OnlyNewRecords(Common.TestPath(@"good\CustomersVerticalBarOlds.txt"),Common.TestPath(@"good\CustomersVerticalBarOlds.txt"));
			Assert.AreEqual(0, res.Length);
		}

		[Test]
		public void DiffEmptyOld()
		{
            var engine = new FileDiffEngine<CustomersVerticalBar>();
			CustomersVerticalBar[] res = engine.OnlyNewRecords(Common.TestPath(@"good\EmptyFile.txt"),Common.TestPath(@"good\CustomersVerticalBarOlds.txt"));
			Assert.AreEqual(81, res.Length);
		}

		[Test]
		public void DiffEmptyNews()
		{
            var engine = new FileDiffEngine<CustomersVerticalBar>();
			CustomersVerticalBar[] res = engine.OnlyNewRecords(Common.TestPath(@"good\CustomersVerticalBarOlds.txt"), Common.TestPath(@"good\EmptyFile.txt"));
			Assert.AreEqual(0, res.Length);
		}

        // Automaticaly Checked now by compiler
        //[Test]
        //public void DiffTypeError()
        //{
        //    Assert.Throws<BadUsageException>(
        //        () => new FileDiffEngine<CustomersFixed>());
        //}

		[Test]
		public void OnlyNewRecords()
		{
            var engine = new FileDiffEngine<DiffOrdersFixed>();
			DiffOrdersFixed[] res = engine.OnlyNewRecords(Common.TestPath(@"good\DiffOrdersOld.txt"), Common.TestPath(@"good\DiffOrdersNew.txt"));
			Assert.AreEqual(5, res.Length);
			
			res = engine.OnlyMissingRecords(Common.TestPath(@"good\DiffOrdersNew.txt"), Common.TestPath(@"good\DiffOrdersOld.txt"));
			Assert.AreEqual(5, res.Length);
		}

		[Test]
		public void MissingRecords()
		{
            var engine = new FileDiffEngine<DiffOrdersFixed>();
			DiffOrdersFixed[] res = engine.OnlyMissingRecords(Common.TestPath(@"good\DiffOrdersOld.txt"), Common.TestPath(@"good\DiffOrdersNew.txt"));
			Assert.AreEqual(2, res.Length);
		}

		[Test]
		public void OnlyNoDuplicatedRecords()
		{
            var engine = new FileDiffEngine<DiffOrdersFixed>();
			DiffOrdersFixed[] res = engine.OnlyNoDuplicatedRecords(Common.TestPath(@"good\DiffOrdersOld.txt"), Common.TestPath(@"good\DiffOrdersNew.txt"));
			Assert.AreEqual(7, res.Length);
		}

		[Test]
		public void OnlyDuplicatedRecords()
		{
            var engine = new FileDiffEngine<DiffOrdersFixed>();
			DiffOrdersFixed[] res = engine.OnlyDuplicatedRecords(Common.TestPath(@"good\DiffOrdersOld.txt"), Common.TestPath(@"good\DiffOrdersNew.txt"));
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