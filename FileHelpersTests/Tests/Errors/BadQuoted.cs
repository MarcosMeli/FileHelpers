using FileHelpers;
using FileHelpersTests.CommonTests;
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
			Common.ReadTest(engine, @"Bad\BadQuoted1.txt");
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadQuoted2()
		{
			Common.ReadTest(engine, @"Bad\BadQuoted2.txt");
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadQuoted3()
		{
			Common.ReadTest(engine, @"Bad\BadQuoted3.txt");
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadQuoted1Async()
		{
			Common.ReadAllAsync(engineAsync, @"Bad\BadQuoted1.txt");
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadQuoted2Async()
		{
			Common.ReadAllAsync(engineAsync, @"Bad\BadQuoted2.txt");
		}

		[Test]
		[ExpectedException(typeof (BadUsageException))]
		public void BadQuoted3Async()
		{
			Common.ReadAllAsync(engineAsync, @"Bad\BadQuoted3.txt");
		}


	}
}