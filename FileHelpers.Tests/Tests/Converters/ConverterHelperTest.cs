using FileHelpers.Converters;
using NFluent;
using NUnit.Framework;

namespace FileHelpers.Tests.Converters
{
    [TestFixture]
    public class ConverterHelperTests
    {
        [Test(Description = "String without spaces into RemoveBlanks")]
        public void RemoveNoBlanks()
        {
            Check.That(ConvertHelpers.RemoveBlanks("+41")).IsEqualTo("+41");
        }

        [Test(Description = "String with leading blanks into RemoveBlanks")]
        public void RemoveLeadingBlanks()
        {
            Check.That(ConvertHelpers.RemoveBlanks("     +41")).IsEqualTo("     +41");
        }

        [Test(Description = "String with blanks after sign logic into RemoveBlanks")]
        public void RemoveBlanksAfterSign()
        {
            Check.That(ConvertHelpers.RemoveBlanks("     + 41")).IsEqualTo("+41");
        }

        [Test(Description = "String with trailing blank into RemoveBlanks")]
        public void RemoveTrailingBlanks()
        {
            Check.That(ConvertHelpers.RemoveBlanks("     + 41")).IsEqualTo("+41");
        }
    }
}
