using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
    [TestFixture]
    public class DelimitedEngine
    {
        private const int ExpectedRecords = 91;

        private void RunTests<type>(string delimiter, params string[] pathElements) where type : class
        {
            var engine = new DelimitedFileEngine<type>();
            engine.Options.Delimiter = delimiter;

            var res = TestCommon.ReadTest<type>(engine, pathElements);


            Assert.AreEqual(ExpectedRecords, res.Length);
        }

        [Test]
        public void Tab()
        {
            RunTests<CustomersTab>("\t", "Good", "CustomersTab.txt");
        }

        [Test]
        public void VerticalBar()
        {
            RunTests<CustomersTab>("|", "Good", "CustomersVerticalBar.txt");
        }

        [Test]
        public void SemiColon()
        {
            RunTests<CustomersTab>(";", "Good", "CustomersSemiColon.txt");
        }


        [Test]
        public void BadRecordType1()
        {
            Assert.Throws<BadUsageException>(
                () => new DelimitedFileEngine(typeof (CustomersFixed)));
        }

        [Test]
        public void BadRecordType2()
        {
            Assert.Throws<BadUsageException>(
                () => new DelimitedFileEngine(null));
        }

        [Test]
        public void CheckSeparator()
        {
            var engSemiColon = new DelimitedFileEngine<CustomersSemiColon>();
            engSemiColon.Options.Delimiter.AssertEqualTo(";");

            var engTab = new DelimitedFileEngine<CustomersTab>();
            engTab.Options.Delimiter.AssertEqualTo("\t");
        }
    }
}