using System.IO;
using NUnit.Framework;

namespace FileHelpers.Tests.Errors
{
    [TestFixture]
    public class ErrorModeValidator
    {
        private FileHelperEngine<SampleType> engine;

        [SetUp]
        public void Setup()
        {
            engine = new FileHelperEngine<SampleType>();
        }

        [Test]
        public void IgnoreAndContinue()
        {
            engine.ErrorManager.ErrorMode = ErrorMode.IgnoreAndContinue;
            TestCommon.ReadTest<SampleType>(engine, "Bad", "BadDate1.txt");
            Assert.AreEqual(0, engine.ErrorManager.ErrorCount);
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
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            Assert.AreEqual(3, TestCommon.ReadTest(engine, "Bad", "BadDate1.txt").Length);

            Assert.AreEqual(4, engine.TotalRecords);

            Assert.AreEqual(1, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());
            Assert.AreEqual(2, engine.ErrorManager.Errors[0].LineNumber);
        }

        [Test]
        public void SaveAndContinue2()
        {
            var engine = new FileHelperEngine<SampleTypeIgnoreFirst>();

            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

            Assert.AreEqual(3, TestCommon.ReadTest<SampleTypeIgnoreFirst>(engine, "Bad", "BadDate1Ignore.txt").Length);

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
                TestCommon.ReadTest<SampleTypeIgnoreFirstLast>(engine, "Bad", "BadDate1IgnoreLast.txt").Length);

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
                TestCommon.ReadTest<SampleTypeIgnoreFirstLast>(engine, "Bad", "BadDate1IgnoreLast2.txt").Length);

            Assert.AreEqual(4, engine.TotalRecords);

            Assert.AreEqual(1, engine.ErrorManager.ErrorCount);

            Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());
            Assert.AreEqual(6, engine.ErrorManager.Errors[0].LineNumber);
        }

        [Test]
        public void ThrowException()
        {
            engine.ErrorManager.ErrorMode = ErrorMode.ThrowException;
            Assert.Throws<ConvertException>(()
                => TestCommon.ReadTest(engine, "Bad", "BadDate1.txt"));
        }

        [Test]
        public void AllBad()
        {
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            Assert.AreEqual(0, TestCommon.ReadTest(engine, "Bad", "BadAll1.txt").Length);
            Assert.AreEqual(4, engine.ErrorManager.Errors.Length);
            Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());
        }

        [Test]
        public void ErrorManagerEnumerable()
        {
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            Assert.AreEqual(0, TestCommon.ReadTest(engine, "Bad", "BadAll1.txt").Length);

            int i = 0;
            foreach (ErrorInfo info in engine.ErrorManager) {
                i++;
                Assert.IsNotNull(info);
            }
            Assert.AreEqual(4, i);
            Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());
        }

        [Test]
        public void ErrorsOrder()
        {
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            Assert.AreEqual(0, TestCommon.ReadTest(engine, "Bad", "BadAll1.txt").Length);
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
            Assert.AreEqual(0, TestCommon.ReadTest(engine, "Bad", "BadAll1.txt").Length);
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
            Assert.AreEqual(3, TestCommon.ReadTest(engine, "Bad", "BadDate1.txt").Length);
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
            Assert.AreEqual(3, TestCommon.ReadTest(engine, "Bad", "BadDate1.txt").Length);
            Assert.AreEqual(1, engine.ErrorManager.ErrorCount);
            Assert.AreEqual(typeof (ConvertException), engine.ErrorManager.Errors[0].ExceptionInfo.GetType());
            Assert.AreEqual(2, engine.ErrorManager.Errors[0].LineNumber);

            var filename = TestCommon.GetTempFile("SavedErrors.txt");
            engine.ErrorManager.SaveErrors(filename);
            Assert.AreEqual(engine.ErrorManager.ErrorCount, ErrorManager.LoadErrors(filename).Length);
            File.Delete(filename);
        }
    }
}