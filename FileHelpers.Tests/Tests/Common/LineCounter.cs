using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class LineCounter
	{

        [Test]
		public void ReadFile()
		{
			var engine = new FileHelperEngine<SampleType>();

			SampleType[] res;
			res = TestCommon.ReadTest<SampleType>(engine, "Good", "Test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(5, engine.LineNumber);
		}


		[Test]
		public void AsyncRead()
		{
			var asyncEngine = new FileHelperAsyncEngine<SampleType>();

			SampleType rec1, rec2;

			Assert.AreEqual(0, asyncEngine.LineNumber);
			TestCommon.BeginReadTest<SampleType>(asyncEngine, "Good", "Test1.txt");

			rec1 = (SampleType) asyncEngine.ReadNext();
			Assert.IsNotNull(rec1);
			Assert.AreEqual(1, asyncEngine.LineNumber);

			rec2 = (SampleType) asyncEngine.ReadNext();
			Assert.IsNotNull(rec1);
			Assert.AreEqual(2, asyncEngine.LineNumber);

			Assert.IsTrue(rec1 != rec2);

			rec1 = (SampleType) asyncEngine.ReadNext();
			Assert.IsNotNull(rec2);
			Assert.AreEqual(3, asyncEngine.LineNumber);
			rec1 = (SampleType) asyncEngine.ReadNext();
			Assert.IsNotNull(rec2);
			Assert.AreEqual(4, asyncEngine.LineNumber);

			Assert.IsTrue(rec1 != rec2);

			Assert.AreEqual(4, asyncEngine.TotalRecords);
			Assert.AreEqual(0, asyncEngine.ErrorManager.ErrorCount);

		}

	}
}