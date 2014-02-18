using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class ConditionalRecords
    {
        [Test]
        public void Conditional1()
        {
            var engine = new FileHelperEngine<ConditionalType1>();

            var res = TestCommon.ReadTest<ConditionalType1>(engine, "Good", "ConditionalRecords1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(8, engine.LineNumber);
        }

        [Test]
        public void Conditional2()
        {
            var engine = new FileHelperEngine<ConditionalType2>();

            var res = TestCommon.ReadTest<ConditionalType2>(engine, "Good", "ConditionalRecords2.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(8, engine.LineNumber);
        }


        [Test]
        public void Conditional3()
        {
            var engine = new FileHelperEngine<ConditionalType3>();

            var res = TestCommon.ReadTest<ConditionalType3>(engine, "Good", "ConditionalRecords3.txt");

            Assert.AreEqual(3, res.Length);
            Assert.AreEqual(7, engine.LineNumber);
        }


        [Test]
        public void Conditional4()
        {
            var engine = new FileHelperEngine<ConditionalType4>();

            ConditionalType4[] res = TestCommon.ReadTest<ConditionalType4>(engine, "Good", "ConditionalRecords4.txt");

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