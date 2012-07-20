using System;
using System.Collections;
using System.Collections.Generic;
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
            CustomersVerticalBar[] res = engine.OnlyNewRecords(FileTest.Good.CustomersVerticalBarOlds.Path, FileTest.Good.CustomersVerticalBar.Path);
			Assert.AreEqual(10, res.Length);
			Assert.AreEqual("BLAUS", res[0].CustomerID);
			Assert.AreEqual("BLONP", res[1].CustomerID);
		}

		[Test]
		public void DiffCustomersAllEquals()
		{
            var engine = new FileDiffEngine<CustomersVerticalBar>();
            CustomersVerticalBar[] res = engine.OnlyNewRecords(FileTest.Good.CustomersVerticalBarOlds.Path, FileTest.Good.CustomersVerticalBarOlds.Path);
			Assert.AreEqual(0, res.Length);
		}

		[Test]
		public void DiffEmptyOld()
		{
            var engine = new FileDiffEngine<CustomersVerticalBar>();
            CustomersVerticalBar[] res = engine.OnlyNewRecords(FileTest.Good.EmptyFile.Path, FileTest.Good.CustomersVerticalBarOlds.Path);
			Assert.AreEqual(81, res.Length);
		}

		[Test]
		public void DiffEmptyNews()
		{
            var engine = new FileDiffEngine<CustomersVerticalBar>();
            CustomersVerticalBar[] res = engine.OnlyNewRecords(FileTest.Good.CustomersVerticalBarOlds.Path, FileTest.Good.EmptyFile.Path);
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
            DiffOrdersFixed[] res = engine.OnlyNewRecords(FileTest.Good.DiffOrdersOld.Path, FileTest.Good.DiffOrdersNew.Path);
			Assert.AreEqual(5, res.Length);

            res = engine.OnlyMissingRecords(FileTest.Good.DiffOrdersNew.Path, FileTest.Good.DiffOrdersOld.Path);
			Assert.AreEqual(5, res.Length);
		}

		[Test]
		public void MissingRecords()
		{
            var engine = new FileDiffEngine<DiffOrdersFixed>();
            DiffOrdersFixed[] res = engine.OnlyMissingRecords(FileTest.Good.DiffOrdersOld.Path, FileTest.Good.DiffOrdersNew.Path);
			Assert.AreEqual(2, res.Length);
		}

		[Test]
		public void OnlyNoDuplicatedRecords()
		{
            var engine = new FileDiffEngine<DiffOrdersFixed>();
            DiffOrdersFixed[] res = engine.OnlyNoDuplicatedRecords(FileTest.Good.DiffOrdersOld.Path, FileTest.Good.DiffOrdersNew.Path);
			Assert.AreEqual(7, res.Length);
		}

		[Test]
		public void OnlyDuplicatedRecords()
		{
            var engine = new FileDiffEngine<DiffOrdersFixed>();
            DiffOrdersFixed[] res = engine.OnlyDuplicatedRecords(FileTest.Good.DiffOrdersOld.Path, FileTest.Good.DiffOrdersNew.Path);
			Assert.AreEqual(10, res.Length);
		}

		[Test]
		public void RemoveDuplicatedRecords()
		{
			var eng = new FileHelperEngine<DiffOrdersFixed>();

            DiffOrdersFixed[] res = FileTest.Good.DiffOrdersDup.ReadWithEngine<DiffOrdersFixed>(eng);

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