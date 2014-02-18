using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Events
{
    /// <summary>Arguments for the <see cref="AfterWriteEventArgs{T}"/></summary>
    public sealed class AfterWriteEventArgs<T>
        : WriteEventArgs<T>
        where T : class
    {
        /// <summary>
        /// Record parsed after engine has finished
        /// </summary>
        /// <param name="engine">engine that created the record</param>
        /// <param name="record">object created</param>
        /// <param name="lineNumber">Record number of the record</param>
        /// <param name="line">LIne to be written</param>
        internal AfterWriteEventArgs(EventEngineBase<T> engine, T record, int lineNumber, string line)
            : base(engine, record, lineNumber)
        {
            RecordLine = line;
        }

        /// <summary>
        /// The line to be written to the destination.
        /// WARNING: you can change the line value and the engines will write it to the destination.
        /// </summary>
        public string RecordLine { get; set; }
    }
}