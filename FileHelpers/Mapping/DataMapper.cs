using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Collections.Generic;
//using Container=FileHelpers.Container;

//#if V_3_0

namespace FileHelpers.Mapping
{

    public sealed class DataMapper
        :DataMapper<object>
    {
        public DataMapper(Type recordType)
            :base(recordType)
        {
        }
    }


	/// <summary>
    /// <para>A class to provide DataTable - DataReader - Records operations.</para>
    /// <para>(Use it at your own risk, API can change a lot in future version)</para>
	/// </summary>
    /// <typeparam name="T">The record Type</typeparam>
    public class DataMapper<T>
    {
		internal IRecordInfo mRecordInfo;
		

        /// <summary>
		/// Create a new Mapping for the record Type 't'.
		/// </summary>
		public DataMapper()
            :this(typeof(T))
		{
			
		}

        internal DataMapper(Type recordType)
        {
            mRecordInfo = RecordInfo.Resolve(recordType); // Container.Resolve<IRecordInfo>(recordType);
        }

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
			MappingInfo map = new MappingInfo(mRecordInfo.RecordType, fieldName);
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
			MappingInfo map = new MappingInfo(mRecordInfo.RecordType, fieldName);
			map.mDataColumnName = columnName;
			mMappings.Add(map);
		}
		
		private ArrayList mMappings  = new ArrayList();
		
		/// <summary>
		/// For each row in the datatable create a record.
		/// </summary>
		/// <param name="dt">The source Datatable</param>
		/// <returns>The mapped records contained in the DataTable</returns>
		public T[] MapDataTable2Records(DataTable dt)
        {
			List<T> arr = new List<T>(dt.Rows.Count);

            mMappings.TrimToSize();
            foreach (DataRow row in dt.Rows)
			{
				arr.Add(MapRow2Record(row));
			}
			
			return arr.ToArray();
        }


