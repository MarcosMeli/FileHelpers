using System;
using System.IO;
using System.Text;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class AppendWriters
	{
		FileHelperEngine engine;

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
		public void AppendToEmpty()
		{

			File.Copy(Common.TestPath(@"Good\TestEmpty.txt"), "tempEmpty.txt", true);
			
			engine = new FileHelperEngine(typeof (SampleType));

			SampleType[] res = new SampleType[1];

			res[0] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1).Date;
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			engine.AppendToFile(@"tempEmpty.txt", res);

			SampleType[] res2 = (SampleType[]) engine.ReadFile(@"tempEmpty.txt");

			Assert.AreEqual(1, res2.Length);
			Assert.AreEqual(res[0].Field1, res2[0].Field1);

			if (File.Exists("tempEmpty.txt")) File.Delete("tempEmpty.txt");

		}


		[Test]
		public void AppendToEmptyAsync()
		{

			
			FileHelperEngine engineOld = new FileHelperEngine(typeof (SampleType));
			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(typeof (SampleType));

			SampleType rec = new SampleType();

			rec.Field1 = DateTime.Now.AddDays(1).Date;
			rec.Field2 = "je";
			rec.Field3 = 0;

			File.Copy(Common.TestPath(@"Good\TestEmpty.txt"), "tempEmpty.txt", true);

			engine.BeginAppendToFile(@"tempEmpty.txt");
			engine.WriteNext(rec);
			engine.EndsWrite();

			SampleType[] res2 = (SampleType[]) engineOld.ReadFile(@"tempEmpty.txt");

			Assert.AreEqual(1, res2.Length);
			Assert.AreEqual(rec.Field1, res2[0].Field1);

			if (File.Exists("tempEmpty.txt")) File.Delete("tempEmpty.txt");

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