

#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

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