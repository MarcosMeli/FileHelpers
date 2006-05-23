#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

namespace FileHelpers.DataLink
{

	public delegate string InsertSqlHandler(object record);
	public delegate object FillRecordHandler(object[] fieldValues);


	/// <summary>This class implements the <see cref="DataStorage"/> and is the base class for Data Base storages.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public abstract class DatabaseStorage : DataStorage
	{

		private Type mRecordType;

		public DatabaseStorage(Type recordType)
		{
			mRecordType = recordType;
		}

		/// <summary>Returns the class that represent the records in the file.</summary>
		public sealed override Type RecordType
		{
			get{ return mRecordType; }
		}


		/// <summary>This method recives the fields values as an array and must return a record object.</summary>
		/// <param name="fieldValues">The record fields values.</param>
		/// <returns>The record object.</returns>
		private object FillRecord(object[] fieldValues)
		{
			if (FillRecordCallback == null)
				throw new BadUsageException("You can´t extract records a null FillRecordCallback. Check the docs for help.");

			return FillRecordCallback(fieldValues);		}

		/// <summary>Must return the Select Sql used to Fetch the records to Extract.</summary>
		/// <returns>The SQL statement.</returns>
		private string GetSelectSql()
		{
			if (mSelectSql == null || mSelectSql == string.Empty)
				throw new BadUsageException("The SelectSql property is empty, please set it before try to get the records.");

			return mSelectSql;
		}

		private string mSelectSql = string.Empty;

		public string SelectSql
		{
			get { return mSelectSql; }
			set { mSelectSql = value; }
		}

		/// <summary>Must return a SQL string with the insert statement for the records.</summary>
		/// <param name="record">The record to insert.</param>
		/// <returns>The Sql string to used to insert the record.</returns>
		private string GetInsertSql(object record)
		{
			if (mInsertSqlCallback == null)
				throw new BadUsageException("You can´t insert records with a null GetInsertSqlCallback. Check the docs for help.");

			return mInsertSqlCallback(record);
		}

		/// <summary>Must create an abstract connection object.</summary>
		/// <returns>An Abstract Connection Object.</returns>
		protected abstract IDbConnection CreateConnection();

		/// <summary>Must create an abstract command object.</summary>
		/// <returns>An Abstract Command Object.</returns>
		protected abstract IDbCommand CreateCommand();

		private IDbConnection mConn;

		private void InitConnection()
		{
			if (mConn == null)
			{
				mConn = CreateConnection();
			}
		}

		#region "  SelectRecords  "

		/// <summary>Must Return the records from the DataSource (DB, Excel, etc)</summary>
		/// <returns>The extracted records.</returns>
		public override object[] ExtractRecords()
		{
			InitConnection();

			ArrayList res = new ArrayList();

			try
			{
				if (mConn.State != ConnectionState.Open)
					mConn.Open();

				IDbCommand command = CreateCommand();
				command.Connection = mConn;
				command.CommandText = GetSelectSql();

				IDataReader reader = command.ExecuteReader();

				object currentObj;
				object[] values = new object[reader.FieldCount];

				ProgressHelper.Notify(mNotifyHandler, mProgressMode, 0, -1);

				int recordNumber = 0;

				while (reader.Read())
				{
					recordNumber++;
					ProgressHelper.Notify(mNotifyHandler, mProgressMode, recordNumber, -1);


					reader.GetValues(values);
					currentObj = FillRecord(values);
					res.Add(currentObj);
				}

				reader.Close();
			}
			finally
			{
				if (mConn.State != ConnectionState.Closed)
					mConn.Close();
			}

			return res.ToArray();
		}

		#endregion

		internal virtual bool ExecuteInBatch
		{
			get { return false; }
		}

		#region "  InsertRecords  "

		/// <summary>Must Insert the records in a DataSource (DB, Excel, etc)</summary>
		/// <param name="records">The records to insert.</param>
		public override void InsertRecords(object[] records)
		{
			try
			{
				InitConnection();

				if (mConn.State != ConnectionState.Open)
					mConn.Open();

				string SQL = String.Empty;

				ProgressHelper.Notify(mNotifyHandler, mProgressMode, 0, records.Length);
				int recordNumber = 0;

				foreach (object record in records)
				{
					// Insert Logic Here, must check duplicates
					recordNumber++;
					ProgressHelper.Notify(mNotifyHandler, mProgressMode, 0, records.Length);

					SQL += GetInsertSql(record) + " ";

					if (ExecuteInBatch)
					{
						if (SQL.Length > 16000)
						{
							ExecuteAndLeaveOpen(SQL);
							SQL = String.Empty;
						}
					}
					else
					{
						ExecuteAndLeaveOpen(SQL);
						SQL = String.Empty;
					}

				}
				if (SQL != null && SQL.Length != 0)
				{
					ExecuteAndLeaveOpen(SQL);
					SQL = String.Empty;
				}
			}
			finally
			{
				if (mConn.State != ConnectionState.Closed)
					mConn.Close();
			}

		}

		#endregion

		#region "  ExecuteNonQuery (HelperMethods) "

		private int ExecuteAndLeaveOpen(string sql)
		{
			InitConnection();

			IDbCommand command = CreateCommand();
			command.Connection = mConn;
			command.CommandText = sql;

			return command.ExecuteNonQuery();
		}

		private int ExecuteAndClose(string sql)
		{
			int res = -1;

			InitConnection();

			try
			{
				if (mConn.State != ConnectionState.Open)
					mConn.Open();

				IDbCommand command = CreateCommand();
				command.Connection = mConn;
				command.CommandText = sql;

				res = command.ExecuteNonQuery();
			}
			finally
			{
				if (mConn.State != ConnectionState.Closed)
					mConn.Close();
			}

			return res;
		}

		#endregion

		private InsertSqlHandler mInsertSqlCallback;

		public InsertSqlHandler InsertSqlCallback
		{
			get { return mInsertSqlCallback; }
			set { mInsertSqlCallback = value; }
		}

		private FillRecordHandler mFillRecordCallback;
		public FillRecordHandler FillRecordCallback
		{
			get { return mFillRecordCallback; }
			set { mFillRecordCallback = value; }
		}

	}
}