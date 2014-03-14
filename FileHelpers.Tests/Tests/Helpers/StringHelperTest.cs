using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

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
            StringHelper.RemoveBlanks("+41").AssertEqualTo("+41");
        }

        [Test(Description = "String with leading blanks into RemoveBlanks")]
        public void RemoveLeadingBlanks()
        {
            StringHelper.RemoveBlanks("     +41").AssertEqualTo("     +41");
        }

        [Test(Description = "String with blanks after sign logic into RemoveBlanks")]
        public void RemoveBlanksAfterSign()
        {
            StringHelper.RemoveBlanks("     + 41").AssertEqualTo("+41");
        }

        [Test(Description = "String with trailing blank into RemoveBlanks")]
        public void RemoveTrailingBlanks()
        {
            StringHelper.RemoveBlanks("     + 41").AssertEqualTo("+41");
        }

        [Test (Description = "String IsNullOrWhiteSpace help method tests")]
         public void IsNullOrWhiteSpace ()
        {
            StringHelper.IsNullOrWhiteSpace ("     ").AssertEqualTo (true, "WhiteSpaces not detected");
            StringHelper.IsNullOrWhiteSpace (null).AssertEqualTo (true, "null string not detected");
            StringHelper.IsNullOrWhiteSpace (String.Empty).AssertEqualTo (true, "empty string not detected");
            StringHelper.IsNullOrWhiteSpace (" test ").AssertEqualTo (false, "valid string not detected");
            StringHelper.IsNullOrWhiteSpace ("test").AssertEqualTo (false, "valid string not detected");
        }

        [Test (Description = "String StartsWithIgnoringWhiteSpaces help method tests")]
        public void StartsWithIgnoringWhiteSpaces ()
        {
            StringHelper.StartsWithIgnoringWhiteSpaces (String.Empty, String.Empty, StringComparison.Ordinal).AssertEqualTo (true, "Empty string not detected: test 1");
            StringHelper.StartsWithIgnoringWhiteSpaces ("", "test", StringComparison.Ordinal).AssertEqualTo (false, "Wrong string detection: test 2");
            StringHelper.StartsWithIgnoringWhiteSpaces (" ", "test", StringComparison.Ordinal).AssertEqualTo (false, "Wrong string detection: test 3");
            StringHelper.StartsWithIgnoringWhiteSpaces ("  test  ", "test", StringComparison.Ordinal).AssertEqualTo (true, "Wrong string detection: test 4");
            StringHelper.StartsWithIgnoringWhiteSpaces ("  test", "test", StringComparison.Ordinal).AssertEqualTo (true, "Wrong string detection: test 5");
            StringHelper.StartsWithIgnoringWhiteSpaces ("test string text", "test", StringComparison.Ordinal).AssertEqualTo (true, "Wrong string detection: test 6");
            StringHelper.StartsWithIgnoringWhiteSpaces (" test string text", "test", StringComparison.Ordinal).AssertEqualTo (true, "Wrong string detection: test 7");
            StringHelper.StartsWithIgnoringWhiteSpaces (" test string text", "hello", StringComparison.Ordinal).AssertEqualTo (false, "Wrong string detection: test 8");
        }
    }
}