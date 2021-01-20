#if !NETCOREAPP
using FileHelpers.Dynamic;
using NUnit.Framework;
using NFluent;


namespace FileHelpers.Tests
{
    [TestFixture]
    public class MessagesTests
    {
        [Test]
        public void MessageBasic()
        {
            var final =
                @"The field: FieldForTest must be marked as optional because the previous field is marked with FieldOptional. (Try adding [FieldOptional] to FieldForTest)";

            Check.That(Messages.Errors.FieldOptional
                .Field("FieldForTest")
                .Text
                ).IsEqualTo(final);

            Check.That(Messages.Errors.FieldOptional
                .Field("FieldForTest")
                .ToString()
                ).IsEqualTo(final);
        }


        [Test]
        public void MessageForExeptions()
        {
            try {
                new DelimitedClassBuilder("", "\t");
                Assert.Fail("No exception :(");
            }
            catch (FileHelpersException ex) {
                Check.That(ex.Message).IsEqualTo("The string '' not is a valid .NET identifier");
            }
        }

        [Test]
        public void Quotes()
        {
            Check.That(Messages.Errors.TestQuote
                .Text
                ).IsEqualTo("The Message class also allows to use \" in any part of the \" text \" .");
        }
    }
}
#endif
