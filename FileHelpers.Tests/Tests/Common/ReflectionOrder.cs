using System;
using System.Collections;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class ReflectionOrder
    {
        [Test]
        public void ReadFile()
        {
            typeof (SampleType).GetField("Field2");

            //typeof(SampleType).GetFields();
            //typeof(SampleType).GetFields();
            //typeof(SampleType).GetFields();

            for (int i = 0; i < 10; i++) {
                typeof (SampleType).GetField("Field2");
                var engine = new FileHelperEngine<SampleType>();
                typeof (SampleType).GetField("Field2");

                Assert.AreEqual("Field1", engine.Options.FieldsNames[0]);
                Assert.AreEqual("Field2", engine.Options.FieldsNames[1]);
                Assert.AreEqual("Field3", engine.Options.FieldsNames[2]);

                SampleType[] res;
                res = TestCommon.ReadTest<SampleType>(engine, "Good", "Test1.txt");

                Assert.AreEqual(4, res.Length);
                Assert.AreEqual(4, engine.TotalRecords);
                Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

                Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
                Assert.AreEqual("901", res[0].Field2);
                Assert.AreEqual(234, res[0].Field3);

                Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
                Assert.AreEqual("012", res[1].Field2);
                Assert.AreEqual(345, res[1].Field3);
            }
        }

        [Test]
        public void ReadFileBulk()
        {
            var temp = new ArrayList();
            for (int i = 0; i < 10000; i++) {
                temp.Add(typeof (SampleType).GetField("Field2"));
                temp.Add(typeof (SampleType).GetField("Field3"));
            }


            //typeof(SampleType).GetFields();
            //typeof(SampleType).GetFields();
            //typeof(SampleType).GetFields();

            for (int i = 0; i < 10; i++) {
                typeof (SampleType).GetField("Field2");
                var engine = new FileHelperEngine<SampleType>();
                typeof (SampleType).GetField("Field2");

                Assert.AreEqual("Field1", engine.Options.FieldsNames[0]);
                Assert.AreEqual("Field2", engine.Options.FieldsNames[1]);
                Assert.AreEqual("Field3", engine.Options.FieldsNames[2]);

                SampleType[] res;
                res = TestCommon.ReadTest<SampleType>(engine, "Good", "Test1.txt");

                Assert.AreEqual(4, res.Length);
                Assert.AreEqual(4, engine.TotalRecords);
                Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

                Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
                Assert.AreEqual("901", res[0].Field2);
                Assert.AreEqual(234, res[0].Field3);

                Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
                Assert.AreEqual("012", res[1].Field2);
                Assert.AreEqual(345, res[1].Field3);
            }
        }


        [Test]
        public void FirstOrder()
        {
            typeof (SampleType).GetField("Field1");
            typeof (SampleType).GetField("Field2");
            typeof (SampleType).GetField("Field3");

            for (int i = 0; i < 10; i++) {
                var engine = new FileHelperEngine<SampleType>();

                Assert.AreEqual("Field1", engine.Options.FieldsNames[0]);
                Assert.AreEqual("Field2", engine.Options.FieldsNames[1]);
                Assert.AreEqual("Field3", engine.Options.FieldsNames[2]);

                SampleType[] res;
                res = TestCommon.ReadTest<SampleType>(engine, "Good", "Test1.txt");

                Assert.AreEqual(4, res.Length);
                Assert.AreEqual(4, engine.TotalRecords);
                Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

                Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
                Assert.AreEqual("901", res[0].Field2);
                Assert.AreEqual(234, res[0].Field3);

                Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
                Assert.AreEqual("012", res[1].Field2);
                Assert.AreEqual(345, res[1].Field3);
            }
        }

        [Test]
        public void ReadFileGenerics()
        {
            typeof (SampleType).GetField("Field2");

            for (int i = 0; i < 10; i++) {
                var engine = new FileHelperEngine<SampleType>();

                SampleType[] res;
                res = engine.ReadFile(FileTest.Good.Test1.Path);

                Assert.AreEqual(4, res.Length);
                Assert.AreEqual(4, engine.TotalRecords);
                Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

                Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
                Assert.AreEqual("901", res[0].Field2);
                Assert.AreEqual(234, res[0].Field3);

                Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
                Assert.AreEqual("012", res[1].Field2);
                Assert.AreEqual(345, res[1].Field3);
            }
        }
    }
}