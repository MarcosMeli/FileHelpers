using System;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class ConditionalRecords
	{
		FileHelperEngine engine;

		[Test]
		public void Conditional1()
		{
			engine = new FileHelperEngine(typeof (ConditionalType1));

			object[] res = Common.ReadTest(engine, @"Good\ConditionalRecords1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(8, engine.LineNumber);
		}

		[Test]
		public void Conditional2()
		{
			engine = new FileHelperEngine(typeof (ConditionalType2));

			object[] res = Common.ReadTest(engine, @"Good\ConditionalRecords2.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(8, engine.LineNumber);
		}
	

		[Test]
		public void Conditional3()
		{
			engine = new FileHelperEngine(typeof (ConditionalType3));

			object[] res = Common.ReadTest(engine, @"Good\ConditionalRecords3.txt");

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(7, engine.LineNumber);
		}


        [Test]
        public void Conditional4()
        {
            engine = new FileHelperEngine(typeof(ConditionalType4));

            ConditionalType4[] res = (ConditionalType4[]) Common.ReadTest(engine, @"Good\ConditionalRecords4.txt");

            Assert.AreEqual(2, res.Length);
            Assert.AreEqual(5, engine.LineNumber);
            Assert.AreEqual('$', res[0].Field2[0]);
            Assert.AreEqual('$', res[1].Field2[0]);
            
        }

		[DelimitedRecord(",")]
		[ConditionalRecord(RecordCondition.ExcludeIfBegins, "//")]
		public class ConditionalType1
		{
			[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
			public DateTime Field1;
			public string Field2;
			public int Field3;
		}

		[DelimitedRecord(",")]
		[ConditionalRecord(RecordCondition.ExcludeIfEnds, "$")]
		public class ConditionalType2
		{
			[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
			public DateTime Field1;
			public string Field2;
			public int Field3;
		}

		[DelimitedRecord(",")]
		[ConditionalRecord(RecordCondition.IncludeIfMatchRegex, "ab*c")]
		public class ConditionalType3
		{
			[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
			public DateTime Field1;
			public string Field2;
			public int Field3;
		}

        [DelimitedRecord(",")]
        [ConditionalRecord(RecordCondition.IncludeIfContains, "$")]
        public class ConditionalType4
        {
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime Field1;
            public string Field2;
            public int Field3;
        }


	}

	

}