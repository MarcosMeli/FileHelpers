using System;
using System.Text;
using NUnit.Framework;
using FileHelpers;
using System.Net;
using System.IO;

namespace FileHelpers.Tests.CommonTests
{

    [TestFixture]
    public class FileEncodingAdvanced
    {

        [Test]
        public void EncodingAdvanced1()
        {
            EncodingRecord[] res = null;
            FileHelperEngine engine = new FileHelperEngine(typeof(EncodingRecord));
            res = (EncodingRecord[])TestCommon.ReadTest(engine, "Good", "EncodingAdv1.txt");

            Assert.AreEqual(res.Length, 28);
        }

        [Test]
        public void EncodingAdvanced2()
        {
            EncodingRecord[] res = null;
            FileHelperEngine engine = new FileHelperEngine(typeof(EncodingRecord));
            res = (EncodingRecord[])TestCommon.ReadTest(engine, "Good", "EncodingAdv2.txt");

            Assert.AreEqual(res.Length, 32);
        }

        [Test]
        public void EncodingAdvanced3()
        {
            var engine = new FileHelperEngine(typeof(EncodingRecord));
            byte[] data = File.ReadAllBytes(FileTest.Good.EncodingAdv3.Path);
            var encoding = new System.Text.ASCIIEncoding();
            string dataString = encoding.GetString(data);
            var res = (EncodingRecord[])engine.ReadString(dataString);

            Assert.AreEqual(res.Length, 18);
        }

        [Test]
        public void EncodingAdvanced4()
        {
            var engine = new FileHelperEngine(typeof(EncodingRecord));

            var res = (EncodingRecord[])engine.ReadFile(FileTest.Good.EncodingAdv3.Path);

            Assert.AreEqual(res.Length, 18);
        }

        [Test]
        public void EncodingAdvanced5()
        {
            var engine = new FileHelperEngine(typeof(EncodingRecord));

            var encode = Encoding.GetEncoding("utf-8");
            var reader = new StreamReader(FileTest.Good.EncodingAdv3.Path, encode);
            var res = (EncodingRecord[])engine.ReadStream(reader);

            Assert.AreEqual(res.Length, 18);
        }


        [FixedLengthRecordAttribute()]
        [IgnoreFirst(7)]
        [IgnoreLast(5)]
        public sealed class EncodingRecord
        {
            [FieldFixedLength(26)]
            public String Location;
            [FieldFixedLength(12)]
            public String County;
            [FieldFixedLength(5)]
            public int Elev;
            [FieldFixedLength(4)]
            public int Hi;
            [FieldFixedLength(4)]
            public int Lo;

            [FieldFixedLength(6)]
            [FieldTrim(TrimMode.Both)]
            public string PCPN;
            [FieldFixedLength(5)]
            public decimal SNOW;
            [FieldFixedLength(5)]
            public decimal SNDPT;
            [FieldFixedLength(6)]
            public decimal MONTH;

        }

    }
}