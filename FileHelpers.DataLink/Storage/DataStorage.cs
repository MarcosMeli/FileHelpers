#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.ComponentModel;
using System.Data;
using FileHelpers.Events;
using Container=FileHelpers.IoC.Container;

namespace FileHelpers.DataLink
{
	/// <summary>Base class for all the Storage classes of the library or the custom Storage classes.</summary>
	public abstract class DataStorage
	{
        /// <summary>Called to notify progress.</summary>
        public event EventHandler<ProgressEventArgs> Progress;

        /// <summary>
        /// Raises the Progress Event
        /// </summary>
        /// <param name="e">The Event Args</param>
        protected void OnProgress(ProgressEventArgs e)
        {
            if (Progress == null)
                return;

            Progress(this, e);
        }

		private Type mRecordType;
		internal IRecordInfo mRecordInfo;
		
		/// <summary>Returns the class that represent the records in the file.</summary>
		public Type RecordType
		{
			get
			{
				return mRecordType;
			}
		}

		/// <summary>Must Return the records from the DataSource (DB, Excel, etc)</summary>
		/// <returns>The extracted records.</returns>
		public abstract object[] ExtractRecords();

		/// <summary>Must Return the records from the DataSource (DB, Excel, etc)</summary>
		/// <returns>The extracted records.</returns>
		public DataTable ExtractRecordsAsDT()
		{
			IRecordInfo ri = Container.Resolve<IRecordInfo>(RecordType);
			return ri.RecordsToDataTable(ExtractRecords());
		}

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
		/// <param name="recordLine">The record values</param>
		protected void AddError(int lineNumber, Exception ex, string recordLine)
		{
			ErrorInfo e = new ErrorInfo();
			e.mLineNumber = lineNumber;
//			e.mColumnNumber = colNum;
			e.mExceptionInfo = ex;
			e.mRecordString = recordLine;

			mErrorManager.AddError(e);
		}

		/// <summary>Creates an instance of this class.</summary>
		protected DataStorage(Type recordClass)
		{
			mRecordType = recordClass;
		    mRecordInfo = Container.Resolve<IRecordInfo>(recordClass);
		}

		#region "  Values <-> Record Convertions "

		/// <summary>Returns a record created from an Array of values</summary>
		/// <param name="values">The values used to created the record.</param>
		/// <returns>The just created record.</returns>
		protected object ValuesToRecord(object[] values)
		{
			return mRecordInfo.ValuesToRecord(values);
		}

		/// <summary>Returns an array of value based on a record.</summary>
		/// <param name="record">The source record.</param>
		/// <returns>An array with the values of each field</returns>
		protected object[] RecordToValues(object record)
		{
			return mRecordInfo.RecordToValues(record);
		}

		#endregion

		/// <summary>The number of fields in the record class.</summary>
		protected int RecordFieldCount
		{
			get
			{
				return mRecordInfo.FieldCount;
			}
		}
	}
}