using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace FileHelpers.Tests.Errors
{
	[TestFixture]
	public class ErrorModeValidatorAsync
	{
		FileHelperAsyncEngine engine;

		[SetUp]
		public void Setup()
		{
			engine = new FileHelperAsyncEngine(typeof (SampleType));
		}

		[Test]
		public void IgnoreAndContinue()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.IgnoreAndContinue;
			TestCommon.ReadAllAsync(engine, "Bad", "BadDate1.txt");
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}


		[Test]
		public void SaveAndContinue()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            Assert.AreEqual(3, TestCommon.ReadAllAsync(engine, "Bad", "BadDate1.txt").Length);

            Assert.AreEqual(4, engine.TotalRecords);
            
            Assert.AreEqual(1, engine.ErrorManager.ErrorCount);
            
            Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());
			Assert.AreEqual(2, engine.ErrorManager.Errors[0].LineNumber);
		}

        [Test]
        public void SaveAndContinue2()
        {
            engine = new FileHelperAsyncEngine(typeof(SampleTypeIgnoreFirst));

            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            Assert.AreEqual(3, TestCommon.ReadAllAsync(engine, "Bad", "BadDate1Ignore.txt").Length);

            Assert.AreEqual(4, engine.TotalRecords);

            Assert.AreEqual(1, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(typeof(ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());
            Assert.AreEqual(3, engine.ErrorManager.Errors[0].LineNumber);
        }

        [Test]
		public void ThrowException()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.ThrowException;
        	Assert.Throws<ConvertException>(()
                => TestCommon.ReadAllAsync(engine, "Bad", "BadDate1.txt"));
		}

		[Test]
		public void AllBad()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
			Assert.AreEqual(0, TestCommon.ReadAllAsync(engine, "Bad", "BadAll1.txt").Length);
			Assert.AreEqual(4, engine.ErrorManager.Errors.Length);
			Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());
		}


		[Test]
		public void ErrorsOrder()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
			Assert.AreEqual(0, TestCommon.ReadAllAsync(engine, "Bad", "BadAll1.txt").Length);
			Assert.AreEqual(4, engine.ErrorManager.ErrorCount);
			Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());

			Assert.AreEqual(1, engine.ErrorManager.Errors[0].LineNumber);
			Assert.AreEqual(2, engine.ErrorManager.Errors[1].LineNumber);
			Assert.AreEqual(3, engine.ErrorManager.Errors[2].LineNumber);
			Assert.AreEqual(4, engine.ErrorManager.Errors[3].LineNumber);
		}

		[Test]
		public void RecordString()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
			Assert.AreEqual(0, TestCommon.ReadAllAsync(engine, "Bad", "BadAll1.txt").Length);
			Assert.AreEqual(4, engine.ErrorManager.ErrorCount);
			Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());


			Assert.AreEqual("30022005001234", engine.ErrorManager.Errors[0].RecordString);
			Assert.AreEqual("01012005001abc", engine.ErrorManager.Errors[1].RecordString);
			Assert.AreEqual("99999999123456", engine.ErrorManager.Errors[2].RecordString);
			Assert.AreEqual("00002005234567", engine.ErrorManager.Errors[3].RecordString);
		}

		[Test]
		public void SaveToFile()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
			Assert.AreEqual(3, TestCommon.ReadAllAsync(engine, "Bad", "BadDate1.txt").Length);
			Assert.AreEqual(1, engine.ErrorManager.ErrorCount);
			Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());
			Assert.AreEqual(2, engine.ErrorManager.Errors[0].LineNumber);

            var filename = TestCommon.GetTempFile("SavedErrors.txt");

			engine.ErrorManager.SaveErrors(filename);

			var e2 = new FileHelperEngine<ErrorInfo>();

			ErrorInfo[] errors = e2.ReadFile(filename);

			Assert.AreEqual(engine.ErrorManager.ErrorCount, errors.Length);
            File.Delete(filename);

		}

		[Test]
		public void SaveToFile2()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
			Assert.AreEqual(3, TestCommon.ReadAllAsync(engine, "Bad", "BadDate1.txt").Length);
			Assert.AreEqual(1, engine.ErrorManager.ErrorCount);
			Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());
			Assert.AreEqual(2, engine.ErrorManager.Errors[0].LineNumber);

            var filename = TestCommon.GetTempFile("SavedErrors.txt");

			engine.ErrorManager.SaveErrors(filename);
			Assert.AreEqual(engine.ErrorManager.ErrorCount, ErrorManager.LoadErrors(filename).Length);
            File.Delete(filename);
		}


        [Test]
        public void BeginReadWhileWriting()
        {
            var eng = new FileHelperAsyncEngine(typeof(SampleType));

            var filename = TestCommon.GetTempFile("TempWrite.txt");

            Assert.Throws<BadUsageException>(()
                                             =>
                                                 {
                                                     try
                                                     {
                                                         eng.BeginWriteFile(filename);
                                                         eng.BeginReadString("jejjeje");
                                                     }
                                                     finally
                                                     {
                                                         eng.Close();
                                                         if (File.Exists(filename))
                                                             File.Delete(filename);
                                                     }
                                                 });
        }

        [Test]
        public void BeginWriteWhileReading()
        {
            var eng = new FileHelperAsyncEngine(typeof(SampleType));
            eng.BeginReadString("jejjeje");

            var filename = TestCommon.GetTempFile("TempWrite.txt");
            Assert.Throws<BadUsageException>(()
                => eng.BeginWriteFile(filename));
        }

	}
}