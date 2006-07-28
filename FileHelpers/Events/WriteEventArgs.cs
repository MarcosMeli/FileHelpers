using System;
using System.ComponentModel;

namespace FileHelpers
{

	[EditorBrowsable(EditorBrowsableState.Never)]
	/// <summary></summary>
	public abstract class WriteRecordEventArgs: EventArgs
	{
		internal WriteRecordEventArgs(object record, int lineNumber)
		{
			mRecord = record;
			mLineNumber = lineNumber;
		}

		private object mRecord;

		/// <summary>The current record.</summary>
		public object Record
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
	public sealed class BeforeWriteRecordEventArgs: WriteRecordEventArgs
	{

		internal BeforeWriteRecordEventArgs(object record, int lineNumber): base(record, lineNumber)
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
	public sealed class AfterWriteRecordEventArgs: WriteRecordEventArgs
	{

		internal AfterWriteRecordEventArgs(object record, int lineNumber, string line): base(line, lineNumber)
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
