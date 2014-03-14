using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class IntegerSeparators
    {
        private string toTest = @"123|456|78910" + "\n" +
                                @"1,234|4,056|78,910" + "\n" +
                                @"0|1|2";

        private string toTestMixed = @"123|456|78910" + "\n" +
                                     @"1.234|4,056|78.910" + "\n" +
                                     @"0|1|2";

        [Test]
        public void ReadFileDefault()
        {
            var engine = new FileHelperEngine<SampleIntegerType>();

            SampleIntegerType[] res = engine.ReadString(toTest);

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
            var engine = new FileHelperEngine<SampleIntegerTypeDefined>();

            var res = (SampleIntegerTypeDefined[]) engine.ReadString(toTestMixed);

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
        private class SampleIntegerType
        {
            public int Field1;
            public short Field2;
            public long Field3;
        }


        [DelimitedRecord("|")]
        private class SampleIntegerTypeDefined
        {
            [FieldConverter(ConverterKind.Int32, ",")]
            public int Field1;

            [FieldConverter(ConverterKind.Int16, ".")]
            public short Field2;

            [FieldConverter(ConverterKind.Int64, ",")]
            public long Field3;
        }

        [DelimitedRecord("|")]
        private class SampleIntegerTypeBad
        {
            public int Field1;
            public short Field2;
            public long Field3;
        }
    }
}