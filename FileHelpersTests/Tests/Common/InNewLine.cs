using System;
using FileHelpers;
using FileHelpers.MasterDetail;
using NUnit.Framework;
using System.IO;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class InNewLineTest
	{
		FileHelperEngine engine;

		[Test]
		public void InNewLine0()
		{
			engine = new FileHelperEngine(typeof(InNewLineType0));

			InNewLineType0[] res = (InNewLineType0[]) engine.ReadFile(TestCommon.TestPath(@"Good\InNewLine0.txt"));

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(3, engine.TotalRecords);

			Assert.AreEqual("166.90.252.2", res[0].IpAddress);
			Assert.AreEqual("67.105.166.35", res[1].IpAddress);
			Assert.AreEqual("67.105.166.35", res[2].IpAddress);

		}

		[Test]
		public void InNewLine1()
		{
			engine = new FileHelperEngine(typeof(InNewLineType1));

            InNewLineType1[] res = (InNewLineType1[]) engine.ReadFile(TestCommon.TestPath(@"Good\InNewLine1.txt"));

            Assert.AreEqual(3, res.Length);
            Assert.AreEqual(3, engine.TotalRecords);

			Assert.AreEqual("166.90.252.2", res[0].IpAddress);
			Assert.AreEqual("67.105.166.35", res[1].IpAddress);
			Assert.AreEqual("67.105.166.35", res[2].IpAddress);

		}

		[Test]
		public void InNewLine2()
		{
			engine = new FileHelperEngine(typeof(InNewLineType2));

			InNewLineType2[] res = (InNewLineType2[]) engine.ReadFile(TestCommon.TestPath(@"Good\InNewLine2.txt"));

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(3, engine.TotalRecords);

			Assert.AreEqual("166.90.252.2", res[0].IpAddress);
			Assert.AreEqual("67.105.166.35", res[1].IpAddress);
			Assert.AreEqual("67.105.166.35", res[2].IpAddress);

			Assert.AreEqual(111, res[0].FieldLast);
			Assert.AreEqual(222, res[1].FieldLast);
			Assert.AreEqual(333, res[2].FieldLast);

		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void InNewLine3Bad()
		{
			engine = new FileHelperEngine(typeof(InNewLineType2));
			engine.ReadFile(TestCommon.TestPath(@"Bad\InNewLine3.txt"));
		}

		[Test]
		[ExpectedException(typeof(BadUsageException))]
		public void InNewLine4Bad()
		{
			engine = new FileHelperEngine(typeof(InNewLineType2));
			engine.ReadFile(TestCommon.TestPath(@"Bad\InNewLine4.txt"));
		}

		[DelimitedRecord(",")]
		private sealed class InNewLineType1
		{
			public byte Field1;

			public long Field2;

			[FieldInNewLine()]
			public string IpAddress;
		}

		[DelimitedRecord(",")]
		private sealed class InNewLineType2
		{
			public byte Field1;

			public long Field2;

			[FieldInNewLine()]
			public string IpAddress;
		
			public int FieldLast;
		}


		[DelimitedRecord(",")]
		private sealed class InNewLineType0
		{

			[FieldConverter(ConverterKind.Date, "ddd MMM dd hh:mm:ss yyyy")]
			public DateTime Date1;
			[FieldConverter(ConverterKind.Date, "ddd MMM dd hh:mm:ss")]
			public DateTime Date2;

			public byte Field1;
			public long Field2;

			[FieldInNewLine()]
			public string IpAddress;
		}


	}
}