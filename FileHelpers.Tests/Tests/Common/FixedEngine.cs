using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class FixedEngine
	{
		[Test]
		public void SimpleTest()
		{
            FixedFileEngine engine = new FixedFileEngine(typeof(CustomersFixed));
            Assert.AreEqual(91, Common.ReadTest(engine, @"Good\CustomersFixed.txt").Length);

            engine.Options.IgnoreFirstLines = 10;
            Assert.AreEqual(81, Common.ReadTest(engine, @"Good\CustomersFixed.txt").Length);

            engine.Options.IgnoreLastLines = 6;
            Assert.AreEqual(75, Common.ReadTest(engine, @"Good\CustomersFixed.txt").Length);

            Assert.AreEqual(183, engine.Options.RecordLength);

        }

        [Test]
        public void SimpleTest2()
        {
            FixedFileEngine engine = new FixedFileEngine(typeof(CustomersFixed));
            Assert.AreEqual(91, Common.ReadTest(engine, @"Good\CustomersFixed.txt").Length);

            engine.Options.RecordCondition.Condition = RecordCondition.IncludeIfBegins;
            engine.Options.RecordCondition.Selector = "F";
            Assert.AreEqual(8, Common.ReadTest(engine, @"Good\CustomersFixed.txt").Length);

        }

		[Test]
		public void SimpleTest3()
		{
			FixedFileEngine engine = new FixedFileEngine(typeof(CustomersFixed2));
			Assert.AreEqual(8, Common.ReadTest(engine, @"Good\CustomersFixed.txt").Length);
		}

        [Test]
        [ExpectedException(typeof(BadUsageException))]
        public void BadRecordType1()
        {
            FixedFileEngine engine = new FixedFileEngine(typeof(CustomersTab));
        }

        [Test]
        [ExpectedException(typeof(BadUsageException))]
        public void BadRecordType2()
        {
            FixedFileEngine engine = new FixedFileEngine(null);
        }

		[FixedLengthRecord]
		[ConditionalRecord(RecordCondition.IncludeIfBegins, "F")]
		public class CustomersFixed2
		{
			[FieldFixedLength(11)]
			public string CustomerID;

			[FieldFixedLength(50 - 12)]
			public string CompanyName;

			[FieldFixedLength(72 - 50)]
			public string ContactName;

			[FieldFixedLength(110 - 72)]
			public string ContactTitle;

			[FieldFixedLength(151 - 110)]
			public string Address;

			[FieldFixedLength(169 - 151)]
			public string City;

			[FieldFixedLength(15)]
			public string Country;
		}
	}
}