using System;
using FileHelpers;
using FileHelpers.Detection;
using FileHelpers.Dynamic;
using NUnit.Framework;
using System.Collections.Generic;


namespace FileHelpers.Tests.Tests
{

    [TestFixture]
    public class Helpers
    {
        [Test]
        public void QuotedMore()
        {
            var delimiter = '\t';
            var quotedChar = '"';

            var line = "asdf\t\t\t\tasd\t";
            Assert.AreEqual(5, QuoteHelper.CountNumberOfDelimiters(line, delimiter, quotedChar));
            
            line = "\t\t\t\tasd\t";
            Assert.AreEqual(5, QuoteHelper.CountNumberOfDelimiters(line, delimiter, quotedChar));

            line = "\t\t\t\tasd\tasd";
            Assert.AreEqual(5, QuoteHelper.CountNumberOfDelimiters(line, delimiter, quotedChar));

            line = "asd\t\t\t\tasd\tasd";
            Assert.AreEqual(5, QuoteHelper.CountNumberOfDelimiters(line, delimiter, quotedChar));

        }

    }

}
