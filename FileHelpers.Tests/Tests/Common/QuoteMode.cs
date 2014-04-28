using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using FileHelpers.Options;
using NUnit.Framework;
using NFluent;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class QuoteModeTests
    {
        private const int ExpectedRecords = 6;

        private readonly string[] ExpectedNames = new string[]
        {"VINET", "TO,SP", "HA\"AR", "VICTE", "S\"U\"P,\"\"", "HA,,,NAR"};


        private void ValidateData(QuoteMode1[] data)
        {
            Assert.AreEqual(ExpectedRecords, data.Length);
            for (int i = 0; i < data.Length; i++)
                Assert.AreEqual(ExpectedNames[i], data[i].CustomerName);
        }

        private void ValidateData(QuoteMode2[] data)
        {
            Assert.AreEqual(ExpectedRecords, data.Length);
            for (int i = 0; i < data.Length; i++)
                Assert.AreEqual(ExpectedNames[i], data[i].CustomerName);
        }

        [Test]
        public void ReadOptionalRead()
        {
            var engine = new FileHelperEngine<QuoteMode1>();
            var res = TestCommon.ReadTest<QuoteMode1>(engine, "Good", "QuoteMode1.txt") as QuoteMode1[];
            ValidateData(res);
        }

        [Test]
        public void ReadOptionalWrite()
        {
            var engine = new FileHelperEngine<QuoteMode2>();
            Assert.Throws<BadUsageException>(()
                => TestCommon.ReadTest<QuoteMode2>(engine, "Good", "QuoteMode1.txt"));
        }

        [Test]
        public void WriteOptionalRead()
        {
            var engine = new FileHelperEngine<QuoteMode1>();
            QuoteMode1[] res = TestCommon.ReadTest<QuoteMode1>(engine, "Good", "QuoteMode1.txt");

            engine.WriteFile("quotetemp1.txt", res);

            res = engine.ReadFile("quotetemp1.txt") as QuoteMode1[];
            ValidateData(res);

            if (File.Exists("quotetemp1.txt"))
                File.Delete("quotetemp1.txt");
        }


        [DelimitedRecord(",")]
        private class QuoteMode1
        {
            public string CustomerID;

            [FieldQuoted(QuoteMode.OptionalForRead)]
            public string CustomerName;
        }

        [DelimitedRecord(",")]
        private class QuoteMode2
        {
            public string CustomerID;

            [FieldQuoted(QuoteMode.OptionalForWrite)]
            public string CustomerName;
        }

        [DelimitedRecord(",")]
        private class QuoteMode3
        {
            public string CustomerID;

            [FieldQuoted(QuoteMode.OptionalForBoth)]
            public string CustomerName;
        }


        [DelimitedRecord(",")]
        private class QuoteMode4
        {
            public string CustomerID;

            [FieldQuoted(QuoteMode.AlwaysQuoted)]
            public string CustomerName;
        }

        [Test]
        public void AutoRemoveQuotes()
        {
            var eng = new CsvEngine(new CsvOptions("YourClass", ',', 2, 0));
            DataTable dt = eng.ReadFileAsDT(TestCommon.GetPath("Good", "QuoteMode1.txt"));

            Assert.AreEqual("VINET", dt.Rows[0][1]);
        }

        [Test]
        public void OptionalForReadOnEmptyFields()
        {
            var eng = new FileHelperEngine<OptionalForReadOnEmptyFieldsClass>();
            var records = eng.ReadString(@"id,text,number
121,""""""not good"""" line"", 4456
120,""good line this one"",789
122,,5446");

            Check.That(records.Length).IsEqualTo(3);

            Check.That(records[0].Text).IsEqualTo("\"not good\" line");
            Check.That(records[2].Text).IsEqualTo("");
        }

        [IgnoreFirst(1)]
        [IgnoreEmptyLines()]
        [DelimitedRecord(",")]
        public sealed class OptionalForReadOnEmptyFieldsClass
        {
            public Int32 Id;

            [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.NotAllow)]
            public string Text;

            public Int32? Number;
        }
    }
}