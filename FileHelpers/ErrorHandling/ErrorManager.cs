using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FileHelpers
{
	/// <summary>
    /// This is the class that handles the errors of the engines process.
    /// </summary>
	/// <remarks>
    /// All the engines and DataStorage utilities contains an ErrorManager.
    /// </remarks>
    [DebuggerDisplay("{ErrorsDescription()}. ErrorMode: {ErrorMode.ToString()}")]
    public sealed class ErrorManager
        : IEnumerable
    {
        private int mErrorLimit = 10000;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorManager"/> class.
        /// </summary>
		public ErrorManager()
		{
		}

		/// <summary>
        /// Initializes a new instance of the <see cref="ErrorManager"/> class.
        /// with the specified <see cref="ErrorMode"/>.
        /// </summary>
		/// <param name="mode">Indicates the error behavior of the class.</param>
		public ErrorManager(ErrorMode mode)
		{
			mErrorMode = mode;
		}


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
        List<ErrorInfo> mErrorsArray = new List<ErrorInfo>();

		/// <summary>
        /// Is an array of <see cref="ErrorInfo"/> that contains the
        /// errors of the last operation in this class.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public ErrorInfo[] Errors
		{
			get { return mErrorsArray.ToArray(); }
		}


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ErrorMode mErrorMode = ErrorMode.ThrowException;
		

		/// <summary>
        /// Indicates the behavior of the <see cref="FileHelperEngine"/>
        /// when it found an error.
        /// </summary>
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
            if (mErrorsArray.Count <= mErrorLimit)
			    mErrorsArray.Add(error);
		}

		/// <summary>Add the specified ErrorInfo to the contained collection.</summary>
		internal void AddErrors(ErrorManager errors)
		{
            if (mErrorsArray.Count <= mErrorLimit)			    
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
			var engine = new FileHelperEngine(typeof (ErrorInfo));

            if (header.IndexOf (StringHelper.NewLine, StringComparison.Ordinal) == header.LastIndexOf (StringHelper.NewLine, StringComparison.Ordinal))
				header +=  StringHelper.NewLine;

			engine.HeaderText = header;
			engine.WriteFile(fileName, Errors);

		}

		/// <summary>Load errors from a file.</summary>
		/// <param name="fileName">The file that contains the errors.</param>
		public static ErrorInfo[] LoadErrors(string fileName)
		{
			var engine = new FileHelperEngine(typeof (ErrorInfo));
			return (ErrorInfo[]) engine.ReadFile(fileName);
		}

        ///<summary>
        /// Returns an enumerator that iterates through a collection.
        ///</summary>
        ///<returns>
        /// An <see cref="T:System.Collections.IEnumerator"></see>
        /// object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public IEnumerator GetEnumerator()
        {
            return mErrorsArray.GetEnumerator();
        }
	}
}