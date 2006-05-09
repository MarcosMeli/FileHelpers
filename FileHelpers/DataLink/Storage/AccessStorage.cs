#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

#if ! MINI

using System;
using System.Data;
using System.Data.OleDb;

namespace FileHelpers.DataLink
{
	public delegate string GetInsertSqlHandler(object record);
	public delegate object FillRecordHandler(object[] fieldValues);

	/// <summary>This is a base class that implements the <see cref="DataStorage"/> for Microsoft Access Files.</summary>
	public sealed class AccessStorage : DataBaseStorage
	{

		private Type mRecordType;
		private string mAccessFile = string.Empty;
		private string mAccessPassword = string.Empty;

		public AccessStorage(Type type, string accessFile)
		{
			mRecordType = type;
			AccessFileName = accessFile;
		}


		private GetInsertSqlHandler mGetInsertSqlCallback;
		private FillRecordHandler mFillRecordCallback;

		#region "  Create Connection and Command  "

		/// <summary>
		///		Returns the class that represent the records in the file.
		/// </summary>
		public override Type RecordType
		{
			get { return mRecordType; }
		}

		/// <summary>This method recives the fields values as an array and must return a record object.</summary>
		/// <param name="fieldValues">The record fields values.</param>
		/// <returns>The record object.</returns>
		protected override object FillRecord(object[] fieldValues)
		{
			if (FillRecordCallback == null)
				throw new BadUsageException("You can´t extract records a null FillRecordCallback. Check the docs for help.");

			return FillRecordCallback(fieldValues);		}

		/// <summary>Must return the Select Sql used to Fetch the records to Extract.</summary>
		/// <returns>The SQL statement.</returns>
		protected override string GetSelectSql()
		{
			if (mSelectSql == null || mSelectSql == string.Empty)
				throw new BadUsageException("The SelectSql property is empty, please set it before try to get the records.");

			return mSelectSql;
		}

		/// <summary>Must return a SQL string with the insert statement for the records.</summary>
		/// <param name="record">The record to insert.</param>
		/// <returns>The Sql string to used to insert the record.</returns>
		protected override string GetInsertSql(object record)
		{
			if (mGetInsertSqlCallback == null)
                throw new BadUsageException("You can´t insert records with a null GetInsertSqlCallback. Check the docs for help.");

			return mGetInsertSqlCallback(record);
		}

		/// <summary>Must create an abstract connection object.</summary>
		/// <returns>An Abstract Connection Object.</returns>
		protected sealed override IDbConnection CreateConnection()
		{
            string conString = DataBaseHelper.GetAccessConnection(AccessFileName, AccessFilePassword);
			return new OleDbConnection(conString);
		}

		/// <summary>Must create an abstract command object.</summary>
		/// <returns>An Abstract Command Object.</returns>
		protected sealed override IDbCommand CreateCommand()
		{
			return new OleDbCommand();
		}

		#endregion

        /// <summary>The password to the access database.</summary>
        public string AccessFilePassword
        {
        	get{ return mAccessPassword; }
			set{ mAccessPassword = value; }
		}

		private string mSelectSql = string.Empty;

		public string SelectSql
		{
			get { return mSelectSql; }
			set { mSelectSql = value; }
		}

		public string AccessFileName
		{
			get { return mAccessFile; }
			set { mAccessFile = value; }
		}

		public GetInsertSqlHandler GetInsertSqlCallback
		{
			get { return mGetInsertSqlCallback; }
			set { mGetInsertSqlCallback = value; }
		}

		public FillRecordHandler FillRecordCallback
		{
			get { return mFillRecordCallback; }
			set { mFillRecordCallback = value; }
		}
	}

}

#endif