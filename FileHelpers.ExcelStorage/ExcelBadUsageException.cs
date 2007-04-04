using System;

#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

namespace FileHelpers.DataLink
{
	/// <summary>Indicates the wrong usage of the ExcelStorage of the library.</summary>
	public class ExcelBadUsageException : BadUsageException
	{
		/// <summary>Creates an instance of an ExcelBadUsageException.</summary>
		/// <param name="message">The exception Message</param>
		internal ExcelBadUsageException(string message) : base(message)
		{
		}

//		/// <summary>Creates an instance of an ExcelBadUsageException.</summary>
//		/// <param name="message">The exception Message</param>
//		/// <param name="innerEx">The inner Exception.</param>
//		internal ExcelBadUsageException(string message, Exception innerEx) : base(message, innerEx)
//		{
//		}

	}
}