using System;
using System.Collections;
using System.Data;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class ReadersFieldIndexer
	{
		string data = "11121314901234" + Environment.NewLine +
			"10111314012345" + Environment.NewLine +
			"11101314123456" + Environment.NewLine +
			"10101314234567" + Environment.NewLine;

		[Test]
		public void AsyncFieldIndex1()
		{

			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof(SampleType));
			engine.BeginReadString(data);

			foreach (SampleType rec in engine)
			{
				Assert.AreEqual(rec.Field1, engine[0]);
				Assert.AreEqual(rec.Field2, engine[1]);
				Assert.AreEqual(rec.Field3, engine[2]);

				Assert.AreEqual(rec.Field1, engine["Field1"]);
				Assert.AreEqual(rec.Field2, engine["Field2"]);
				Assert.AreEqual(rec.Field3, engine["Field3"]);

			}

			engine.Close();
		}

		[Test]
		public void AsyncFieldIndex2()
		{


			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof(SampleType));
			engine.BeginReadString(data);

			while(engine.ReadNext() != null)
			{
				Assert.AreEqual(engine[0], engine.LastRecordValues[0]);
				Assert.AreEqual(engine[1], engine.LastRecordValues[1]);
				Assert.AreEqual(engine[2], engine.LastRecordValues[2]);
			}

			engine.Close();
		}

		[Test]
		public void AsyncFieldIndex3()
		{


			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof(SampleType));
			engine.BeginReadString(data);

			while(engine.ReadNext() != null)
			{
				Assert.AreEqual(engine["Field1"], engine.LastRecordValues[0]);
				Assert.AreEqual(engine["Field2"], engine.LastRecordValues[1]);
				Assert.AreEqual(engine["Field3"], engine.LastRecordValues[2]);
			}

			engine.Close();
		}

		[Test]
		public void AsyncFieldIndex4()
		{

			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof(SampleType));
			engine.BeginReadString(data);

			Assert.AreEqual(3, engine.Options.FieldCount);

			while(engine.ReadNext() != null)
			{
				for(int i = 0; i < engine.Options.FieldCount; i++)
					Assert.IsNotNull(engine[i]);
			}

			engine.Close();
		}

		[Test]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void AsyncFieldIndexBad1()
		{
			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof(SampleType));
			engine.BeginReadString(data);

			while(engine.ReadNext() != null)
			{
				object val = engine[10];
			}

			engine.Close();
		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void AsyncFieldIndexBad2()
		{
			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof(SampleType));
			engine.BeginReadString(data);

			while(engine.ReadNext() != null)
			{
				object val = engine["FieldNoThere"];
			}

			engine.Close();
		}


		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void AsyncFieldIndexBad3()
		{
			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof(SampleType));
			object val = engine[2];
		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void AsyncFieldIndexBad4()
		{
			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof(SampleType));
			object val = engine["Field1"];
		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void AsyncFieldIndexBad5()
		{
			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof(SampleType));
			engine.BeginReadString(data);
			while(engine.ReadNext() != null)
			{
			}
			engine.Close();
			object val = engine[2];

		}

	}
}