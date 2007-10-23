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
        private void AssertDelimitedFormat(string file, string delimiter, int fields, int confidence)
        {
            SmartFormatDetector detector = new SmartFormatDetector();
            RecordFormatInfo[] formats;

            detector.SampleLines = 10;
            formats = detector.DetectFileFormat(file);
            AssertFormat(formats, delimiter, fields, confidence);

            detector.SampleLines = 20;
            formats = detector.DetectFileFormat(file);
            AssertFormat(formats, delimiter, fields, confidence);

            detector.SampleLines = 50;
            formats = detector.DetectFileFormat(file);
            AssertFormat(formats, delimiter, fields, confidence);

            detector.SampleLines = 100;
            formats = detector.DetectFileFormat(file);
            AssertFormat(formats, delimiter, fields, confidence);
        }

        private void AssertFormat(RecordFormatInfo[] formats, string delimiter, int fields, int confidence)
        {
            Assert.AreEqual(1, formats.Length);
            
            if (confidence > 0)
                Assert.AreEqual(confidence, formats[0].Confidence);

            Assert.IsTrue(formats[0].ClassBuilder is DelimitedClassBuilder);
            Assert.AreEqual(delimiter, ((DelimitedClassBuilder)formats[0].ClassBuilder).Delimiter);
            Assert.AreEqual(fields, formats[0].ClassBuilder.FieldCount);
        }

        [Test]
        public void DelimitedTab()
        {
            string file = Common.TestPath(@"Detection\CustomersTab.txt");
            AssertDelimitedFormat(file, "\t", 7, 100);
        }


        [Test]
        public void DelimitedSemiColon()
        {
            string file = Common.TestPath(@"Detection\CustomersSemiColon.txt");
            AssertDelimitedFormat(file, ";", 7, 100);
        }

        [Test]
        public void DelimitedComma()
        {
            string file = Common.TestPath(@"Detection\CustomersComma.txt");
            AssertDelimitedFormat(file, ",", 7, 100);
        }

        [Test]
        public void DelimitedMedium()
        {
            string file = Common.TestPath(@"Detection\DelimitedMedium.txt");
            AssertDelimitedFormat(file, "\t", 41, -1);
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

    }

}
