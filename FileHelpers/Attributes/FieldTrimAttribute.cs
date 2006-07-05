#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

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
		#region "  TrimChars  "

		private Char[] mTrimChars;

		/// <summary>The chars to Trim.</summary>
		public char[] TrimChars
		{
			get { return mTrimChars; }
		}

		#endregion

		#region "  TrimMode  "

		private TrimMode mTrimMode;

		/// <summary>The Trim behavior.</summary>
		public TrimMode TrimMode
		{
			get { return mTrimMode; }
		}

		#endregion

		#region "  Constructors  "

		/// <summary>Indicates the <see cref="TrimMode"/> used after read to truncate the field. By default trims the blank spaces and tabs.</summary>
		/// <param name="mode">The <see cref="TrimMode"/> used after read.</param>
		public FieldTrimAttribute(TrimMode mode) : this(mode, new char[] {' ', '\t'})
		{
		}

		/// <summary>Indicates the <see cref="TrimMode"/> used after read to truncate the field. </summary>
		/// <param name="mode">The <see cref="TrimMode"/> used after read.</param>
		/// <param name="chars">A list of chars used to trim.</param>
		public FieldTrimAttribute(TrimMode mode, params char[] chars)
		{
			mTrimMode = mode;
			mTrimChars = chars;
		}

		/// <summary>Indicates the <see cref="TrimMode"/> used after read to truncate the field. </summary>
		/// <param name="mode">The <see cref="TrimMode"/> used after read.</param>
		/// <param name="trimChars">A string of chars used to trim.</param>
		public FieldTrimAttribute(TrimMode mode, string trimChars)
		{
			mTrimMode = mode;
			mTrimChars = trimChars.ToCharArray();
		}

		#endregion
	}
}