using NUnit.Framework;

namespace FileHelpers.Tests.Errors
{
    [TestFixture]
    public class BadDelimited
    {
        [Test]
        public void Reading_EmptyLine_Throws_FileHelpersException()
        {
            Assert.Throws<FileHelpersException>
                (() => FileTest.Bad.EmptyLine.ReadWithEngine<CustomersTab>(),
                    "Line: 1 Column: 0. The line 7 is empty. Maybe you need to use the attribute [IgnoreEmptyLines] in your record class.");
        }

        [Test]
        public void Reading_EmptyLineInTheMiddle_Throws_FileHelpersException()
        {
            Assert.Throws<FileHelpersException>
                (() => FileTest.Bad.EmptyLineInTheMiddle.ReadWithEngine<CustomersTab>(),
                    "Line: 4 Column: 0. The line 7 is empty. Maybe you need to use the attribute [IgnoreEmptyLines] in your record class.");
        }

        [Test]
        public void Reading_EmptyLineAtEnd_Throws_FileHelpersException()
        {
            Assert.Throws<FileHelpersException>
                (() => FileTest.Bad.EmptyLineAtEnd.ReadWithEngine<CustomersTab>(),
                    "Line: 7 Column: 0. The line 7 is empty. Maybe you need to use the attribute [IgnoreEmptyLines] in your record class.");
        }

        [Test]
        public void Reading_DelimiterNotFoundFirstField_Throws_FileHelpersException()
        {
            Assert.Throws<FileHelpersException>
                (() => FileTest.Bad.DelimiterNotFoundFirstField.ReadWithEngine<CustomersTab>(),
                    "Line: 1 Column: 0. Delimiter '	' not found after field 'CustomerID' (the record has less fields, the delimiter is wrong or the next field must be marked as optional).");
        }

        [Test]
        public void Reading_DelimiterNotFoundMiddleField_Throws_FileHelpersException()
        {
            Assert.Throws<FileHelpersException>
                (() => FileTest.Bad.DelimiterNotFoundMiddleField.ReadWithEngine<CustomersTab>(),
                    "Line: 1 Column: 74. Delimiter '	' not found after field 'City' (the record has less fields, the delimiter is wrong or the next field must be marked as optional).");
        }

        [Test]
        public void Reading_DelimiterNotFoundLastField_Throws_FileHelpersException()
        {
            Assert.Throws<FileHelpersException>
                (() => FileTest.Bad.DelimiterNotFoundLastField.ReadWithEngine<CustomersTab>(),
                    "Line: 1 Column: 74. Delimiter '	' not found after field 'City' (the record has less fields, the delimiter is wrong or the next field must be marked as optional).");
        }
    }
}