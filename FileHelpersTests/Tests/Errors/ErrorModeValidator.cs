using FileHelpers;
using NUnit.Framework;

namespace FileHelpersTests.Errors
{
	[TestFixture]
	public class ErrorModeValidator
	{
		FileHelperEngine engine;

		[SetUp]
		public void Setup()
		{
			engine = new FileHelperEngine(typeof (SampleType));
		}

		[Test]
		public void IgnoreAndContinue()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.IgnoreAndContinue;
			TestCommon.ReadTest(engine, @"Bad\BadDate1.txt");
			Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
		}

		[Test]
		public void Constructors()
		{
			ErrorManager err = new ErrorManager(ErrorMode.IgnoreAndContinue);
			Assert.AreEqual(ErrorMode.IgnoreAndContinue, err.ErrorMode);
			Assert.AreEqual(0, err.ErrorCount);
			Assert.AreEqual(0, err.LastErrors.Length);
		}

		[Test]
		public void Constructors2()
		{
			ErrorManager err = new ErrorManager();
			Assert.AreEqual(ErrorMode.ThrowException, err.ErrorMode);
			Assert.AreEqual(0, err.ErrorCount);
			Assert.AreEqual(0, err.LastErrors.Length);
		}

		[Test]
		public void SaveAndContinue()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            Assert.AreEqual(3, TestCommon.ReadTest(engine, @"Bad\BadDate1.txt").Length);

            Assert.AreEqual(4, engine.TotalRecords);
            
            Assert.AreEqual(1, engine.ErrorManager.ErrorCount);
            
            Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.LastErrors[0].ExceptionInfo.GetType());
			Assert.AreEqual(2, engine.ErrorManager.LastErrors[0].LineNumber);
		}

        [Test]
        public void SaveAndContinue2()
        {
            engine = new FileHelperEngine(typeof(SampleTypeIgnoreFirst));

            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            Assert.AreEqual(3, TestCommon.ReadTest(engine, @"Bad\BadDate1Ignore.txt").Length);

            Assert.AreEqual(4, engine.TotalRecords);

            Assert.AreEqual(1, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(typeof(ConvertException), engine.ErrorManager.LastErrors[0].ExceptionInfo.GetType());
            Assert.AreEqual(3, engine.ErrorManager.LastErrors[0].LineNumber);
        }

        [Test]
		[ExpectedException(typeof (ConvertException))]
		public void ThrowException()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.ThrowException;
			TestCommon.ReadTest(engine, @"Bad\BadDate1.txt");
		}

		[Test]
		public void AllBad()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
			Assert.AreEqual(0, TestCommon.ReadTest(engine, @"Bad\BadAll1.txt").Length);
			Assert.AreEqual(4, engine.ErrorManager.LastErrors.Length);
			Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.LastErrors[0].ExceptionInfo.GetType());
		}


		[Test]
		public void ErrorsOrder()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
			Assert.AreEqual(0, TestCommon.ReadTest(engine, @"Bad\BadAll1.txt").Length);
			Assert.AreEqual(4, engine.ErrorManager.ErrorCount);
			Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.LastErrors[0].ExceptionInfo.GetType());

			Assert.AreEqual(1, engine.ErrorManager.LastErrors[0].LineNumber);
			Assert.AreEqual(2, engine.ErrorManager.LastErrors[1].LineNumber);
			Assert.AreEqual(3, engine.ErrorManager.LastErrors[2].LineNumber);
			Assert.AreEqual(4, engine.ErrorManager.LastErrors[3].LineNumber);
		}

		[Test]
		public void RecordString()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
			Assert.AreEqual(0, TestCommon.ReadTest(engine, @"Bad\BadAll1.txt").Length);
			Assert.AreEqual(4, engine.ErrorManager.ErrorCount);
			Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.LastErrors[0].ExceptionInfo.GetType());


			Assert.AreEqual("30022005001234", engine.ErrorManager.LastErrors[0].RecordString);
			Assert.AreEqual("01012005001abc", engine.ErrorManager.LastErrors[1].RecordString);
			Assert.AreEqual("99999999123456", engine.ErrorManager.LastErrors[2].RecordString);
			Assert.AreEqual("00002005234567", engine.ErrorManager.LastErrors[3].RecordString);
		}

		[Test]
		public void SaveToFile()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
			Assert.AreEqual(3, TestCommon.ReadTest(engine, @"Bad\BadDate1.txt").Length);
			Assert.AreEqual(1, engine.ErrorManager.ErrorCount);
			Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.LastErrors[0].ExceptionInfo.GetType());
			Assert.AreEqual(2, engine.ErrorManager.LastErrors[0].LineNumber);

			engine.ErrorManager.SaveErrors(@"C:\SavedErrors.txt");

			FileHelperEngine e2 = new FileHelperEngine(typeof(ErrorInfo));

			ErrorInfo[] errors = (ErrorInfo[]) e2.ReadFile(@"C:\SavedErrors.txt");

			Assert.AreEqual(engine.ErrorManager.ErrorCount, errors.Length);
		}

		[Test]
		public void SaveToFile2()
		{
			engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
			Assert.AreEqual(3, TestCommon.ReadTest(engine, @"Bad\BadDate1.txt").Length);
			Assert.AreEqual(1, engine.ErrorManager.ErrorCount);
			Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.LastErrors[0].ExceptionInfo.GetType());
			Assert.AreEqual(2, engine.ErrorManager.LastErrors[0].LineNumber);

			engine.ErrorManager.SaveErrors(@"C:\SavedErrors.txt");

			ErrorManager manager = new ErrorManager(@"C:\SavedErrors.txt");

			Assert.AreEqual(engine.ErrorManager.ErrorCount, manager.ErrorCount);
		}


	}
}