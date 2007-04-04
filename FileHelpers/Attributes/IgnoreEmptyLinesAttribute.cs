#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates that the engine must ignore the empty lines while reading.</summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class IgnoreEmptyLinesAttribute : Attribute
	{
		internal bool mIgnoreSpaces = false;
		/// <summary>Indicates that the engine must ignore the empty lines while reading.</summary>
		public IgnoreEmptyLinesAttribute()
		{}

		/// <summary>Indicates that the engine must ignore the empty lines while reading.</summary>
		/// <param name="ignoreSpaces">Indicates if also must ignore lines with spaces.</param>
		public IgnoreEmptyLinesAttribute(bool ignoreSpaces)
		{
			mIgnoreSpaces = ignoreSpaces;
		}
	}
}