using System;
using FileHelpers;
using FileHelpers.Detection;
using FileHelpers.RunTime;
using NUnit.Framework;
using System.Collections.Generic;


namespace FileHelpersTests.Tests.Detector
{

    [TestFixture]
    public class BasicDetection
    {
        private void AssertDelimitedFormat(string file, string delimiter, int fields, int confidence, int numFormats)
        {
            file = Common.TestPath(@"Detection\" + file);

            SmartFormatDetector detector = new SmartFormatDetector();
            RecordFormatInfo[] formats;

            detector.MaxSampleLines = 10;
            formats = detector.DetectFileFormat(file);
            AssertFormat(formats, delimiter, fields, confidence, numFormats);

            detector.MaxSampleLines = 20;
            formats = detector.DetectFileFormat(file);
            AssertFormat(formats, delimiter, fields, confidence, numFormats);

            detector.MaxSampleLines = 50;
            formats = detector.DetectFileFormat(file);
            AssertFormat(formats, delimiter, fields, confidence, numFormats);

            detector.MaxSampleLines = 100;
            formats = detector.DetectFileFormat(file);
            AssertFormat(formats, delimiter, fields, confidence, numFormats);
        }

        private void AssertFormat(RecordFormatInfo[] formats, string delimiter, int fields, int confidence, int numFormats)
        {
            
            if (numFormats > 0)
                Assert.AreEqual(numFormats, formats.Length);
            else
                Assert.IsTrue(formats.Length > 0);
            
            if (confidence > 0)
                Assert.IsTrue(formats[0].Confidence >= confidence);

            Assert.IsTrue(formats[0].ClassBuilder is DelimitedClassBuilder);
            Assert.AreEqual(delimiter, ((DelimitedClassBuilder)formats[0].ClassBuilder).Delimiter);
            Assert.AreEqual(fields, formats[0].ClassBuilder.FieldCount);
        }

        [Test]
        public void DelimitedTab()
        {
            string file = "CustomersTab.txt";
            AssertDelimitedFormat(file, "\t", 7, 100, 1);
        }


        [Test]
        public void DelimitedSemiColon()
        {
            string file = "CustomersSemiColon.txt";
            AssertDelimitedFormat(file, ";", 7, 100, 1);
        }

        [Test]
        public void DelimitedComma()
        {
            string file = "CustomersComma.txt";
            AssertDelimitedFormat(file, ",", 7, 100, 1);
        }

        [Test]
        public void DelimitedMedium()
        {
            string file = "DelimitedMedium.txt";
            AssertDelimitedFormat(file, "\t", 41, -1, 1);
        }

        [Test]
        public void FixedLength()
        {
            SmartFormatDetector detector = new SmartFormatDetector();
            RecordFormatInfo[] formats = detector.DetectFileFormat(Common.TestPath(@"Detection\CustomersFixed.txt"));

            Assert.AreEqual(1, formats.Length);
            Assert.IsTrue(formats[0].ClassBuilder is FixedLengthClassBuilder);
            Assert.AreEqual(100, formats[0].Confidence);
            Assert.AreEqual(7, formats[0].ClassBuilder.FieldCount);            
        }

        [Test]
        public void Cities()
        {
            string file;
            file = "Cities.txt";
            AssertDelimitedFormat(file, ",", 5, 100, 0);

            file = "Cities2.txt";
            AssertDelimitedFormat(file, ",", 12, 100, 0);

            file = "Locations.txt";
            AssertDelimitedFormat(file, ",", 7, 90, 0);

        }

        [Test]
        public void TestDataUrlEtc()
        {
            string file;
            file = "SampleData.txt";
            AssertDelimitedFormat(file, ",", 26, 100, 0);

        }

        [Test]
        public void Quoted()
        {
            string file;
            file = "SuperQuoted.txt";
            AssertDelimitedFormat(file, ",", 11, 100, 0);

        }

        [Test]
        public void QuotedMore()
        {
            string file;
            file = "SuperQuoted2.txt";
            AssertDelimitedFormat(file, ",", 12, 90, 0);

        }

    }

}
