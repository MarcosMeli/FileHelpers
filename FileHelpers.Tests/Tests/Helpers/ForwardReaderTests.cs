using System.IO;
using NUnit.Framework;
using NFluent;


namespace FileHelpers.Tests
{
    [TestFixture]
    public class ForwardReaderTests
    {
        [Test(Description = "Check if we read forward 0,0 it discards nothing")]
        public void BasicProperties()
        {
            var reader =
                new ForwardReader(new NewLineDelimitedRecordReader(new StreamReader(FileTest.Good.CustomersTab.Path)),
                    0,
                    0);
            Check.That(reader.DiscardForward).IsEqualTo(false);
            Check.That(reader.FowardLines).IsEqualTo(0);
            Check.That(reader.LineNumber).IsEqualTo(0);

            reader.ReadNextLine();

            Check.That(reader.LineNumber).IsEqualTo(1);
        }

        //  TODO:   Add more tests for forward reader
    }
}