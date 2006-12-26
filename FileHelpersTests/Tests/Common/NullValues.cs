using System;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class NullWriters
	{
		FileHelperEngine engine;
		FileHelperAsyncEngine asyncEngine;

		[Test]
		public void WriteNull()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res = new SampleType[3];
			res[0] = new SampleType();
			res[1] = new SampleType();
			res[2] = new SampleType();

			engine.WriteFile("tempNull.txt", res);
			res = (SampleType[]) engine.ReadFile("tempNull.txt");

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(3, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

			Assert.AreEqual(DateTime.MinValue, res[0].Field1);
			Assert.AreEqual("", res[0].Field2);
			Assert.AreEqual(0, res[0].Field3);
			if (File.Exists("tempNull.txt")) File.Delete("tempNull.txt");
		}

		[Test]
		public void WriteNullAsync()
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (SampleType));

			asyncEngine.BeginWriteFile("tempNull.txt");

			asyncEngine.WriteNext(new SampleType());
			asyncEngine.WriteNext(new SampleType());
			asyncEngine.WriteNext(new SampleType());

			asyncEngine.Close();

			asyncEngine.BeginReadFile("tempNull.txt");
			SampleType[] res = (SampleType[]) asyncEngine.ReadNexts(5000);
			asyncEngine.Close();

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(3, asyncEngine.TotalRecords);
			Assert.AreEqual(0, asyncEngine.ErrorManager.ErrorCount);

			Assert.AreEqual(DateTime.MinValue, res[0].Field1);
			Assert.AreEqual("", res[0].Field2);
			Assert.AreEqual(0, res[0].Field3);

			if (File.Exists("tempNull.txt")) File.Delete("tempNull.txt");
		}

	}
}