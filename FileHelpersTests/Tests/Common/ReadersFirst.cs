using System;
using System.Collections;
using System.Data;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class ReadersFirst
	{
		FileHelperEngine engine;
	

		[Test]
		public void ReadFileSomes01()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) Common.ReadTest(engine, @"Good\test1.txt", 2);

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
		public void ReadFileSomes02()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) Common.ReadTest(engine, @"Good\test1.txt", int.MaxValue);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}


		[Test]
		public void ReadFileSomes03()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) Common.ReadTest(engine, @"Good\test1.txt", -1);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
		public void ReadFileSomes04()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) Common.ReadTest(engine, @"Good\test1.txt", -1283623);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
		public void ReadFileSomes05()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) Common.ReadTest(engine, @"Good\test1.txt", 0);

			Assert.AreEqual(0, res.Length);
			Assert.AreEqual(0, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
		public void ReadFileSomes06()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) Common.ReadTest(engine, @"Good\test1.txt", 1);

			Assert.AreEqual(1, res.Length);
			Assert.AreEqual(1, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
		public void ReadFileSomes07()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) Common.ReadTest(engine, @"Good\test1.txt");
			string temp = engine.WriteString(res);
			res = (SampleType[]) engine.ReadString(temp, 2);

			Assert.AreEqual(2, res.Length);
			Assert.AreEqual(2, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}


		[Test]
		public void ReadFileSomes08()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res;
			res = (SampleType[]) Common.ReadTest(engine, @"Good\test1.txt");
			string temp = engine.WriteString(res);
			res = (SampleType[]) engine.ReadString(temp, int.MaxValue);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
		public void ReadFileSomes09()
		{
			object[] res = CommonEngine.ReadFile(typeof(SampleType), Common.TestPath(@"Good\test1.txt"), 2);
			Assert.AreEqual(2, res.Length);
		}

		[Test]
		public void ReadFileSomes10()
		{
			object[] res = CommonEngine.ReadFile(typeof(SampleType), Common.TestPath(@"Good\test1.txt"), -1);

			Assert.AreEqual(4, res.Length);
		}

	}
}