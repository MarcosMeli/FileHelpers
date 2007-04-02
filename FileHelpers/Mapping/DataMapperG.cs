//#undef GENERICS
#define GENERICS
#if NET_2_0

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;
#if GENERICS
using System.Collections.Generic;
#endif


namespace FileHelpers.Mapping
{

	/// <summary>
    /// <para>A class to provide Record-DataTable operations.</para>
    /// <para>(BETA QUALITY, use it at your own risk :P, API can change in future version)</para>
	/// </summary>
#if ! GENERICS
    public sealed class DataMapper
#else
    /// <typeparam name="T">The record Type</typeparam>
    public sealed class DataMapper<T>
#endif
    {
		RecordInfo mRecordInfo;
		
#if ! GENERICS
        /// <summary>
        /// Create a new Mapping for the record Type 't'.
        /// </summary>
        /// <param name="recordType">The record class.</param>
        public DataMapper(Type recordType)
        {
            mRecordInfo = new RecordInfo(recordType);
        }

#else
        /// <summary>
		/// Create a new Mapping for the record Type 't'.
		/// </summary>
		/// <param name="t">The record class.</param>
        public DataMapper()
		{
			mRecordInfo = new RecordInfo(typeof(T));
		}

#endif
        private int mInitialColumnOffset = 0;

        /// <summary>
        /// Indicates the number of columns to discard in the result.
        /// </summary>
        public int InitialColumnOffset
        {
            get { return mInitialColumnOffset; }
            set { mInitialColumnOffset = value; }
        }

		/// <summary>
		/// Add a new mapping between column at <paramref>columnIndex</paramref> and the fieldName with the specified <paramref>fieldName</paramref> name.
		/// </summary>
		/// <param name="columnIndex">The index in the Datatable</param>
		/// <param name="fieldName">The name of a fieldName in the Record Class</param>
		public void AddMapping(int columnIndex, string fieldName)
		{
			MappingInfo map = new MappingInfo(mRecordInfo.mRecordType, fieldName);
			map.mDataColumnIndex = columnIndex + mInitialColumnOffset;
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
#if ! GENERICS
		public object[] MapDataTable2Records(DataTable dt)
        {
            ArrayList arr = new ArrayList(dt.Rows.Count);
#else
		public T[] MapDataTable2Records(DataTable dt)
        {
			List<T> arr = new List<T>(dt.Rows.Count);
#endif

            mMappings.TrimToSize();
            foreach (DataRow row in dt.Rows)
			{
				arr.Add(MapRow2Record(row));
			}
			
#if ! GENERICS
			return (object[]) arr.ToArray(mRecordInfo.mRecordType);
#else
			return arr.ToArray();
#endif
        }


		/// <summary>
		/// Map a source row to a record.
		/// </summary>
		/// <param name="dr">The source DataRow</param>
		/// <returns>The mapped record containing the values of the DataRow</returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
#if ! GENERICS
		public object MapRow2Record(DataRow dr)
        {
            object record = mRecordInfo.CreateRecordObject();
#else
		public T MapRow2Record(DataRow dr)
        {
			T record = (T) mRecordInfo.CreateRecordObject();
#endif
			
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
#if ! GENERICS
		public object MapRow2Record(IDataReader dr)
		{
            object record = mRecordInfo.CreateRecordObject();
#else
		public T MapRow2Record(IDataReader dr)
		{
            T record = (T) mRecordInfo.CreateRecordObject();
#endif
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
#if ! GENERICS
        public object[] AutoMapDataTable2RecordsByIndex(DataTable dt)
        {
            ArrayList arr = new ArrayList(dt.Rows.Count);
#else
        public T[] AutoMapDataTable2RecordsByIndex(DataTable dt)
		{
            List<T> arr = new List<T>(dt.Rows.Count);
#endif

            FieldInfo[] fields =
				mRecordInfo.mRecordType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField |
				BindingFlags.Instance | BindingFlags.IgnoreCase);
			

			if (fields.Length > dt.Columns.Count)
				throw new FileHelpersException("The data table has less fields than fields in the Type: " +
					mRecordInfo.mRecordType.Name);
			
			for(int i = 0; i < fields.Length; i++)
			{
				MappingInfo map = new MappingInfo(fields[i]);
				map.mDataColumnIndex = i;
				mMappings.Add(map);
			}
			

			foreach (DataRow row in dt.Rows)
			{
#if ! GENERICS
				object record = mRecordInfo.CreateRecordObject();
#else
				T record = (T) mRecordInfo.CreateRecordObject();
#endif
                //TypedReference t = TypedReference.MakeTypedReference(record, new FieldInfo[]) null);
				
				for(int i = 0; i < mMappings.Count; i++)
				{
					((MappingInfo) mMappings[i]).DataToField(row, record);
				}
				
				arr.Add(record);
			}
			
#if ! GENERICS
            return (object[])arr.ToArray(mRecordInfo.mRecordType);
#else
            return arr.ToArray();
#endif
        }

	
		/// <summary>
		/// For each row in the datatable create a record.
		/// </summary>
		/// <param name="connection">A valid connection (Opened or not)</param>
		/// <param name="selectSql">The Sql statement used to return the records.</param>
		/// <returns>The mapped records contained in the DataTable</returns>
#if ! GENERICS
        public object[] MapDataReader2Records(IDbConnection connection, string selectSql)
        {
            object[] res;
#else
        public T[] MapDataReader2Records(IDbConnection connection, string selectSql)
        {
            T[] res;
#endif
            
            ExHelper.CheckNullParam(connection, "connection");
			ExHelper.CheckNullOrEmpty(selectSql, "selectSql");

			IDbCommand cmd = connection.CreateCommand();
			cmd.CommandText = selectSql;

			IDataReader dr = null;
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
#if ! GENERICS
		public object[] MapDataReader2Records(IDataReader dr)
        {
            ArrayList arr = new ArrayList();
#else
		public T[] MapDataReader2Records(IDataReader dr)
        {
            List<T> arr = new List<T>();
#endif

            ExHelper.CheckNullParam(dr, "dr");

			mMappings.TrimToSize();
			

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
#if ! GENERICS			
            return (object[])arr.ToArray(mRecordInfo.mRecordType);
#else
			return arr.ToArray();
#endif
        }

	
	
	}


}
#endif