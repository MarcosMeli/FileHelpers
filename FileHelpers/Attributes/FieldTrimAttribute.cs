#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates the <see cref="TrimMode"/> used after read to truncate the field. </summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldTrimAttribute : Attribute
	{

		internal Char[] TrimChars;
		internal TrimMode TrimMode;
		private static char[] WhitespaceChars = new char[] 
			 { 
				 '\t', '\n', '\v', '\f', '\r', ' ', '\x00a0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', 
				 '\u2009', '\u200a', '\u200b', '\u3000', '\ufeff'
			 };

		#region "  Constructors  "

		/// <summary>Indicates the <see cref="TrimMode"/> used after read to truncate the field. By default trims the blank spaces and tabs.</summary>
		/// <param name="mode">The <see cref="TrimMode"/> used after read.</param>
		public FieldTrimAttribute(TrimMode mode) 
			: this(mode, WhitespaceChars)
		{
		}

		/// <summary>Indicates the <see cref="TrimMode"/> used after read to truncate the field. </summary>
		/// <param name="mode">The <see cref="TrimMode"/> used after read.</param>
		/// <param name="chars">A list of chars used to trim.</param>
		public FieldTrimAttribute(TrimMode mode, params char[] chars)
		{
			TrimMode = mode;
			Array.Sort(chars);
			TrimChars = chars;
		}

		/// <summary>Indicates the <see cref="TrimMode"/> used after read to truncate the field. </summary>
		/// <param name="mode">The <see cref="TrimMode"/> used after read.</param>
		/// <param name="trimChars">A string of chars used to trim.</param>
		public FieldTrimAttribute(TrimMode mode, string trimChars)
				:this(mode, trimChars.ToCharArray())
		{
		}

		#endregion
	}
}