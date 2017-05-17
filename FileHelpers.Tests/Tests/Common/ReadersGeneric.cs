using System;
using System.Data;
using System.IO;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class ReadersGenerics
    {
        [Test]
        public void ReadFile()
        {
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


        [Test]
        public void ReadFileStatic()
        {
            SampleType[] res;
            res = CommonEngine.ReadFile<SampleType>(FileTest.Good.Test1.Path);

            Assert.AreEqual(4, res.Length);

            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
            Assert.AreEqual("901", res[0].Field2);
            Assert.AreEqual(234, res[0].Field3);

            Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
            Assert.AreEqual("012", res[1].Field2);
            Assert.AreEqual(345, res[1].Field3);
        }


        [Test]
        public void AsyncRead()
        {
            var asyncEngine = new FileHelperAsyncEngine<SampleType>();

            SampleType rec1, rec2;
            asyncEngine.BeginReadFile(FileTest.Good.Test1.Path);

            rec1 = asyncEngine.ReadNext();
            Assert.IsNotNull(rec1);
            rec2 = asyncEngine.ReadNext();
            Assert.IsNotNull(rec1);

            Assert.IsTrue(rec1 != rec2);

            rec1 = asyncEngine.ReadNext();
            Assert.IsNotNull(rec2);
            rec1 = asyncEngine.ReadNext();
            Assert.IsNotNull(rec2);

            Assert.IsTrue(rec1 != rec2);

            Assert.AreEqual(0, asyncEngine.ErrorManager.ErrorCount);

            asyncEngine.Close();
        }

        [Test]
        public void AsyncReadMoreAndMore()
        {
            var asyncEngine = new FileHelperAsyncEngine<SampleType>();
            SampleType rec1;
            asyncEngine.BeginReadFile(FileTest.Good.Test1.Path);

            rec1 = asyncEngine.ReadNext();
            rec1 = asyncEngine.ReadNext();
            rec1 = asyncEngine.ReadNext();
            rec1 = asyncEngine.ReadNext();
            rec1 = asyncEngine.ReadNext();

            Assert.IsTrue(rec1 == null);

            rec1 = (SampleType) asyncEngine.ReadNext();
            Assert.AreEqual(0, asyncEngine.ErrorManager.ErrorCount);

            asyncEngine.Close();
        }


        [Test]
        public void AsyncRead2()
        {
            SampleType rec1;

            var asyncEngine = new FileHelperAsyncEngine<SampleType>();
            asyncEngine.BeginReadFile(FileTest.Good.Test1.Path);

            int lineAnt = asyncEngine.LineNumber;
            while (asyncEngine.ReadNext() != null) {
                rec1 = asyncEngine.LastRecord;
                Assert.IsNotNull(rec1);
                Assert.AreEqual(lineAnt + 1, asyncEngine.LineNumber);
                lineAnt = asyncEngine.LineNumber;
            }

            Assert.AreEqual(4, asyncEngine.TotalRecords);
            Assert.AreEqual(0, asyncEngine.ErrorManager.ErrorCount);
            asyncEngine.Close();
        }

        [Test]
        public void AsyncReadEnumerable()
        {
            var asyncEngine = new FileHelperAsyncEngine<SampleType>();
            asyncEngine.BeginReadFile(FileTest.Good.Test1.Path);

            int lineAnt = asyncEngine.LineNumber;

            foreach (var rec1 in asyncEngine) {
                Assert.IsNotNull(rec1);
                Assert.AreEqual(lineAnt + 1, asyncEngine.LineNumber);
                lineAnt = asyncEngine.LineNumber;
            }

            Assert.AreEqual(4, asyncEngine.TotalRecords);
            Assert.AreEqual(0, asyncEngine.ErrorManager.ErrorCount);

            asyncEngine.Close();
        }

        [Test]
        public void AsyncReadEnumerableBad()
        {
            var asyncEngine = new FileHelperAsyncEngine<SampleType>();

            Assert.Throws<FileHelpersException>(()
                => {
                foreach (var rec1 in asyncEngine)
                    rec1.ToString();
            });
            asyncEngine.Close();
        }

        [Test]
        public void AsyncReadEnumerable2()
        {
            using (var asyncEngine = new FileHelperAsyncEngine<SampleType>()) {
                asyncEngine.BeginReadFile(FileTest.Good.Test1.Path);

                int lineAnt = asyncEngine.LineNumber;

                foreach (var rec1 in asyncEngine) {
                    Assert.IsNotNull(rec1);
                    Assert.AreEqual(lineAnt + 1, asyncEngine.LineNumber);
                    lineAnt = asyncEngine.LineNumber;
                }

                Assert.AreEqual(4, asyncEngine.TotalRecords);
                Assert.AreEqual(0, asyncEngine.ErrorManager.ErrorCount);
            }
        }

        [Test]
        public void AsyncReadEnumerableAutoDispose()
        {
            var asyncEngine = new FileHelperAsyncEngine<SampleType>();
            asyncEngine.BeginReadFile(FileTest.Good.Test1.Path);

            asyncEngine.ReadNext();
            asyncEngine.ReadNext();
        }

        [Test]
        public void ReadStream()
        {
            string data = "11121314901234" + Environment.NewLine +
                          "10111314012345" + Environment.NewLine +
                          "11101314123456" + Environment.NewLine +
                          "10101314234567" + Environment.NewLine;

            var engine = new FileHelperEngine<SampleType>();

            SampleType[] res;
            res = engine.ReadStream(new StringReader(data));

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

        [Test]
        public void ReadString()
        {
            string data = "11121314901234" + Environment.NewLine +
                          "10111314012345" + Environment.NewLine +
                          "11101314123456" + Environment.NewLine +
                          "10101314234567" + Environment.NewLine;

            var engine = new FileHelperEngine<SampleType>();

            SampleType[] res;
            res = engine.ReadFile(FileTest.Good.Test1.Path);

            res = engine.ReadString(data);

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


        [Test]
        public void ReadStringStatic()
        {
            string data = "11121314901234" + Environment.NewLine +
                          "10111314012345" + Environment.NewLine +
                          "11101314123456" + Environment.NewLine +
                          "10101314234567" + Environment.NewLine;

            SampleType[] res;
            res = CommonEngine.ReadString<SampleType>(data);

            Assert.AreEqual(4, res.Length);

            Assert.AreEqual(new DateTime(1314, 12, 11), res[0].Field1);
            Assert.AreEqual("901", res[0].Field2);
            Assert.AreEqual(234, res[0].Field3);

            Assert.AreEqual(new DateTime(1314, 11, 10), res[1].Field1);
            Assert.AreEqual("012", res[1].Field2);
            Assert.AreEqual(345, res[1].Field3);
        }


        [Test]
        public void ReadEmpty()
        {
            string data = "";

            var engine = new FileHelperEngine<SampleType>();

            SampleType[] res;
            res = engine.ReadStream(new StringReader(data));

            Assert.AreEqual(0, res.Length);
            Assert.AreEqual(0, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
        }

        [Test]
        public void ReadEmptyStream()
        {
            var engine = new FileHelperEngine<SampleType>();

            SampleType[] res;
            res = engine.ReadFile(TestCommon.GetPath("Good", "TestEmpty.txt"));

            Assert.AreEqual(0, res.Length);
            Assert.AreEqual(0, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
        }


        [Test]
        public void ReadFileAsDataTable()
        {
            var engine = new FileHelperEngine<SampleType>();

            DataTable res;
            res = engine.ReadFileAsDT(FileTest.Good.Test1.Path);

            Assert.AreEqual(4, res.Rows.Count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), res.Rows[0]["Field1"]);
            Assert.AreEqual("901", res.Rows[0]["Field2"]);
            Assert.AreEqual(234, res.Rows[0]["Field3"]);

            Assert.AreEqual(new DateTime(1314, 11, 10), res.Rows[1]["Field1"]);
            Assert.AreEqual("012", res.Rows[1]["Field2"]);
            Assert.AreEqual(345, res.Rows[1]["Field3"]);
        }
    }
}