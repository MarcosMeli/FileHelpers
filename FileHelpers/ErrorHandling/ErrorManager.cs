#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;

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
		public ErrorManager(ErrorMode mode) : this()
		{
			mErrorMode = mode;
		}

		/// <summary>Initializes a new instance of the <see cref="ErrorManager"/> class. with the specified <see cref="ErrorMode"/> for browse previusly saved errors.</summary>
		/// <param name="fileName">Indicates the fileName from where the Manager load the errors.</param>
		public ErrorManager(string fileName) : this()
		{
			LoadErrors(fileName);
		}

		ArrayList mErrorsArray = new ArrayList();

		/// <summary>Is an array of <see cref="ErrorInfo"/> that contains the errors of the last operation in this class.</summary>
		public ErrorInfo[] LastErrors
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
			FileHelperEngine engine = new FileHelperEngine(typeof (ErrorInfo));
			engine.HeaderText = "FileHelper Errors Saved at " + DateTime.Now.ToLongDateString();
			engine.WriteFile(fileName, LastErrors);

		}

		/// <summary>Load errors from a file.</summary>
		/// <param name="fileName">The file that contains the errors.</param>
		public void LoadErrors(string fileName)
		{
			FileHelperEngine engine = new FileHelperEngine(typeof (ErrorInfo));
			ClearErrors();
			mErrorsArray.AddRange(engine.ReadFile(fileName));
		}

	}
}