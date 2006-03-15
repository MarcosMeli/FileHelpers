#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

namespace FileHelpers.DataLink
{
	/// <summary>This class implements the <see cref="DataStorage"/> and is the base class for Data Base storages.</summary>
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public abstract class DataBaseStorage : DataStorage
	{
		/// <summary>
		///		Returns the class that represent the records in the file.
		/// </summary>
		public abstract override Type RecordType { get; }

		/// <summary>This method recives the fields values as an array and must return a record object.</summary>
		/// <param name="fieldValues">The record fields values.</param>
		/// <returns>The record object.</returns>
		protected abstract object FillRecord(object[] fieldValues);

		/// <summary>Must return the Select Sql used to Fetch the records to Extract.</summary>
		/// <returns>The SQL statement.</returns>
		protected abstract string GetSelectSql();

		/// <summary>Must return a SQL string with the insert statement for the records.</summary>
		/// <param name="record">The record to insert.</param>
		/// <returns>The Sql string to used to insert the record.</returns>
		protected abstract string GetInsertSql(object record);

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

				while (reader.Read())
				{
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
				foreach (object record in records)
				{
					// Insert Logic Here, must check duplicates

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
	}
}