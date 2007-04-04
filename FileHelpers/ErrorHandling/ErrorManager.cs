#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace FileHelpers
{
	/// <summary>This is the class that handles the errors of the engines process.</summary>
	/// <remarks>All the engines and DataStorages contains a ErrorManager.</remarks>
#if NET_2_0
    [DebuggerDisplay("{ErrorsDescription()}. ErrorMode: {ErrorMode.ToString()}")]
#endif
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


#if NET_2_0
        private string ErrorsDescription()
        {
            if (ErrorCount == 1)
                return ErrorCount.ToString() + " Error";
            else if (ErrorCount == 0)
                return "No Errors";
            else
                return ErrorCount.ToString() + " Errors";
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        ArrayList mErrorsArray = new ArrayList();

		/// <summary>Is an array of <see cref="ErrorInfo"/> that contains the errors of the last operation in this class.</summary>
#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
#endif
        public ErrorInfo[] Errors
		{
			get { return (ErrorInfo[]) mErrorsArray.ToArray(typeof (ErrorInfo)); }
		}

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
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

		/// <summary>Indicates if contains one or more errors.</summary>
		public bool HasErrors
		{
			get { return mErrorsArray.Count > 0; }
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

		/// <summary>Add the specified ErrorInfo to the contained collection.</summary>
		internal void AddErrors(ErrorManager errors)
		{
			mErrorsArray.AddRange(errors.mErrorsArray);
		}

//		public void ProcessError(Exception ex, string line)
//		{
//		}


		/// <summary>Saves the contained errors to the specified file.</summary>
		/// <param name="fileName">The file that contains the errors.</param>
		public void SaveErrors(string fileName)
		{
			string header;
			if (ErrorCount > 0)
				header = "FileHelpers - Errors Saved ";
			else
				header = "FileHelpers - NO Errors Found ";

			header += "at " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
			header += StringHelper.NewLine + "LineNumber | LineString |ErrorDescription";

			SaveErrors(fileName, header);
		}

		/// <summary>Saves the contained errors to the specified file.</summary>
		/// <param name="fileName">The file that contains the errors.</param>
		/// <param name="header">The header line of the errors file.</param>
		public void SaveErrors(string fileName, string header)
		{
			FileHelperEngine engine = new FileHelperEngine(typeof (ErrorInfo));

			if (header.IndexOf(StringHelper.NewLine) == header.LastIndexOf(StringHelper.NewLine))
				header +=  StringHelper.NewLine;

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