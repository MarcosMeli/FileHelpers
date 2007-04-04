#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.ComponentModel;

namespace FileHelpers
{
	/// <summary>Base class of <see cref="FieldFixedLengthAttribute"/> and <see cref="FieldDelimiterAttribute"/></summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class FieldAttribute : Attribute
	{
		/// <summary>Abstract class, see the inheritors.</summary>
		protected FieldAttribute()
		{
		}
	}
}