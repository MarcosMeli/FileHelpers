using System;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class IgnoreEmpties
	{
		FileHelperEngine engine;
		FileHelperAsyncEngine asyncEngine;

		[Test]
		public void IgnoreEmpty1()
		{
			engine = new FileHelperEngine(typeof (IgnoreEmptyType1));

			object[] res = TestCommon.ReadTest(engine, @"Good\IgnoreEmpty1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(7, engine.LineNumber);
		}

		[Test]
		public void IgnoreEmpty1Async()
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (IgnoreEmptyType1));

			object[] res = TestCommon.ReadAllAsync(asyncEngine, @"Good\IgnoreEmpty1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(7, asyncEngine.LineNumber);
		}

	}

	[FixedLengthRecord]
	[IgnoreEmptyLines()]
	public class IgnoreEmptyType1
	{
		[FieldFixedLength(8)]
		[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
		public DateTime Field1;

		[FieldFixedLength(3)]
		public string Field2;

		[FieldFixedLength(3)]
		[FieldConverter(ConverterKind.Int32)]
		public int Field3;
	}

	[DelimitedRecord("|")]
	[IgnoreEmptyLines()]
	public class IgnoreEmptyType2
	{
		[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
		public DateTime Field1;

		[FieldFixedLength(3)]
		public string Field2;

		public int Field3;
	}

}