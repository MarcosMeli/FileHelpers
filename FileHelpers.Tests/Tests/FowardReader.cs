using System;
using FileHelpers;
using FileHelpers.Detection;
using FileHelpers.RunTime;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;


namespace FileHelpers.Tests.Tests
{

    [TestFixture]
    public class ForwardReaderTests
    {
        [Test]
        public void BasicProperties()
        {
            var reader = new ForwardReader(new NewLineDelimitedRecordReader(new StreamReader(FileTest.Good.CustomersTab.Path)), 0, 0);
            reader.DiscardForward.AssertEqualTo(false);
            reader.FowardLines.AssertEqualTo(0);
            reader.LineNumber.AssertEqualTo(0);
                        
            reader.ReadNextLine();

            reader.LineNumber.AssertEqualTo(1);

        }

    }

}
