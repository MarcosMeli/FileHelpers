using System;

#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

namespace FileHelpers
{
	/// <summary>Indicates the behavior of quoted fields.</summary>
	public enum QuoteMode
	{
		AlwaysQuoted = 0,
		OptionalForRead,
		OptionalForWrite,
		OptionalForBoth
	}
}