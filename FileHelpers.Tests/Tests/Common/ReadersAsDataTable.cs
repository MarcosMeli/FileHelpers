using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Data;


namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class ReadersAsDataTable
    {
        [Test]
        public void ReadFile()
        {
            var engine = new FileHelperEngine<SampleType>();
            var records = engine.ReadFile(FileTest.Good.Test1.Path);

            var dt = records.ToDataTable<SampleType>();

            Assert.AreEqual(4, dt.Rows.Count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), (DateTime)dt.Rows[0]["Field1"]);
            Assert.AreEqual("901", (string)dt.Rows[0]["Field2"]);
            Assert.AreEqual(234, (int)dt.Rows[0]["Field3"]);

            Assert.AreEqual(new DateTime(1314, 11, 10), (DateTime)dt.Rows[1]["Field1"]);
            Assert.AreEqual("012", (string)dt.Rows[1]["Field2"]);
            Assert.AreEqual(345, dt.Rows[1]["Field3"]);
        }

        [Test]
        public void ReadNullableTypes()
        {
            var engine = new FileHelperEngine<NullableType>();
            var records = engine.ReadFile(FileTest.Good.NullableTypes1.Path);

            var res = records.ToDataTable<NullableType>();

            Assert.AreEqual(4, res.Rows.Count);
            Assert.AreEqual(4, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(1314, 12, 11), (DateTime) res.Rows[0]["Field1"]);
            Assert.AreEqual("901", (string) res.Rows[0]["Field2"]);
            Assert.AreEqual(234, (int) res.Rows[0]["Field3"]);

            Assert.AreEqual(DBNull.Value, res.Rows[1]["Field1"]);
            Assert.AreEqual("012", (string) res.Rows[1]["Field2"]);
            Assert.AreEqual(345, (int) res.Rows[1]["Field3"]);


            Assert.AreNotEqual(DBNull.Value, res.Rows[2]["Field1"]);
        }

        [FixedLengthRecord]
        public class NullableType
        {
            [FieldFixedLength(8)]
            [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
            public DateTime? Field1;

            [FieldFixedLength(3)]
            [FieldTrim(TrimMode.Both)]
            public string Field2;

            [FieldFixedLength(3)]
            public int? Field3;
        }
    }
}