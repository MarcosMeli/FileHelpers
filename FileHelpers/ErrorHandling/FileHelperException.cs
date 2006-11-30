#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Base class for all the library Exceptions.</summary>
	public class FileHelperException : Exception
	{
		/// <summary>Basic constructor of the exception.</summary>
		/// <param name="message">Message of the exception.</param>
		public FileHelperException(string message) : base(message)
		{
		}

		/// <summary>Basic constructor of the exception.</summary>
		/// <param name="message">Message of the exception.</param>
		/// <param name="innerEx">The inner Exception.</param>
		public FileHelperException(string message, Exception innerEx) : base(message, innerEx)
		{
			
		}

	}
}