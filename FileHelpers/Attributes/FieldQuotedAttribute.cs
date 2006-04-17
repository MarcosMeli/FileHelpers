#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.ComponentModel;

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

		private QuoteMode mQuoteMode = QuoteMode.Allways;

		/// <summary>Indicates if the Quoted char can be optional (default is false)</summary>
		public QuoteMode QuoteMode
		{
			get { return mQuoteMode; }
		}

		/// <summary>Indicates that the field must be read and written like a Quoted String. (by default "")</summary>
		public FieldQuotedAttribute() : this('\"')
		{
		}

		/// <summary>Indicates that the field must be read and written like a "Quoted String"  (that can be optional).</summary>
		/// <param name="mode">Indicates if the handling of optionals in the quoted field.</param>
		public FieldQuotedAttribute(QuoteMode mode) : this('\"', mode)
		{
		}

		/// <summary>Indicates that the field must be read and written like a Quoted String.</summary>
		/// <param name="quoteChar">The char used to quote the string.</param>
		public FieldQuotedAttribute(char quoteChar)
		{
			mQuoteChar = quoteChar;
		}

		/// <summary>Indicates that the field must be read and written like a Quoted String (that can be optional).</summary>
		/// <param name="quoteChar">The char used to quote the string.</param>
		/// <param name="mode">Indicates if the handling of optionals in the quoted field.</param>
		public FieldQuotedAttribute(char quoteChar, QuoteMode mode)
		{
			mQuoteChar = quoteChar;
			mQuoteMode = mode;
		}

		/// <summary>Indicates that the field must be read and written like a Quoted String (that can be optional).</summary>
		/// <param name="quoteChar">The char used to quote the string.</param>
		[Obsolete("You must use the constructor with the new QuoteMode Enum")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public FieldQuotedAttribute(char quoteChar, bool optional)
		{
			mQuoteChar = quoteChar;
			if (optional)
				mQuoteMode = QuoteMode.OptionalBoth;
		}

	}
}