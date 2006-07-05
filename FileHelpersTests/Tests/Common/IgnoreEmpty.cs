using System;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
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

			object[] res = Common.ReadTest(engine, @"Good\IgnoreEmpty1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(8, engine.LineNumber);
		}

		[Test]
		public void IgnoreEmpty2()
		{
			engine = new FileHelperEngine(typeof (IgnoreEmptyType1));

			object[] res = Common.ReadTest(engine, @"Good\IgnoreEmpty2.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(8, engine.LineNumber);
		}

		[Test]
		public void IgnoreEmpty3()
		{
			engine = new FileHelperEngine(typeof (IgnoreEmptyType1));

			object[] res = Common.ReadTest(engine, @"Good\IgnoreEmpty3.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(8, engine.LineNumber);
		}

		[Test]
		public void IgnoreEmpty1Async()
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (IgnoreEmptyType1));

			object[] res = Common.ReadAllAsync(asyncEngine, @"Good\IgnoreEmpty1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(8, asyncEngine.LineNumber);
		}

		[Test]
		public void IgnoreEmpty3Async()
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (IgnoreEmptyType1));

			object[] res = Common.ReadAllAsync(asyncEngine, @"Good\IgnoreEmpty3.txt");

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