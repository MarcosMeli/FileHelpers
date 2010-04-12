using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Text;

namespace FileHelpers
{
    internal sealed class RecordOperations 
        //: IRecordOperations

    {
        public IRecordInfo RecordInfo { get; private set; }

        public RecordOperations(IRecordInfo recordInfo)
        {
            RecordInfo = recordInfo;
        }

        #region "  StringToRecord  "
        public object StringToRecord(LineInfo line, object[] values)
        {
            if (MustIgnoreLine(line.mLineStr))
                return null;

            for (int i = 0; i < RecordInfo.FieldCount; i++)
            {
                values[i] = RecordInfo.Fields[i].ExtractFieldValue(line);
            }

            try
            {
                // Asign all values via dinamic method that creates an object and assign values
                return CreateHandler(values);
            }
            catch (InvalidCastException)
            {
                // Occurrs when the a custom converter returns an invalid value for the field.
                for (int i = 0; i < RecordInfo.FieldCount; i++)
                {
                    if (values[i] != null && !RecordInfo.Fields[i].FieldTypeInternal.IsInstanceOfType(values[i]))
                        throw new ConvertException(null,
                                                   RecordInfo.Fields[i].FieldTypeInternal,
                                                   RecordInfo.Fields[i].FieldInfo.Name,
                                                   line.mReader.LineNumber,
                                                   -1,
                                                   Messages.Errors.WrongConverter
                                                       .FieldName(RecordInfo.Fields[i].FieldInfo.Name)
                                                       .ConverterReturnedType(values[i].GetType().Name)
                                                       .FieldType(RecordInfo.Fields[i].FieldInfo.FieldType.Name)
                                                       .Text
                                                   ,
                                                   null);
                }
                return null;
            }
        }

        private bool MustIgnoreLine(string line)
        {
            if (RecordInfo.IgnoreEmptyLines)
                if ((RecordInfo.IgnoreEmptySpaces && line.TrimStart().Length == 0) ||
                    line.Length == 0)
                    return true;

            if (!String.IsNullOrEmpty(RecordInfo.CommentMarker))
                if ((RecordInfo.CommentAnyPlace && line.TrimStart().StartsWith(RecordInfo.CommentMarker)) ||
                    line.StartsWith(RecordInfo.CommentMarker))
                    return true;

            if (RecordInfo.RecordCondition != RecordCondition.None)
            {
                switch (RecordInfo.RecordCondition)
                {
                    case RecordCondition.ExcludeIfBegins:
                        return ConditionHelper.BeginsWith(line, RecordInfo.RecordConditionSelector);
                    case RecordCondition.IncludeIfBegins:
                        return !ConditionHelper.BeginsWith(line, RecordInfo.RecordConditionSelector);

                    case RecordCondition.ExcludeIfContains:
                        return ConditionHelper.Contains(line, RecordInfo.RecordConditionSelector);
                    case RecordCondition.IncludeIfContains:
                        return !ConditionHelper.Contains(line, RecordInfo.RecordConditionSelector);

                    case RecordCondition.ExcludeIfEnclosed:
                        return ConditionHelper.Enclosed(line, RecordInfo.RecordConditionSelector);
                    case RecordCondition.IncludeIfEnclosed:
                        return !ConditionHelper.Enclosed(line, RecordInfo.RecordConditionSelector);

                    case RecordCondition.ExcludeIfEnds:
                        return ConditionHelper.EndsWith(line, RecordInfo.RecordConditionSelector);
                    case RecordCondition.IncludeIfEnds:
                        return !ConditionHelper.EndsWith(line, RecordInfo.RecordConditionSelector);

                    case RecordCondition.ExcludeIfMatchRegex:
                        return RecordInfo.RecordConditionRegEx.IsMatch(line);

                    case RecordCondition.IncludeIfMatchRegex:
                        return !RecordInfo.RecordConditionRegEx.IsMatch(line);
                }
            }

            return false;
        }
        #endregion

        #region "  RecordToString  "

        public string RecordToString(object record)
        {
            var sb = new StringBuilder(RecordInfo.SizeHint);

            object[] mValues = ObjectToValuesHandler(record);

            for (int f = 0; f < RecordInfo.FieldCount; f++)
            {
                RecordInfo.Fields[f].AssignToString(sb, mValues[f]);
            }

            return sb.ToString();
        }

        public string RecordValuesToString(object[] recordValues)
        {
            var sb = new StringBuilder(RecordInfo.SizeHint);

            for (int f = 0; f < RecordInfo.FieldCount; f++)
            {
                RecordInfo.Fields[f].AssignToString(sb, recordValues[f]);
            }

            return sb.ToString();
        }
        #endregion

        #region "  ValuesToRecord  "
        /// <summary>Returns a record formed with the passed values.</summary>
        /// <param name="values">The source Values.</param>
        /// <returns>A record formed with the passed values.</returns>
        public object ValuesToRecord(object[] values)
        {
            for (int i = 0; i < RecordInfo.FieldCount; i++)
            {
                if (RecordInfo.Fields[i].FieldTypeInternal == typeof(DateTime) && values[i] is double)
                    values[i] = DoubleToDate((int)(double)values[i]);

                values[i] = RecordInfo.Fields[i].CreateValueForField(values[i]);
            }

            // Asign all values via dinamic method that creates an object and assign values
            return CreateHandler(values);
        }

        private static DateTime DoubleToDate(int serialNumber)
        {
            if (serialNumber < 59)
            {
                // Because of the 29-02-1900 bug, any serial date 
                // under 60 is one off... Compensate. 
                serialNumber++;
            }

            return new DateTime((serialNumber + 693593) * (10000000L * 24 * 3600));
        }
        #endregion

        #region "  RecordToValues  "
        /// <summary>Get an object[] of the values in the fields of the passed record.</summary>
        /// <param name="record">The source record.</param>
        /// <returns>An object[] of the values in the fields.</returns>
        public object[] RecordToValues(object record)
        {
            return ObjectToValuesHandler(record);
        }
        #endregion

        #region "  RecordsToDataTable  "
        public DataTable RecordsToDataTable(ICollection records)
        {
            return RecordsToDataTable(records, -1);
        }

        public DataTable RecordsToDataTable(ICollection records, int maxRecords)
        {
            DataTable res = CreateEmptyDataTable();

            res.BeginLoadData();

            res.MinimumCapacity = records.Count;

            if (maxRecords == -1)
            {
                foreach (object r in records)
                    res.Rows.Add(RecordToValues(r));
            }
            else
            {
                int i = 0;
                foreach (object r in records)
                {
                    if (i == maxRecords)
                        break;

                    res.Rows.Add(RecordToValues(r));
                    i++;
                }
            }

            res.EndLoadData();
            return res;
        }

        public DataTable CreateEmptyDataTable()
        {
            var res = new DataTable();

            foreach (FieldBase f in RecordInfo.Fields)
            {
                DataColumn column1 = res.Columns.Add(f.FieldInfo.Name, f.FieldInfo.FieldType);
                column1.ReadOnly = true;
            }
            return res;
        }


        #endregion



        #region "  Lightweight code generation (NET 2.0)  "

        // Create on first usage
        private ValuesToObjectDelegate mCreateHandler;
        private CreateObjectDelegate mFastConstructor;
        private ObjectToValuesDelegate mObjectToValuesHandler;

        private ObjectToValuesDelegate ObjectToValuesHandler
        {
            get
            {
                if (mObjectToValuesHandler == null)
                    mObjectToValuesHandler = ReflectionHelper.ObjectToValuesMethod(RecordInfo.RecordType, GetFieldInfoArray());
                return mObjectToValuesHandler;
            }
        }


        private ValuesToObjectDelegate CreateHandler
        {
            get
            {
                if (mCreateHandler == null)
                    mCreateHandler = ReflectionHelper.ValuesToObjectMethod(RecordInfo.RecordType, GetFieldInfoArray());
                return mCreateHandler;
            }
        }

        internal CreateObjectDelegate CreateRecordHandler
        {
            get
            {
                if (mFastConstructor == null)
                    mFastConstructor = ReflectionHelper.CreateFastConstructor(RecordInfo.RecordType);
                return mFastConstructor;
            }
        }


        #endregion

        private FieldInfo[] GetFieldInfoArray()
        {
            var res = new FieldInfo[RecordInfo.Fields.Length];

            for (int i = 0; i < RecordInfo.Fields.Length; i++)
            {
                res[i] = RecordInfo.Fields[i].FieldInfo;
            }
            return res;
        }

        public RecordOperations Clone(RecordInfo ri)
        {
            var res = new RecordOperations(ri);
            res.mCreateHandler = mCreateHandler;
            res.mFastConstructor = mFastConstructor;
            res.mObjectToValuesHandler = mObjectToValuesHandler;
            return res;
        }

       
    }
}