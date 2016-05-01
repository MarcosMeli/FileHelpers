using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class IgnoreEmpties
    {
        [Test]
        public void IgnoreEmpty1()
        {
            var engine = new FileHelperEngine<IgnoreEmptyType1>();

            var res = TestCommon.ReadTest<IgnoreEmptyType1>(engine, "Good", "IgnoreEmpty1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(8, engine.LineNumber);
        }

        [Test]
        public void IgnoreEmpty2()
        {
            var engine = new FileHelperEngine<IgnoreEmptyType1>();

            object[] res = TestCommon.ReadTest(engine, "Good", "IgnoreEmpty2.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(8, engine.LineNumber);
        }

        [Test]
        public void IgnoreEmpty3()
        {
            var engine = new FileHelperEngine<IgnoreEmptyType1>();

            var res = TestCommon.ReadTest<IgnoreEmptyType1>(engine, "Good", "IgnoreEmpty3.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(8, engine.LineNumber);
        }

        [Test]
        public void IgnoreEmpty1Async()
        {
            var asyncEngine = new FileHelperAsyncEngine<IgnoreEmptyType1>();

            var res = TestCommon.ReadAllAsync<IgnoreEmptyType1>(asyncEngine, "Good", "IgnoreEmpty1.txt");

            Assert.AreEqual(4, res.Count);
            Assert.AreEqual(8, asyncEngine.LineNumber);
        }

        [Test]
        public void IgnoreEmpty3Async()
        {
            var asyncEngine = new FileHelperAsyncEngine<IgnoreEmptyType1>();

            var res = TestCommon.ReadAllAsync<IgnoreEmptyType1>(asyncEngine, "Good", "IgnoreEmpty3.txt");

            Assert.AreEqual(4, res.Count);
            Assert.AreEqual(7, asyncEngine.LineNumber);
        }

        [Test]
        public void IgnoreEmpty4Bad()
        {
            var engine = new FileHelperEngine<IgnoreEmptyType1>();
            Assert.Throws<BadUsageException>(
                () => TestCommon.ReadTest<IgnoreEmptyType1>(engine, "Good", "IgnoreEmpty4.txt"));
        }

        [Test]
        public void IgnoreEmpty4BadAsync()
        {
            var asyncEngine = new FileHelperAsyncEngine<IgnoreEmptyType1>();
            Assert.Throws<BadUsageException>(
                () => TestCommon.ReadAllAsync<IgnoreEmptyType1>(asyncEngine, "Good", "IgnoreEmpty4.txt"));
        }

        [Test]
        public void IgnoreEmpty4()
        {
            var engine = new FileHelperEngine<IgnoreEmptyType1Spaces>();
            object[] res = TestCommon.ReadTest<IgnoreEmptyType1Spaces>(engine, "Good", "IgnoreEmpty4.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(7, engine.LineNumber);
        }

        [Test]
        public void IgnoreEmpty5()
        {
            var engine = new FileHelperEngine<IgnoreEmptyType1Spaces>();
            var res = TestCommon.ReadTest<IgnoreEmptyType1Spaces>(engine, "Good", "IgnoreEmpty5.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(7, engine.LineNumber);
        }

        [Test]
        public void IgnoreComment1()
        {
            var engine = new FileHelperEngine<IgnoreCommentsType>();
            object[] res = TestCommon.ReadTest<IgnoreCommentsType>(engine, "Good", "IgnoreComments1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(7, engine.LineNumber);
        }

        [Test]
        public void IgnoreComment1Async()
        {
            var asyncEngine = new FileHelperAsyncEngine<IgnoreCommentsType>();
            var res = TestCommon.ReadAllAsync<IgnoreCommentsType>(asyncEngine, "Good", "IgnoreComments1.txt");

            Assert.AreEqual(4, res.Count);
            Assert.AreEqual(7, asyncEngine.LineNumber);
        }

        [Test]
        public void IgnoreComment2()
        {
            var engine = new FileHelperEngine<IgnoreCommentsType>();
            var res = TestCommon.ReadTest<IgnoreCommentsType>(engine, "Good", "IgnoreComments2.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(7, engine.LineNumber);
        }


        [Test]
        public void IgnoreComment3()
        {
            var engine = new FileHelperEngine<IgnoreCommentsType2>();
            var res = TestCommon.ReadTest<IgnoreCommentsType2>(engine, "Good", "IgnoreComments1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(7, engine.LineNumber);
        }

        [Test]
        public void IgnoreComment4()
        {
            var engine = new FileHelperEngine<IgnoreCommentsType2>();
            Assert.Throws<ConvertException>(
                () => TestCommon.ReadTest<IgnoreCommentsType2>(engine, "Good", "IgnoreComments2.txt"));

            Assert.AreEqual(3, engine.LineNumber);
        }


        [FixedLengthRecord]
#pragma warning disable CS0618 // Type or member is obsolete
        [IgnoreCommentedLines("//")]
#pragma warning restore CS0618 // Type or member is obsolete
        public class IgnoreCommentsType
        {
            [FieldFixedLength(8)]
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime Field1;

            [FieldFixedLength(3)]
            public string Field2;

            [FieldFixedLength(3)]
            [FieldConverter(ConverterKind.Int32)]
            public int Field3;
        }

        [FixedLengthRecord]
#pragma warning disable CS0618 // Type or member is obsolete
        [IgnoreCommentedLines("//", false)]
#pragma warning restore CS0618 // Type or member is obsolete
        public class IgnoreCommentsType2
        {
            [FieldFixedLength(8)]
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime Field1;

            [FieldFixedLength(3)]
            public string Field2;

            [FieldFixedLength(3)]
            [FieldConverter(ConverterKind.Int32)]
            public int Field3;
        }


        [FixedLengthRecord]
        [IgnoreEmptyLines]
        public class IgnoreEmptyType1
        {
            [FieldFixedLength(8)]
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime Field1;

            [FieldFixedLength(3)]
            public string Field2;

            [FieldFixedLength(3)]
            [FieldConverter(ConverterKind.Int32)]
            public int Field3;
        }

        [FixedLengthRecord]
        [IgnoreEmptyLines(true)]
        public class IgnoreEmptyType1Spaces
        {
            [FieldFixedLength(8)]
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime Field1;

            [FieldFixedLength(3)]
            public string Field2;

            [FieldFixedLength(3)]
            [FieldConverter(ConverterKind.Int32)]
            public int Field3;
        }


        [DelimitedRecord("|")]
        [IgnoreEmptyLines]
        public class IgnoreEmptyType2
        {
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime Field1;

            [FieldFixedLength(3)]
            public string Field2;

            public int Field3;
        }
    }
}