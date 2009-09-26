using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class Orders
	{
		FileHelperEngine engine;

		private const int ExpectedRecords = 830;

        private void RunTests(Type type, params string[] pathElements)
		{
			engine = new FileHelperEngine(type);

			object[] res = TestCommon.ReadTest(engine, pathElements);

			Assert.AreEqual(ExpectedRecords, res.Length);
		}

		[Test]
		public void Fixed()
		{
			RunTests(typeof (OrdersFixed), "Good", "OrdersFixed.txt");
		}

		[Test]
		public void Tab()
		{
			RunTests(typeof (OrdersTab), "Good", "OrdersTab.txt");
		}

		[Test]
		public void VerticalBar()
		{
			RunTests(typeof (OrdersVerticalBar), "Good", "OrdersVerticalBar.txt");
		}

		[Test]
		public void SemiColon()
		{
			RunTests(typeof (OrdersSemiColon), "Good", "OrdersSemiColon.txt");
		}

	}
}