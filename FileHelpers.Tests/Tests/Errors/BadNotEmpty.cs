using FileHelpers.Tests.CommonTests;
using NUnit.Framework;

namespace FileHelpers.Tests.Errors
{
    [TestFixture]
    public class BadNotEmpty
    {
        [Test]
        public void FieldNotEmpty1()
        {
            Assert.Throws<ConvertException>(() => FileTest.Bad.FieldNotEmpty1.ReadWithEngine<NotEmptyType>());
        }

        [Test]
        public void FieldNotEmpty2()
        {
            Assert.Throws<ConvertException>(() => FileTest.Bad.FieldNotEmpty2.ReadWithEngine<NotEmptyType>());
        }
    }
}