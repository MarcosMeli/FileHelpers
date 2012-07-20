using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class Orders
	{

		private const int ExpectedRecords = 830;

        private void RunTests<type>( params string[] pathElements) where type : class
		{
			var engine = new FileHelperEngine<type>();

			var res = TestCommon.ReadTest<type>(engine, pathElements);

			Assert.AreEqual(ExpectedRecords, res.Length);
		}

		[Test]
		public void Fixed()
		{
			RunTests<OrdersFixed>( "Good", "OrdersFixed.txt");
		}

		[Test]
		public void Tab()
		{
			RunTests<OrdersTab>( "Good", "OrdersTab.txt");
		}

		[Test]
		public void VerticalBar()
		{
			RunTests<OrdersVerticalBar>( "Good", "OrdersVerticalBar.txt");
		}

		[Test]
		public void SemiColon()
		{
			RunTests<OrdersSemiColon>( "Good", "OrdersSemiColon.txt");
		}

	}
}