using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace FileHelpers.Mapping
{

	/// <summary>
	/// A class to provide Record-DataTable operations. (BETA QUALITY, use it at your own risk :P, API can change in future version)
	/// </summary>
	public sealed class DataMapper
	{
		RecordInfo mRecordInfo;
		
		/// <summary>
		/// Create a new Mapping for the record Type 't'.
		/// </summary>
		/// <param name="t">The record class.</param>
		public DataMapper(Type t)
		{
			mRecordInfo = new RecordInfo(t);
		}

		private int mGlobalOffset = 0;

		/// <summary>
		/// 
		/// </summary>
		public int GlobalOffset
		{
			get { return mGlobalOffset; }
			set { mGlobalOffset = value; }
		}

		/// <summary>
		/// Add a new mapping between column at <paramref>columnIndex</paramref> and the fieldName with the specified <paramref>fieldName</paramref> name.
		/// </summary>
		/// <param name="columnIndex">The index in the Datatable</param>
		/// <param name="fieldName">The name of a fieldName in the Record Class</param>
		public void AddMapping(int columnIndex, string fieldName)
		{
			MappingInfo map = new MappingInfo(mRecordInfo.mRecordType, fieldName);
			map.mDataColumnIndex = columnIndex + mGlobalOffset;
			mMappings.Add(map);
		}
		
		/// <summary>
		/// Add a new mapping between column with <paramref>columnName</paramref> and the fieldName with the specified <paramref>fieldName</paramref> name.
		/// </summary>
		/// <param name="columnName">The name of the Column</param>
		/// <param name="fieldName">The name of a fieldName in the Record Class</param>
		public void AddMapping(string columnName, string fieldName)
		{
			MappingInfo map = new MappingInfo(mRecordInfo.mRecordType, fieldName);
			map.mDataColumnName = columnName;
			mMappings.Add(map);
		}
		
		private ArrayList mMappings  = new ArrayList();
		
		/// <summary>
		/// For each row in the datatable create a record.
		/// </summary>
		/// <param name="dt">The source Datatable</param>
		/// <returns>The mapped records contained in the DataTable</returns>
		public object[] MapDataTable2Records(DataTable dt)
		{
			mMappings.TrimToSize();
			
			ArrayList arr = new ArrayList(dt.Rows.Count);

			foreach (DataRow row in dt.Rows)
			{
				arr.Add(MapRow2Record(row));
			}
			
			return (object[]) arr.ToArray(mRecordInfo.mRecordType);
		}


		/// <summary>
		/// Map a source row to a record.
		/// </summary>
		/// <param name="dr">The source DataRow</param>
		/// <returns>The mapped record containing the values of the DataRow</returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object MapRow2Record(DataRow dr)
		{
			object record = mRecordInfo.CreateRecordObject();
			//TypedReference t = TypedReference.MakeTypedReference(record, new FieldInfo[]) null);
				
			for(int i = 0; i < mMappings.Count; i++)
			{
				((MappingInfo) mMappings[i]).DataToField(dr, record);
			}

			return record;
		}

		/// <summary>
		/// Map a source row to a record.
		/// </summary>
		/// <param name="dr">The already opened DataReader</param>
		/// <returns>The mapped record containing the values of the DataReader</returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public object MapRow2Record(IDataReader dr)
		{
			object record = mRecordInfo.CreateRecordObject();
			//TypedReference t = TypedReference.MakeTypedReference(record, new FieldInfo[]) null);
				
			for(int i = 0; i < mMappings.Count; i++)
			{
				((MappingInfo) mMappings[i]).DataToField(dr, record);
			}

			return record;
		}

		/// <summary>
		/// Create an automatic mapping for each column in the dt and each record field 
		/// (the mapping is made by Index)
		/// </summary>
		/// <param name="dt">The source Datatable</param>
		/// <returns>The mapped records contained in the DataTable</returns>
		public object[] AutoMapDataTable2RecordsByIndex(DataTable dt)
		{
			//mMappings.TrimToSize();

			FieldInfo[] fields =
				mRecordInfo.mRecordType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField |
				BindingFlags.Instance | BindingFlags.IgnoreCase);
			

			if (fields.Length > dt.Columns.Count)
				throw new FileHelperException("The data table has less fields than fields in the Type: " +
					mRecordInfo.mRecordType.Name);
			
			for(int i = 0; i < fields.Length; i++)
			{
				MappingInfo map = new MappingInfo(fields[i]);
				map.mDataColumnIndex = i;
				mMappings.Add(map);
			}
			
			ArrayList arr = new ArrayList(dt.Rows.Count);

			foreach (DataRow row in dt.Rows)
			{
				object record = mRecordInfo.CreateRecordObject();
				//TypedReference t = TypedReference.MakeTypedReference(record, new FieldInfo[]) null);
				
				for(int i = 0; i < mMappings.Count; i++)
				{
					((MappingInfo) mMappings[i]).DataToField(row, record);
				}
				
				arr.Add(record);
			}
			
			return (object[]) arr.ToArray(mRecordInfo.mRecordType);
		}

	
		/// <summary>
		/// For each row in the datatable create a record.
		/// </summary>
		/// <param name="connection">A valid connection (Opened or not)</param>
		/// <param name="selectSql">The Sql statement used to return the records.</param>
		/// <returns>The mapped records contained in the DataTable</returns>
		public object[] MapDataReader2Records(IDbConnection connection, string selectSql)
		{
			ExHelper.CheckNullParam(connection, "connection");
			ExHelper.CheckNullOrEmpty(selectSql, "selectSql");

			IDbCommand cmd = connection.CreateCommand();
			cmd.CommandText = selectSql;

			IDataReader dr = null;
			object[] res;
			bool connectionOpened = false;

			try
			{
				if (connection.State == ConnectionState.Closed)
				{
					connection.Open();
					connectionOpened = true;
				}

				dr = cmd.ExecuteReader(CommandBehavior.SingleResult);
				res = MapDataReader2Records(dr);
			}
			finally
			{
				if (dr != null)
					dr.Close();

				if (connectionOpened)
					connection.Close();
			}

			return res;
		}
	
		/// <summary>
		/// For each row in the datatable create a record.
		/// </summary>
		/// <param name="dr">The source DataReader</param>
		/// <returns>The mapped records contained in the DataTable</returns>
		public object[] MapDataReader2Records(IDataReader dr)
		{
			ExHelper.CheckNullParam(dr, "dr");

			mMappings.TrimToSize();
			
			ArrayList arr = new ArrayList();

			bool hasRows = true;
			if (dr is System.Data.SqlClient.SqlDataReader)
				hasRows = ((System.Data.SqlClient.SqlDataReader) dr).HasRows;
			else if (dr is System.Data.OleDb.OleDbDataReader)
				hasRows = ((System.Data.OleDb.OleDbDataReader) dr).HasRows;
			

			if (hasRows)
			{
				while (dr.Read())
				{
					arr.Add(MapRow2Record(dr));
				}
			}
			
			return (object[]) arr.ToArray(mRecordInfo.mRecordType);
		}

	
	
	}

	internal class MappingInfo
	{
		public MappingInfo(Type t, string field)
		{
			mField = t.GetField(field, (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance | BindingFlags.IgnoreCase));
			if (mField == null)
				throw new FileHelperException("The field: " + field + " was not found in the Type: " + t.Name);
		}

		public MappingInfo(FieldInfo fi)
		{
			mField = fi;
		}

		//public string mRecordField;
		public string mDataColumnName = null;
		public int mDataColumnIndex = -1;
		
		FieldInfo mField;
		
		public void FieldToData(object rec, DataRow row)
		{
			GetColumnIndex(row);
		}

		private void GetColumnIndex(DataRow row)
		{
			if (mDataColumnIndex == -1)
			{
				try
				{
					mDataColumnIndex = row.Table.Columns[mDataColumnName].Ordinal;
					if (mDataColumnIndex == -1)
						throw new FileHelperException("The column : " + mDataColumnName + " was not found in the datatable.");
				}
				catch
				{
					throw new FileHelperException("The column : " + mDataColumnName + " was not found in the datatable.");
				}
				
			}
			
		}

		private void GetColumnIndex(IDataReader dr)
		{
			if (mDataColumnIndex == -1)
			{
				try
				{
					mDataColumnIndex = dr.GetOrdinal(mDataColumnName);
					if (mDataColumnIndex == -1)
						throw new FileHelperException("The column : " + mDataColumnName + " was not found in the data reader.");
				}
				catch
				{
					throw new FileHelperException("The column : " + mDataColumnName + " was not found in the data reader.");
				}
				
			}
			
		}

		byte mCheckTypeFlag = 0;
		Type mColumnType;
		
		public void DataToField(DataRow row, object record) // TypedReference t)
		{
			try
			{
				GetColumnIndex(row);
			
				if (mCheckTypeFlag == 0)
				{
					mColumnType = row.Table.Columns[mDataColumnIndex].DataType;
					if (mColumnType == mField.FieldType)
						mCheckTypeFlag = 1;
					else
						mCheckTypeFlag = 2;
				}
			
				if (mCheckTypeFlag == 1)
					mField.SetValue(record, row[mDataColumnIndex]);
				else if (row[mDataColumnIndex] != DBNull.Value)
					mField.SetValue(record, Convert.ChangeType(row[mDataColumnIndex], mField.FieldType));
				else // IS DB NULl
				{
					if (mColumnType == typeof(string))
						mField.SetValue(record, string.Empty);
				}
				
			}
			catch
			{
				throw new FileHelperException(string.Format("Error converting value: {0} to {1} in the column with index {2} and the field {3}", row[mDataColumnIndex], mField.FieldType, mDataColumnIndex, mField.Name));
			}
			
		}

	
		public void DataToField(IDataReader dr, object record) // TypedReference t)
		{
			try
			{
				GetColumnIndex(dr);
			
				if (mCheckTypeFlag == 0)
				{
					mColumnType = dr.GetFieldType(mDataColumnIndex);
					if (mColumnType == mField.FieldType)
						mCheckTypeFlag = 1;
					else
						mCheckTypeFlag = 2;
				}
			
				if (mCheckTypeFlag == 1)
					mField.SetValue(record, dr[mDataColumnIndex]);
				else if (dr[mDataColumnIndex] != DBNull.Value)
					mField.SetValue(record, Convert.ChangeType(dr[mDataColumnIndex], mField.FieldType));
				else // IS DB NULl
				{
					if (mColumnType == typeof(string))
						mField.SetValue(record, string.Empty);
				}
				
			}
			catch
			{
				throw new FileHelperException(string.Format("Error converting value: {0} to {1} in the column with index {2} and the field {3}", dr[mDataColumnIndex], mField.FieldType, mDataColumnIndex, mField.Name));
			}
			
		}

	}
	
}
