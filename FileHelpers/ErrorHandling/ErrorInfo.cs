#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Diagnostics;

namespace FileHelpers
{
	/// <summary>Contains error information of the <see cref="FileHelperEngine"/> class.</summary>
	[DelimitedRecord("|")]
	[IgnoreFirst(2)]
    [DebuggerDisplay("Line: {LineNumber}. Error: {ExceptionInfo.Message}.")]
    public sealed class ErrorInfo
	{
		internal ErrorInfo()
		{
		}

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal int mLineNumber;

		/// <summary>The line number of the error</summary>
		public int LineNumber
		{
			get { return mLineNumber; }
		}

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [FieldQuoted(QuoteMode.OptionalForBoth)]
		internal string mRecordString = string.Empty;

		/// <summary>The string of the record of the error.</summary>
		public string RecordString
		{
			get { return mRecordString; }
		}


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        [FieldConverter(typeof(ExceptionConverter))] 
		[FieldQuoted(QuoteMode.OptionalForBoth)]
		internal Exception mExceptionInfo;

		/// <summary>The exception that indicates the error.</summary>
		public Exception ExceptionInfo
		{
			get { return mExceptionInfo; }
		}

		internal class ExceptionConverter : ConverterBase
		{
			public override string FieldToString(object from)
			{
				if (from == null)
					return String.Empty;
				else
				{
					if (from is ConvertException)
						return "In the field '" + ((ConvertException) from).FieldName + "': " + ((ConvertException) from).Message.Replace(StringHelper.NewLine, " -> ");
					else
						return ((Exception) from).Message.Replace(StringHelper.NewLine, " -> ");
				}
			}

			public override object StringToField(string from)
			{
				return new Exception(from);
			}
		}
	}
}