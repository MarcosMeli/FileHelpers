using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    [TestFixture]
    public class MultiRecords
    {
        private MultiRecordEngine mEngine;

        [Test]
        public void MultipleRecordsFile()
        {
            mEngine = new MultiRecordEngine(CustomSelector,
                typeof(OrdersVerticalBar),
                typeof(CustomersSemiColon),
                typeof(SampleType));

            object[] res = mEngine.ReadFile(FileTest.Good.MultiRecord1.Path);

            Assert.AreEqual(12, res.Length);
            Assert.AreEqual(12, mEngine.TotalRecords);

            Assert.AreEqual(typeof(OrdersVerticalBar), res[0].GetType());
            Assert.AreEqual(typeof(OrdersVerticalBar), res[1].GetType());
            Assert.AreEqual(typeof(CustomersSemiColon), res[2].GetType());
            Assert.AreEqual(typeof(SampleType), res[5].GetType());
        }

        [Test]
        public void MultipleRecordsFileAsync()
        {
            mEngine = new MultiRecordEngine(CustomSelector,
                typeof(OrdersVerticalBar),
                typeof(CustomersSemiColon),
                typeof(SampleType));

            var res = new ArrayList();
            mEngine.BeginReadFile(FileTest.Good.MultiRecord1.Path);
            foreach (var o in mEngine)
                res.Add(o);

            Assert.AreEqual(12, res.Count);
            Assert.AreEqual(12, mEngine.TotalRecords);

            Assert.AreEqual(typeof(OrdersVerticalBar), res[0].GetType());
            Assert.AreEqual(typeof(OrdersVerticalBar), res[1].GetType());
            Assert.AreEqual(typeof(CustomersSemiColon), res[2].GetType());
            Assert.AreEqual(typeof(SampleType), res[5].GetType());
        }

        [Test]
        public void MultipleRecordsWriteAsync()
        {
            mEngine = new MultiRecordEngine(CustomSelector,
                typeof(OrdersVerticalBar),
                typeof(CustomersSemiColon),
                typeof(SampleType));

            object[] records = mEngine.ReadFile(FileTest.Good.MultiRecord1.Path);

            mEngine.BeginWriteFile("tempoMulti.txt");
            foreach (var o in records)
                mEngine.WriteNext(o);
            mEngine.Close();
            File.Delete("tempoMulti.txt");

            object[] res = mEngine.ReadFile(FileTest.Good.MultiRecord1.Path);

            Assert.AreEqual(12, res.Length);
            Assert.AreEqual(12, mEngine.TotalRecords);

            Assert.AreEqual(typeof(OrdersVerticalBar), res[0].GetType());
            Assert.AreEqual(typeof(OrdersVerticalBar), res[1].GetType());
            Assert.AreEqual(typeof(CustomersSemiColon), res[2].GetType());
            Assert.AreEqual(typeof(SampleType), res[5].GetType());
        }

        [Test]
        public void MultipleRecordsFileAsyncBad()
        {
            mEngine = new MultiRecordEngine(CustomSelector,
                typeof(OrdersVerticalBar),
                typeof(CustomersSemiColon),
                typeof(SampleType));

            Assert.Throws<FileHelpersException>(
                () =>
                {
                    foreach (var o in mEngine)
                        o.ToString();
                });
        }

        [Test]
        public void MultipleRecordsFileReadWrite()
        {
            mEngine = new MultiRecordEngine(CustomSelector, typeof(OrdersVerticalBar), typeof(CustomersSemiColon), typeof(SampleType));

            object[] res2 = mEngine.ReadFile(FileTest.Good.MultiRecord1.Path);

            Assert.AreEqual(12, res2.Length);
            Assert.AreEqual(12, mEngine.TotalRecords);

            mEngine.WriteFile("tempMR.txt", res2);
            var res = mEngine.ReadFile("tempMR.txt");
            File.Delete("tempMR.txt");

            Assert.AreEqual(12, res.Length);
            Assert.AreEqual(12, mEngine.TotalRecords);

            Assert.AreEqual(typeof(OrdersVerticalBar), res[0].GetType());
            Assert.AreEqual(typeof(OrdersVerticalBar), res[1].GetType());
            Assert.AreEqual(typeof(CustomersSemiColon), res[2].GetType());
            Assert.AreEqual(typeof(SampleType), res[5].GetType());
        }

        [Test]
        public void NoTypes()
        {
            Assert.Throws<BadUsageException>(() =>
                new MultiRecordEngine());
        }

        [Test]
        public void NullTypeArray()
        {
            Assert.Throws<BadUsageException>(() =>
                new MultiRecordEngine((Type[]) null));
        }

        [Test]
        public void NoSelector()
        {
            mEngine = new MultiRecordEngine(typeof(CustomersVerticalBar), typeof(CustomersTab));
        }

        [Test]
        public void TwiceSameType()
        {
            Assert.Throws<BadUsageException>(()
                => new MultiRecordEngine(typeof(CustomersVerticalBar), typeof(CustomersVerticalBar)));
        }

        [Test]
        public void OneType()
        {
            Assert.Throws<BadUsageException>(()
                => new MultiRecordEngine(typeof(CustomersVerticalBar)));
        }

        [Test]
        public void NullTypes()
        {
            Assert.Throws<BadUsageException>(()
                => new MultiRecordEngine(typeof(CustomersVerticalBar), null));
        }

        [Test]
        public void WhenSelectorReturnsTypeThatIsNotInEngine_ShouldThrowBadUsageException_WhenReadingFileAtATime()
        {
            mEngine = new MultiRecordEngine(CustomSelectorReturningBadType,
                typeof(OrdersVerticalBar),
                typeof(CustomersSemiColon),
                typeof(SampleType));

            Assert.Throws<BadUsageException>(() => mEngine.ReadFile(FileTest.Good.MultiRecord1.Path));
        }

        [Test]
        public void WhenSelectorReturnsTypeThatIsNotInEngine_ShouldThrowBadUsageException_WhenReadingRecordAtATime()
        {
            mEngine = new MultiRecordEngine(CustomSelectorReturningBadType,
                typeof(OrdersVerticalBar),
                typeof(CustomersSemiColon),
                typeof(SampleType));

            mEngine.BeginReadFile(FileTest.Good.MultiRecord1.Path);

            Assert.Throws<BadUsageException>(() => mEngine.ReadNext());
        }

        private static Type CustomSelector(MultiRecordEngine engine, string record)
        {
            if (char.IsLetter(record[0]))
                return typeof(CustomersSemiColon);
            else if (record.Length == 14)
                return typeof(SampleType);
            else
                return typeof(OrdersVerticalBar);
        }

        private static Type CustomSelectorReturningBadType(MultiRecordEngine engine, string record)
        {
            return typeof(string);
        }
    }
}