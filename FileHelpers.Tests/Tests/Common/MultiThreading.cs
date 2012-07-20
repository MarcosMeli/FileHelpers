using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;

namespace FileHelpers.Tests
{
    [TestFixture]
    public class MultiThreading
    {
        private ManualResetEvent flagStart;
        private CountdownEvent flagFinish;
        private Exception initializationException;

        /// <summary>
        /// Concurrency test to ensure two file helpers asyncs can run at the same time.
        /// </summary>
        [Test]
        public void AsyncEngineInitialization()
        {
            flagStart = new ManualResetEvent(false);
            flagFinish = new CountdownEvent(2);

            new Thread(InitializeAsyncEngineWhenFlagIsRaised).Start();
            new Thread(InitializeAsyncEngineWhenFlagIsRaised).Start();
            flagStart.Set();
            Console.WriteLine("Async set finished, waiting");
            flagFinish.Wait();

            if (initializationException != null)
                throw new ApplicationException("Failure during AsyncEngine initialization", initializationException);
        }

        /// <summary>
        /// Try and create async engine and grab an exception if one occurs
        /// </summary>
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
            finally
            {
                flagFinish.Signal();
            }
        }
    }
}