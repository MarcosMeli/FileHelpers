using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class DiffRecords
	{
		

		[Test]
		public void DiffCustomers()
		{
            var engine = new FileDiffEngine<CustomersVerticalBar>();
			CustomersVerticalBar[] res = engine.OnlyNewRecords(TestCommon.GetPath("Good", "CustomersVerticalBarOlds.txt"), TestCommon.GetPath("Good", "CustomersVerticalBar.txt"));
			Assert.AreEqual(10, res.Length);
			Assert.AreEqual("BLAUS", res[0].CustomerID);
			Assert.AreEqual("BLONP", res[1].CustomerID);
		}

		[Test]
		public void DiffCustomersAllEquals()
		{
            var engine = new FileDiffEngine<CustomersVerticalBar>();
			CustomersVerticalBar[] res = engine.OnlyNewRecords(TestCommon.GetPath("Good", "CustomersVerticalBarOlds.txt"),TestCommon.GetPath("Good", "CustomersVerticalBarOlds.txt"));
			Assert.AreEqual(0, res.Length);
		}

		[Test]
		public void DiffEmptyOld()
		{
            var engine = new FileDiffEngine<CustomersVerticalBar>();
			CustomersVerticalBar[] res = engine.OnlyNewRecords(TestCommon.GetPath("Good", "EmptyFile.txt"),TestCommon.GetPath("Good", "CustomersVerticalBarOlds.txt"));
			Assert.AreEqual(81, res.Length);
		}

		[Test]
		public void DiffEmptyNews()
		{
            var engine = new FileDiffEngine<CustomersVerticalBar>();
			CustomersVerticalBar[] res = engine.OnlyNewRecords(TestCommon.GetPath("Good", "CustomersVerticalBarOlds.txt"), TestCommon.GetPath("Good", "EmptyFile.txt"));
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
			DiffOrdersFixed[] res = engine.OnlyNewRecords(TestCommon.GetPath("Good", "DiffOrdersOld.txt"), TestCommon.GetPath("Good", "DiffOrdersNew.txt"));
			Assert.AreEqual(5, res.Length);
			
			res = engine.OnlyMissingRecords(TestCommon.GetPath("Good", "DiffOrdersNew.txt"), TestCommon.GetPath("Good", "DiffOrdersOld.txt"));
			Assert.AreEqual(5, res.Length);
		}

		[Test]
		public void MissingRecords()
		{
            var engine = new FileDiffEngine<DiffOrdersFixed>();
			DiffOrdersFixed[] res = engine.OnlyMissingRecords(TestCommon.GetPath("Good", "DiffOrdersOld.txt"), TestCommon.GetPath("Good", "DiffOrdersNew.txt"));
			Assert.AreEqual(2, res.Length);
		}

		[Test]
		public void OnlyNoDuplicatedRecords()
		{
            var engine = new FileDiffEngine<DiffOrdersFixed>();
			DiffOrdersFixed[] res = engine.OnlyNoDuplicatedRecords(TestCommon.GetPath("Good", "DiffOrdersOld.txt"), TestCommon.GetPath("Good", "DiffOrdersNew.txt"));
			Assert.AreEqual(7, res.Length);
		}

		[Test]
		public void OnlyDuplicatedRecords()
		{
            var engine = new FileDiffEngine<DiffOrdersFixed>();
			DiffOrdersFixed[] res = engine.OnlyDuplicatedRecords(TestCommon.GetPath("Good", "DiffOrdersOld.txt"), TestCommon.GetPath("Good", "DiffOrdersNew.txt"));
			Assert.AreEqual(10, res.Length);
		}

		[Test]
		public void RemoveDuplicatedRecords()
		{
			FileHelperEngine eng = new FileHelperEngine(typeof (DiffOrdersFixed));
			
			DiffOrdersFixed[] res = (DiffOrdersFixed[]) TestCommon.ReadTest(eng, "Good", "DiffOrdersDup.txt");

			Assert.AreEqual(14, res.Length);
			res = (DiffOrdersFixed[]) CommonEngine.RemoveDuplicateRecords(res);
			Assert.AreEqual(4, res.Length);
		}


		[FixedLengthRecord]
		private class DiffOrdersFixed
            : IComparableRecord<DiffOrdersFixed>
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
            
		    public bool IsEqualRecord(DiffOrdersFixed other)
		    {
                return this.OrderID == other.OrderID;
		    }
		}


	}
}