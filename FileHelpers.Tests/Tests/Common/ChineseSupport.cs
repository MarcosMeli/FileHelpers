using System;
using System.Text;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class ChineseSupport
    {
        [Test]
        public void ReadString()
        {
            var engine = new FileHelperEngine<ChineseTest>();

            ChineseTest[] res;
            engine.Encoding = Encoding.Unicode;
            res = engine.ReadString(@"A123456789�x�_�����v�F�F��3��           20061008
A987654321�x�_�����v�F�F��5��           20061008");

            Assert.AreEqual(2, res.Length);
            Assert.AreEqual(2, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(2006, 10, 08), res[0].fecha);
            Assert.AreEqual("�x�_�����v�F�F��3��           ", res[0].chino);
            Assert.AreEqual("A123456789", res[0].id);
        }

        [Test]
        public void ReadFile()
        {
            var engine = new FileHelperEngine<ChineseTest>();

            ChineseTest[] res;
            //engine.Encoding = Encoding.Unicode;
            res = TestCommon.ReadTest<ChineseTest>(engine, "Good", "Chinese.txt");

            Assert.AreEqual(2, res.Length);
            Assert.AreEqual(2, engine.TotalRecords);
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(new DateTime(2006, 10, 08), res[0].fecha);
            Assert.AreEqual("�x�_�����v�F�F��3��           ", res[0].chino);
            Assert.AreEqual("A123456789", res[0].id);
        }

        [FixedLengthRecord(FixedMode.ExactLength)]
        private class ChineseTest
        {
            [FieldFixedLength(10)]
            public string id;

            [FieldFixedLength(30)]
            public string chino;

            [FieldFixedLength(8)]
            [FieldConverter(ConverterKind.Date, "yyyyMMdd")]
            public DateTime fecha;
        }
    }
}