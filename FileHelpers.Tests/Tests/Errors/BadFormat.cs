using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Errors
{
	[TestFixture]
	public class BadFormat
	{
		FileHelperEngine engine;

		[SetUp]
		public void Setup()
		{
			engine = new FileHelperEngine(typeof (SampleType));
		}

		[Test]
		public void BadDate1()
		{
            Assert.Throws<ConvertException>(()
                => Common.ReadTest(engine, @"Bad\BadDate1.txt"));
		}

		[Test]
		public void BadDate2()
		{
            Assert.Throws<ConvertException>(()
                => Common.ReadTest(engine, @"Bad\BadDate2.txt"));
		}

		[Test]
		public void BadInt1()
		{
			Assert.Throws<ConvertException>(()
                => Common.ReadTest(engine, @"Bad\BadInt1.txt"));
		}

		[Test]
		public void BadInt2()
		{
			Assert.Throws<ConvertException>(()
                => Common.ReadTest(engine, @"Bad\BadInt2.txt"));
		}

		[Test]
		public void BadInt3()
		{
			engine = new FileHelperEngine(typeof (SampleTypeInt));
			Assert.Throws<ConvertException>(()
                => Common.ReadTest(engine, @"Bad\BadInt3.txt"));
		}

		[Test]
		public void BadInt4()
		{
			engine = new FileHelperEngine(typeof (SampleTypeInt));
			Assert.Throws<ConvertException>(()
                => Common.ReadTest(engine, @"Bad\BadInt4.txt"));
		}

		[Test]
		public void NoPendingNullValue()
		{
			engine = new FileHelperEngine(typeof (SampleType));
			Common.ReadTest(engine, @"Bad\NoBadNullValue.txt");
		}

	}
}