

#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

namespace FileHelpers
{
	/// <summary>Indicates the behavior when variable length records are found in a [<see cref="FixedLengthRecordAttribute"/>]. (Note: nothing in common with [FieldOptional])</summary>
	public enum FixedMode
	{
		/// <summary>The records must have the length equals to the sum of each field length.</summary>
		ExactLength = 0,
		/// <summary>The records can contain less chars in the last field.</summary>
		AllowMoreChars,
		/// <summary>The records can contain more chars in the last field.</summary>
		AllowLessChars,
		/// <summary>The records can contain more or less chars in the last field.</summary>
		AllowVariableLength
	}
}