using System;

#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

namespace FileHelpers
{
	/// <summary>Indicates the behavior of multiline fields.</summary>
	public enum MultilineMode
	{
		/// <summary>The engine can handle multiline values for read or write.</summary>
		AllowForBoth = 0,
		/// <summary>The engine can handle multiline values only for read.</summary>
		AllowForRead,
		/// <summary>The engine can handle multiline values only for write.</summary>
		AllowForWrite,
		/// <summary>The engine don´t allow multiline values for this field.</summary>
		NotAllow
	}
}