using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class Customers
	{
		FileHelperEngine engine;

		private const int ExpectedRecords = 91;

        private void RunTests(Type type, params string[] pathElements)
		{
			engine = new FileHelperEngine(type);

			object[] res = TestCommon.ReadTest(engine, pathElements);

			Assert.AreEqual(ExpectedRecords, res.Length);
		}

		[Test]
		public void Fixed()
		{
			RunTests(typeof (CustomersFixed), "Good", "CustomersFixed.txt");
		}

		[Test]
		public void Tab()
		{
			RunTests(typeof (CustomersTab), "Good", "CustomersTab.txt");
		}

		[Test]
		public void VerticalBar()
		{
			RunTests(typeof (CustomersVerticalBar), "Good", "CustomersVerticalBar.txt");
		}

		[Test]
		public void SemiColon()
		{
			RunTests(typeof (CustomersSemiColon), "Good", "CustomersSemiColon.txt");
		}

	}
}