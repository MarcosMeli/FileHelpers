

namespace FileHelpers
{
	/// <summary>Indicates the behavior when variable length records are found in a [<see cref="FixedLengthRecordAttribute"/>]. (Note: nothing in common with [FieldOptional])</summary>
	public enum FixedMode
	{
		/// <summary>The records must have the length equals to the sum of each field length. Default Behavior.</summary>
		ExactLength = 0,
		/// <summary>The records can contain more chars the last field.</summary>
		AllowMoreChars,
		/// <summary>The records can contain less chars. Based on the combination with FielOptional the records can contain less fiels in the last, or if it is marked as optional, in the previous field.</summary>
		AllowLessChars,
		/// <summary>The records can contain more or less chars in the last field.</summary>
		AllowVariableLength

	}
}