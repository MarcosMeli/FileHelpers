using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;


namespace FileHelpers.Tests
{

    [TestFixture]
    public class ForwardReaderTests
    {
        [Test(Description="Check if we read forward 0,0 it discards nothing")]
        public void BasicProperties()
        {
            var reader = new ForwardReader(new NewLineDelimitedRecordReader(new StreamReader(FileTest.Good.CustomersTab.Path)), 0, 0);
            reader.DiscardForward.AssertEqualTo(false);
            reader.FowardLines.AssertEqualTo(0);
            reader.LineNumber.AssertEqualTo(0);
                        
            reader.ReadNextLine();

            reader.LineNumber.AssertEqualTo(1);

        }

        //  TODO:   Add more tests for forward reader
    }
}
