#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates the target field has a new line before his value (i.e. indicates that the records has multiple lines, and this field is in the begining of a line)</summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldInNewLineAttribute: Attribute
	{
		/// <summary>Indicates the target field has a new line before his value (i.e. indicates that the records has multiple lines, and this field is in the begining of a line)</summary>
		public FieldInNewLineAttribute()
		{}
	}
}