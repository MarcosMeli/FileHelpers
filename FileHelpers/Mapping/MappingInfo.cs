using System;
using System.Data;
using System.Reflection;
using System.Text;

//#if V_3_0
namespace FileHelpers.Mapping
{
    /// <summary>
    /// Extract mapping information from a type definition
    /// </summary>
    internal class MappingInfo
    {
        /// <summary>
        /// Extract mapping information from type for a given name
        /// </summary>
        /// <param name="t">Type of object we are basing mapping on</param>
        /// <param name="field">Field name we are trying to find</param>
        public MappingInfo(Type t, string field)
        {
            Type useThis = t;

            do
            {
                mField = useThis.GetField(field,
                                    (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance |
                                     BindingFlags.IgnoreCase | BindingFlags.FlattenHierarchy));

                if (mField == null)
                    useThis = useThis.BaseType;

            } while (mField == null && useThis != null);

            if (mField == null)
                throw new FileHelpersException("The field: " + field + " was not found in the Type: " + t.Name);
        }

        /// <summary>
        /// Provide mapping information given field information
        /// </summary>
        /// <param name="fi"></param>
        public MappingInfo(FieldInfo fi)
        {
            mField = fi;
        }

        //public string mRecordField;

        /// <summary>
        /// Column name in data table
        /// </summary>
        public string mDataColumnName = null;

        /// <summary>
        /// Index in the data table
        /// </summary>
        public int mDataColumnIndex = -1;

        /// <summary>
        /// Field information we are mapping to
        /// </summary>
        FieldInfo mField;

        /// <summary>
        /// Update DataTable with the contents of rec
        /// </summary>
        /// <param name="rec">Object record to get data from</param>
        /// <param name="row">DataRow to update</param>
        public void FieldToData(object rec, DataRow row)
        {
            GetColumnIndex(row);
            // TODO:  Implement this feature....
        }

        /// <summary>
        /// Get the index into the DataTable given a row of data and the data column name
        /// </summary>
        /// <param name="row">Row to test for column name</param>
        private void GetColumnIndex(DataRow row)
        {
            if (mDataColumnIndex == -1)
            {
                try
                {
                    mDataColumnIndex = row.Table.Columns[mDataColumnName].Ordinal;
                    if (mDataColumnIndex == -1)
                        throw new FileHelpersException("The column : " + mDataColumnName + " was not found in the datatable.");
                }
                catch
                {
                    throw new FileHelpersException("The column : " + mDataColumnName + " was not found in the datatable.");
                }
            }
        }

        /// <summary>
        /// Update the data column index based on the DataTable column name
        /// </summary>
        /// <param name="dr">Data reader we are checking</param>
        private void GetColumnIndex(IDataReader dr)
        {
            if (mDataColumnIndex == -1)
            {
                try
                {
                    mDataColumnIndex = dr.GetOrdinal(mDataColumnName);
                    if (mDataColumnIndex == -1)
                        throw new FileHelpersException("The column : " + mDataColumnName + " was not found in the data reader.");
                }
                catch
                {
                    throw new FileHelpersException("The column : " + mDataColumnName + " was not found in the data reader.");
                }
            }
        }

        byte mCheckTypeFlag = 0;
        Type mColumnType;

        /// <summary>
        /// Assign cell from DataTable into an object
        /// </summary>
        /// <param name="row">Row to extract cell from</param>
        /// <param name="record">Field record to update</param>
        public void DataToField(DataRow row, object record) // TypedReference t)
        {
            try
            {
                GetColumnIndex(row);


                //  TODO:  Repeated code between this and the following subroutine should be factored out
                if (mCheckTypeFlag == 0)
                {
                    mColumnType = row.Table.Columns[mDataColumnIndex].DataType;
                    if (mColumnType == mField.FieldType)
                        mCheckTypeFlag = 1;
                    else
                        mCheckTypeFlag = 2;
                }

                if (row[mDataColumnIndex] == DBNull.Value)
                {
                    // Is DB Null
                    if (mColumnType == typeof(string))
                        mField.SetValue(record, string.Empty);
                }
                else if (mCheckTypeFlag == 1)
                    mField.SetValue(record, row[mDataColumnIndex]);
                else
                    mField.SetValue(record, Convert.ChangeType(row[mDataColumnIndex], mField.FieldType));
            }
            catch
            {
                throw new FileHelpersException(string.Format("Error converting: {0} ({4}) to {1} in the column with index {2} and the field {3}", row[mDataColumnIndex], mField.FieldType, mDataColumnIndex, mField.Name, row[mDataColumnIndex]));
            }
        }


        /// <summary>
        /// Assign cell from DataTable reader into an object
        /// </summary>
        /// <param name="row">date reader to extract cell from</param>
        /// <param name="record">Field record to update</param>
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
                throw new FileHelpersException(string.Format("Error converting value: {0} to {1} in the column with index {2} and the field {3}", dr[mDataColumnIndex], mField.FieldType, mDataColumnIndex, mField.Name));
            }
        }
    }
}

//#endif