

using System;
using System.ComponentModel;

namespace FileHelpers.Events
{
    /// <summary>Base class of <see cref="BeforeWriteRecordEventArgs&lt;T&gt;"/> and <see cref="AfterWriteRecordEventArgs&lt;T&gt;"/></summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class WriteRecordEventArgs<T> : EventArgs
	{
		internal WriteRecordEventArgs(T record, int lineNumber)
		{
			mRecord = record;
			mLineNumber = lineNumber;
		}

		private T mRecord;

		/// <summary>The current record.</summary>
		public T Record
		{
			get { return mRecord; }
		}

		private int mLineNumber;

		/// <summary>The current line number.</summary>
		public int LineNumber
		{
			get { return mLineNumber; }
		}

	}

	/// <summary>Arguments for the <see cref="BeforeWriteRecordHandler"/></summary>

    public sealed class BeforeWriteRecordEventArgs: BeforeWriteRecordEventArgs<object>
    {
        internal BeforeWriteRecordEventArgs(object record, int lineNumber)
			: base(record, lineNumber)
		{}

    }
	/// <summary>Arguments for the <see cref="BeforeWriteRecordHandler"/></summary>
    public class BeforeWriteRecordEventArgs<T>: WriteRecordEventArgs<T>
	{
		internal BeforeWriteRecordEventArgs(T record, int lineNumber)
			: base(record, lineNumber)
		{}

		private bool mSkipThisRecord = false;
		/// <summary>Set this property as true if you want to bypass the current record.</summary>
		public bool SkipThisRecord
		{
			get { return mSkipThisRecord; }
			set { mSkipThisRecord = value; }
		}

	}

	/// <summary>Arguments for the <see cref="AfterWriteRecordHandler"/></summary>
   	public sealed class AfterWriteRecordEventArgs: AfterWriteRecordEventArgs<object>
    {
        internal AfterWriteRecordEventArgs(object record, int lineNumber, string line)
            :base(record, lineNumber, line)
        { }
    }
	/// <summary>Arguments for the <see cref="AfterWriteRecordHandler"/></summary>
	public class AfterWriteRecordEventArgs<T>: WriteRecordEventArgs<T>
	{
		internal AfterWriteRecordEventArgs(T record, int lineNumber, string line): base(record, lineNumber)
		{
			mRecordLine = line;
		}

		private string mRecordLine;
		/// <summary>The line to be written to the file. WARNING: you can change this and the engines will write it to the file.</summary>
		public string RecordLine
		{
			get { return mRecordLine; }
			set { mRecordLine = value; }
		}

	}

}
