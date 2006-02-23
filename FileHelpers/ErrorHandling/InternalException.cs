

#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

namespace FileHelpers
{
	/// <summary>Indicates an internal error of the library, please report this to marcosdotnet[at]yahoo.com.ar</summary>
	public class InternalException : FileHelperException
	{
		internal InternalException(string message) : base(message + ". This is an internal Exception of the library, you can report this to marcosdotnet[at]yahoo.com.ar, thanks.")
		{
		}
	}
}