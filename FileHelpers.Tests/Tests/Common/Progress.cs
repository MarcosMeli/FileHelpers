using System;
using System.IO;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.CommonTests
{
	[TestFixture]
	public class Progress
	{
		FileHelperEngine engine;

		[Test]
		public void ReadFileNotifyRecord()
		{
			actual = 0;
			actualAdd = 1;
			engine = new FileHelperEngine(typeof (SampleType));

			engine.SetProgressHandler(new ProgressChangeHandler(ProgressChange));

			SampleType[] res;
			res = (SampleType[]) TestCommon.ReadTest(engine, @"Good\test1.txt");

			Assert.AreEqual(4, res.Length);
			Assert.AreEqual(4, engine.TotalRecords);
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}
		

		[Test]
		public void WriteFileNotifyRecord()
		{
			actual = 0;
			actualAdd = 1; 
			engine = new FileHelperEngine(typeof (SampleType));
			engine.SetProgressHandler(new ProgressChangeHandler(ProgressChange));

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			engine.WriteFile("prog1.txt", res);
			if (File.Exists("prog1.txt")) File.Delete("prog1.txt");
		}



		
		[Test]
		public void WriteFileNotifyPercent()
		{
			actual = 0;
			actualAdd = 25;

			engine = new FileHelperEngine(typeof (SampleType));
			engine.SetProgressHandler(new ProgressChangeHandler(ProgressChange), ProgressMode.NotifyPercent);

			SampleType[] res = new SampleType[2];

			res[0] = new SampleType();
			res[1] = new SampleType();

			res[0].Field1 = DateTime.Now.AddDays(1);
			res[0].Field2 = "je";
			res[0].Field3 = 0;

			res[1].Field1 = DateTime.Now;
			res[1].Field2 = "ho";
			res[1].Field3 = 2;

			engine.WriteFile("prog1.txt", res);
			if (File.Exists("prog1.txt")) File.Delete("prog1.txt");
		}


		int actual = 0;
		int actualAdd = 0;

		private void ProgressChange(ProgressEventArgs e)
		{
			Assert.AreEqual(actual, e.ProgressCurrent);
			actual += actualAdd;
		}


	}
}