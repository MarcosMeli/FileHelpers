#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates the <see cref="AlignMode"/> used for <b>write</b> operations.</summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldAlignAttribute : Attribute
	{
		#region "  Constructors  "

		/// <summary>Uses the ' ' char to align.</summary>
		/// <param name="align">The position of the alignment.</param>
		public FieldAlignAttribute(AlignMode align) : this(align, ' ')
		{
		}

		/// <summary>You can indicate the align char.</summary>
		/// <param name="align">The position of the alignment.</param>
		/// <param name="alignChar">The character used to align.</param>
		public FieldAlignAttribute(AlignMode align, char alignChar)
		{
			Align = align;
			AlignChar = alignChar;
		}

		#endregion

		/// <summary>The position of the alignment.</summary>
		internal AlignMode Align;

		/// <summary>The character used to align.</summary>
		internal char AlignChar;
	}
}