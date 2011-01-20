using System;
using System.IO;
using System.Text;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class WritersMaxRecords
	{
	    private readonly int newLineLen = Environment.NewLine.Length;

		[Test]
		public void WriteFile()
		{
			var engine = new FileHelperEngine<SampleType>();

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			engine.WriteFile(@"miprueba.txt", res, 1);
			
			res = (SampleType[]) engine.ReadFile(@"miprueba.txt");

			if (File.Exists(@"miprueba.txt"))
				File.Delete(@"miprueba.txt");

			Assert.AreEqual(1, res.Length);

		}


		[Test]
		public void WriteStream()
		{
			var engine = new FileHelperEngine<SampleType>();

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			StringBuilder sb = new StringBuilder();
			StringWriter writer = new StringWriter(sb);
			engine.WriteStream(writer, res, 1);

			Assert.AreEqual(14 + newLineLen, sb.ToString().Length);
			Assert.AreEqual(sb.ToString(0, 8), DateTime.Now.AddDays(1).ToString("ddMMyyyy"));

		}


		[Test]
		public void WriteString()
		{
			var engine = new FileHelperEngine<SampleType>();

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			string resStr = engine.WriteString(res, 1);

			Assert.AreEqual(14 + newLineLen, resStr.Length);
			Assert.AreEqual(resStr.Substring(0, 8), DateTime.Now.AddDays(1).ToString("ddMMyyyy"));

		}


		[Test]
		public void WriteFile2()
		{
			var engine = new FileHelperEngine<SampleType>();

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			engine.WriteFile(@"miprueba.txt", res, -10);
			
			res = (SampleType[]) engine.ReadFile(@"miprueba.txt");

			if (File.Exists(@"miprueba.txt"))
				File.Delete(@"miprueba.txt");

			Assert.AreEqual(2, res.Length);

		}


		[Test]
		public void WriteStream2()
		{
			var engine = new FileHelperEngine<SampleType>();

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			StringBuilder sb = new StringBuilder();
			StringWriter writer = new StringWriter(sb);
			engine.WriteStream(writer, res, -10);

			Assert.AreEqual(14 + newLineLen + 14 + newLineLen, sb.ToString().Length);
			Assert.AreEqual(sb.ToString(0, 8), DateTime.Now.AddDays(1).ToString("ddMMyyyy"));

		}


		[Test]
		public void WriteString2()
		{
			var engine = new FileHelperEngine<SampleType>();

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			string resStr = engine.WriteString(res, -10);

			Assert.AreEqual(14 + newLineLen + 14 + newLineLen, resStr.Length);
			Assert.AreEqual(resStr.Substring(0, 8), DateTime.Now.AddDays(1).ToString("ddMMyyyy"));

		}
	}
}