using System;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    [TestFixture]
    public class EncodingAdvanced
    {
        [Test]
        public void GetMSWSReportsFromFile_20060709_28Records()
        {
            var res = FileTest.Good.EncodingAdv1.ReadWithEngine<MSWSDailyReportRecord>();

            Assert.AreEqual(res.Length, 28);
        }

        [Test]
        public void GetMSWSReportsFromFile_20060720_32Records()
        {
            var res = FileTest.Good.EncodingAdv2.ReadWithEngine<MSWSDailyReportRecord>();

            Assert.AreEqual(res.Length, 32);
        }
    }

    [FixedLengthRecord]
    [IgnoreFirst(7)]
    [IgnoreLast(5)]
    public sealed class MSWSDailyReportRecord
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