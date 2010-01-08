using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class DelimitedEngine
	{
		private const int ExpectedRecords = 91;

		private void RunTests(Type type, string delimiter, params string[] pathElements)
		{
            var engine = new DelimitedFileEngine(type);
            engine.Options.Delimiter = delimiter;

			object[] res = TestCommon.ReadTest(engine, pathElements);

            
			Assert.AreEqual(ExpectedRecords, res.Length);
		}

		[Test]
		public void Tab()
		{
			RunTests(typeof (CustomersTab), "\t", "Good", "CustomersTab.txt");
		}

		[Test]
		public void VerticalBar()
		{
            RunTests(typeof(CustomersTab), "|", "Good", "CustomersVerticalBar.txt");
		}

		[Test]
		public void SemiColon()
		{
            RunTests(typeof(CustomersTab), ";", "Good", "CustomersSemiColon.txt");
		}


        [Test]
        public void BadRecordType1()
        {
            Assert.Throws<BadUsageException>(
                () => new DelimitedFileEngine(typeof(CustomersFixed)));
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