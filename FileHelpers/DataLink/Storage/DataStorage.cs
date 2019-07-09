using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using FileHelpers.Events;

namespace FileHelpers.DataLink
{
    /// <summary>
    /// Base class for all the Storage classes of the library or the custom
    /// Storage classes.
    /// </summary>
    [Obsolete("Datalink feature is outdated and will be rewritten, see https://www.filehelpers.net/mustread/")]
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

        /// <summary>
        /// Returns the class that represent the records in the file.
        /// </summary>
        public Type RecordType
        {
            get { return mRecordType; }
        }

        /// <summary>
        /// Returns the records from the DataSource (DB, Excel, etc)
        /// </summary>
        /// <returns>The extracted records.</returns>
        public abstract object[] ExtractRecords();

        /// <summary>
        /// Returns the records from the DataSource (DB, Excel, etc)
        /// </summary>
        /// <returns>The extracted records as a DataTable.</returns>
        public DataTable ExtractRecordsAsDT()
        {
            IRecordInfo ri = RecordInfo.Resolve(RecordType);
            return ri.Operations.RecordsToDataTable(ExtractRecords());
        }

        /// <summary>
        /// Inserts the records into the DataSource (DB, Excel, etc)
        /// </summary>
        /// <param name="records">Records to insert.</param>
        public abstract void InsertRecords(object[] records);

        /// <summary>Returns the human-readable names of the storage fields.</summary>
        public IEnumerable<string> FieldFriendlyNames
        {
            get { return mRecordInfo.Fields.Select(f => f.FieldFriendlyName); }
        }

        /// <summary>Dynamically removes a field from consideration when extracting records.</summary>
        public void RemoveField(string fieldName)
        {
            mRecordInfo.RemoveField(fieldName);
        }

        /// <summary>
        /// The Object responsible for managing the errors.
        /// </summary>
        protected ErrorManager mErrorManager = new ErrorManager();

        /// <summary>
        /// The Object responsible for managing the errors.
        /// </summary>
        public ErrorManager ErrorManager
        {
            get { return mErrorManager; }
        }

        /// <summary>
        /// Add an error to the ErrorCollection.
        /// </summary>
        /// <param name="lineNumber">The line when the error occurs.</param>
        /// <param name="ex">The exception thrown, can be null.</param>
        /// <param name="recordLine">The record values</param>
        /// <param name="recordTypeName">The name of the record type</param>
        protected void AddError(int lineNumber, Exception ex, string recordLine, string recordTypeName)
        {
            ErrorInfo e = new ErrorInfo
            {
                mLineNumber = lineNumber,
                mExceptionInfo = ex,
                mRecordString = recordLine,
                mRecordTypeName = recordTypeName
            };

            mErrorManager.AddError(e);
        }

        /// <summary>
        /// Creates an instance DataStorage for Type
        /// </summary>
        /// <param name="recordClass">Type of the record object</param>
        protected DataStorage(Type recordClass)
        {
            mRecordType = recordClass;
            mRecordInfo = RecordInfo.Resolve(recordClass);
        }

        #region "  Values <-> Record Conversions "

        /// <summary>
        /// Returns a record created from an Array of values
        /// </summary>
        /// <param name="values">The values used to created the record.</param>
        /// <returns>The record created from 'values'</returns>
        protected object ValuesToRecord(object[] values)
        {
            return mRecordInfo.Operations.ValuesToRecord(values);
        }

        /// <summary>
        /// Returns an array of values based on a record.
        /// </summary>
        /// <param name="record">The source record.</param>
        /// <returns>An array with the values of each field</returns>
        protected object[] RecordToValues(object record)
        {
            return mRecordInfo.Operations.RecordToValues(record);
        }

        #endregion

        /// <summary>
        /// The number of fields in the record class.
        /// </summary>
        protected int RecordFieldCount
        {
            get { return mRecordInfo.FieldCount; }
        }
    }
}