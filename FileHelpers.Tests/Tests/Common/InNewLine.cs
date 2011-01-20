using System;
using FileHelpers;
using FileHelpers.MasterDetail;
using NUnit.Framework;
using System.IO;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class InNewLineTest
	{
		[Test]
		public void InNewLine0()
		{
			var engine = new FileHelperEngine<InNewLineType0>();

			InNewLineType0[] res = engine.ReadFile(TestCommon.GetPath("Good", "InNewLine0.txt"));

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(3, engine.TotalRecords);

			Assert.AreEqual("166.90.252.2", res[0].IpAddress);
			Assert.AreEqual("67.105.166.35", res[1].IpAddress);
			Assert.AreEqual("67.105.166.35", res[2].IpAddress);

		}

		[Test]
		public void InNewLine0rw()
		{
			var engine = new FileHelperEngine<InNewLineType0>();

			InNewLineType0[] res = engine.ReadFile(TestCommon.GetPath("Good", "InNewLine0.txt"));

  		    string tmp = engine.WriteString(res);
			res = (InNewLineType0[]) engine.ReadString(tmp);

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(3, engine.TotalRecords);

			Assert.AreEqual("166.90.252.2", res[0].IpAddress);
			Assert.AreEqual("67.105.166.35", res[1].IpAddress);
			Assert.AreEqual("67.105.166.35", res[2].IpAddress);
		}

		[Test]
		public void InNewLine1()
		{
			var engine = new FileHelperEngine<InNewLineType1>();

            InNewLineType1[] res = engine.ReadFile(TestCommon.GetPath("Good", "InNewLine1.txt"));

            Assert.AreEqual(3, res.Length);
            Assert.AreEqual(3, engine.TotalRecords);

			Assert.AreEqual("166.90.252.2", res[0].IpAddress);
			Assert.AreEqual("67.105.166.35", res[1].IpAddress);
			Assert.AreEqual("67.105.166.35", res[2].IpAddress);

		}

		[Test]
		public void InNewLine1rw()
		{
			var engine = new FileHelperEngine<InNewLineType1>();

			InNewLineType1[] res = engine.ReadFile(TestCommon.GetPath("Good", "InNewLine1.txt"));

			string tmp = engine.WriteString(res);
			res = (InNewLineType1[]) engine.ReadString(tmp);

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(3, engine.TotalRecords);

			Assert.AreEqual("166.90.252.2", res[0].IpAddress);
			Assert.AreEqual("67.105.166.35", res[1].IpAddress);
			Assert.AreEqual("67.105.166.35", res[2].IpAddress);

		}


		[Test]
		public void InNewLineFixed1()
		{
			var engine = new FileHelperEngine<InNewLineFixedType1>();

			InNewLineFixedType1[] res = engine.ReadFile(TestCommon.GetPath("Good", "InNewLineFixed1.txt"));

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(3, engine.TotalRecords);

			Assert.AreEqual("166.090.252.002", res[0].IpAddress);
			Assert.AreEqual("067.105.166.035", res[1].IpAddress);
			Assert.AreEqual("067.105.166.035", res[2].IpAddress);

		}

		[Test]
		public void InNewLineFixed1rw()
		{
			var engine = new FileHelperEngine<InNewLineFixedType1>();

			InNewLineFixedType1[] res = engine.ReadFile(TestCommon.GetPath("Good", "InNewLineFixed1.txt"));

			string tmp = engine.WriteString(res);
			res = engine.ReadString(tmp);

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(3, engine.TotalRecords);

			Assert.AreEqual("166.090.252.002", res[0].IpAddress);
			Assert.AreEqual("067.105.166.035", res[1].IpAddress);
			Assert.AreEqual("067.105.166.035", res[2].IpAddress);

		}


		[Test]
		public void InNewLine2()
		{
			var engine = new FileHelperEngine<InNewLineType2>();

			InNewLineType2[] res = engine.ReadFile(TestCommon.GetPath("Good", "InNewLine2.txt"));

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
		public void InNewLine2rw()
		{
			var engine = new FileHelperEngine<InNewLineType2>();

			InNewLineType2[] res = engine.ReadString(engine.WriteString(engine.ReadFile(TestCommon.GetPath("Good", "InNewLine2.txt"))));

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
		public void InNewLineFixed2()
		{
			var engine = new FileHelperEngine<InNewLineFixedType2>();

			InNewLineFixedType2[] res = engine.ReadFile(TestCommon.GetPath("Good", "InNewLineFixed2.txt"));

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(3, engine.TotalRecords);

			Assert.AreEqual("166.090.252.002", res[0].IpAddress);
			Assert.AreEqual("067.105.166.035", res[1].IpAddress);
			Assert.AreEqual("067.105.166.035", res[2].IpAddress);
			Assert.AreEqual(111, res[0].FieldLast);
			Assert.AreEqual(222, res[1].FieldLast);
			Assert.AreEqual(333, res[2].FieldLast);

		}

		[Test]
		public void InNewLineFixed2rw()
		{
			var engine = new FileHelperEngine<InNewLineFixedType2>();

			InNewLineFixedType2[] res = engine.ReadFile(TestCommon.GetPath("Good", "InNewLineFixed2.txt"));

			string tmp = engine.WriteString(res);
			res = engine.ReadString(tmp);

			Assert.AreEqual(3, res.Length);
			Assert.AreEqual(3, engine.TotalRecords);

			Assert.AreEqual("166.090.252.002", res[0].IpAddress);
			Assert.AreEqual("067.105.166.035", res[1].IpAddress);
			Assert.AreEqual("067.105.166.035", res[2].IpAddress);
			Assert.AreEqual(111, res[0].FieldLast);
			Assert.AreEqual(222, res[1].FieldLast);
			Assert.AreEqual(333, res[2].FieldLast);

		}

		[Test]
		public void InNewLine3Bad()
		{
			var engine = new FileHelperEngine<InNewLineType2>();

			Assert.Throws<BadUsageException>(() => 
                engine.ReadFile(TestCommon.GetPath("Bad", "InNewLine3.txt")));
		}

		[Test]
		public void InNewLine4Bad()
		{
			var engine = new FileHelperEngine<InNewLineType2>();
			Assert.Throws<BadUsageException>(() => 
                engine.ReadFile(TestCommon.GetPath("Bad", "InNewLine4.txt")));
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

			[FieldConverter(ConverterKind.Date, "dd-MM hh:mm:ss yyyy")]
			public DateTime Date1;
			[FieldConverter(ConverterKind.Date, "dd-MM-yyyy hh:mm:ss")]
			public DateTime Date2;

			public byte Field1;
			public long Field2;

			[FieldInNewLine()]
			public string IpAddress;
		}

		[FixedLengthRecord()]
		private sealed class InNewLineFixedType1
		{
			[FieldFixedLength(3)]
			public byte Field1;

			[FieldFixedLength(8)]
			public long Field2;

			[FieldFixedLength(15)]
			[FieldInNewLine()]
			public string IpAddress;
		}

		[FixedLengthRecord()]
		private sealed class InNewLineFixedType2
		{
			[FieldFixedLength(3)]
			public byte Field1;

			[FieldFixedLength(8)]
			public long Field2;

			[FieldFixedLength(15)]
			[FieldInNewLine()]
			public string IpAddress;

			[FieldFixedLength(3)]
			public int FieldLast;
		}


	}
}