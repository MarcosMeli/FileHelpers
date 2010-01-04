using System;
using System.IO;
using System.Text;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
{
	[TestFixture]
	public class Writers
	{
		FileHelperEngine engine;
	    private readonly int newLineLen = Environment.NewLine.Length;

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

			CommonEngine.WriteFile(@"prueba.txt", res);

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

			Assert.AreEqual(14 + newLineLen + 14 + newLineLen, sb.ToString().Length);
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

			Assert.AreEqual(14 + newLineLen + 14 + newLineLen, resStr.Length);
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

			string resStr = CommonEngine.WriteString(res);

			Assert.AreEqual(14 + newLineLen + 14 + newLineLen, resStr.Length);
			Assert.AreEqual(resStr.Substring(0, 8), DateTime.Now.AddDays(1).ToString("ddMMyyyy"));

		}


        [Test] 
        public void WriteStringNullableGuid() 
        { 
            engine = new FileHelperEngine(typeof(SampleTypeNullableGuid)); 
  
            string resStr = engine.WriteString(new []{new SampleTypeNullableGuid()}); 
 
            Assert.AreEqual(Environment.NewLine, resStr); 
        } 
 
	}
}