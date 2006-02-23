#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates an error in the parsing of a Quoted String</summary>
	public class QuotedStringException : FileHelperException
	{
		private string mQuotedString;

		/// <summary>The string that fails at parsing time.</summary>
		public string QuotedString
		{
			get { return mQuotedString; }
		}

		internal QuotedStringException(string message) : this(message, String.Empty)
		{
		}

		internal QuotedStringException(string message, string quotedString) : base(message)
		{
			mQuotedString = quotedString;
		}
	}
}