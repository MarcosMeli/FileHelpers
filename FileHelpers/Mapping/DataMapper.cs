using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace FileHelpers.Mapping
{

	/// <summary>
	/// A class to provide Record-DataTable operations.
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
	}
	
}
