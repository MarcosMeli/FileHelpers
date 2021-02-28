#if !NETCOREAPP
using FileHelpers.Detection;
using NUnit.Framework;


namespace FileHelpers.Tests
{
    [TestFixture]
    public class QuoteHelperTests
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
#endif
