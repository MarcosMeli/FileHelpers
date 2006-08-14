#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Contains error information of the <see cref="FileHelperEngine"/> class.</summary>
	[DelimitedRecord("|")]
	[IgnoreFirst(2)]
	public sealed class ErrorInfo
	{
		internal ErrorInfo()
		{
		}

		internal int mLineNumber;

		/// <summary>The line number of the error</summary>
		public int LineNumber
		{
			get { return mLineNumber; }
		}

		[FieldQuoted(QuoteMode.OptionalForBoth)]
		internal string mRecordString = string.Empty;

		/// <summary>The string of the record of the error.</summary>
		public string RecordString
		{
			get { return mRecordString; }
		}

		[FieldConverter(typeof (ExceptionConverter))] 
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