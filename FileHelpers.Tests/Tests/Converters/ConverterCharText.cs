using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using FileHelpers;

namespace FileHelpers.Tests.Converters
{
    [TestFixture]
    class ConverterCharText
    {

        [Test]
        public void UpperAndLowerTest()
        {
            var LowerText = "lowercase sample;another";
            var UpperText = "Upper sample;Another";

            var engine = new FileHelperEngine<LowerCharClass>();
            var res = engine.ReadString(UpperText);

            res[0].fldChar.AssertEqualTo('u', "Should be lower case U from UpperText");
            res[0].fldChar2.AssertEqualTo('a', "Should be lower case a from UpperText");

            var engine2 = new FileHelperEngine<UpperCharClass>();

            var res2 = engine2.ReadString(LowerText);
            res2[0].fldChar.AssertEqualTo('L', "Should be upper case L from LowerText");
            res2[0].fldChar2.AssertEqualTo('A', "Should be upper case A from LowerText");

            var engine3 = new FileHelperEngine<NoChangeCharClass>();
            var res4 = engine3.ReadString(LowerText);
            res4[0].fldChar.AssertEqualTo('l', "Should be lower case L from LowerText");

            res4 = engine3.ReadString(UpperText);
            res4[0].fldChar.AssertEqualTo('U', "Should be Uppper case U from UpperText");
        }

        [Test]
        public void CrashTest()
        {
            Assert.Throws<BadUsageException>(() => new FileHelperEngine<IAmBadCharClass>());
        }

        [DelimitedRecord(";")]
        private class NoChangeCharClass
        {

            public System.Char fldChar;
            public System.Char fldChar2;
        }

        [DelimitedRecord(";")]
        private class LowerCharClass
        {

            [FieldConverter(ConverterKind.Char, "x")]
            public System.Char fldChar;

            [FieldConverter(ConverterKind.Char, "lower")]
            public System.Char fldChar2;
        }

        [DelimitedRecord(";")]
        private class UpperCharClass
        {

            [FieldConverter(ConverterKind.Char, "X")]
            public System.Char fldChar;

            [FieldConverter(ConverterKind.Char, "upper")]
            public System.Char fldChar2;
        }

        [DelimitedRecord(";")]
        private class IAmBadCharClass
        {

            [FieldConverter(ConverterKind.Char, "Rubbish")]
            public System.Char fldChar;
        }
    }
}
