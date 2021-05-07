using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FileHelpers.Events;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    [TestFixture]
    public class EventsAsync
    {
        private FileHelperAsyncEngine<SampleType> mEngine;
        private int mBefore;
        private int mAfter;

        [SetUp]
        public void Setup()
        {
            mBefore = 0;
            mAfter = 0;
            mEngine = new FileHelperAsyncEngine<SampleType>();
        }

        [Test]
        public void ReadEvents()
        {
            mEngine.BeforeReadRecord += BeforeEvent;
            mEngine.AfterReadRecord += AfterEvent;

            mEngine.BeginReadFile(FileTest.Good.Test1.Path);

            var count = 0;
            foreach (var t in mEngine)
                count++;

            Assert.AreEqual(4, count);
            Assert.AreEqual(4, mEngine.TotalRecords);
            Assert.AreEqual(4, mBefore);
            Assert.AreEqual(4, mAfter);
        }

        [Test]
        public void WriteEvents()
        {
            mEngine.BeforeWriteRecord += BeforeWriteRecord;
            mEngine.AfterWriteRecord += AfterWriteRecord;

            var res = new SampleType[2];

            res[0] = new SampleType();
            res[1] = new SampleType();

            res[0].Field1 = DateTime.Now.AddDays(1);
            res[0].Field2 = "je";
            res[0].Field3 = 0;

            res[1].Field1 = DateTime.Now;
            res[1].Field2 = "ho";
            res[1].Field3 = 2;

            mEngine.BeginWriteFile("tempEvent.txt");
            mEngine.WriteNexts(res);
            mEngine.Close();

            File.Delete("tempEvent.txt");
            Assert.AreEqual(2, mEngine.TotalRecords);
            Assert.AreEqual(2, mBefore);
            Assert.AreEqual(2, mAfter);
        }

        [Test]
        public void ReadEventsCancelAfter()
        {
            mEngine.AfterReadRecord += AfterEvent2;

            mEngine.BeginReadFile(FileTest.Good.Test1.Path);

            var count = 0;
            foreach (var t in mEngine)
                count++;

            Assert.AreEqual(0, count);
            Assert.AreEqual(4, mEngine.TotalRecords);
            Assert.AreEqual(0, mBefore);
            Assert.AreEqual(4, mAfter);
        }

        [Test]
        public void ReadEventsCancelBefore()
        {
            mEngine.BeforeReadRecord += BeforeEvent2;

            mEngine.BeginReadFile(FileTest.Good.Test1.Path);

            var count = 0;
            foreach (var t in mEngine)
                count++;

            Assert.AreEqual(0, count);
            Assert.AreEqual(4, mEngine.TotalRecords);
            Assert.AreEqual(4, mBefore);
            Assert.AreEqual(0, mAfter);
        }

        [Test]
        public void ReadEventsCancelAll()
        {
            mEngine.BeforeReadRecord += BeforeEvent2;
            mEngine.AfterReadRecord += AfterEvent2;

            mEngine.BeginReadFile(FileTest.Good.Test1.Path);
            var count = 0;
            foreach (var t in mEngine)
                count++;

            Assert.AreEqual(0, count);
            Assert.AreEqual(4, mEngine.TotalRecords);
            Assert.AreEqual(4, mBefore);
            Assert.AreEqual(0, mAfter);
        }

        private void BeforeEvent(EngineBase sender, BeforeReadEventArgs<SampleType> e)
        {
            if (e.RecordLine.StartsWith(" ") ||
                e.RecordLine.StartsWith("-"))
                e.SkipThisRecord = true;

            mBefore++;
        }

        private void AfterEvent(EngineBase sender, AfterReadEventArgs<SampleType> e)
        {
            mAfter++;
        }

        private void BeforeWriteRecord(EngineBase sender, BeforeWriteEventArgs<SampleType> e)
        {
            mBefore++;
        }

        private void AfterWriteRecord(EngineBase sender, AfterWriteEventArgs<SampleType> e)
        {
            mAfter++;
        }

        private void AfterEvent2(EngineBase sender, AfterReadEventArgs<SampleType> e)
        {
            e.SkipThisRecord = true;
            mAfter++;
        }

        private void BeforeEvent2(EngineBase sender, BeforeReadEventArgs<SampleType> e)
        {
            e.SkipThisRecord = true;
            mBefore++;
        }

        [Test(Description = "3 empty lines as input and the events give the original line")]
        public void ChangeLineInEvent()
        {
            var input = "\n\n\n";
            mEngine = new FileHelperAsyncEngine<SampleType>();
            mEngine.BeforeReadRecord += BeforeEventChange;

            mEngine.BeginReadString(input);
            var res = mEngine.ReadNexts(3);

            Assert.AreEqual(3, res.Length);
            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
            Assert.AreEqual("901", res[0].Field2);
            Assert.AreEqual(234, res[0].Field3);

            Assert.AreEqual(new DateTime(1314, 12, 11), res[1].Field1);
            Assert.AreEqual("901", res[2].Field2);
            Assert.AreEqual(234, res[2].Field3);
        }

        private static void BeforeEventChange(EngineBase engine, BeforeReadEventArgs<SampleType> e)
        {
            Assert.IsFalse(e.RecordLineChanged);
            e.RecordLine = "11121314901234";
            Assert.IsTrue(e.RecordLineChanged);
        }
    }
}