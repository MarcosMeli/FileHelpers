using System;
using System.Data;
using System.Reflection;
using System.Text;

namespace FileHelpers.Mapping
{
    internal class MappingInfo
    {
        public MappingInfo(Type t, string field)
        {
            mField = t.GetField(field, (BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance | BindingFlags.IgnoreCase));
            if (mField == null)
                throw new FileHelpersException("The field: " + field + " was not found in the Type: " + t.Name);
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
                        throw new FileHelpersException("The column : " + mDataColumnName + " was not found in the datatable.");
                }
                catch
                {
                    throw new FileHelpersException("The column : " + mDataColumnName + " was not found in the datatable.");
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
                throw new FileHelpersException(string.Format("Error converting value: {0} to {1} in the column with index {2} and the field {3}", row[mDataColumnIndex], mField.FieldType, mDataColumnIndex, mField.Name));
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
                throw new FileHelpersException(string.Format("Error converting value: {0} to {1} in the column with index {2} and the field {3}", dr[mDataColumnIndex], mField.FieldType, mDataColumnIndex, mField.Name));
            }

        }

    }
}
