#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates that the field must be read and written like a Quoted String. (by default "")</summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldQuotedAttribute : Attribute
	{
		private char mQuoteChar;

		/// <summary>The char used to quote the string.</summary>
		public char QuoteChar
		{
			get { return mQuoteChar; }
		}

		/// <summary>Indicates that the field must be read and written like a Quoted String. (by default "")</summary>
		public FieldQuotedAttribute() : this('\"')
		{
		}

		/// <summary>Indicates that the field must be read and written like a Quoted String.</summary>
		/// <param name="quoteChar">The char used to quote the string.</param>
		public FieldQuotedAttribute(char quoteChar)
		{
			mQuoteChar = quoteChar;
		}

	}
}