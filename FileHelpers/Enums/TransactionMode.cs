

#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

namespace FileHelpers.DataLink
{
	/// <summary>
	/// Define the diferent Modes of Transaction that uses the <see cref="DatabaseStorage" />
	/// </summary>
	public enum TransactionMode
	{
		/// <summary>No transaction used.</summary>
		NoTransaction = 0,
		/// <summary>Default Transaction Mode.</summary>
		UseDefault,
		/// <summary>Chaos Level Transaction Mode.</summary>
		UseChaosLevel,
		/// <summary>ReadCommitted Transaction Mode.</summary>
		UseReadCommitted,
		/// <summary>ReadUnCommitted Transaction Mode.</summary>
		UseReadUnCommitted,
		/// <summary>Repeatable Transaction Mode.</summary>
		UseRepeatableRead,
		/// <summary>Serializable Transaction Mode.</summary>
		UseSerializable
	}
}