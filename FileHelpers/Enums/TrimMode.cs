

namespace FileHelpers
{
	/// <summary>
	/// Indicates the trimming behavior of the leading and trailing whitespace.
	/// </summary>
	public enum TrimMode
	{
		/// <summary>No trimming is performed.</summary>
		None,
		/// <summary>The field is trimmed on both sides.</summary>
		Both,
		/// <summary>The field is trimmed on the left.</summary>
		Left,
		/// <summary>The field is trimmed on the right.</summary>
		Right
	}
}