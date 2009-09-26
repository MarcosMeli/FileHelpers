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

            object[] res = engine.ReadFile(TestCommon.GetPath("Good", "MultiRecord1.txt"));

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
			engine.BeginReadFile(TestCommon.GetPath("Good", "MultiRecord1.txt"));
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
		public void MultpleRecordsWriteAsync()
		{
			engine = new MultiRecordEngine(new RecordTypeSelector(CustomSelector), typeof(OrdersVerticalBar), typeof(CustomersSemiColon), typeof(SampleType));

			object[] records = engine.ReadFile(TestCommon.GetPath("Good", "MultiRecord1.txt"));

			engine.BeginWriteFile("tempoMulti.txt");
			foreach (object o in records)
			{
				engine.WriteNext(o);
			}
			engine.Close();
			File.Delete("tempoMulti.txt");


			object[] res = engine.ReadFile(TestCommon.GetPath("Good", "MultiRecord1.txt"));

			Assert.AreEqual(12, res.Length);
			Assert.AreEqual(12, engine.TotalRecords);

			Assert.AreEqual(typeof(OrdersVerticalBar), res[0].GetType());
			Assert.AreEqual(typeof(OrdersVerticalBar), res[1].GetType());
			Assert.AreEqual(typeof(CustomersSemiColon), res[2].GetType());
			Assert.AreEqual(typeof(SampleType), res[5].GetType());
		}


		[Test]
		public void MultpleRecordsFileAsyncBad()
		{
		    engine = new MultiRecordEngine(typeof (OrdersVerticalBar), typeof (CustomersSemiColon), typeof (SampleType));
		    engine.RecordSelector = new RecordTypeSelector(CustomSelector);

		    Assert.Throws<FileHelpersException>(
		        () =>
		            {
		                foreach (object o in engine)
		                {
		                    o.ToString();
		                }
		            });
		}

	    [Test]
		public void MultpleRecordsFileRW()
		{
			engine = new MultiRecordEngine(typeof(OrdersVerticalBar), typeof(CustomersSemiColon), typeof(SampleType));
			engine.RecordSelector = new RecordTypeSelector(CustomSelector);

			object[] res2 = engine.ReadFile(TestCommon.GetPath("Good", "MultiRecord1.txt"));

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
		public void NoTypes()
		{
			Assert.Throws<BadUsageException>(() => 
                new MultiRecordEngine(new Type[] {}));
		}

		[Test]
		public void NullTypeArray()
		{
            Assert.Throws<BadUsageException>(() => 
                new MultiRecordEngine((Type[])null));
		}
				
		[Test]
		public void NoSelector()
		{
			engine = new MultiRecordEngine(typeof(CustomersVerticalBar), typeof(CustomersTab));
		}

		[Test]
		public void TwiceSameType()
		{
            Assert.Throws<BadUsageException>(() 
                => new MultiRecordEngine(typeof(CustomersVerticalBar), typeof(CustomersVerticalBar)));
		}

		[Test]
		public void OneType()
		{
            Assert.Throws<BadUsageException>(() 
                => new MultiRecordEngine(typeof(CustomersVerticalBar)));
		}

		[Test]
		public void NullTypes()
		{
			Assert.Throws<BadUsageException>(() 
                => new MultiRecordEngine(typeof(CustomersVerticalBar), null));
		}

	    [Test]
	    public void WhenSelectorReturnsTypeThatIsNotInEngine_ShouldThrowBadUsageException_WhenReadingFileAtATime()
	    {
	        engine = new MultiRecordEngine(new RecordTypeSelector(CustomSelectorReturningBadType),
	                                       typeof (OrdersVerticalBar), typeof (CustomersSemiColon), typeof (SampleType));

            Assert.Throws<BadUsageException>(() => engine.ReadFile(TestCommon.GetPath("Good", "MultiRecord1.txt")));
	    }

        [Test]
        public void WhenSelectorReturnsTypeThatIsNotInEngine_ShouldThrowBadUsageException_WhenReadingRecordAtATime()
        {
            engine = new MultiRecordEngine(new RecordTypeSelector(CustomSelectorReturningBadType),
                                           typeof(OrdersVerticalBar), typeof(CustomersSemiColon), typeof(SampleType));

            engine.BeginReadFile(TestCommon.GetPath("Good", "MultiRecord1.txt"));

            Assert.Throws<BadUsageException>(() => engine.ReadNext());
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

        Type CustomSelectorReturningBadType(MultiRecordEngine engine, string record)
        {
            return typeof (String);
        }
	}
}