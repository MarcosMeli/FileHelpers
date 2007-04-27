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
		public void ArrayFields1()
		{
			engine = new FileHelperEngine(typeof (ArrayType1));
			ArrayType1[] res = engine.ReadFile(Common.TestPath(@"good\ArrayFields.txt")) as ArrayType1[];

            SimpleComparer(res);

        }

        private static void SimpleComparer(ArrayType1[] res)
        {
            Assert.AreEqual(3, res.Length);

            Assert.AreEqual(58745, res[0].CustomerID);
            Assert.AreEqual(13, res[0].BuyedArts[0]);
            Assert.AreEqual(8, res[0].BuyedArts[1]);
            Assert.AreEqual(3, res[0].BuyedArts[2]);
            Assert.AreEqual(-7, res[0].BuyedArts[3]);
            Assert.AreEqual(20, res[0].BuyedArts.Length);

            Assert.AreEqual(31245, res[1].CustomerID);
            Assert.AreEqual(6, res[1].BuyedArts[0]);
            Assert.AreEqual(17, res[1].BuyedArts.Length);

            Assert.AreEqual(1245, res[2].CustomerID);
            Assert.AreEqual(0, res[2].BuyedArts.Length);
        }


        [Test]
        public void ArrayFieldsRW()
        {
            engine = new FileHelperEngine(typeof(ArrayType1));
            ArrayType1[] res = engine.ReadFile(Common.TestPath(@"good\ArrayFields.txt")) as ArrayType1[];
            SimpleComparer(res);
            
            res = engine.ReadString(engine.WriteString(res)) as ArrayType1[];

            SimpleComparer(res);
        }



		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void ArrayFieldsBad1()
		{
			engine = new FileHelperEngine(typeof (ArrayTypeBad1));
		}

        [Test]
        public void ArrayFieldsBad2()
        {
            engine = new FileHelperEngine(typeof(ArrayType2));
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            ArrayType2[] res = engine.ReadFile(Common.TestPath(@"good\ArrayFields2.txt")) as ArrayType2[];
            
            Assert.AreEqual(0, res.Length);
            Assert.AreEqual(2, engine.ErrorManager.ErrorCount);

            Assert.AreEqual("The array has only 4 values, less than the minimum length of 5", engine.ErrorManager.Errors[0].ExceptionInfo.Message);
            Assert.AreEqual("The array has 6 values, more than the maximum length of 5", engine.ErrorManager.Errors[1].ExceptionInfo.Message);
        }


		[FixedLengthRecord(FixedMode.ExactLength)]
		private class ArrayType1
		{
			[FieldFixedLength(5)]
			public int CustomerID;

			[FieldFixedLength(7)]
			public int[] BuyedArts;

		}


        [FixedLengthRecord(FixedMode.ExactLength)]
        private class ArrayType2
        {
            [FieldFixedLength(5)]
            public int CustomerID;

            [FieldFixedLength(7)]
            [FieldArrayLength(5)]
            public int[] BuyedArts;

        }

		
		[DelimitedRecord("|")]
		private class ArrayTypeBad1
		{
			[FieldArrayLength(2, 30)]
			public int CustomerID;
		}

	}
}