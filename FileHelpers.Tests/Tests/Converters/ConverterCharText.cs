﻿using System;
using FileHelpers.Converters;
using NUnit.Framework;
using NFluent;

namespace FileHelpers.Tests.Converters
{
    [TestFixture]
    internal class ConverterCharText
    {
        [Test]
        public void UpperAndLowerTest()
        {
            var LowerText = "lowercase sample;another";
            var UpperText = "Upper sample;Another";

            var engine = new FileHelperEngine<LowerCharClass>();
            var res = engine.ReadString(UpperText);

            Check.That(res[0].fldChar).IsEqualTo('u');
            Check.That(res[0].fldChar2).IsEqualTo('a');

            var engine2 = new FileHelperEngine<UpperCharClass>();

            var res2 = engine2.ReadString(LowerText);
            Check.That(res2[0].fldChar).IsEqualTo('L');
            Check.That(res2[0].fldChar2).IsEqualTo('A');

            var engine3 = new FileHelperEngine<NoChangeCharClass>();
            var res4 = engine3.ReadString(LowerText);
            Check.That(res4[0].fldChar).IsEqualTo('l');

            res4 = engine3.ReadString(UpperText);
            Check.That(res4[0].fldChar).IsEqualTo('U');
        }

        [Test]
        public void CrashTest()
        {
            Assert.Throws<BadUsageException>(() => new FileHelperEngine<IAmBadCharClass>());
        }

        [DelimitedRecord(";")]
        private class NoChangeCharClass
        {
            public char fldChar;
            public char fldChar2;
        }

        [DelimitedRecord(";")]
        private class LowerCharClass
        {
            [CharConverter("x")]
            public char fldChar;

            [CharConverter("lower")]
            public char fldChar2;
        }

        [DelimitedRecord(";")]
        private class UpperCharClass
        {
            [CharConverter("X")]
            public char fldChar;

            [CharConverter("upper")]
            public char fldChar2;
        }

        [DelimitedRecord(";")]
        private class IAmBadCharClass
        {
            [CharConverter("Rubbish")]
            public char fldChar;
        }
    }
}