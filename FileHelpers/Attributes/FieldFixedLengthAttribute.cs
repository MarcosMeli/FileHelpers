#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates the length of a FixedLength field.</summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class FieldFixedLengthAttribute : FieldAttribute
	{
		private int mLength;

		/// <summary>The String Length of the record defined inside a class with the <see cref="FixedLengthRecordAttribute"/>. </summary>
		public int Length
		{
			get { return mLength; }
		}

		/// <summary>Indicates the length of a FixedLength field.</summary>
		/// <param name="length">The length of the field.</param>
		public FieldFixedLengthAttribute(int length)
		{
			if (length > 0)
				this.mLength = length;
			else
				throw new BadUsageException("The length parameter debe ser > 0");
		}

	}
}