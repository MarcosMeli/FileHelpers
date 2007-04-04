using System;

#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

namespace FileHelpers
{
	/// <summary>Indicates the behavior of quoted fields.</summary>
	public enum QuoteMode
	{
		/// <summary>The engine always expects a quote when read and always adds the quotes when write.</summary>
		AlwaysQuoted = 0,
		/// <summary>The engine expects or not a quote when read and always adds the quotes when write.</summary>
		OptionalForRead,
		/// <summary>The engine always expects a quote when read and adds the quotes when write only if the field contains: quotes, new lines or the separator char.</summary>
		OptionalForWrite,
		/// <summary>The engine expects or not a quote when read and adds the quotes when write only if the field contains: quotes, new lines or the separator char.</summary>
		OptionalForBoth
	}
}