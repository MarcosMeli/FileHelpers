

#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

namespace FileHelpers
{
	public enum TransactionMode
	{
		NoTransaction = 0,
		UseDefault,
		UseChaosLevel,
		UseReadCommitted,
		UseReadUnCommitted,
		UseRepeatableRead,
		UseSerializable
	}
}