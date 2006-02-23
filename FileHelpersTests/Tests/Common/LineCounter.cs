using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class LineCounter
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
			Assert.AreEqual(5, engine.LineNum);
		}


		[Test]
		public void AsyncRead()
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (SampleType));

			SampleType rec1, rec2;

			Assert.AreEqual(0, asyncEngine.LineNum);
			TestCommon.BeginReadTest(asyncEngine, @"Good\test1.txt");

			rec1 = (SampleType) asyncEngine.ReadNext();
			Assert.IsNotNull(rec1);
			Assert.AreEqual(1, asyncEngine.LineNum);

			rec2 = (SampleType) asyncEngine.ReadNext();
			Assert.IsNotNull(rec1);
			Assert.AreEqual(2, asyncEngine.LineNum);

			Assert.IsTrue(rec1 != rec2);

			rec1 = (SampleType) asyncEngine.ReadNext();
			Assert.IsNotNull(rec2);
			Assert.AreEqual(3, asyncEngine.LineNum);
			rec1 = (SampleType) asyncEngine.ReadNext();
			Assert.IsNotNull(rec2);
			Assert.AreEqual(4, asyncEngine.LineNum);

			Assert.IsTrue(rec1 != rec2);

			Assert.AreEqual(4, asyncEngine.TotalRecords);
			Assert.AreEqual(0, asyncEngine.ErrorManager.ErrorCount);

		}

	}
}