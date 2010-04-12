using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class FixedEngine
	{
		[Test]
		public void SimpleTest()
		{
            var engine = new FixedFileEngine<CustomersFixed>();
            Assert.AreEqual(91, FileTest.Good.CustomersFixed.ReadWithEngine(engine).Length);

            engine.Options.IgnoreFirstLines = 10;
            Assert.AreEqual(81, FileTest.Good.CustomersFixed.ReadWithEngine(engine).Length);

            engine.Options.IgnoreLastLines = 6;
            Assert.AreEqual(75, FileTest.Good.CustomersFixed.ReadWithEngine(engine).Length);

            Assert.AreEqual(183, engine.Options.RecordLength);

        }

        [Test]
        public void SimpleTest2()
        {
            var engine = new FixedFileEngine(typeof(CustomersFixed));
            Assert.AreEqual(91, FileTest.Good.CustomersFixed.ReadWithEngine(engine).Length);

            engine.Options.RecordCondition.Condition = RecordCondition.IncludeIfBegins;
            engine.Options.RecordCondition.Selector = "F";
            Assert.AreEqual(8, FileTest.Good.CustomersFixed.ReadWithEngine(engine).Length);

        }

		[Test]
		public void SimpleTest3()
		{
			FixedFileEngine engine = new FixedFileEngine(typeof(CustomersFixed2));
			Assert.AreEqual(8, FileTest.Good.CustomersFixed.ReadWithEngine(engine).Length);
		}

        [Test]
        public void BadRecordType1()
        {
            Assert.Throws<BadUsageException>(
                () => new FixedFileEngine(typeof(CustomersTab)));
        }

        [Test]
        public void BadRecordType2()
        {
            Assert.Throws<BadUsageException>(
                () => new FixedFileEngine(null));
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