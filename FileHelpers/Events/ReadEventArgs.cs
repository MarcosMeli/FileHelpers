using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace FileHelpers.Events
{
    /// <summary>
    /// Base class of 
    /// <see cref="BeforeReadEventArgs"/>
    /// and 
    /// <see cref="AfterReadEventArgs"/>
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class ReadEventArgs
        : FileHelpersEventArgs
    {
        /// <summary>
        /// Create a read event argument, contains line number and record read
        /// </summary>
        /// <param name="engine">Engine used to parse data</param>
        /// <param name="line">record to be analysed</param>
        /// <param name="lineNumber">record count read</param>
        internal ReadEventArgs(EngineBase engine, string line, int lineNumber)
            : base(engine, lineNumber)
        {
            RecordLineChanged = false;
            mRecordLine = line;
        }

        string mRecordLine;

        /// <summary>The record line just read.</summary>
        public string RecordLine
        {
            get { return mRecordLine; }
            set
            {
                if (mRecordLine == value)
                    return;

                mRecordLine = value;
                RecordLineChanged = true;
            }
        }

        /// <summary>Whether the RecordLine property has been written-to.</summary>
        public bool RecordLineChanged { get; protected set; }

        /// <summary>Set this property to true if you want to bypass the current record.</summary>
        public bool SkipThisRecord { get; set; }
    }
}