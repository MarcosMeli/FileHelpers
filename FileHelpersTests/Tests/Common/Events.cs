using System;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
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
			engine.BeforeReadRecord += new BeforeReadRecordHandler(BeforeEvent);
			engine.AfterReadRecord += new AfterReadRecordHandler(AfterEvent);

			object[] res = Common.ReadTest(engine, @"Good\test1.txt");

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
			engine.BeforeWriteRecord +=new BeforeWriteRecordHandler(engine_BeforeWriteRecord);
			engine.AfterWriteRecord += new AfterWriteRecordHandler(engine_AfterWriteRecord);

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

		int before = 0;
		int after = 0;

		private void BeforeEvent(EngineBase engine, BeforeReadRecordEventArgs e)
		{
			if (e.RecordLine.StartsWith(" ") || e.RecordLine.StartsWith("-"))
				e.SkipThisRecord = true;

			before++;
		}

		private void AfterEvent(EngineBase engine, AfterReadRecordEventArgs e)
		{
			after++;
		}

		private void engine_BeforeWriteRecord(EngineBase engine, BeforeWriteRecordEventArgs e)
		{
			before++;
		}

		private void engine_AfterWriteRecord(EngineBase engine, AfterWriteRecordEventArgs e)
		{
			after++;
		}
	}
}