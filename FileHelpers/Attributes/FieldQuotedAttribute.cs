#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

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
		internal char QuoteChar;

		internal QuoteMode QuoteMode = QuoteMode.AlwaysQuoted;

		internal MultilineMode QuoteMultiline = MultilineMode.AllowForBoth;

		/// <summary>Indicates that the field must be read and written like a Quoted String with double quotes.</summary>
		public FieldQuotedAttribute() : this('\"')
		{
		}

		/// <summary>Indicates that the field must be read and written like a Quoted String with the specified char.</summary>
		/// <param name="quoteChar">The char used to quote the string.</param>
		public FieldQuotedAttribute(char quoteChar):this(quoteChar, QuoteMode.OptionalForRead, MultilineMode.AllowForBoth) 
		{
		}

		/// <summary>Indicates that the field must be read and written like a "Quoted String"  (that can be optional depending of the mode).</summary>
		/// <param name="mode">Indicates if the handling of optionals in the quoted field.</param>
		public FieldQuotedAttribute(QuoteMode mode) : this('\"', mode)
		{}

		/// <summary>Indicates that the field must be read and written like a Quoted String (that can be optional).</summary>
		/// <param name="mode">Indicates if the handling of optionals in the quoted field.</param>
		/// <param name="multiline">Indicates if the field can span multiple lines.</param>
		public FieldQuotedAttribute(QuoteMode mode, MultilineMode multiline):this('"', mode, multiline)
		{}

		/// <summary>Indicates that the field must be read and written like a Quoted String (that can be optional).</summary>
		/// <param name="quoteChar">The char used to quote the string.</param>
		/// <param name="mode">Indicates if the handling of optionals in the quoted field.</param>
		public FieldQuotedAttribute(char quoteChar, QuoteMode mode):this(quoteChar, mode, MultilineMode.AllowForBoth) 
		{}

		/// <summary>Indicates that the field must be read and written like a Quoted String (that can be optional).</summary>
		/// <param name="quoteChar">The char used to quote the string.</param>
		/// <param name="mode">Indicates if the handling of optionals in the quoted field.</param>
		/// <param name="multiline">Indicates if the field can span multiple lines.</param>
		public FieldQuotedAttribute(char quoteChar, QuoteMode mode, MultilineMode multiline)
		{
			QuoteChar = quoteChar;
			QuoteMode = mode;
			QuoteMultiline = multiline;
		}

		/// <summary>Indicates that the field must be read and written like a Quoted String with double quotes.</summary>
		/// <param name="multiline">Indicates if the field can span multiple lines.</param>
		public FieldQuotedAttribute(MultilineMode multiline) : this('\"', QuoteMode.OptionalForRead, multiline)
		{}





	}
}