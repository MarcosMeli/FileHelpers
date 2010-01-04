using System;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.Errors
{
	[TestFixture]
	public class BadOptional
	{
		FileHelperEngine engine;

		[Test]
		public void DelimitedBad1()
		{
            Assert.Throws<BadUsageException>(() 
                => new FileHelperEngine(typeof (OptionalBad1)));
		}

		[Test]
		public void DelimitedBad2()
		{
			engine = new FileHelperEngine(typeof (OptionalBad2));
            Assert.Throws<BadUsageException>(() 
                => TestCommon.ReadTest(engine, "Bad", "OptionalBad1.txt"));
		}


		[DelimitedRecord("|")]
		private class OptionalBad1
		{
			public string CustomerID;
			public string CompanyName;
			[FieldOptional()]
			public string ContactName;
			public string ContactTitle;
		}

		[DelimitedRecord("|")]
		private class OptionalBad2
		{
			public string CustomerID;
			public string CompanyName;
			public string ContactName;
			[FieldOptional()]
			public int ContactTitle;
		}

		
	}
}