using System;
using System.Threading;
using FileHelpers;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    [TestFixture]
    public class MultiThreading
    {
        private ManualResetEvent flagStart;
        private CountdownEvent flagFinish;
        private Exception initializationException;

        [Test]
        public void AsyncEngineInitialization()
        {
            flagStart = new ManualResetEvent(false);
            flagFinish = new CountdownEvent(2);

            new Thread(InitializeAsyncEngineWhenFlagIsRaised).Start();
            new Thread(InitializeAsyncEngineWhenFlagIsRaised).Start();

            flagStart.Set();
            flagFinish.Wait();

            if (initializationException != null) throw new ApplicationException("Failure during AsyncEngine initialization", initializationException);
        }

        private void InitializeAsyncEngineWhenFlagIsRaised()
        {
            flagStart.WaitOne();
            try
            {
                new FileHelperAsyncEngine<SampleType>();
            }
            catch (Exception e)
            {
                initializationException = e;
            }
            flagFinish.Decrement();
        }

    }
}