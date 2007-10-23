

using System;
using System.Data.OleDb;
using FileHelpers;
using FileHelpers.DataLink;
using FileHelpers.Detection;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;

namespace FileHelpersTests.Tests.Detector
{

    [TestFixture]
    public class BasicDetection
    {
        [Test]
        public void CsvFile()
        {
            SmartFormatDetector detector = new SmartFormatDetector();
            detector.FormatHint = FormatHint.Delimited;
            FormatOption[] formats = detector.DetectFileFormat(Common.TestPath(@"Detection\CustomersTab.txt"));

            Assert.AreEqual(1, formats.Length);
        }
    }

}
