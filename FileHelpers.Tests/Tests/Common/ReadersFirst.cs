using System;
using System.Collections;
using System.Data;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class ReadersFirst
	{
		FileHelperEngine engine;
	

		[Test]
        public void ReadFileMaxRecords01()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) TestCommon.ReadTest(engine, 2, "Good", "Test1.txt");

			Assert.AreEqual(2, res.Length);
			Assert.AreEqual(2, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
			Assert.AreEqual("901", res[0].Field2);
			Assert.AreEqual(234, res[0].Field3);

			Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
			Assert.AreEqual("012", res[1].Field2);
			Assert.AreEqual(345, res[1].Field3);

		}


		[Test]
		public void ReadFileMaxRecords02()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) TestCommon.ReadTest(engine, int.MaxValue, "Good", "Test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}


		[Test]
        public void ReadFileMaxRecords03()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) TestCommon.ReadTest(engine, -1, "Good", "Test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
        public void ReadMaxRecords04()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) TestCommon.ReadTest(engine, -1283623, "Good", "Test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
        public void ReadMaxRecords05()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) TestCommon.ReadTest(engine, 0, "Good", "Test1.txt");

			Assert.AreEqual(0, res.Length);
			Assert.AreEqual(0, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
        public void ReadFileMaxRecords06()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) TestCommon.ReadTest(engine, 1, "Good", "Test1.txt");

			Assert.AreEqual(1, res.Length);
			Assert.AreEqual(1, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
        public void ReadFileMaxRecords07()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) TestCommon.ReadTest(engine, "Good", "Test1.txt");
			string temp = engine.WriteString(res);
			res = (SampleType[]) engine.ReadString(temp, 2);

			Assert.AreEqual(2, res.Length);
			Assert.AreEqual(2, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}


		[Test]
        public void ReadFileMaxRecords08()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) TestCommon.ReadTest(engine, "Good", "Test1.txt");
			string temp = engine.WriteString(res);
			res = (SampleType[]) engine.ReadString(temp, int.MaxValue);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
        public void ReadFileMaxRecords09()
		{
			object[] res = CommonEngine.ReadFile(typeof(SampleType), TestCommon.GetPath("Good", "Test1.txt"), 2);
			Assert.AreEqual(2, res.Length);
		}

		[Test]
        public void ReadFileMaxRecords10()
		{
			object[] res = CommonEngine.ReadFile(typeof(SampleType), TestCommon.GetPath("Good", "Test1.txt"), -1);

			Assert.AreEqual(4, res.Length);
		}

	}
}