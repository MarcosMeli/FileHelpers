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
		//[Ignore("Not Implemented yet")]
		public void ArrayFields1()
		{
			engine = new FileHelperEngine(typeof (ArrayType1));
			ArrayType1[] res = engine.ReadFile(Common.TestPath(@"good\ArrayFields.txt")) as ArrayType1[];
			Assert.AreEqual(2, res.Length);
		}



		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void ArrayFieldsBad1()
		{
			engine = new FileHelperEngine(typeof (ArrayTypeBad1));
		}


		[FixedLengthRecord(FixedMode.ExactLength)]
		private class ArrayType1
		{
			[FieldFixedLength(5)]
			public string CustomerID;

			[FieldArrayLength(2, 30)]
			[FieldFixedLength(7)]
			public int[] BuyedArts;

		}



		
		[DelimitedRecord("|")]
		private class ArrayTypeBad1
		{
			[FieldArrayLength(2, 30)]
			public string CustomerID;
		}

	}
}