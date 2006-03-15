using System;
using System.IO;
using System.Text;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Common
{
	[TestFixture]
	public class Writers
	{
		FileHelperEngine engine;

		[Test]
		public void WriteFile()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			engine.WriteFile(@"prueba.txt", res);

			if (File.Exists(@"prueba.txt"))
				File.Delete(@"prueba.txt");
		}


		[Test]
		public void WriteFileStatic()
		{
			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			CommonEngine.WriteFile(typeof (SampleType), @"prueba.txt", res);

			if (File.Exists(@"prueba.txt"))
				File.Delete(@"prueba.txt");
		}


		[Test]
		public void WriteStream()
		{
			engine = new FileHelperEngine(typeof (SampleType));

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
			engine.WriteStream(writer, res);

			Assert.AreEqual(14 + 2 + 14 + 2, sb.ToString().Length);
			Assert.AreEqual(sb.ToString(0, 8), DateTime.Now.AddDays(1).ToString("ddMMyyyy"));

		}


		[Test]
		public void WriteString()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			string resStr = engine.WriteString(res);

			Assert.AreEqual(14 + 2 + 14 + 2, resStr.Length);
			Assert.AreEqual(resStr.Substring(0, 8), DateTime.Now.AddDays(1).ToString("ddMMyyyy"));

		}

		[Test]
		public void WriteStringStatic()
		{
			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			string resStr = CommonEngine.WriteString(typeof (SampleType), res);

			Assert.AreEqual(14 + 2 + 14 + 2, resStr.Length);
			Assert.AreEqual(resStr.Substring(0, 8), DateTime.Now.AddDays(1).ToString("ddMMyyyy"));

		}

		[Test]
		public void AppendToFile()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1).Date;
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now.Date;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			engine.WriteFile(@"test.txt", res);
			engine.AppendToFile(@"test.txt", res);

			SampleType[] res2 = (SampleType[]) engine.ReadFile(@"test.txt");

			Assert.AreEqual(4, res2.Length);
			Assert.AreEqual(res[0].Field1, res2[0].Field1);
			Assert.AreEqual(res[1].Field1, res2[1].Field1);
			Assert.AreEqual(res[0].Field1, res2[2].Field1);
			Assert.AreEqual(res[1].Field1, res2[3].Field1);
		}

		[Test]
		public void AppendOneToFile()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1).Date;
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now.Date;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			engine.WriteFile(@"test.txt", res);

			SampleType record = new SampleType();

			record.Field1 = DateTime.Now.Date;
			record.Field2 = "h2";
			record.Field3 = 2;

			engine.AppendToFile(@"test.txt", record);

			SampleType[] res2 = (SampleType[]) engine.ReadFile(@"test.txt");

			Assert.AreEqual(3, res2.Length);
			Assert.AreEqual(res[0].Field1, res2[0].Field1);
			Assert.AreEqual(res[1].Field1, res2[1].Field1);
			Assert.AreEqual(DateTime.Now.Date, res2[2].Field1);
		}

		[Test]
		public void BadAppend()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			File.Copy(@"..\data\Bad\BadAdd1.txt", "BadAddTemp1.txt", true);

			SampleType record = new SampleType();

			record.Field1 = DateTime.Now.Date;
			record.Field2 = "AS";
			record.Field3 = 66;

			engine.AppendToFile(@"BadAddTemp1.txt", record);

			SampleType[] res2 = (SampleType[]) engine.ReadFile(@"BadAddTemp1.txt");
			Assert.AreEqual(4, res2.Length);
			Assert.AreEqual("AS", res2[3].Field2);
			Assert.AreEqual(66, res2[3].Field3);
		}

		[Test]
		public void BadAppend2()
		{
			engine = new FileHelperEngine(typeof (SampleType));

			File.Copy(@"..\data\Bad\BadAdd2.txt", "BadAddTemp2.txt", true);

			SampleType record = new SampleType();

			record.Field1 = DateTime.Now.Date;
			record.Field2 = "AS";
			record.Field3 = 66;

			engine.AppendToFile(@"BadAddTemp2.txt", record);

			SampleType[] res2 = (SampleType[]) engine.ReadFile(@"BadAddTemp2.txt");
			Assert.AreEqual(4, res2.Length);
			Assert.AreEqual("AS", res2[3].Field2);
			Assert.AreEqual(66, res2[3].Field3);
		}


	}
}