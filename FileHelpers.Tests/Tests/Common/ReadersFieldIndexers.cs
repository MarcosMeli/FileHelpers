using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class ReadersFieldIndexer
    {
        private readonly string data = "11121314901234" + Environment.NewLine +
                                       "10111314012345" + Environment.NewLine +
                                       "11101314123456" + Environment.NewLine +
                                       "10101314234567" + Environment.NewLine;

        [Test]
        public void AsyncFieldIndex1()
        {
            var engine = new FileHelperAsyncEngine(typeof (SampleType));
            engine.BeginReadString(data);

            foreach (SampleType rec in engine) {
                Assert.AreEqual(rec.Field1, engine[0]);
                Assert.AreEqual(rec.Field2, engine[1]);
                Assert.AreEqual(rec.Field3, engine[2]);

                Assert.AreEqual(rec.Field1, engine["Field1"]);
                Assert.AreEqual(rec.Field2, engine["Field2"]);
                Assert.AreEqual(rec.Field3, engine["Field3"]);
            }

            engine.Close();
        }


        [Test]
        public void AsyncFieldIndex2()
        {
            var engine = new FileHelperAsyncEngine(typeof (SampleType));
            engine.BeginReadString(data);

            while (engine.ReadNext() != null) {
                Assert.AreEqual(engine[0], engine.LastRecordValues[0]);
                Assert.AreEqual(engine[1], engine.LastRecordValues[1]);
                Assert.AreEqual(engine[2], engine.LastRecordValues[2]);
            }

            engine.Close();
        }

        [Test]
        public void AsyncFieldIndex3()
        {
            var engine = new FileHelperAsyncEngine(typeof (SampleType));
            engine.BeginReadString(data);

            while (engine.ReadNext() != null) {
                Assert.AreEqual(engine["Field1"], engine.LastRecordValues[0]);
                Assert.AreEqual(engine["Field2"], engine.LastRecordValues[1]);
                Assert.AreEqual(engine["Field3"], engine.LastRecordValues[2]);
            }

            engine.Close();
        }

        [Test]
        public void AsyncFieldIndex4()
        {
            var engine = new FileHelperAsyncEngine(typeof (SampleType));
            engine.BeginReadString(data);

            Assert.AreEqual(3, engine.Options.FieldCount);

            while (engine.ReadNext() != null) {
                for (int i = 0; i < engine.Options.FieldCount; i++)
                    Assert.IsNotNull(engine[i]);
            }

            engine.Close();
        }

        [Test]
        public void AsyncFieldIndexBad1()
        {
            var engine = new FileHelperAsyncEngine(typeof (SampleType));
            engine.BeginReadString(data);

            while (engine.ReadNext() != null) {
                Assert.Throws<IndexOutOfRangeException>(()
                    => { object val = engine[10]; });
            }

            engine.Close();
        }

        [Test]
        public void AsyncFieldIndexBad2()
        {
            var engine = new FileHelperAsyncEngine(typeof (SampleType));
            engine.BeginReadString(data);

            Assert.Throws<BadUsageException>(()
                => {
                while (engine.ReadNext() != null) {
                    object val = engine["FieldNoThere"];
                }
            });

            engine.Close();
        }


        [Test]
        public void AsyncFieldIndexBad3()
        {
            var engine = new FileHelperAsyncEngine(typeof (SampleType));
            Assert.Throws<BadUsageException>(()
                => { object val = engine[2]; });
        }

        [Test]
        public void AsyncFieldIndexBad4()
        {
            var engine = new FileHelperAsyncEngine(typeof (SampleType));
            Assert.Throws<BadUsageException>(()
                => { object val = engine["Field1"]; });
        }

        [Test]
        public void AsyncFieldIndexBad5()
        {
            var engine = new FileHelperAsyncEngine(typeof (SampleType));
            engine.BeginReadString(data);
            while (engine.ReadNext() != null) {}
            engine.Close();
            Assert.Throws<BadUsageException>(()
                => { object val = engine[2]; });
        }


        [Test]
        public void FieldNames()
        {
            var engine = new FileHelperAsyncEngine(typeof (SampleType));
            string[] names = engine.Options.FieldsNames;

            Assert.AreEqual(3, names.Length);
            Assert.AreEqual("Field1", names[0]);
            Assert.AreEqual("Field2", names[1]);
            Assert.AreEqual("Field3", names[2]);
        }

        [Test]
        public void FieldTypes()
        {
            var engine = new FileHelperAsyncEngine(typeof (SampleType));
            Type[] types = engine.Options.FieldsTypes;

            Assert.AreEqual(3, types.Length);
            Assert.AreEqual(typeof (DateTime), types[0]);
            Assert.AreEqual(typeof (string), types[1]);
            Assert.AreEqual(typeof (int), types[2]);
        }
    }
}