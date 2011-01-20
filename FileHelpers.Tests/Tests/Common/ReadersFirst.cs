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
		
		[Test]
        public void ReadFileMaxRecords01()
		{
			var engine = new FileHelperEngine<SampleType>();

			SampleType[] res;
			res = engine.ReadFile(FileTest.Good.Test1.Path, 2);

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
			var engine = new FileHelperEngine<SampleType>();

			SampleType[] res;
            res = (SampleType[])engine.ReadFile(FileTest.Good.Test1.Path, int.MaxValue);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}


		[Test]
        public void ReadFileMaxRecords03()
		{
			var engine = new FileHelperEngine<SampleType>();

			SampleType[] res;
            res = engine.ReadFile(FileTest.Good.Test1.Path, -1);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
        public void ReadMaxRecords04()
		{
            var engine = new FileHelperEngine<SampleType>();

			SampleType[] res;
			res = engine.ReadFile(FileTest.Good.Test1.Path, -1283623);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
        public void ReadMaxRecords05()
		{
			var engine = new FileHelperEngine<SampleType>();

		    var res = (SampleType[])engine.ReadFile(FileTest.Good.Test1.Path, 0);

			Assert.AreEqual(0, res.Length);
			Assert.AreEqual(0, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
        public void ReadFileMaxRecords06()
		{
			var engine = new FileHelperEngine<SampleType>();

			SampleType[] res;
            res = engine.ReadFile(FileTest.Good.Test1.Path, 1);

			Assert.AreEqual(1, res.Length);
			Assert.AreEqual(1, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
        public void ReadFileMaxRecords07()
		{
			var engine = new FileHelperEngine<SampleType>();

		    SampleType[] res = FileTest.Good.Test1.ReadWithEngine(engine);

			string temp = engine.WriteString(res);
			res = engine.ReadString(temp, 2);

			Assert.AreEqual(2, res.Length);
			Assert.AreEqual(2, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}


		[Test]
        public void ReadFileMaxRecords08()
		{
			var engine = new FileHelperEngine<SampleType>();

		    var res = FileTest.Good.Test1.ReadWithEngine(engine);
			string temp = engine.WriteString(res);
			res = engine.ReadString(temp, int.MaxValue);

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
        public void ReadFileMaxRecords09()
		{
			var res = FileTest.Good.Test1.ReadWithEngine<SampleType>(2);
			Assert.AreEqual(2, res.Length);
		}

		[Test]
        public void ReadFileMaxRecords10()
		{
            var res = FileTest.Good.Test1.ReadWithEngine<SampleType>(-1);
			Assert.AreEqual(4, res.Length);
		}

	}
}