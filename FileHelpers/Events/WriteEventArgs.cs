

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
			Record = record;
			LineNumber = lineNumber;
		}

        /// <summary>The current record.</summary>
        public T Record { get; private set; }

        /// <summary>The current line number.</summary>
        public int LineNumber { get; private set; }
	}


	/// <summary>Arguments for the <see cref="BeforeWriteRecordHandler"/></summary>
    public sealed class BeforeWriteRecordEventArgs<T>: WriteRecordEventArgs<T>
	{
		internal BeforeWriteRecordEventArgs(T record, int lineNumber)
			: base(record, lineNumber)
		{
		    SkipThisRecord = false;
		}

	    /// <summary>Set this property as true if you want to bypass the current record.</summary>
	    public bool SkipThisRecord { get; set; }
	}


	/// <summary>Arguments for the <see cref="AfterWriteRecordHandler"/></summary>
    public sealed class AfterWriteRecordEventArgs<T> : WriteRecordEventArgs<T>
	{
		internal AfterWriteRecordEventArgs(T record, int lineNumber, string line): base(record, lineNumber)
		{
			RecordLine = line;
		}

	    /// <summary>The line to be written to the file. WARNING: you can change this and the engines will write your version to the file.</summary>
	    public string RecordLine { get; set; }
	}

}
