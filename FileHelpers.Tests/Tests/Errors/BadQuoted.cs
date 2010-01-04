using FileHelpers;
using FileHelpers.Tests.CommonTests;
using NUnit.Framework;

namespace FileHelpers.Tests.Errors
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
		public void BadQuoted1()
		{
            Assert.Throws<BadUsageException>(() 
                => TestCommon.ReadTest(engine, "Bad", "BadQuoted1.txt"));
		}

		[Test]
		public void BadQuoted2()
		{
            Assert.Throws<BadUsageException>(()
                => TestCommon.ReadTest(engine, "Bad", "BadQuoted2.txt"));
		}

		[Test]
		public void BadQuoted3()
		{
            Assert.Throws<BadUsageException>(()
                => TestCommon.ReadTest(engine, "Bad", "BadQuoted3.txt"));
		}

		[Test]
		public void BadQuoted1Async()
		{
            Assert.Throws<BadUsageException>(()
                => TestCommon.ReadAllAsync(engineAsync, "Bad", "BadQuoted1.txt"));
		}

		[Test]
		public void BadQuoted2Async()
		{
            Assert.Throws<BadUsageException>(()
                => TestCommon.ReadAllAsync(engineAsync, "Bad", "BadQuoted2.txt"));
		}

		[Test]
		public void BadQuoted3Async()
		{
            Assert.Throws<BadUsageException>(()
                => TestCommon.ReadAllAsync(engineAsync, "Bad", "BadQuoted3.txt"));
		}


	}
}