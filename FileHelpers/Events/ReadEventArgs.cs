using System;
using System.ComponentModel;

namespace FileHelpers.Events
{
    /// <summary>Base class of <see cref="BeforeReadEventArgs{T}<T>" /> and <see cref="AfterReadEventArgs{T}<T>"/></summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class ReadEventArgs<T>
            : FileHelpersEventArgs<T>
            where T : class
    {
        internal ReadEventArgs(EventEngineBase<T> engine, string line, int lineNumber)
            :base (engine, lineNumber)
        {
            RecordLineChanged = false;
            mRecordLine = line;
        }

        private string mRecordLine;

        /// <summary>The just read record line.</summary>
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