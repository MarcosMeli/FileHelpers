

namespace FileHelpers
{
	/// <summary>Indicates the trimming behavior of the trailing characters.</summary>
	public enum TrimMode
	{
		/// <summary>No trimming is performed.</summary>
		None,
		/// <summary>The field is trimmed in both sides.</summary>
		Both,
		/// <summary>The field is trimmed in the left.</summary>
		Left,
		/// <summary>The field is trimmed in the right.</summary>
		Right
	}
}