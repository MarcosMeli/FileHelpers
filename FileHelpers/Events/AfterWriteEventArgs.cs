using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Events
{
    /// <summary>Arguments for the <see cref="AfterWriteEventArgs{T}"/></summary>
    public class AfterWriteEventArgs
        : WriteEventArgs
    {
        /// <summary>
        /// Record parsed after engine has finished
        /// </summary>
        /// <param name="engine">engine that created the record</param>
        /// <param name="lineNumber">Record number of the record</param>
        /// <param name="line">LIne to be written</param>
        internal AfterWriteEventArgs(EngineBase engine, int lineNumber, string line)
            : base(engine, lineNumber)
        {
            RecordLine = line;
        }

        /// <summary>
        /// The line to be written to the destination.
        /// WARNING: you can change the line value and the engines will write it to the destination.
        /// </summary>
        public string RecordLine { get; set; }
        
    }

    /// <summary>Arguments for the <see cref="AfterWriteEventArgs{T}"/></summary>
    public sealed class AfterWriteEventArgs<T>
        : AfterWriteEventArgs
        where T : class
    {
        /// <summary>
        /// Record parsed after engine has finished
        /// </summary>
        /// <param name="engine">engine that created the record</param>
        /// <param name="record">object created</param>
        /// <param name="lineNumber">Record number of the record</param>
        /// <param name="line">LIne to be written</param>
        internal AfterWriteEventArgs(EngineBase engine, T record, int lineNumber, string line)
            : base(engine, lineNumber, line)
        {
            Record = record;
        }


        /// <summary>The current record.</summary>
        public T Record { get; private set; }
    }
}