

using System;
using System.Data.OleDb;
using FileHelpers;
using FileHelpers.DataLink;
using FileHelpers.Detection;
using FileHelpers.RunTime;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text;

namespace FileHelpersTests.Tests.Detector
{

    [TestFixture]
    public class BasicDetection
    {
        [Test]
        public void DelimitedTab()
        {
            SmartFormatDetector detector = new SmartFormatDetector();
            FormatOption[] formats = detector.DetectFileFormat(Common.TestPath(@"Detection\CustomersTab.txt"));

            Assert.AreEqual(1, formats.Length);
            Assert.IsTrue(formats[0].ClassBuilder is DelimitedClassBuilder);
            Assert.AreEqual("\t", ((DelimitedClassBuilder)formats[0].ClassBuilder).Delimiter);
            Assert.AreEqual(6, formats[0].ClassBuilder.FieldCount);
        }

        [Test]
        public void DelimitedSemiColon()
        {
            SmartFormatDetector detector = new SmartFormatDetector();
            FormatOption[] formats = detector.DetectFileFormat(Common.TestPath(@"Detection\CustomersSemiColon.txt"));

            Assert.AreEqual(1, formats.Length);
            Assert.IsTrue(formats[0].ClassBuilder is DelimitedClassBuilder);
            Assert.AreEqual(";", ((DelimitedClassBuilder)formats[0].ClassBuilder).Delimiter);
            Assert.AreEqual(6, formats[0].ClassBuilder.FieldCount);
        }


        [Test]
        public void FixedLength()
        {
            SmartFormatDetector detector = new SmartFormatDetector();
            FormatOption[] formats = detector.DetectFileFormat(Common.TestPath(@"Detection\CustomersFixed.txt"));

            Assert.AreEqual(1, formats.Length);
            Assert.IsTrue(formats[0].ClassBuilder is FixedLengthClassBuilder);
            Assert.AreEqual(6, formats[0].ClassBuilder.FieldCount);            
        }

    }

}
