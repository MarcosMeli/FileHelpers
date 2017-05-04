using System.Reflection;
using Moq;
using NUnit.Framework;

namespace FileHelpers.Tests.FileHelpers
{
    [TestFixture]
    public class DelimitedFieldTests
    {
        [Test]
        public void GivenModeIsAlwaysQuotedWhenLineInfoIsNotQuotedThenAnExceptionIsThrown() {
            var fi = new Mock<FieldInfo>();
            fi.Setup(f => f.FieldType).Returns(typeof (string));
            var fieldUnderTest = new DelimitedField(fi.Object, ";") {
                QuoteChar = '%',
                QuoteMode = QuoteMode.AlwaysQuoted
            };
            var invalidLineInfo = new LineInfo("Hello, World") {
                mReader = new ForwardReader(new Mock<IRecordReader>().Object, 0),
            };

            Assert.Throws<BadUsageException>(() => fieldUnderTest.ExtractFieldString(invalidLineInfo));
        } 
    }
}