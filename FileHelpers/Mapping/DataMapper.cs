using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace FileHelpers.Mapping
{

	public class DataMapper
	{
		RecordInfo mRecordInfo;
		
		public DataMapper(Type t)
		{
			mRecordInfo = new RecordInfo(t);
		}

		private int mGlobalOffset = 0;

		public int GlobalOffset
		{
			get { return mGlobalOffset; }
			set { mGlobalOffset = value; }
		}

		public void AddMapping(int columnIndex, string field)
		{
			MappingInfo map = new MappingInfo(mRecordInfo.mRecordType, field);
			map.mDataColumnIndex = columnIndex + mGlobalOffset;
			mMappings.Add(map);
		}
		
		public void AddMapping(string columnName, string field)
		{
			MappingInfo map = new MappingInfo(mRecordInfo.mRecordType, field);
			map.mDataColumnName = columnName;
			mMappings.Add(map);
		}
		
		private ArrayList mMappings  = new ArrayList();
		
		public object[] MapRecords(DataTable dt)
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


		
		public object[] MapRecordsByIndex(DataTable dt)
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

		public string mRecordField;
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
				catch (Exception ex)
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
			catch(Exception ex)
			{
				throw new FileHelperException(string.Format("Error converting value: {0} to {1} in the column with index {2} and the field {3}", row[mDataColumnIndex], mField.FieldType, mDataColumnIndex, mField.Name));
			}
			
		}
	}
	
}
