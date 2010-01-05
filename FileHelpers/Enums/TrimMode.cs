

namespace FileHelpers
{
	/// <summary>Indicates the triming behavior of the trailing characters.</summary>
	public enum TrimMode
	{
		/// <summary>No triming is performed.</summary>
		None,
		/// <summary>The field is trimed in both sides.</summary>
		Both,
		/// <summary>The field is trimed in the left.</summary>
		Left,
		/// <summary>The field is trimed in the right.</summary>
		Right
	}
}