		/// <summary>
		/// Map a source row to a record.
		/// </summary>
		/// <param name="dr">The source DataRow</param>
		/// <returns>The mapped record containing the values of the DataRow</returns>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public T MapRow2Record(DataRow dr)
        {
			T record = (T) mRecordInfo.Operations.CreateRecordHandler();
			
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
		public T MapRow2Record(IDataReader dr)
		{
            T record = (T) mRecordInfo.Operations.CreateRecordHandler();
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
        public T[] AutoMapDataTable2RecordsByIndex(DataTable dt)
		{
            List<T> arr = new List<T>(dt.Rows.Count);

            FieldInfo[] fields =
				mRecordInfo.RecordType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField |
				BindingFlags.Instance | BindingFlags.IgnoreCase);
			

			if (fields.Length > dt.Columns.Count)
				throw new FileHelpersException("The data table has less fields than fields in the Type: " +
					mRecordInfo.RecordType.Name);
			
			for(int i = 0; i < fields.Length; i++)
			{
				MappingInfo map = new MappingInfo(fields[i]);
				map.mDataColumnIndex = i;
				mMappings.Add(map);
			}
			

			foreach (DataRow row in dt.Rows)
			{
				T record = (T) mRecordInfo.Operations.CreateRecordHandler();
                //TypedReference t = TypedReference.MakeTypedReference(record, new FieldInfo[]) null);
				
				for(int i = 0; i < mMappings.Count; i++)
				{
					((MappingInfo) mMappings[i]).DataToField(row, record);
				}
				
				arr.Add(record);
			}
			
            return arr.ToArray();
        }

	
		/// <summary>
		/// For each row in the datatable create a record.
		/// </summary>
		/// <param name="connection">A valid connection (Opened or not)</param>
		/// <param name="selectSql">The Sql statement used to return the records.</param>
		/// <returns>The mapped records contained in the DataTable</returns>
        public T[] MapDataReader2Records(IDbConnection connection, string selectSql)
        {
            T[] res;
            
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
		/// For each row in the data reader create a record and return them.
		/// </summary>
		/// <param name="dr">The source DataReader</param>
		/// <returns>The mapped records contained in the DataTable</returns>
		public T[] MapDataReader2Records(IDataReader dr)
        {
            List<T> arr = new List<T>();
            ExHelper.CheckNullParam(dr, "dr");

			mMappings.TrimToSize();
			
			if (HasRows(dr))
			{
				while (dr.Read())
				{
					arr.Add(MapRow2Record(dr));
				}
			}
			return arr.ToArray();
        }



#if NET_2_0
        /// <summary>
		/// For each row in the data reader create a record and return them.
		/// </summary>
		/// <param name="dr">The source DataReader</param>
		/// <returns>The mapped records contained in the DataTable</returns>
		public IEnumerator<T> MapDataReader2RecordsEnum(IDataReader dr)
        {
            ExHelper.CheckNullParam(dr, "dr");

			mMappings.TrimToSize();
			
			if (HasRows(dr))
			{
				while (dr.Read())
				{
					yield return MapRow2Record(dr);
				}
			}
        }

#endif
        

		/// <summary>
		/// For each row in the data reader create a record and write them to the file
		/// </summary>
		/// <param name="filename">The destination file path</param>
		/// <param name="dr">The source DataReader</param>
		public void MapDataReader2File(IDataReader dr, string filename)
		{
			MapDataReader2File(dr, filename, false);
		}
	
		/// <summary>
		/// For each row in the data reader create a record and write them to the file
		/// </summary>
		/// <param name="filename">The destination file path</param>
		/// <param name="dr">The source DataReader</param>
		/// <param name="append">Indicates if the engine must append to the file or create a new one</param>
		public void MapDataReader2File(IDataReader dr, string filename, bool append)
		{
			ExHelper.CheckNullParam(dr, "dr");
			ExHelper.CheckNullOrEmpty(filename, "filename");

			mMappings.TrimToSize();
			
			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(mRecordInfo.RecordType);

			if (append)
				engine.BeginAppendToFile(filename);
			else
				engine.BeginWriteFile(filename);

			if (HasRows(dr))
			{
				while (dr.Read())
				{
					engine.WriteNext(MapRow2Record(dr));
				}
			}

			engine.Close();
		}

		/// <summary>
		/// For each row in the data table create a record and write them to the file
		/// </summary>
		/// <param name="filename">The destination file path</param>
		/// <param name="dt">The source Datatable</param>
		public void MapDataTable2File(DataTable dt, string filename)
		{
			MapDataTable2File(dt, filename, false);
		}
		/// <summary>
		/// For each row in the data table create a record and write them to the file
		/// </summary>
		/// <param name="filename">The destination file path</param>
		/// <param name="dt">The source Datatable</param>
		/// <param name="append">Indicates if the engine must append to the file or create a new one</param>
		public void MapDataTable2File(DataTable dt, string filename, bool append)
		{
			ExHelper.CheckNullParam(dt, "dt");
			ExHelper.CheckNullOrEmpty(filename, "filename");

			mMappings.TrimToSize();
			
			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(mRecordInfo.RecordType);
			
			if (append)
				engine.BeginAppendToFile(filename);
			else
				engine.BeginWriteFile(filename);

			for(int i = 0; i < dt.Rows.Count; i++)
			{
				engine.WriteNext(MapRow2Record(dt.Rows[i]));
			}

			engine.Close();
		}

		private static bool HasRows(IDataReader dr)
		{
#if NET_2_0
			if (dr is System.Data.Common.DbDataReader)
                return ((System.Data.Common.DbDataReader)dr).HasRows;
#else
			if (dr is System.Data.SqlClient.SqlDataReader)
				return ((System.Data.SqlClient.SqlDataReader) dr).HasRows;
			else if (dr is System.Data.OleDb.OleDbDataReader)
				return ((System.Data.OleDb.OleDbDataReader) dr).HasRows;
#endif
			return true;
		}
		
	}


}

//#endif