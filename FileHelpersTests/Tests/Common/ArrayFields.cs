using System;
using System.Collections;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class ArrayFields
	{
		FileHelperEngine engine;

		[Test]
		[Ignore("Not Implemented yet")]
		public void DiffCustomers()
		{
			engine = new FileHelperEngine(typeof (ArrayType1));
			ArrayType1[] res = engine.ReadFile(Common.TestPath(@"good\ArrayFields.txt")) as ArrayType1[];
			Assert.AreEqual(2, res.Length);
		}



		[DelimitedRecord("|")]
		private class ArrayType1
		{
			public string CustomerID;

			public int[] BuyedArts;

		}


	}
}