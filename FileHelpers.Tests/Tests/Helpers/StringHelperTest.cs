using System;
using FileHelpers.Helpers;
using NUnit.Framework;
using NFluent;

namespace FileHelpers.Tests
{
    /// <summary>
    /// FileHelpers Helpers StringHelper tests
    /// </summary>
    [TestFixture]
    public class StringHelperTest
    {
        [Test(Description = "String StartsWithIgnoringWhiteSpaces help method tests")]
        public void StartsWithIgnoringWhiteSpaces()
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