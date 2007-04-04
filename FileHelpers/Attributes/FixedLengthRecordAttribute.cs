#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Indicates that this class represents a fixed length record.</summary>
	/// <remarks>See the <a href="attributes.html">Complete Attributes List</a> for more clear info and examples of each one.</remarks>
	/// <seealso href="attributes.html">Attributes List</seealso>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class FixedLengthRecordAttribute : TypedRecordAttribute
	{
		internal FixedMode mFixedMode = FixedMode.ExactLength;

		/// <summary>Indicates that this class represents a fixed length record. By default requieres that the records has the length equals to the sum of each field length.</summary>
		public FixedLengthRecordAttribute()
		{}

		/// <summary>Indicates that this class represents a fixed length record with the specified variable record behavior.</summary>
		/// <param name="mode">The <see cref="FixedMode"/> used for variable length records.</param>
		public FixedLengthRecordAttribute(FixedMode mode)
		{
			mFixedMode = mode;
		}
		
	}
}