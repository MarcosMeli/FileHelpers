using System;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class Readers
	{
		FileHelperEngine engine;
		FileHelperAsyncEngine asyncEngine;

		[Test]
		public void ReadFile()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) TestCommon.ReadTest(engine, @"Good\test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
			Assert.AreEqual("901", res[0].Field2);
			Assert.AreEqual(234, res[0].Field3);

			Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
			Assert.AreEqual("012", res[1].Field2);
			Assert.AreEqual(345, res[1].Field3);

		}


		[Test]
		public void ReadFileStatic()
		{
			SampleType[] res;
			res = (SampleType[]) CommonEngine.ReadFile(typeof (SampleType), TestCommon.TestPath(@"Good\test1.txt"));

			Assert.AreEqual(4, res.Length);

			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
			Assert.AreEqual("901", res[0].Field2);
			Assert.AreEqual(234, res[0].Field3);

			Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
			Assert.AreEqual("012", res[1].Field2);
			Assert.AreEqual(345, res[1].Field3);

		}


		[Test]
		public void AsyncRead()
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (SampleType));

			SampleType rec1, rec2;

			TestCommon.BeginReadTest(asyncEngine, @"Good\test1.txt");

			rec1 = (SampleType) asyncEngine.ReadNext();
			Assert.IsNotNull(rec1);
			rec2 = (SampleType) asyncEngine.ReadNext();
			Assert.IsNotNull(rec1);

			Assert.IsTrue(rec1 != rec2);

			rec1 = (SampleType) asyncEngine.ReadNext();
			Assert.IsNotNull(rec2);
			rec1 = (SampleType) asyncEngine.ReadNext();
			Assert.IsNotNull(rec2);

			Assert.IsTrue(rec1 != rec2);

			Assert.AreEqual(0, asyncEngine.ErrorManager.ErrorCount);

		}

		[Test]
		public void AsyncRead2()
		{
			SampleType rec1;

			asyncEngine = new FileHelperAsyncEngine(typeof (SampleType));

			TestCommon.BeginReadTest(asyncEngine, @"Good\test1.txt");
			int lineAnt = asyncEngine.LineNumber;
			while (asyncEngine.ReadNext() != null)
			{
				rec1 = (SampleType) asyncEngine.LastRecord;
				Assert.IsNotNull(rec1);
				Assert.AreEqual(lineAnt + 1, asyncEngine.LineNumber);
				lineAnt = asyncEngine.LineNumber;
			}

			Assert.AreEqual(4, asyncEngine.TotalRecords);
			Assert.AreEqual(0, asyncEngine.ErrorManager.ErrorCount);

		}


		[Test]
		public void ReadStream()
		{
			string data = "11121314901234" + Environment.NewLine +
				"10111314012345" + Environment.NewLine +
				"11101314123456" + Environment.NewLine +
				"10101314234567" + Environment.NewLine;

			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) engine.ReadStream(new StringReader(data));

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
			Assert.AreEqual("901", res[0].Field2);
			Assert.AreEqual(234, res[0].Field3);

			Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
			Assert.AreEqual("012", res[1].Field2);
			Assert.AreEqual(345, res[1].Field3);

		}

		[Test]
		public void ReadString()
		{
			string data = "11121314901234" + Environment.NewLine +
				"10111314012345" + Environment.NewLine +
				"11101314123456" + Environment.NewLine +
				"10101314234567" + Environment.NewLine;

			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) engine.ReadString(data);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
			Assert.AreEqual("901", res[0].Field2);
			Assert.AreEqual(234, res[0].Field3);

			Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
			Assert.AreEqual("012", res[1].Field2);
			Assert.AreEqual(345, res[1].Field3);

		}


		[Test]
		public void ReadStringStatic()
		{
			string data = "11121314901234" + Environment.NewLine +
				"10111314012345" + Environment.NewLine +
				"11101314123456" + Environment.NewLine +
				"10101314234567" + Environment.NewLine;

			SampleType[] res;
			res = (SampleType[]) CommonEngine.ReadString(typeof(SampleType), data);

			Assert.AreEqual(4, res.Length);

			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
			Assert.AreEqual("901", res[0].Field2);
			Assert.AreEqual(234, res[0].Field3);

			Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
			Assert.AreEqual("012", res[1].Field2);
			Assert.AreEqual(345, res[1].Field3);

		}


		[Test]
		public void ReadEmpty()
		{
			string data = "";

			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) engine.ReadStream(new StringReader(data));

			Assert.AreEqual(0, res.Length);
			Assert.AreEqual(0, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

		}

		[Test]
		public void ReadEmptyStream()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) TestCommon.ReadTest(engine, @"Good\TestEmpty.txt");

			Assert.AreEqual(0, res.Length);
			Assert.AreEqual(0, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

		}

	}
}