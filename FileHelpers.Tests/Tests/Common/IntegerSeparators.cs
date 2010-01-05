using System;
using System.Collections;
using System.Data;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class IntegerSeparators
    {
        FileHelperEngine engine;

        string toTest = @"123|456|78910" + "\n" +
                        @"1,234|4,056|78,910" + "\n" +
                        @"0|1|2";

        string toTestMixed = @"123|456|78910" + "\n" +
                             @"1.234|4,056|78.910" + "\n" +
                             @"0|1|2";

        [Test]
        public void ReadFileDefault()
        {
            engine = new FileHelperEngine(typeof(SampleIntegerType));

            SampleIntegerType[] res = (SampleIntegerType[]) engine.ReadString(toTest);

            Assert.AreEqual(3, res.Length);
            Assert.AreEqual(3, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(123, res[0].Field1);
            Assert.AreEqual(456, res[0].Field2);
            Assert.AreEqual(78910, res[0].Field3);

            Assert.AreEqual(1234, res[1].Field1);
            Assert.AreEqual(4056, res[1].Field2);
            Assert.AreEqual(78910, res[1].Field3);

        }


        [Test]
        public void ReadFileDefined()
        {
            engine = new FileHelperEngine(typeof(SampleIntegerTypeDefined));

            SampleIntegerTypeDefined[] res = (SampleIntegerTypeDefined[])engine.ReadString(toTestMixed);

            Assert.AreEqual(3, res.Length);
            Assert.AreEqual(3, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(123, res[0].Field1);
            Assert.AreEqual(456, res[0].Field2);
            Assert.AreEqual(78910, res[0].Field3);

            Assert.AreEqual(1234, res[1].Field1);
            Assert.AreEqual(4056, res[1].Field2);
            Assert.AreEqual(78910, res[1].Field3);

        }


        [DelimitedRecord("|")]
        class SampleIntegerType
        {
                public int Field1;
                public short Field2;
                public long Field3;
        }


        [DelimitedRecord("|")]
        class SampleIntegerTypeDefined
        {
            [FieldConverter(ConverterKind.Int32, ",")]
            public int Field1;
            [FieldConverter(ConverterKind.Int16, ".")]
            public short Field2;
            [FieldConverter(ConverterKind.Int64, ",")]
            public long Field3;
        }

        [DelimitedRecord("|")]
        class SampleIntegerTypeBad
        {
            public int Field1;
            public short Field2;
            public long Field3;
        }

    }
}