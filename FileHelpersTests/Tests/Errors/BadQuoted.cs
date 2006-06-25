using FileHelpers;
using FileHelpersTests.Common;
using NUnit.Framework;

namespace FileHelpersTests.Errors
{
	[TestFixture]
	public class BadQuoted
	{
		FileHelperEngine engine;
		FileHelperAsyncEngine engineAsync;

		[SetUp]
		public void Setup()
		{
			engine = new FileHelperEngine(typeof (CustomersQuotedType));
			engineAsync = new FileHelperAsyncEngine(typeof (CustomersQuotedType));
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadQuoted1()
		{
			TestCommon.ReadTest(engine, @"Bad\BadQuoted1.txt");
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadQuoted2()
		{
			TestCommon.ReadTest(engine, @"Bad\BadQuoted2.txt");
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadQuoted3()
		{
			TestCommon.ReadTest(engine, @"Bad\BadQuoted3.txt");
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadQuoted1Async()
		{
			TestCommon.ReadAllAsync(engineAsync, @"Bad\BadQuoted1.txt");
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadQuoted2Async()
		{
			TestCommon.ReadAllAsync(engineAsync, @"Bad\BadQuoted2.txt");
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadQuoted3Async()
		{
			TestCommon.ReadAllAsync(engineAsync, @"Bad\BadQuoted3.txt");
		}


	}
}