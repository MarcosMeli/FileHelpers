#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;

namespace FileHelpers
{
	/// <summary>Base class for all the library Exceptions.</summary>
	public class FileHelpersException : Exception
	{
		/// <summary>Basic constructor of the exception.</summary>
		/// <param name="message">Message of the exception.</param>
		public FileHelpersException(string message) : base(message)
		{
		}

		/// <summary>Basic constructor of the exception.</summary>
		/// <param name="message">Message of the exception.</param>
		/// <param name="innerEx">The inner Exception.</param>
		public FileHelpersException(string message, Exception innerEx) : base(message, innerEx)
		{
			
		}

	}
}