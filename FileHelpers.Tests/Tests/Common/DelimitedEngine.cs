using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class DelimitedEngine
	{
		private const int ExpectedRecords = 91;

		private void RunTests(string fileName, Type type, string delimiter)
		{
            DelimitedFileEngine engine = new DelimitedFileEngine(type);
            engine.Options.Delimiter = delimiter;

			object[] res = TestCommon.ReadTest(engine, fileName);

            
			Assert.AreEqual(ExpectedRecords, res.Length);
		}

		[Test]
		public void Tab()
		{
			RunTests(@"Good\CustomersTab.txt", typeof (CustomersTab), "\t");
		}

		[Test]
		public void VerticalBar()
		{
            RunTests(@"Good\CustomersVerticalBar.txt", typeof(CustomersTab), "|");
		}

		[Test]
		public void SemiColon()
		{
            RunTests(@"Good\CustomersSemiColon.txt", typeof(CustomersTab), ";");
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

	}
}