using System.Collections.Generic;
using System;

namespace FileHelpers
{
    /// <summary>
    /// Generic template of running a test with various operations
    /// already defined to keep the test executions way down.
    /// </summary>
    public abstract class FileTestBase
    {
        /// <summary>
        /// This will return the filename relative to the Data test data directory
        /// </summary>
        /// <returns></returns>
        protected abstract string GetFullPathName();

        /// <summary>
        /// This returns the complete path reading for reading (relative to Data directory).
        /// </summary>
        public string Path
        {
            get
            {
                return System.IO.Path.Combine(Tests.TestCommon.DataDirectory, GetFullPathName());
            }
        }

        /// <summary>
        /// Perform a function and return the result on the path
        /// </summary>
        /// <typeparam name="T">Type of data</typeparam>
        /// <param name="func">Function to execute</param>
        /// <returns>Type of T</returns>
        public T Execute<T>(Func<string, T> func)
        {
            return func(Path);
        }

        /// <summary>
        /// Perform a simple engine call on type T
        /// </summary>
        /// <typeparam name="T">Type of record to parse</typeparam>
        /// <returns>Parsed data</returns>
        public T[] ReadWithEngine<T>() where T : class
        {
            var engine = new FileHelperEngine<T>();
            return ReadWithEngine(engine);
        }

        /// <summary>
        /// Perform a simple engine call restricting number of record
        /// </summary>
        /// <typeparam name="T">Type of data to parse</typeparam>
        /// <param name="maxRecords">Number of records to process</param>
        /// <returns>Array of type T</returns>
        public T[] ReadWithEngine<T>(int maxRecords) where T : class
        {
            var engine = new FileHelperEngine<T>();
            return ReadWithEngine(engine, maxRecords);
        }

        /// <summary>
        /// array of data for a file
        /// </summary>
        /// <typeparam name="T">type or record</typeparam>
        /// <param name="engine">engine to process data</param>
        /// <returns>array of data</returns>
        public T[] ReadWithEngine<T>(FileHelperEngine<T> engine) where T : class
        {
            return engine.ReadFile(Path);
        }

        /// <summary>
        /// Read an array of data from a supplied engine
        /// </summary>
        /// <typeparam name="T">Type of record to parse</typeparam>
        /// <param name="engine">Engine to read record from</param>
        /// <param name="maxRecords">Maximum number of records</param>
        /// <returns>array of data</returns>
        public T[] ReadWithEngine<T>(FileHelperEngine<T> engine, int maxRecords) where T : class
        {
            return engine.ReadFile(Path, maxRecords);
        }

        /// <summary>
        /// Create the engine and begin the read to
        /// start IO running
        /// </summary>
        /// <typeparam name="T">Type of record data</typeparam>
        /// <returns>Engine ready to process data</returns>
        public FileHelperAsyncEngine<T> BeginRead<T>() where T : class
        {
            var engine = new FileHelperAsyncEngine<T>();
            engine.BeginReadFile(Path);
            return engine;
        }

        /// <summary>
        /// Read with asnc engine, creating the engine
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T[] ReadWithAsyncEngine<T>() where T : class
        {
            var engine = new FileHelperAsyncEngine<T>();
            return ReadWithAsyncEngine(engine);
        }

        /// <summary>
        /// Process a file with the async engine
        /// </summary>
        /// <typeparam name="T">Type of record to parse</typeparam>
        /// <param name="engine">Engine to use</param>
        /// <returns>Array of data</returns>
        public T[] ReadWithAsyncEngine<T>(FileHelperAsyncEngine<T> engine) where T : class
        {
            var res = new List<T>();

            using (engine.BeginReadFile(Path)) {
                while (engine.ReadNext() != null)
                    res.Add(engine.LastRecord);
            }

            return res.ToArray();
        }
    }
}
