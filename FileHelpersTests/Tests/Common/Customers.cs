using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class Customers
	{
		FileHelperEngine engine;

		private const int ExpectedRecords = 91;

		private void RunTests(string fileName, Type type)
		{
			engine = new FileHelperEngine(type);

			object[] res = TestCommon.ReadTest(engine, fileName);

			Assert.AreEqual(ExpectedRecords, res.Length);
		}

		[Test]
		public void Fixed()
		{
			RunTests(@"Good\CustomersFixed.txt", typeof (CustomersFixed));
		}

		[Test]
		public void Tab()
		{
			RunTests(@"Good\CustomersTab.txt", typeof (CustomersTab));
		}

		[Test]
		public void VerticalBar()
		{
			RunTests(@"Good\CustomersVerticalBar.txt", typeof (CustomersVerticalBar));
		}

		[Test]
		public void SemiColon()
		{
			RunTests(@"Good\CustomersSemiColon.txt", typeof (CustomersSemiColon));
		}

	}
}