using System;
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
            Check.That(RecordOperations.StartsWithIgnoringWhiteSpaces("", "", StringComparison.Ordinal)).IsTrue();
            Check.That(RecordOperations.StartsWithIgnoringWhiteSpaces("", "test", StringComparison.Ordinal)).IsFalse();
            Check.That(RecordOperations.StartsWithIgnoringWhiteSpaces(" ", "test", StringComparison.Ordinal)).IsFalse();
            Check.That(RecordOperations.StartsWithIgnoringWhiteSpaces("  test  ", "test", StringComparison.Ordinal)).IsTrue();
            Check.That(RecordOperations.StartsWithIgnoringWhiteSpaces("  test", "test", StringComparison.Ordinal)).IsTrue();
            Check.That(RecordOperations.StartsWithIgnoringWhiteSpaces("test string text", "test", StringComparison.Ordinal)).IsTrue();
            Check.That(RecordOperations.StartsWithIgnoringWhiteSpaces(" test string text", "test", StringComparison.Ordinal)).IsTrue();
            Check.That(RecordOperations.StartsWithIgnoringWhiteSpaces(" test string text", "hello", StringComparison.Ordinal)).IsFalse();
        }
    }
}