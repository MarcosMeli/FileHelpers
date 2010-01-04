using System;
using System.IO;
using FileHelpers;
using FileHelpers.Events;
using NUnit.Framework;

namespace FileHelpers.Tests.CommonTests
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

			engine.Progress += ProgressChange;

			SampleType[] res;
			res = (SampleType[]) TestCommon.ReadTest(engine, "Good", "Test1.txt");

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
            engine.Progress += ProgressChange;

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
			actualPerc = 0;
			actualAdd = 50;

			engine = new FileHelperEngine(typeof (SampleType));
            engine.Progress += ProgressChangePercent;

			var res = new SampleType[2];

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

        double actualPerc = 0;

		private void ProgressChange(object sender, ProgressEventArgs e)
		{
			Assert.AreEqual(actual, e.CurrentRecord);
			actual += actualAdd;
		}
        
        private void ProgressChangePercent(object sender, ProgressEventArgs e)
        {
            Assert.AreEqual(actualPerc, e.Percent);
            actualPerc += actualAdd;
        }

	}
}