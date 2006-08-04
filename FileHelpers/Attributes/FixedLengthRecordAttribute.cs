#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

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
		internal bool mVariableRecordLength = false;

		/// <summary>Indicates that this class represents a fixed length record.</summary>
		public FixedLengthRecordAttribute()
		{}

		/// <summary>Indicates that this class represents a fixed length record.</summary>
		/// <param name="variableRecordLength">Indicates if the engines allow the last record to contain more or less chars.</param>
		public FixedLengthRecordAttribute(bool variableRecordLength)
		{
			mVariableRecordLength = variableRecordLength;
		}

	}
}