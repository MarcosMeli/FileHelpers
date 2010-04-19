using System;
using System.IO;
using FileHelpers;
using FileHelpers.Events;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class Events
	{
		FileHelperEngine engine;

		[Test]
		public void ReadEvents()
		{
			before = 0;
			after = 0;

			engine = new FileHelperEngine(typeof (SampleType));
			engine.BeforeReadRecord += new BeforeReadHandler<object>(BeforeEvent);
            engine.AfterReadRecord += new AfterReadHandler<object>(AfterEvent);

			object[] res = TestCommon.ReadTest(engine, "Good", "Test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(4, before);
			Assert.AreEqual(4, after);
		}
		

		[Test]
		public void WriteEvents()
		{
			before = 0;
			after = 0;

			engine = new FileHelperEngine(typeof (SampleType));

            engine.BeforeWriteRecord += new BeforeWriteHandler<object>(engine_BeforeWriteRecord);
            engine.AfterWriteRecord += new AfterWriteHandler<object>(engine_AfterWriteRecord);

            SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			engine.WriteString(res);

			Assert.AreEqual(2, engine.TotalRecords);
			Assert.AreEqual(2, before);
			Assert.AreEqual(2, after);

		}

		
		[Test]
		public void ReadEventsCancelAfter()
		{
			before = 0;
			after = 0;

			engine = new FileHelperEngine(typeof (SampleType));
            engine.AfterReadRecord += new AfterReadHandler<object>(AfterEvent2);

			object[] res = TestCommon.ReadTest(engine, "Good", "Test1.txt");

			Assert.AreEqual(0, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, before);
			Assert.AreEqual(4, after);
		}

		[Test]
		public void ReadEventsCancelBefore()
		{
			before = 0;
			after = 0;

			engine = new FileHelperEngine(typeof (SampleType));
            engine.BeforeReadRecord += new BeforeReadHandler<object>(BeforeEvent2);

			object[] res = TestCommon.ReadTest(engine, "Good", "Test1.txt");

			Assert.AreEqual(0, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(4, before);
			Assert.AreEqual(0, after);
		}
		
		[Test]
		public void ReadEventsCancelAll()
		{
			before = 0;
			after = 0;

			engine = new FileHelperEngine(typeof (SampleType));
            engine.BeforeReadRecord += new BeforeReadHandler<object>(BeforeEvent2);
            engine.AfterReadRecord += new AfterReadHandler<object>(AfterEvent2);

			object[] res = TestCommon.ReadTest(engine, "Good", "Test1.txt");

			Assert.AreEqual(0, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(4, before);
			Assert.AreEqual(0, after);
		}
		
		int before = 0;
		int after = 0;

        
		private void BeforeEvent(EngineBase sender, BeforeReadEventArgs<object> e)
		{
			if (e.RecordLine.StartsWith(" ") || e.RecordLine.StartsWith("-"))
				e.SkipThisRecord = true;

			before++;
		}

		private void AfterEvent(EngineBase sender, AfterReadEventArgs<object> e)
		{
			after++;
		}

        private void engine_BeforeWriteRecord(EngineBase sender, BeforeWriteEventArgs<object> e)
		{
			before++;
		}

        private void engine_AfterWriteRecord(EngineBase sender, AfterWriteEventArgs<object> e)
		{
			after++;
		}

        private void AfterEvent2(EngineBase sender, AfterReadEventArgs<object> e)
		{
			e.SkipThisRecord = true;
			after++;
		}

        private void BeforeEvent2(EngineBase sender, BeforeReadEventArgs<object> e)
		{
			e.SkipThisRecord = true;
			before++;
		}




        [Test(Description = "3 empty lines as input and the events give the original line")]
        public void ChangeLineInEvent()
        {
            string input = "\n\n\n";
            engine = new FileHelperEngine(typeof(SampleType));
            engine.BeforeReadRecord += new BeforeReadHandler<object>(BeforeEventChange);

            SampleType[] res = (SampleType[]) engine.ReadString(input);

            Assert.AreEqual(3, res.Length);
            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
            Assert.AreEqual("901", res[0].Field2);
            Assert.AreEqual(234, res[0].Field3);

            Assert.AreEqual(new DateTime(1314, 12, 11), res[1].Field1);
            Assert.AreEqual("901", res[2].Field2);
            Assert.AreEqual(234, res[2].Field3);

        }

        private static void BeforeEventChange(EngineBase engine, BeforeReadEventArgs<object> e)
	    {
            Assert.IsFalse(e.RecordLineChanged);
	        e.RecordLine = "11121314901234";
            Assert.IsTrue(e.RecordLineChanged);

	    }
	}
}