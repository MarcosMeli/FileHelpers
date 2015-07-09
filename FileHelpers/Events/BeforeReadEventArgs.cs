using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Events
{

    /// <summary>Arguments for the <see cref="BeforeReadHandler{T}"/></summary>
    public class BeforeReadEventArgs
        : ReadEventArgs
    {
        /// <summary>
        /// Record before being parsed by the engine
        /// </summary>
        /// <param name="engine">Engine that will analyse the record</param>
        /// <param name="line">Record read from the source</param>
        /// <param name="lineNumber">record number read</param>
        internal BeforeReadEventArgs(EngineBase engine, string line, int lineNumber)
            : base(engine, line, lineNumber)
        {
            SkipThisRecord = false;
        }


    }
    /// <summary>Arguments for the <see cref="BeforeReadHandler{T}"/></summary>
    public sealed class BeforeReadEventArgs<T>
        : BeforeReadEventArgs
        where T : class
    {
        /// <summary>
        /// Record before being parsed by the engine
        /// </summary>
        /// <param name="engine">Engine that will analyse the record</param>
        /// <param name="record">Object to be created</param>
        /// <param name="line">Record read from the source</param>
        /// <param name="lineNumber">record number read</param>
        internal BeforeReadEventArgs(EngineBase engine, T record, string line, int lineNumber)
            : base(engine, line, lineNumber)
        {
            Record = record;
        }

        /// <summary>The current record that was just assigned not yet filled</summary>
        public T Record { get; private set; }
     
    }
}