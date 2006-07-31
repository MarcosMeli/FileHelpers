using System;
using System.ComponentModel;

namespace FileHelpers
{

	/// <summary> </summary>
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

		/// <summary>The current line number.</summary>
		public int LineNumber
		{
			get { return mLineNumber; }
		}

		/// <summary>The just read read record line.</summary>
		public string RecordLine
		{
			get { return mRecordLine; }
		}

	}

	/// <summary>Arguments for the <see cref="BeforeReadRecordHandler"/></summary>
	public sealed class BeforeReadRecordEventArgs: ReadRecordEventArgs
	{
		internal BeforeReadRecordEventArgs(string line): this(line, -1)
		{}

		internal BeforeReadRecordEventArgs(string line, int lineNumber): base(line, lineNumber)
		{}

		private bool mSkipThisRecord = false;

		/// <summary>Set this property to true if you want want to bypass the current line.</summary>
		public bool SkipThisRecord
		{
			get { return mSkipThisRecord; }
			set { mSkipThisRecord = value; }
		}

	}

	/// <summary>Arguments for the <see cref="AfterReadRecordHandler"/></summary>
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

	}


}
