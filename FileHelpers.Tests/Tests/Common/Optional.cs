using System;
using System.Drawing;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class OptionalFields
	{
		FileHelperEngine engine;
		private const int ExpectedRecords = 8;

		[Test]
//		[Ignore("Not Implementes")]
		public void Fixed0()
		{
			engine = new FileHelperEngine(typeof (OptionalFixed1));
			OptionalFixed1[] res = (OptionalFixed1[]) TestCommon.ReadTest(engine, "Good", "OptionalFixed0.txt");
			Assert.AreEqual(ExpectedRecords, res.Length);
		}

		[Test]
//		[Ignore("Not Implementes")]
		public void Fixed1()
		{
			engine = new FileHelperEngine(typeof (OptionalFixed1));
			OptionalFixed1[] res = (OptionalFixed1[]) TestCommon.ReadTest(engine, "Good", "OptionalFixed1.txt");
			Assert.AreEqual(ExpectedRecords, res.Length);
		}

		[Test]
		public void Fixed2()
		{
			engine = new FileHelperEngine(typeof (OptionalFixed2));
			OptionalFixed2[] res = (OptionalFixed2[]) TestCommon.ReadTest(engine, "Good", "OptionalFixed2.txt");
			Assert.AreEqual(ExpectedRecords, res.Length);
		}

		[Test]
		public void Delimited0()
		{
			engine = new FileHelperEngine(typeof (OptionalDelimited1));
			OptionalDelimited1[] res = (OptionalDelimited1[]) TestCommon.ReadTest(engine, "Good", "OptionalDelimited0.txt");

			Assert.AreEqual(ExpectedRecords, res.Length);
		}

		[Test]
		public void Delimited1()
		{
			engine = new FileHelperEngine(typeof (OptionalDelimited1));
			OptionalDelimited1[] res = (OptionalDelimited1[]) TestCommon.ReadTest(engine, "Good", "OptionalDelimited1.txt");

			Assert.AreEqual(ExpectedRecords, res.Length);
			Assert.AreEqual("", res[2].ContactTitle);
			Assert.AreEqual("", res[5].ContactTitle);
		}


		[Test]
		public void Delimited1Quoted()
		{
			engine = new FileHelperEngine(typeof (OptionalDelimited1Quoted));
			OptionalDelimited1Quoted[] res = (OptionalDelimited1Quoted[]) TestCommon.ReadTest(engine, "Good", "OptionalDelimited1Quoted.txt");

			Assert.AreEqual(ExpectedRecords, res.Length);
			Assert.AreEqual("", res[2].ContactTitle);
			Assert.AreEqual("", res[5].ContactTitle);
		}


		[Test]
		public void Delimited2()
		{
			engine = new FileHelperEngine(typeof (OptionalDelimited2));
			OptionalDelimited2[] res = (OptionalDelimited2[]) TestCommon.ReadTest(engine, "Good", "OptionalDelimited2.txt");

			Assert.AreEqual(ExpectedRecords, res.Length);
			Assert.AreEqual("", res[2].ContactTitle);
			Assert.AreEqual("", res[2].ContactName);
			Assert.AreEqual("", res[7].ContactTitle);
			Assert.AreEqual("", res[7].ContactName);

		}

		[Test]
		public void Delimited2Quoted()
		{
			engine = new FileHelperEngine(typeof (OptionalDelimited2Quoted));
			OptionalDelimited2Quoted[] res = (OptionalDelimited2Quoted[]) TestCommon.ReadTest(engine, "Good", "OptionalDelimited2Quoted.txt");

			Assert.AreEqual(ExpectedRecords, res.Length);
			Assert.AreEqual("", res[2].ContactTitle);
			Assert.AreEqual("", res[2].ContactName);
			Assert.AreEqual("", res[7].ContactTitle);
			Assert.AreEqual("", res[7].ContactName);

		}

		[Test]
		public void Delimited3()
		{
			engine = new FileHelperEngine(typeof (OptionalDelimited3));
			OptionalDelimited3[] res = (OptionalDelimited3[]) TestCommon.ReadTest(engine, "Good", "OptionalDelimited3.txt");

			Assert.AreEqual(ExpectedRecords, res.Length);
			Assert.AreEqual("", res[2].ContactTitle);
			Assert.AreEqual("", res[5].ContactTitle);
		}

		[Test]
		public void Delimited4()
		{
			engine = new FileHelperEngine(typeof (OptionalDelimited4));
			OptionalDelimited4[] res = (OptionalDelimited4[]) TestCommon.ReadTest(engine, "Good", "OptionalDelimited4.txt");

			Assert.AreEqual(ExpectedRecords, res.Length);
			Assert.AreEqual("", res[2].ContactTitle);
			Assert.AreEqual("", res[5].ContactTitle);
		}


		[Test]
		public void Delimited5()
		{
			engine = new FileHelperEngine(typeof (OptionalDelimited5));
			OptionalDelimited5[] res = (OptionalDelimited5[]) TestCommon.ReadTest(engine, "Good", "OptionalDelimited5.txt");

			Assert.AreEqual(ExpectedRecords, res.Length);
			Assert.AreEqual("", res[2].ContactTitle);
			Assert.AreEqual("", res[5].ContactTitle);
		}


		[Test]
		public void DelimitedFull()
		{
			engine = new FileHelperEngine(typeof (OptionalFull));
			OptionalFull[] res = (OptionalFull[]) TestCommon.ReadTest(engine, "Good", "OptionalDelimitedFull.txt");

			Assert.AreEqual(8, res.Length);
		}
		
		[Test]
		public void DelimitedFull2()
		{
			engine = new FileHelperEngine(typeof (OptionalFull2));
			OptionalFull2[] res = (OptionalFull2[]) TestCommon.ReadTest(engine, "Good", "OptionalDelimitedFull.txt");

			Assert.AreEqual(8, res.Length);
		}


        [Test]
        public void OptionalWithAutoProps()
        {
            Assert.Throws<BadUsageException>(() =>
                                             new FileHelperEngine<OptionalWithAutoPropsType>(),
            "You must only use all standard fields or all automatic properties, but you can't mix them like in the OptionalWithAutoPropsType class.");
        }

		[FixedLengthRecord]
		private class OptionalFixed1
		{
			[FieldFixedLength(11)]
			public string CustomerID;
			[FieldFixedLength(50 - 12)]
			public string CompanyName;
			[FieldFixedLength(72 - 50)]
			public string ContactName;
			[FieldFixedLength(110 - 72)]
			[FieldOptional()]
			public string ContactTitle;

		}

		[FixedLengthRecord]
		private class OptionalFixed2
		{
			[FieldFixedLength(11)]
			public string CustomerID;
			[FieldFixedLength(50 - 12)]
			public string CompanyName;
			[FieldFixedLength(72 - 50)]
			[FieldOptional()]
			public string ContactName;
			[FieldFixedLength(110 - 72)]
			[FieldOptional()]
			public string ContactTitle;

		}

		[DelimitedRecord("|")]
		private class OptionalDelimited1
		{
			public string CustomerID;
			public string CompanyName;
			public string ContactName;
			[FieldOptional()]
			public string ContactTitle;
		}

		[DelimitedRecord("|")]
		private class OptionalDelimited1Quoted
		{
			public string CustomerID;
			public string CompanyName;
			[FieldQuoted(QuoteMode.AlwaysQuoted)]
			public string ContactName;
			[FieldOptional()]
			public string ContactTitle;
		}


		[DelimitedRecord("|")]
		private class OptionalDelimited2Quoted
		{
			public string CustomerID;
			public string CompanyName;
			[FieldQuoted(QuoteMode.AlwaysQuoted)]
			[FieldOptional()]
			public string ContactName;
			[FieldOptional()]
			public string ContactTitle;
		}

		[DelimitedRecord("|")]
		private class OptionalDelimited2
		{
			public string CustomerID;
			public string CompanyName;
			[FieldOptional()]
			public string ContactName;
			[FieldOptional()]
			public string ContactTitle;
		}

		[DelimitedRecord("|")]
		private class OptionalDelimited3
		{
			public string CustomerID;
			public string CompanyName;
			[FieldQuoted()]
			public string ContactName;
			[FieldOptional()]
			public string ContactTitle;
		}

		[DelimitedRecord("|")]
		private class OptionalDelimited4
		{
			public string CustomerID;
			public string CompanyName;
			[FieldQuoted(QuoteMode.OptionalForBoth)]
			public string ContactName;
			[FieldOptional()]
			public string ContactTitle;
		}

		[DelimitedRecord(",")]
		private class OptionalDelimited5
		{
			public string CustomerID;
			public string CompanyName;
			[FieldQuoted(QuoteMode.OptionalForBoth)]
			public string ContactName;
			[FieldOptional()]
			public string ContactTitle;
		}

		[IgnoreFirst(1),
		 DelimitedRecord(",")]
		public sealed class OptionalFull
		{
			public string PLT_OrganizationID;
			[FieldQuoted('"', QuoteMode.OptionalForBoth)]
			public string PartnerTier;
			[FieldQuoted('"', QuoteMode.OptionalForBoth)]
			public string EngagementType;
			public string MOSBFlag;
			public string MCEFlag;
			[FieldQuoted('"', QuoteMode.OptionalForBoth)]
			public string PAM;
			[FieldQuoted('"', QuoteMode.OptionalForBoth)]
			[FieldOptional()]
			public string PAMAlias;
		}

		[IgnoreFirst(1),
		 DelimitedRecord(",")]
		public sealed class OptionalFull2
		{
			public string PLT_OrganizationID;
			[FieldQuoted('"', QuoteMode.OptionalForBoth)]
			public string PartnerTier;
			[FieldQuoted('"', QuoteMode.OptionalForBoth)]
			public string EngagementType;
			public string MOSBFlag;
			public string MCEFlag;
			[FieldQuoted('"', QuoteMode.OptionalForBoth)]
			public string PAM;
			[FieldQuoted('"', QuoteMode.OptionalForBoth)]
			[FieldOptional()]
			public string PAMAlias;
			[FieldIgnored()]
			public string Ignored;
		}



        [DelimitedRecord("\t")]
        public class OptionalWithAutoPropsType
        {
         
            public int TiempoRespuestaInMs { get; set; }
            
            [FieldOptional]
            private string mMetodo;

            public string Metodo
            {
                get { return mMetodo; }
                set { mMetodo = value; }
            }

        }

	}
}