#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates the number of first lines to be discarded.</summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class IgnoreFirstAttribute : Attribute
	{
		internal int NumberOfLines;


		/// <summary>Indicates that the first line must be discarded.</summary>
		public IgnoreFirstAttribute() : this(1)
		{
		}

		/// <summary>Indicates the number of first lines to be ignored.</summary>
		/// <param name="numberOfLines">The number of first lines to be discarded.</param>
		public IgnoreFirstAttribute(int numberOfLines)
		{
			NumberOfLines = numberOfLines;
		}
	}
}