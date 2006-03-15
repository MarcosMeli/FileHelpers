#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;

namespace FileHelpers.DataLink
{
	/// <summary>Base class for all the Storage classes of the library or the custom Storage classes.</summary>
	public abstract class DataStorage
	{
		/// <summary>Returns the class that represent the records in the file.</summary>
		public abstract Type RecordType { get; }

		/// <summary>Must Return the records from the DataSource (DB, Excel, etc)</summary>
		/// <returns>The extracted records.</returns>
		public abstract object[] ExtractRecords();

		/// <summary>Must Insert the records in a DataSource (DB, Excel, etc)</summary>
		/// <param name="records">The records to insert.</param>
		public abstract void InsertRecords(object[] records);


		/// <summary>The Object responsable for manage the errors.</summary>
		protected ErrorManager mErrorManager = new ErrorManager();

		/// <summary>The Object responsable for manage the errors.</summary>
		public ErrorManager ErrorManager
		{
			get { return mErrorManager; }
		}

		/// <summary>Add an error to the ErrorCollection.</summary>
		/// <param name="lineNumber">The line when the error occurs.</param>
		/// <param name="ex">The exception throwed, can be null.</param>
		protected void AddError(int lineNumber, Exception ex)
		{
			ErrorInfo e = new ErrorInfo();
			e.mLineNumber = lineNumber;
//			e.mColumnNumber = colNum;
			e.mExceptionInfo = ex;

			mErrorManager.AddError(e);
		}

		/// <summary>Creates an instance of this class.</summary>
		protected DataStorage()
		{
		}
/// <summary>Creates an instance of the RecordInfo class. This method is used because hte constructor of the record info is internal.</summary>
/// <param name="recordClass">The class passed to the RecordInfo constructor.</param>
/// <returns>A RecordInfo instance.</returns>
		protected static RecordInfo CreateRecordInfo(Type recordClass)
		{
			return new RecordInfo(recordClass);
		}
	}
}