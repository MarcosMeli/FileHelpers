using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NFluent;

namespace FileHelpers.Tests
{
    /// <summary>
    /// FileHelpers Helpers  STringHelper tests
    /// </summary>
    [TestFixture]
    internal class StringHelperTest
    {
        [Test(Description = "String without spaces into RemoveBlanks")]
        public void RemoveNoBlanks()
        {
            Check.That(StringHelper.RemoveBlanks("+41")).IsEqualTo("+41");
        }

        [Test(Description = "String with leading blanks into RemoveBlanks")]
        public void RemoveLeadingBlanks()
        {
            Check.That(StringHelper.RemoveBlanks("     +41")).IsEqualTo("     +41");
        }

        [Test(Description = "String with blanks after sign logic into RemoveBlanks")]
        public void RemoveBlanksAfterSign()
        {
            Check.That(StringHelper.RemoveBlanks("     + 41")).IsEqualTo("+41");
        }

        [Test(Description = "String with trailing blank into RemoveBlanks")]
        public void RemoveTrailingBlanks()
        {
            Check.That(StringHelper.RemoveBlanks("     + 41")).IsEqualTo("+41");
        }

        [Test (Description = "String IsNullOrWhiteSpace help method tests")]
         public void IsNullOrWhiteSpace ()
        {
            Check.That(StringHelper.IsNullOrWhiteSpace ("     ")).IsEqualTo(true);
            Check.That(StringHelper.IsNullOrWhiteSpace (null)).IsEqualTo(true);
            Check.That(StringHelper.IsNullOrWhiteSpace ("")).IsEqualTo(true);
            Check.That(StringHelper.IsNullOrWhiteSpace (" test ")).IsEqualTo(false);
            Check.That(StringHelper.IsNullOrWhiteSpace("test")).IsEqualTo(false);
        }

        [Test (Description = "String StartsWithIgnoringWhiteSpaces help method tests")]
        public void StartsWithIgnoringWhiteSpaces ()
        {
            Check.That(StringHelper.StartsWithIgnoringWhiteSpaces("", "", StringComparison.Ordinal)).IsTrue();
            Check.That(StringHelper.StartsWithIgnoringWhiteSpaces("", "test", StringComparison.Ordinal)).IsFalse();
            Check.That(StringHelper.StartsWithIgnoringWhiteSpaces(" ", "test", StringComparison.Ordinal)).IsFalse();
            Check.That(StringHelper.StartsWithIgnoringWhiteSpaces("  test  ", "test", StringComparison.Ordinal)).IsTrue();
            Check.That(StringHelper.StartsWithIgnoringWhiteSpaces("  test", "test", StringComparison.Ordinal)).IsTrue();
            Check.That(StringHelper.StartsWithIgnoringWhiteSpaces("test string text", "test", StringComparison.Ordinal)).IsTrue();
            Check.That(StringHelper.StartsWithIgnoringWhiteSpaces(" test string text", "test", StringComparison.Ordinal)).IsTrue();
            Check.That(StringHelper.StartsWithIgnoringWhiteSpaces(" test string text", "hello", StringComparison.Ordinal)).IsFalse();
        }
    }
}