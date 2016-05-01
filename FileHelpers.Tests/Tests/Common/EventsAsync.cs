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
        private FileHelperAsyncEngine<SampleType> engine;

        [Test]
        public void ReadEvents()
        {
            before = 0;
            after = 0;

            engine = new FileHelperAsyncEngine<SampleType>();
            engine.BeforeReadRecord += new BeforeReadHandler<SampleType>(BeforeEvent);
            engine.AfterReadRecord += new AfterReadHandler<SampleType>(AfterEvent);

            engine.BeginReadFile(FileTest.Good.Test1.Path);

            int count = 0;
            foreach (var t in engine)
                count++;

            Assert.AreEqual(4, count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(4, before);
            Assert.AreEqual(4, after);
        }


        [Test]
        public void WriteEvents()
        {
            before = 0;
            after = 0;

            engine = new FileHelperAsyncEngine<SampleType>();

            engine.BeforeWriteRecord += new BeforeWriteHandler<SampleType>(engine_BeforeWriteRecord);
            engine.AfterWriteRecord += new AfterWriteHandler<SampleType>(engine_AfterWriteRecord);

            var res = new SampleType[2];

            res[0] = new SampleType();
            res[1] = new SampleType();

            res[0].Field1 = DateTime.Now.AddDays(1);
            res[0].Field2 = "je";
            res[0].Field3 = 0;

            res[1].Field1 = DateTime.Now;
            res[1].Field2 = "ho";
            res[1].Field3 = 2;

            engine.BeginWriteFile("tempEvent.txt");
            engine.WriteNexts(res);
            engine.Close();

            File.Delete("tempEvent.txt");
            Assert.AreEqual(2, engine.TotalRecords);
            Assert.AreEqual(2, before);
            Assert.AreEqual(2, after);
        }


        [Test]
        public void ReadEventsCancelAfter()
        {
            before = 0;
            after = 0;

            engine = new FileHelperAsyncEngine<SampleType>();
            engine.AfterReadRecord += new AfterReadHandler<SampleType>(AfterEvent2);

            engine.BeginReadFile(FileTest.Good.Test1.Path);

            int count = 0;
            foreach (var t in engine)
                count++;

            Assert.AreEqual(0, count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, before);
            Assert.AreEqual(4, after);
        }

        [Test]
        public void ReadEventsCancelBefore()
        {
            before = 0;
            after = 0;

            engine = new FileHelperAsyncEngine<SampleType>();
            engine.BeforeReadRecord += new BeforeReadHandler<SampleType>(BeforeEvent2);

            engine.BeginReadFile(FileTest.Good.Test1.Path);

            int count = 0;
            foreach (var t in engine)
                count++;

            Assert.AreEqual(0, count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(4, before);
            Assert.AreEqual(0, after);
        }

        [Test]
        public void ReadEventsCancelAll()
        {
            before = 0;
            after = 0;

            engine = new FileHelperAsyncEngine<SampleType>();
            engine.BeforeReadRecord += new BeforeReadHandler<SampleType>(BeforeEvent2);
            engine.AfterReadRecord += new AfterReadHandler<SampleType>(AfterEvent2);

            engine.BeginReadFile(FileTest.Good.Test1.Path);
            int count = 0;
            foreach (var t in engine)
                count++;

            Assert.AreEqual(0, count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(4, before);
            Assert.AreEqual(0, after);
        }

        private int before = 0;
        private int after = 0;

        private void BeforeEvent(EngineBase sender, BeforeReadEventArgs<SampleType> e)
        {
            if (e.RecordLine.StartsWith(" ") ||
                e.RecordLine.StartsWith("-"))
                e.SkipThisRecord = true;

            before++;
        }

        private void AfterEvent(EngineBase sender, AfterReadEventArgs<SampleType> e)
        {
            after++;
        }

        private void engine_BeforeWriteRecord(EngineBase sender, BeforeWriteEventArgs<SampleType> e)
        {
            before++;
        }

        private void engine_AfterWriteRecord(EngineBase sender, AfterWriteEventArgs<SampleType> e)
        {
            after++;
        }

        private void AfterEvent2(EngineBase sender, AfterReadEventArgs<SampleType> e)
        {
            e.SkipThisRecord = true;
            after++;
        }

        private void BeforeEvent2(EngineBase sender, BeforeReadEventArgs<SampleType> e)
        {
            e.SkipThisRecord = true;
            before++;
        }


        [Test(Description = "3 empty lines as input and the events give the original line")]
        public void ChangeLineInEvent()
        {
            string input = "\n\n\n";
            engine = new FileHelperAsyncEngine<SampleType>();
            engine.BeforeReadRecord += new BeforeReadHandler<SampleType>(BeforeEventChange);

            engine.BeginReadString(input);
            var res = (SampleType[]) engine.ReadNexts(3);

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