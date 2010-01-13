

using System;
using System.ComponentModel;

namespace FileHelpers.Events
{

    /// <summary>Base class of <see cref="BeforeReadRecordEventArgs"/> and <see cref="AfterReadRecordEventArgs"/></summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class ReadRecordEventArgs: EventArgs
	{
		internal ReadRecordEventArgs(string line, int lineNumber)
		{
		    RecordLineChanged = false;
		    mRecordLine = line;
			LineNumber = lineNumber;
		}

		private string mRecordLine;

        /// <summary>The current line number.</summary>
        public int LineNumber { get; private set; }

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

	
    /// <summary>Arguments for the <see cref="BeforeReadRecordHandler"/></summary>
    public sealed class BeforeReadRecordEventArgs<T> : ReadRecordEventArgs
	{
		internal BeforeReadRecordEventArgs(string line): this(line, -1)
		{}

		internal BeforeReadRecordEventArgs(string line, int lineNumber): base(line, lineNumber)
		{
		    SkipThisRecord = false;
		}

	}


    /// <summary>Arguments for the <see cref="AfterReadRecordHandler"/></summary>
    public sealed class AfterReadRecordEventArgs<T> : ReadRecordEventArgs
	{
        internal AfterReadRecordEventArgs(string line, bool lineChanged, T newRecord)
            : this(line, lineChanged, newRecord, -1)
		{}

        internal AfterReadRecordEventArgs(string line, bool lineChanged, T newRecord, int lineNumber)
            : base(line, lineNumber)
		{
		    SkipThisRecord = false;
		    Record = newRecord;
            RecordLineChanged = lineChanged;
		}

        /// <summary>The current record.</summary>
        public T Record { get; set; }

	}


}
