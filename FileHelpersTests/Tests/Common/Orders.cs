using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class Orders
	{
		FileHelperEngine engine;

		private const int ExpectedRecords = 830;

		private void RunTests(string fileName, Type type)
		{
			engine = new FileHelperEngine(type);

			object[] res = TestCommon.ReadTest(engine, fileName);

			Assert.AreEqual(ExpectedRecords, res.Length);
		}

		[Test]
		public void Fixed()
		{
			RunTests(@"Good\OrdersFixed.txt", typeof (OrdersFixed));
		}

		[Test]
		public void Tab()
		{
			RunTests(@"Good\OrdersTab.txt", typeof (OrdersTab));
		}

		[Test]
		public void VerticalBar()
		{
			RunTests(@"Good\OrdersVerticalBar.txt", typeof (OrdersVerticalBar));
		}

		[Test]
		public void SemiColon()
		{
			RunTests(@"Good\OrdersSemiColon.txt", typeof (OrdersSemiColon));
		}

	}
}