using FileHelpers;
using FileHelpersTests.Common;
using NUnit.Framework;

namespace FileHelpersTests.Errors
{
	[TestFixture]
	public class BadQuoted
	{
		FileHelperEngine engine;

		[SetUp]
		public void Setup()
		{
			engine = new FileHelperEngine(typeof (CustomersQuotedType));
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

	}
}