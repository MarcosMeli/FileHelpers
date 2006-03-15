#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
using System.ComponentModel;

namespace FileHelpers
{
	/// <summary>This is the class that handles the errors of the library process.</summary>
	/// <remarks>This is shared by the FileHelper Engine and all the DataStorage.</remarks>
	public sealed class ErrorManager
	{
		/// <summary>Initializes a new instance of the <see cref="ErrorManager"/> class.</summary>
		public ErrorManager()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="ErrorManager"/> class. with the specified <see cref="ErrorMode"/>.</summary>
		/// <param name="mode">Indicates the error behavior of the class.</param>
		public ErrorManager(ErrorMode mode)
		{
			mErrorMode = mode;
		}

		ArrayList mErrorsArray = new ArrayList();

		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("This property name is obsolete use Errors instead.")]
		public ErrorInfo[] LastErrors
		{
			get { return Errors; }
		}

		/// <summary>Is an array of <see cref="ErrorInfo"/> that contains the errors of the last operation in this class.</summary>
		public ErrorInfo[] Errors
		{
			get { return (ErrorInfo[]) mErrorsArray.ToArray(typeof (ErrorInfo)); }
		}

		private ErrorMode mErrorMode = ErrorMode.ThrowException;

		/// <summary>Indicates the behavior of the <see cref="FileHelperEngine"/> when it found an error.</summary>
		public ErrorMode ErrorMode
		{
			get { return mErrorMode; }
			set { mErrorMode = value; }
		}


		/// <summary>Number of contained errors.</summary>
		public int ErrorCount
		{
			get { return mErrorsArray.Count; }
		}


		/// <summary>Clears the error collection.</summary>
		public void ClearErrors()
		{
			mErrorsArray.Clear();
		}

		/// <summary>Add the specified ErrorInfo to the contained collection.</summary>
		/// <param name="error"></param>
		internal void AddError(ErrorInfo error)
		{
			mErrorsArray.Add(error);
		}

//		public void ProcessError(Exception ex, string line)
//		{
//		}


		/// <summary>Saves the contained errors to the specified file.</summary>
		/// <param name="fileName">The file that contains the errors.</param>
		public void SaveErrors(string fileName)
		{
			SaveErrors(fileName, "FileHelpers Errors Saved at " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());
		}

		/// <summary>Saves the contained errors to the specified file.</summary>
		/// <param name="fileName">The file that contains the errors.</param>
		/// <param name="header">The header line of the errors file.</param>
		public void SaveErrors(string fileName, string header)
		{
			FileHelperEngine engine = new FileHelperEngine(typeof (ErrorInfo));
			engine.HeaderText = header;
			engine.WriteFile(fileName, Errors);

		}

		/// <summary>Load errors from a file.</summary>
		/// <param name="fileName">The file that contains the errors.</param>
		public static ErrorInfo[] LoadErrors(string fileName)
		{
			FileHelperEngine engine = new FileHelperEngine(typeof (ErrorInfo));
			return (ErrorInfo[]) engine.ReadFile(fileName);
		}

	}
}