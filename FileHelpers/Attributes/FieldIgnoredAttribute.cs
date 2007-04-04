#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates that the target field is completely ignored by the Engine (i.e. This field don´t exists for the library).</summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldIgnoredAttribute : FieldAttribute
	{
		/// <summary>Indicates that the target field is ignored by the Engine AND NOT IS IN THE FILE.</summary>
		public FieldIgnoredAttribute()
		{}
	}
}