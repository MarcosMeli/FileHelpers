using System;
using NUnit.Framework;

namespace FileHelpers.Tests.Converters
{
    [TestFixture]
    public class AllConverters
    {
        

        [Test]
        public void Test()
        {
            string sampleData =
                @"1;1;-1;-11;11;-111;111;-1111;1111;-1111.11;1111.11;111.111;31.12.2007 23:51:51;äaxx; eab37088-1785-4aba-b43c-04656487e68e
1;2;-2;-12;12;-112;112;-1112;1112;-1111.22;1111.22;222.222;31.12.2007 23:52:52;ÄBxx; 35b88d7a-f6e5-4d20-ac30-0802acad73dc
0;3;-3;-13;13;-113;113;-1113;1113;-1111.33;1111.33;333.333;31.12.2007 23:52:53;ßCxx; 075222d6-40d6-4bc8-a022-aea70c20331b
0;4;-4;-14;14;-114;114;-1114;1114;-1111.44;1111.44;444.444;31.12.2007 23:53:54;üdxx; 1be6bf2a-08d2-468b-a3aa-d7b88072e2b1
1;5;-5;-15;15;-115;115;-1115;1115;-1111.55;1111.55;555.555;31.12.2007 23:54:55;ÜExx; 15a8c3d0-cb42-4684-b573-c519af0c0b69";

            var engine = new FileHelperEngine<AllTypesClass>();
            AllTypesClass[] res = engine.ReadString(sampleData);

            Assert.AreEqual('ä', res[0].fldChar);
            Assert.AreEqual("eab37088-1785-4aba-b43c-04656487e68e", res[0].fldGuid.ToString());
            Assert.AreEqual("15a8c3d0-cb42-4684-b573-c519af0c0b69", res[4].fldGuid.ToString());
        }


        [DelimitedRecord(";")]
        private class AllTypesClass
        {
            [FieldConverter(ConverterKind.Boolean, "1", "0")]
            public bool fldBoolean;

            public byte fldByte;

            public sbyte fldSByte;

            public short fldInt16;

            public ushort fldUInt16;

            public int fldInt32;

            public uint fldUInt32;

            public long fldInt64;

            public ulong fldUInt64;

            public float fldSingle;

            public double fldDouble;

            public decimal fldDecimal;

            [FieldConverter(ConverterKind.Date, "dd.MM.yyyy HH:mm:ss")]
            public DateTime fldDateTime;

            public char fldChar;

            [FieldConverter(ConverterKind.Guid, "D")]
            public Guid fldGuid;
        }
    }
}