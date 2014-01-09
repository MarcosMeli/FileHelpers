using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Events
{
    /// <summary>Arguments for the <see cref="AfterReadHandler{T}"/></summary>
    public sealed class AfterReadEventArgs<T>
        : ReadEventArgs<T>
        where T : class
    {
        //internal AfterReadEventArgs(EventEngineBase<T> engine, string line, bool lineChanged, T newRecord)
        //    : this(engine, line, lineChanged, newRecord, -1)
        //{}

        /// <summary>
        /// After the record is read,  allow details to be inspected.
        /// </summary>
        /// <param name="engine">Engine that parsed the record</param>
        /// <param name="line">Record that was analysed</param>
        /// <param name="lineChanged">Was it changed before</param>
        /// <param name="newRecord">Object created</param>
        /// <param name="lineNumber">Record number read</param>
        internal AfterReadEventArgs(EventEngineBase<T> engine,
            string line,
            bool lineChanged,
            T newRecord,
            int lineNumber)
            : base(engine, line, lineNumber)
        {
            SkipThisRecord = false;
            Record = newRecord;
            RecordLineChanged = lineChanged;
        }

        /// <summary>The current record.</summary>
        public T Record { get; set; }
    }
}