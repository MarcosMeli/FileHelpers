using System;
using FileHelpers;
using FileHelpers.MasterDetail;
using NUnit.Framework;
using System.IO;

namespace FileHelpersTests
{
	[TestFixture]
	public class MultiRecords
	{
		MultiRecordEngine engine;

		[Test]
		public void MultpleRecordsFile()
		{
			engine = new MultiRecordEngine(new Type[] {typeof(OrdersVerticalBar), typeof(CustomersSemiColon), typeof(SampleType)}, new RecordTypeSelector(Test1Selector));

            object[] res = engine.ReadFile(Common.TestPath(@"Good\MultiRecord1.txt"));

            Assert.AreEqual(12, res.Length);
            Assert.AreEqual(12, engine.TotalRecords);

			Assert.AreEqual(typeof(OrdersVerticalBar), res[0].GetType());
			Assert.AreEqual(typeof(OrdersVerticalBar), res[1].GetType());
			Assert.AreEqual(typeof(CustomersSemiColon), res[2].GetType());
			Assert.AreEqual(typeof(SampleType), res[5].GetType());
		}

		[Test]
		public void MultpleRecordsFileRW()
		{
			engine = new MultiRecordEngine(new Type[] {typeof(OrdersVerticalBar), typeof(CustomersSemiColon), typeof(SampleType)}, new RecordTypeSelector(Test1Selector));

			object[] res2 = engine.ReadFile(Common.TestPath(@"Good\MultiRecord1.txt"));

			Assert.AreEqual(12, res2.Length);
			Assert.AreEqual(12, engine.TotalRecords);

			engine.WriteFile("tempMR.txt", res2);
			object[] res = engine.ReadFile("tempMR.txt");
			File.Delete("tempMR.txt");

			Assert.AreEqual(12, res.Length);
			Assert.AreEqual(12, engine.TotalRecords);

			Assert.AreEqual(typeof(OrdersVerticalBar), res[0].GetType());
			Assert.AreEqual(typeof(OrdersVerticalBar), res[1].GetType());
			Assert.AreEqual(typeof(CustomersSemiColon), res[2].GetType());
			Assert.AreEqual(typeof(SampleType), res[5].GetType());
		}

		
		[Test]
		[ExpectedException(typeof(ArgumentException))]
		public void NoTypes()
		{
			engine = new MultiRecordEngine(new Type[] {}, null);
		}

				
		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void NoSelector()
		{
			engine = new MultiRecordEngine(new Type[] {typeof(CustomersVerticalBar)}, null);
		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void NullTypes()
		{
			engine = new MultiRecordEngine(new Type[] {typeof(CustomersVerticalBar), null} ,new RecordTypeSelector(Test1Selector));
		}

        Type Test1Selector(MultiRecordEngine engine, string record)
        {
            if (Char.IsLetter(record[0]))
                return typeof(CustomersSemiColon);
			else if (record.Length == 14)
				return typeof(SampleType);
            else
                return typeof(OrdersVerticalBar);
        }

	}
}