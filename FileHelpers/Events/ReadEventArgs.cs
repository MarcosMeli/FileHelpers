#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.ComponentModel;

namespace FileHelpers
{

    /// <summary>Base class of <see cref="BeforeReadRecordEventArgs"/> and <see cref="AfterReadRecordEventArgs"/></summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class ReadRecordEventArgs: EventArgs
	{
		internal ReadRecordEventArgs(string line, int lineNumber)
		{
			mRecordLine = line;
			mLineNumber = lineNumber;
		}

		private string mRecordLine;

		private int mLineNumber;

        private bool mRecordLineChanged = false;

		/// <summary>The current line number.</summary>
		public int LineNumber
		{
			get { return mLineNumber; }
		}

		/// <summary>The just read record line.</summary>
		public string RecordLine
		{
			get { return mRecordLine; }
            set 
            { 
                mRecordLine = value;
                mRecordLineChanged = true; 
            }
		}

        /// <summary>Whether the RecordLine property has been written-to.</summary>
        public bool RecordLineChanged
        {
            get { return mRecordLineChanged; }
        }
	}

	/// <summary>Arguments for the <see cref="BeforeReadRecordHandler"/></summary>
#if NET_1_1
	public sealed class BeforeReadRecordEventArgs: ReadRecordEventArgs
#else
    public sealed class BeforeReadRecordEventArgs: BeforeReadRecordEventArgs<object>
    {
        internal BeforeReadRecordEventArgs(string line)
            : this(line, -1)
        { }

        internal BeforeReadRecordEventArgs(string line, int lineNumber)
            : base(line, lineNumber)
        { }
    }

    /// <summary>Arguments for the <see cref="BeforeReadRecordHandler"/></summary>
    public class BeforeReadRecordEventArgs<T> : ReadRecordEventArgs
#endif
	{
		internal BeforeReadRecordEventArgs(string line): this(line, -1)
		{}

		internal BeforeReadRecordEventArgs(string line, int lineNumber): base(line, lineNumber)
		{}

		private bool mSkipThisRecord = false;

		/// <summary>Set this property to true if you want to bypass the current line.</summary>
		public bool SkipThisRecord
		{
			get { return mSkipThisRecord; }
			set { mSkipThisRecord = value; }
		}

	}

	/// <summary>Arguments for the <see cref="AfterReadRecordHandler"/></summary>
#if NET_1_1
	public sealed class AfterReadRecordEventArgs: ReadRecordEventArgs
	{
		internal AfterReadRecordEventArgs(string line, object newRecord): this(line, newRecord, -1)
		{}

		internal AfterReadRecordEventArgs(string line, object newRecord, int lineNumber): base(line, lineNumber)
		{
			mRecord = newRecord;
		}

		private object mRecord;

		/// <summary>The current record.</summary>
		public object Record
		{
			get { return mRecord; }
			set { mRecord = value; }
		}

#else
    public sealed class AfterReadRecordEventArgs: AfterReadRecordEventArgs<object>
    {
        internal AfterReadRecordEventArgs(string line, object newRecord)
            : this(line, newRecord, -1)
        { }

        internal AfterReadRecordEventArgs(string line, object newRecord, int lineNumber)
            : base(line, newRecord, lineNumber)
        {
        }
    }

    /// <summary>Arguments for the <see cref="AfterReadRecordHandler"/></summary>
    public class AfterReadRecordEventArgs<T> : ReadRecordEventArgs
	{
		internal AfterReadRecordEventArgs(string line, T newRecord): this(line, newRecord, -1)
		{}

		internal AfterReadRecordEventArgs(string line, T newRecord, int lineNumber): base(line, lineNumber)
		{
			mRecord = newRecord;
		}

		private T mRecord;

		/// <summary>The current record.</summary>
		public T Record
		{
			get { return mRecord; }
			set { mRecord = value; }
		}
#endif


		private bool mSkipThisRecord = false;

		/// <summary>Set this property to true if you want to bypass the current record.</summary>
		public bool SkipThisRecord
		{
			get { return mSkipThisRecord; }
			set { mSkipThisRecord = value; }
		}

	}


}
