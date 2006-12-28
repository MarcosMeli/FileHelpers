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

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void IgnoreEmpty4Bad()
		{
			engine = new FileHelperEngine(typeof (IgnoreEmptyType1));
			Common.ReadTest(engine, @"Good\IgnoreEmpty4.txt");
		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void IgnoreEmpty4BadAsync()
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (IgnoreEmptyType1));
			Common.ReadAllAsync(asyncEngine, @"Good\IgnoreEmpty4.txt");
		}

		[Test]
		public void IgnoreEmpty4()
		{
			engine = new FileHelperEngine(typeof (IgnoreEmptyType1Spaces));
			object[] res = Common.ReadTest(engine, @"Good\IgnoreEmpty4.txt");
			
			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(7, engine.LineNumber);
		}

		[Test]
		public void IgnoreEmpty5()
		{
			engine = new FileHelperEngine(typeof (IgnoreEmptyType1Spaces));
			object[] res = Common.ReadTest(engine, @"Good\IgnoreEmpty5.txt");
			
			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(7, engine.LineNumber);
		}

		[Test]
		public void IgnoreComment1()
		{
			engine = new FileHelperEngine(typeof (IgnoreCommentsType));
			object[] res = Common.ReadTest(engine, @"Good\IgnoreComments1.txt");
			
			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(7, engine.LineNumber);
		}

		[Test]
		public void IgnoreComment1Async()
		{
			asyncEngine = new FileHelperAsyncEngine(typeof (IgnoreCommentsType));
			object[] res = Common.ReadAllAsync(asyncEngine, @"Good\IgnoreComments1.txt");
			
			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(7, asyncEngine.LineNumber);
		}
		[Test]
		public void IgnoreComment2()
		{
			engine = new FileHelperEngine(typeof (IgnoreCommentsType));
			object[] res = Common.ReadTest(engine, @"Good\IgnoreComments2.txt");
			
			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(7, engine.LineNumber);
		}

		
		[Test]
		public void IgnoreComment3()
		{
			engine = new FileHelperEngine(typeof (IgnoreCommentsType2));
			object[] res = Common.ReadTest(engine, @"Good\IgnoreComments1.txt");
			
			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(7, engine.LineNumber);
		}

		[Test]
		[ExpectedException(typeof(ConvertException))]
		public void IgnoreComment4()
		{
			engine = new FileHelperEngine(typeof (IgnoreCommentsType2));
			object[] res = Common.ReadTest(engine, @"Good\IgnoreComments2.txt");
			
			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(7, engine.LineNumber);
		}

	}

	
	[FixedLengthRecord]
	[IgnoreCommentedLines("//")]
	public class IgnoreCommentsType
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

	[FixedLengthRecord]
	[IgnoreCommentedLines("//", true)]
	public class IgnoreCommentsType2
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

	[FixedLengthRecord]
	[IgnoreEmptyLines(true)]
	public class IgnoreEmptyType1Spaces
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