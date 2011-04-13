using System;
using FileHelpers;
using FileHelpers.Detection;
using FileHelpers.Dynamic;
using NUnit.Framework;
using System.Collections.Generic;


namespace FileHelpers.Tests.Detector
{

    [TestFixture]
    public class BasicDetection
    {
        #region "  Asserts  "

        private void AssertDelimitedFormat(string file, string delimiter, int fields, int confidence, int numFormats)
        {
            var detector = new SmartFormatDetector();
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

        #endregion

        [Test]
        public void DelimitedTab()
        {

            AssertDelimitedFormat(FileTest.Detection.CustomersTab.Path, "\t", 7, 100, 1);
        }


        [Test]
        public void DelimitedSemiColon()
        {
            AssertDelimitedFormat(FileTest.Detection.CustomersSemiColon.Path, ";", 7, 100, 1);
        }

        [Test]
        public void DelimitedComma()
        {
            AssertDelimitedFormat(FileTest.Detection.CustomersComma.Path, ",", 7, 100, 1);
        }

        [Test]
        public void DelimitedMedium()
        {
            AssertDelimitedFormat(FileTest.Detection.DelimitedMedium.Path, "\t", 41, -1, 1);
        }

        [Test]
        public void FixedLength()
        {
            var detector = new SmartFormatDetector();
            var formats = detector.DetectFileFormat(FileTest.Detection.CustomersFixed.Path);

            Assert.AreEqual(1, formats.Length);
            Assert.IsTrue(formats[0].ClassBuilder is FixedLengthClassBuilder);
            Assert.AreEqual(100, formats[0].Confidence);
            Assert.AreEqual(7, formats[0].ClassBuilder.FieldCount);


            var builder = formats[0].ClassBuilder as FixedLengthClassBuilder;
            Assert.AreEqual(11, builder.Fields[0].FieldLength);
            Assert.AreEqual(38, builder.Fields[1].FieldLength);
            Assert.AreEqual(22, builder.Fields[2].FieldLength);
            Assert.AreEqual(38, builder.Fields[3].FieldLength);
            Assert.AreEqual(41, builder.Fields[4].FieldLength);
            Assert.AreEqual(18, builder.Fields[5].FieldLength);
            Assert.AreEqual(15, builder.Fields[6].FieldLength);

        }


        [Test]
        public void OneColumnFixed()
        {
            var detector = new SmartFormatDetector();
            var formats = detector.DetectFileFormat(FileTest.Detection.OnColumnFixed.Path);

            Assert.AreEqual(1, formats.Length);
            Assert.IsTrue(formats[0].ClassBuilder is FixedLengthClassBuilder);
            Assert.AreEqual(100, formats[0].Confidence);
            Assert.AreEqual(1, formats[0].ClassBuilder.FieldCount);


            var builder = (FixedLengthClassBuilder) formats[0].ClassBuilder;
            Assert.AreEqual(10, builder.Fields[0].FieldLength);
        }

        [Test]
        public void OneColumnNonFixed()
        {
            var detector = new SmartFormatDetector();
            RecordFormatInfo[] formats = detector.DetectFileFormat(FileTest.Detection.OnColumnNonFixed.Path);

            Assert.AreEqual(0, formats.Length);
        }


        [Test]
        public void Cities()
        {
            AssertDelimitedFormat(FileTest.Detection.Cities.Path, ",", 5, 100, 0);

            AssertDelimitedFormat(FileTest.Detection.Cities2.Path, ",", 12, 100, 0);

            AssertDelimitedFormat(FileTest.Detection.Locations.Path, ",", 7, 90, 0);

        }

        [Test]
        public void TestDataUrlEtc()
        {
            AssertDelimitedFormat(FileTest.Detection.SampleData.Path, ",", 26, 100, 0);

        }

        [Test]
        public void Quoted()
        {
            AssertDelimitedFormat(FileTest.Detection.SuperQuoted.Path, ",", 11, 100, 0);

        }

        //[Test]
        //public void QuotedMore()
        //{
        //    AssertDelimitedFormat(FileTest.Detection.SuperQuoted2.Path, ",", 12, 90, 0);
        //}

    }

}
