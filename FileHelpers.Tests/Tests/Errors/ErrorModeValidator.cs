using System.IO;
using NUnit.Framework;

namespace FileHelpers.Tests.Errors
{
    [TestFixture]
    public class ErrorModeValidator
    {
        private FileHelperEngine<SampleType> mEngine;

        [SetUp]
        public void Setup()
        {
            mEngine = new FileHelperEngine<SampleType>();
        }

        [Test]
        public void IgnoreAndContinue()
        {
            mEngine.ErrorManager.ErrorMode = ErrorMode.IgnoreAndContinue;
            TestCommon.ReadTest(mEngine, "Bad", "BadDate1.txt");
            Assert.AreEqual(0, mEngine.ErrorManager.ErrorCount);
        }

        [Test]
        public void Constructors()
        {
            var err = new ErrorManager(ErrorMode.IgnoreAndContinue);
            Assert.AreEqual(ErrorMode.IgnoreAndContinue, err.ErrorMode);
            Assert.AreEqual(0, err.ErrorCount);
            Assert.AreEqual(0, err.Errors.Length);
        }

        [Test]
        public void Constructors2()
        {
            var err = new ErrorManager();
            Assert.AreEqual(ErrorMode.ThrowException, err.ErrorMode);
            Assert.AreEqual(0, err.ErrorCount);
            Assert.AreEqual(0, err.Errors.Length);
        }

        [Test]
        public void SaveAndContinue()
        {
            mEngine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            Assert.AreEqual(3, TestCommon.ReadTest(mEngine, "Bad", "BadDate1.txt").Length);

            Assert.AreEqual(4, mEngine.TotalRecords);

            Assert.AreEqual(1, mEngine.ErrorManager.ErrorCount);

            Assert.AreEqual(typeof (ConvertException), mEngine.ErrorManager.Errors[0].ExceptionInfo.GetType());
            Assert.AreEqual(2, mEngine.ErrorManager.Errors[0].LineNumber);
        }

        [Test]
        public void SaveAndContinue2()
        {
            var engine = new FileHelperEngine<SampleTypeIgnoreFirst>();

            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            Assert.AreEqual(3, TestCommon.ReadTest(engine, "Bad", "BadDate1Ignore.txt").Length);

            Assert.AreEqual(4, engine.TotalRecords);

            Assert.AreEqual(1, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());
            Assert.AreEqual(3, engine.ErrorManager.Errors[0].LineNumber);
        }


        [Test]
        public void SaveAndContinue3()
        {
            var engine = new FileHelperEngine<SampleTypeIgnoreFirstLast>();

            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            Assert.AreEqual(3,
                TestCommon.ReadTest(engine, "Bad", "BadDate1IgnoreLast.txt").Length);

            Assert.AreEqual(4, engine.TotalRecords);

            Assert.AreEqual(1, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());
            Assert.AreEqual(4, engine.ErrorManager.Errors[0].LineNumber);
        }

        [Test]
        public void SaveAndContinue4()
        {
            var engine = new FileHelperEngine<SampleTypeIgnoreFirstLast>();

            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            Assert.AreEqual(3,
                TestCommon.ReadTest(engine, "Bad", "BadDate1IgnoreLast2.txt").Length);

            Assert.AreEqual(4, engine.TotalRecords);

            Assert.AreEqual(1, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());
            Assert.AreEqual(6, engine.ErrorManager.Errors[0].LineNumber);
        }

        [Test]
        public void ThrowException()
        {
            mEngine.ErrorManager.ErrorMode = ErrorMode.ThrowException;
            Assert.Throws<ConvertException>(()
                => TestCommon.ReadTest(mEngine, "Bad", "BadDate1.txt"));
        }

        [Test]
        public void AllBad()
        {
            mEngine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            Assert.AreEqual(0, TestCommon.ReadTest(mEngine, "Bad", "BadAll1.txt").Length);
            Assert.AreEqual(4, mEngine.ErrorManager.Errors.Length);
            Assert.AreEqual(typeof (ConvertException), mEngine.ErrorManager.Errors[0].ExceptionInfo.GetType());
        }

