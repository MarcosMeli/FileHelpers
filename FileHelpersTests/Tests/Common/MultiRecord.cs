using System;
using System.Collections;
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
			engine = new MultiRecordEngine(new RecordTypeSelector(CustomSelector), typeof(OrdersVerticalBar), typeof(CustomersSemiColon), typeof(SampleType));

            object[] res = engine.ReadFile(Common.TestPath(@"Good\MultiRecord1.txt"));

            Assert.AreEqual(12, res.Length);
            Assert.AreEqual(12, engine.TotalRecords);

			Assert.AreEqual(typeof(OrdersVerticalBar), res[0].GetType());
			Assert.AreEqual(typeof(OrdersVerticalBar), res[1].GetType());
			Assert.AreEqual(typeof(CustomersSemiColon), res[2].GetType());
			Assert.AreEqual(typeof(SampleType), res[5].GetType());
		}

		[Test]
		public void MultpleRecordsFileAsync()
		{
			engine = new MultiRecordEngine(new RecordTypeSelector(CustomSelector), typeof(OrdersVerticalBar), typeof(CustomersSemiColon), typeof(SampleType));

			ArrayList res = new ArrayList();
			engine.BeginReadFile(Common.TestPath(@"Good\MultiRecord1.txt"));
			foreach (object o in engine)
			{
				res.Add(o);
			}

			Assert.AreEqual(12, res.Count);
			Assert.AreEqual(12, engine.TotalRecords);

			Assert.AreEqual(typeof(OrdersVerticalBar), res[0].GetType());
			Assert.AreEqual(typeof(OrdersVerticalBar), res[1].GetType());
			Assert.AreEqual(typeof(CustomersSemiColon), res[2].GetType());
			Assert.AreEqual(typeof(SampleType), res[5].GetType());
		}

		[Test]
		[ExpectedException(typeof(FileHelperException))]
		public void MultpleRecordsFileAsyncBad()
		{
			engine = new MultiRecordEngine(typeof(OrdersVerticalBar), typeof(CustomersSemiColon), typeof(SampleType));
			engine.RecordSelector = new RecordTypeSelector(CustomSelector);

			foreach (object o in engine)
			{
				o.ToString();
			}
		}
		
		[Test]
		public void MultpleRecordsFileRW()
		{
			engine = new MultiRecordEngine(typeof(OrdersVerticalBar), typeof(CustomersSemiColon), typeof(SampleType));
			engine.RecordSelector = new RecordTypeSelector(CustomSelector);

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
			engine = new MultiRecordEngine(new Type[] {});
		}

		[Test]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NullTypeArray()
		{
			engine = new MultiRecordEngine((Type[])null);
		}
				
		[Test]
		public void NoSelector()
		{
			engine = new MultiRecordEngine(typeof(CustomersVerticalBar));
		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void NullTypes()
		{
			engine = new MultiRecordEngine(typeof(CustomersVerticalBar), null);
			engine.RecordSelector = new RecordTypeSelector(CustomSelector);
		}

        Type CustomSelector(MultiRecordEngine engine, string record)
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