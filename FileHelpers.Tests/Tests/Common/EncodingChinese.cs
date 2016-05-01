using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NFluent;

namespace FileHelpers.Tests
{
    [TestFixture]
    public class FileEncodingChinese
    {
        [Test]
        [Category("NotOnMono")]
        public void EncodingTest()
        {
            // var lines = File.ReadAllLines(FileTest.Good.EncodingChinese.Path, Encoding.GetEncoding(950));

            //var engine = new FileHelperEngine<ChineseRecord>(Encoding.GetEncoding(950));
            var engine = new FileHelperEngine<ChineseRecord>();
            var res = FileTest.Good.EncodingChinese.ReadWithEngine(engine);

            Check.That(res[0].rname).IsEqualTo("�]����                                                      ");
            Check.That(res[0].sname).IsEqualTo("�x�W�٤j�k�v���|�n�Ѫ���ƳB                                ");
            Check.That(res[1].rname).IsEqualTo("�ޤK��                                                      ");
        }


        [FixedLengthRecord(FixedMode.AllowVariableLength)]
        public class ChineseRecord
        {
            [FieldFixedLength(4)]
            public string sbrno; //4

            [FieldFixedLength(3)]
            public string brno; // 3

            [FieldFixedLength(6)]
            public string txday; //6

            [FieldFixedLength(7)]
            public string rembk; //7

            [FieldFixedLength(7)]
            public string trfbk; // 7

            [FieldFixedLength(10)]
            public int remamt; //n10

            [FieldFixedLength(5)]
            public int charge; //n5

            [FieldFixedLength(16)]
            public string actno; //16

            [FieldFixedLength(16)]
            public string mactno; //16 

            [FieldFixedLength(60)]
            public string rname; //60

            [FieldFixedLength(60)]
            public string sname; //60

            [FieldFixedLength(60)]
            public string cmemo; //60

            [FieldFixedLength(4)]
            [FieldOptional()]
            [FieldNullValue(typeof (int), "0")]
            public int recnt; // n4

            [FieldFixedLength(10)]
            [FieldOptional()]
            [FieldNullValue(typeof (int), "0")]
            public int txamt; //n10

            [FieldFixedLength(10)]
            [FieldOptional()]
            [FieldNullValue(typeof (string), "")]
            public string idno; // 10
        }


/*
 
 * SBRNO C(4),
 * BRNO C(3),
 * TXDAY C(6),
 * REMBK C(7),
 * TRFBK C(7),
 * REMAMT N(10),
 * CHARGE N(5),
 * ACTNO C(16),
 * MACTNO C(16),
 * RNAME C(60),
 * SNAME C(60),
 * CMEMO C(60),
 * RECNT N(4),
 * TXAMT N(10),
 * IDNO C(10))
 */
    }
}