        [Test]
        public void ErrorManagerEnumerable()
        {
            mEngine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            Assert.AreEqual(0, TestCommon.ReadTest(mEngine, "Bad", "BadAll1.txt").Length);

            int i = 0;
            foreach (ErrorInfo info in mEngine.ErrorManager) {
                i++;
                Assert.IsNotNull(info);
            }
            Assert.AreEqual(4, i);
            Assert.AreEqual(typeof (ConvertException), mEngine.ErrorManager.Errors[0].ExceptionInfo.GetType());
        }

        [Test]
        public void ErrorsOrder()
        {
            mEngine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            Assert.AreEqual(0, TestCommon.ReadTest(mEngine, "Bad", "BadAll1.txt").Length);
            Assert.AreEqual(4, mEngine.ErrorManager.ErrorCount);
            Assert.AreEqual(typeof (ConvertException), mEngine.ErrorManager.Errors[0].ExceptionInfo.GetType());

            Assert.AreEqual(1, mEngine.ErrorManager.Errors[0].LineNumber);
            Assert.AreEqual(2, mEngine.ErrorManager.Errors[1].LineNumber);
            Assert.AreEqual(3, mEngine.ErrorManager.Errors[2].LineNumber);
            Assert.AreEqual(4, mEngine.ErrorManager.Errors[3].LineNumber);
        }

        [Test]
        public void RecordString()
        {
            mEngine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            Assert.AreEqual(0, TestCommon.ReadTest(mEngine, "Bad", "BadAll1.txt").Length);
            Assert.AreEqual(4, mEngine.ErrorManager.ErrorCount);
            Assert.AreEqual(typeof (ConvertException), mEngine.ErrorManager.Errors[0].ExceptionInfo.GetType());


            Assert.AreEqual("30022005001234", mEngine.ErrorManager.Errors[0].RecordString);
            Assert.AreEqual("01012005001abc", mEngine.ErrorManager.Errors[1].RecordString);
            Assert.AreEqual("99999999123456", mEngine.ErrorManager.Errors[2].RecordString);
            Assert.AreEqual("00002005234567", mEngine.ErrorManager.Errors[3].RecordString);
        }

        [Test]
        public void SaveToFile()
        {
            mEngine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            Assert.AreEqual(3, TestCommon.ReadTest(mEngine, "Bad", "BadDate1.txt").Length);
            Assert.AreEqual(1, mEngine.ErrorManager.ErrorCount);
            Assert.AreEqual(typeof (ConvertException), mEngine.ErrorManager.Errors[0].ExceptionInfo.GetType());
            Assert.AreEqual(2, mEngine.ErrorManager.Errors[0].LineNumber);

            var filename = TestCommon.GetTempFile("SavedErrors.txt");

            mEngine.ErrorManager.SaveErrors(filename);

            var e2 = new FileHelperEngine<ErrorInfo>();

            ErrorInfo[] errors = e2.ReadFile(filename);

            Assert.AreEqual(mEngine.ErrorManager.ErrorCount, errors.Length);
            File.Delete(filename);
        }

        [Test]
        public void SaveToFile2()
        {
            mEngine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            Assert.AreEqual(3, TestCommon.ReadTest(mEngine, "Bad", "BadDate1.txt").Length);
            Assert.AreEqual(1, mEngine.ErrorManager.ErrorCount);
            Assert.AreEqual(typeof (ConvertException), mEngine.ErrorManager.Errors[0].ExceptionInfo.GetType());
            Assert.AreEqual(2, mEngine.ErrorManager.Errors[0].LineNumber);

            var filename = TestCommon.GetTempFile("SavedErrors.txt");
            mEngine.ErrorManager.SaveErrors(filename);
            Assert.AreEqual(mEngine.ErrorManager.ErrorCount, ErrorManager.LoadErrors(filename).Length);
            File.Delete(filename);
        }
    }
}