using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers.Detection;
using FileHelpers.Dynamic;
using NUnit.Framework;


namespace FileHelpers.Tests.Detector
{
    [TestFixture]
    public class MultipleFiles
    {
        #region "  Asserts  "

		private void AssertDelimitedFormat(string[] file, string delimiter, int fields, int confidence, int numFormats, bool hasHeaders)
        {
            var detector = new SmartFormatDetector();
            RecordFormatInfo[] formats;

            detector.MaxSampleLines = 10;
            formats = detector.DetectFileFormat(file);
            AssertFormat(formats, delimiter, fields, confidence, numFormats, hasHeaders);

            detector.MaxSampleLines = 20;
            formats = detector.DetectFileFormat(file);
			AssertFormat(formats, delimiter, fields, confidence, numFormats, hasHeaders);

            detector.MaxSampleLines = 50;
            formats = detector.DetectFileFormat(file);
			AssertFormat(formats, delimiter, fields, confidence, numFormats, hasHeaders);

            detector.MaxSampleLines = 100;
            formats = detector.DetectFileFormat(file);
			AssertFormat(formats, delimiter, fields, confidence, numFormats, hasHeaders);
        }

        private void AssertFormat(RecordFormatInfo[] formats,
            string delimiter,
            int fields,
            int confidence,
			int numFormats, 
			bool hasHeaders)
        {
            if (numFormats > 0)
                Assert.AreEqual(numFormats, formats.Length);
            else
                Assert.IsTrue(formats.Length > 0);

            if (confidence > 0)
                Assert.IsTrue(formats[0].Confidence >= confidence);

            Assert.IsTrue(formats[0].ClassBuilder is DelimitedClassBuilder);
            Assert.AreEqual(delimiter, ((DelimitedClassBuilder) formats[0].ClassBuilder).Delimiter);
            Assert.AreEqual(fields, formats[0].ClassBuilder.FieldCount);

			Assert.AreEqual(hasHeaders ? 1 : 0, formats[0].ClassBuilder.IgnoreFirstLines);
        }

        #endregion

        [Test]
        public void DelimitedTab()
        {
			AssertDelimitedFormat(new[]{ TestCommon.GetPath("Detection","CustomersTab2.txt"),
				TestCommon.GetPath("Detection","CustomersTab.txt")}, "\t", 7, 100, 1, false);
        }

		/*
        [Test]
        public void DelimitedSemiColon()
        {
            AssertDelimitedFormat(FileTest.Detection.CustomersSemiColon.Path, ";", 7, 100, 1, false);
        }

        [Test]
        public void DelimitedComma()
        {
            AssertDelimitedFormat(FileTest.Detection.CustomersComma.Path, ",", 7, 100, 1, false);
        }

        [Test]
        public void DelimitedMedium()
        {
            AssertDelimitedFormat(FileTest.Detection.DelimitedMedium.Path, "\t", 41, -1, 1, false);
        }

        [Test]
        public void Cities()
        {
            AssertDelimitedFormat(FileTest.Detection.Cities.Path, ",", 5, 100, 0, true);

            AssertDelimitedFormat(FileTest.Detection.Cities2.Path, ",", 12, 100, 0, true);

            AssertDelimitedFormat(FileTest.Detection.Locations.Path, ",", 7, 90, 0, true);
        }

        [Test]
        public void TestDataUrlEtc()
        {
            AssertDelimitedFormat(FileTest.Detection.SampleData.Path, ",", 26, 100, 0, true);
        }

        [Test]
        public void Quoted()
        {
            AssertDelimitedFormat(FileTest.Detection.SuperQuoted.Path, ",", 11, 100, 0, true);
        }

        //[Test]
        //public void QuotedMore()
        //{
        //    AssertDelimitedFormat(FileTest.Detection.SuperQuoted2.Path, ",", 12, 90, 0);
        //}

*/
    }
}