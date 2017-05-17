using System;
using System.Collections;
using System.IO;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class IgnoreLasts
    {
        private readonly string expectedLongFooterText = "you can get this lines" + Environment.NewLine +
                                                         "with the FileHelperEngine.FooterText property" +
                                                         Environment.NewLine;

        private readonly string expectedShortFooterText = "This is a new Footer...." + Environment.NewLine;

        [Test]
        public void DiscardLast1()
        {
            var engine = new FileHelperEngine<DiscardLastType0>();

            var res = TestCommon.ReadTest<DiscardLastType0>(engine, "Good", "DiscardLast0.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
        }

        [Test]
        public void DiscardLast2()
        {
            var engine = new FileHelperEngine<DiscardLastType1>();

            var res = TestCommon.ReadTest<DiscardLastType1>(engine, "Good", "DiscardLast1.txt");

            Assert.AreEqual(engine.TotalRecords, res.Length);

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
        }

        [Test]
        public void DiscardLast2bis()
        {
            var engine = new FileHelperEngine<DiscardLastType1bis>();

            var res = TestCommon.ReadTest<DiscardLastType1bis>(engine, "Good", "DiscardLast1.txt");

            Assert.AreEqual(engine.TotalRecords, res.Length);

            Assert.AreEqual(3, res.Length);
            Assert.AreEqual(new DateTime(1314, 11, 10), res[0].Field1);
        }

        [Test]
        public void DiscardLast3()
        {
            var engine = new FileHelperEngine<DiscardLastType11>();

            DiscardLastType11[] res = TestCommon.ReadTest<DiscardLastType11>(engine, "Good", "DiscardLast1.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
        }

        [Test]
        public void DiscardLast4()
        {
            var engine = new FileHelperEngine<DiscardLastType2>();

            DiscardLastType2[] res = TestCommon.ReadTest<DiscardLastType2>(engine, "Good", "DiscardLast2.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
            Assert.AreEqual(expectedLongFooterText, engine.FooterText);
        }

        [Test]
        public void DiscardLast5()
        {
            var engine = new FileHelperEngine<DiscardLastType2>();

            DiscardLastType2[] res = TestCommon.ReadTest<DiscardLastType2>(engine, "Good", "DiscardLast3.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
        }

        [Test]
        public void DiscardLast6()
        {
            var asyncEngine = new FileHelperAsyncEngine<DiscardLastType2>();

            TestCommon.BeginReadTest<DiscardLastType2>(asyncEngine, "Good", "DiscardLast2.txt");

            var arr = new ArrayList();
            while (asyncEngine.ReadNext() != null)
                arr.Add(asyncEngine.LastRecord);

            Assert.AreEqual(4, asyncEngine.TotalRecords);

            Assert.AreEqual(expectedLongFooterText, asyncEngine.FooterText);

            Assert.AreEqual(new DateTime(1314, 12, 11), ((DiscardLastType2) arr[0]).Field1);
        }


        [Test]
        public void DiscardWriteRead()
        {
            var engine = new FileHelperEngine<DiscardLastType1>();

            DiscardLastType1[] res = TestCommon.ReadTest<DiscardLastType1>(engine, "Good", "DiscardLast1.txt");
            engine.FooterText = expectedShortFooterText;

            engine.WriteFile("tempo.txt", res);

            engine.FooterText = "none none";

            DiscardLastType1[] res2 = engine.ReadFile(@"tempo.txt");

            Assert.AreEqual(res.Length, res2.Length);
            Assert.AreEqual(expectedShortFooterText, engine.FooterText);

            if (File.Exists("tempo.txt"))
                File.Delete("tempo.txt");

            Assert.AreEqual(4, res.Length);
            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
        }

        [Test]
        public void DiscardWriteRead2()
        {
            var engine = new FileHelperEngine<DiscardLastType1>();

            DiscardLastType1[] res = TestCommon.ReadTest<DiscardLastType1>(engine, "Good", "DiscardLast1.txt");


            var asyncEngine = new FileHelperAsyncEngine<DiscardLastType1>();

            asyncEngine.FooterText = "This is a new Footer....";

            asyncEngine.BeginWriteFile("temp.txt");
            asyncEngine.WriteNexts(res);
            asyncEngine.Close();

            asyncEngine.FooterText = "none none";

            asyncEngine.BeginReadFile("temp.txt");

            while (asyncEngine.ReadNext() != null) {}

            Assert.AreEqual(res.Length, asyncEngine.TotalRecords);
            Assert.AreEqual(expectedShortFooterText, asyncEngine.FooterText);

            asyncEngine.Close();

            Assert.AreEqual(res.Length, asyncEngine.TotalRecords);
            Assert.AreEqual(expectedShortFooterText, asyncEngine.FooterText);

            if (File.Exists("tempo.txt"))
                File.Delete("tempo.txt");
        }
    }

    [FixedLengthRecord]
    [IgnoreLast(0)]
    [IgnoreFirst(0)]
    public class DiscardLastType0
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
    [IgnoreLast]
    public class DiscardLastType1
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
    [IgnoreFirst]
    [IgnoreLast]
    public class DiscardLastType1bis
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
    [IgnoreLast(1)]
    public class DiscardLastType11
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
    [IgnoreLast(2)]
    public class DiscardLastType2
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
}