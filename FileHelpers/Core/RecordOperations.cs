using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Text;
using FileHelpers.Core;

namespace FileHelpers
{
    /// <summary>
    /// Collection of operations that we perform on a type, cached for reuse
    /// </summary>
    internal sealed class RecordOperations
    {
        /// <summary>
        /// Record Info we use to parse the record and generate an object instance
        /// </summary>
        private readonly IRecordInfo mRecordInfo;

        /// <summary>
        /// Create a set of operations for a particular type
        /// </summary>
        /// <param name="recordInfo">Record details we create objects off</param>
        public RecordOperations(IRecordInfo recordInfo)
        {
            mRecordInfo = recordInfo;
        }

        #region "  StringToRecord  "

        /// <summary>
        /// Process a line and turn it into an object
        /// </summary>
        /// <param name="line">Line of data to process</param>
        /// <param name="values">Values to assign to object</param>
        /// <returns>Object created or null if record skipped</returns>
        /// <exception cref="ConvertException">Could not convert data from input file</exception>
        public object StringToRecord(LineInfo line, object[] values)
        {
            if (MustIgnoreLine(line.mLineStr))
                return null;

            for (int i = 0; i < mRecordInfo.FieldCount; i++)
                values[i] = mRecordInfo.Fields[i].ExtractFieldValue(line);

            try {
                // Assign all values via dynamic method that creates an object and assign values
                return CreateHandler(values);
            }
            catch (InvalidCastException ex) {
                // Occurs when a custom converter returns an invalid value for the field.
                for (int i = 0; i < mRecordInfo.FieldCount; i++) {
                    if (values[i] != null &&
                        !mRecordInfo.Fields[i].FieldTypeInternal.IsInstanceOfType(values[i])) {
                        throw new ConvertException(null,
                            mRecordInfo.Fields[i].FieldTypeInternal,
                            mRecordInfo.Fields[i].FieldInfo.Name,
                            line.mReader.LineNumber,
                            -1,
                            $"The converter for the field: {mRecordInfo.Fields[i].FieldInfo.Name} returns an object of Type: {values[i].GetType().Name} and the field is of type: {mRecordInfo.Fields[i].FieldInfo.FieldType.Name}"
                            ,
                            ex);
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Extract fields from record and assign values to the object
        /// </summary>
        /// <param name="record">Object to assign to</param>
        /// <param name="line">Line of data</param>
        /// <param name="values">Array of values extracted</param>
        /// <returns>true if we processed the line and updated object</returns>
        public bool StringToRecord(object record, LineInfo line, object[] values)
        {
            if (MustIgnoreLine(line.mLineStr))
                return false;

            for (int i = 0; i < mRecordInfo.FieldCount; i++)
                values[i] = mRecordInfo.Fields[i].ExtractFieldValue(line);

            try {
                // Assign all values via dynamic method that
                AssignHandler(record, values);
                return true;
            }
            catch (InvalidCastException ex) {
                // Occurs when a custom converter returns an invalid value for the field.
                for (int i = 0; i < mRecordInfo.FieldCount; i++) {
                    if (values[i] != null &&
                        !mRecordInfo.Fields[i].FieldTypeInternal.IsInstanceOfType(values[i])) {
                        throw new ConvertException(null,
                            mRecordInfo.Fields[i].FieldTypeInternal,
                            mRecordInfo.Fields[i].FieldInfo.Name,
                            line.mReader.LineNumber,
                            -1,
                            $"The converter for the field: {mRecordInfo.Fields[i].FieldInfo.Name} returns an object of Type: {values[i].GetType().Name} and the field is of type: {mRecordInfo.Fields[i].FieldInfo.FieldType.Name}"
                            ,
                            ex);
                    }
                }
                throw;
            }
        }

        /// <summary>
        /// If we skip empty lines or it is a comment or is is excluded by settings
        /// </summary>
        /// <param name="line">Input line we are testing</param>
        /// <returns>True if line is skipped</returns>
        private bool MustIgnoreLine(string line)
        {
            if (mRecordInfo.IgnoreEmptyLines) {
                if ((mRecordInfo.IgnoreEmptySpaces && string.IsNullOrWhiteSpace(line)) ||
                    line.Length == 0)
                    return true;
            }

            if (!string.IsNullOrEmpty(mRecordInfo.CommentMarker)) {
                if ((mRecordInfo.CommentAnyPlace && StartsWithIgnoringWhiteSpaces(line, mRecordInfo.CommentMarker, StringComparison.Ordinal)) ||
                    line.StartsWith(mRecordInfo.CommentMarker, StringComparison.Ordinal))
                    return true;
            }

            if (mRecordInfo.RecordCondition != RecordCondition.None) {
                switch (mRecordInfo.RecordCondition) {
                    case RecordCondition.ExcludeIfBegins:
                        return ConditionHelper.BeginsWith(line, mRecordInfo.RecordConditionSelector);
                    case RecordCondition.IncludeIfBegins:
                        return !ConditionHelper.BeginsWith(line, mRecordInfo.RecordConditionSelector);

                    case RecordCondition.ExcludeIfContains:
                        return ConditionHelper.Contains(line, mRecordInfo.RecordConditionSelector);
                    case RecordCondition.IncludeIfContains:
                        return !ConditionHelper.Contains(line, mRecordInfo.RecordConditionSelector);

                    case RecordCondition.ExcludeIfEnclosed:
                        return ConditionHelper.Enclosed(line, mRecordInfo.RecordConditionSelector);
                    case RecordCondition.IncludeIfEnclosed:
                        return !ConditionHelper.Enclosed(line, mRecordInfo.RecordConditionSelector);

                    case RecordCondition.ExcludeIfEnds:
                        return ConditionHelper.EndsWith(line, mRecordInfo.RecordConditionSelector);
                    case RecordCondition.IncludeIfEnds:
                        return !ConditionHelper.EndsWith(line, mRecordInfo.RecordConditionSelector);

                    case RecordCondition.ExcludeIfMatchRegex:
                        return mRecordInfo.RecordConditionRegEx.IsMatch(line);

                    case RecordCondition.IncludeIfMatchRegex:
                        return !mRecordInfo.RecordConditionRegEx.IsMatch(line);
                }
            }

            return false;
        }

        #endregion

        #region "  RecordToString  "

        /// <summary>
        /// Create a string of the object based on a record information supplied
        /// </summary>
        /// <param name="record">Object to convert</param>
        /// <returns>String representing the object</returns>
        public string RecordToString(object record)
        {
            var sb = new StringBuilder(mRecordInfo.SizeHint);

            var values = ObjectToValuesHandler(record);

            for (int f = 0; f < mRecordInfo.FieldCount; f++)
                mRecordInfo.Fields[f].AssignToString(sb, values[f]);

            return sb.ToString();
        }

        /// <summary>
        /// Assign a series of values out to a string based on file info layout
        /// </summary>
        /// <param name="recordValues">Values to write in order</param>
        /// <returns>String representing values</returns>
        public string RecordValuesToString(object[] recordValues)
        {
            var sb = new StringBuilder(mRecordInfo.SizeHint);

            for (int f = 0; f < mRecordInfo.FieldCount; f++)
                mRecordInfo.Fields[f].AssignToString(sb, recordValues[f]);

            return sb.ToString();
        }

        #endregion

        #region "  ValuesToRecord  "

        /// <summary>Returns a record formed with the passed values.</summary>
        /// <param name="values">The source Values.</param>
        /// <returns>A record formed with the passed values.</returns>
        public object ValuesToRecord(object[] values)
        {
            for (int i = 0; i < mRecordInfo.FieldCount; i++)
            {
                try
                {
                    values[i] = mRecordInfo.Fields[i].CreateValueForField(values[i]);
                }
                catch (ConvertException ex)
                {
                    ex.FieldName = mRecordInfo.Fields[i].FieldName;
                    ex.ColumnNumber = i + 1;
                    throw;
                }
            }

            // Assign all values via dynamic method that creates an object and assign values
            return CreateHandler(values);
        }

        #endregion

        #region "  RecordToValues  "

        /// <summary>Get an object[] of the values in the fields of the instance.</summary>
        /// <param name="record">Instance of the type.</param>
        /// <returns>An object[] of the values in the fields.</returns>
        public object[] RecordToValues(object record)
        {
            return ObjectToValuesHandler(record);
        }

        #endregion

        #region "  RecordsToDataTable  "

        /// <summary>
        /// Create a datatable based on a collection of records
        /// </summary>
        /// <param name="records">Collection of records to process</param>
        /// <returns>datatable representing all records in collection</returns>
        public DataTable RecordsToDataTable(ICollection records)
        {
            return RecordsToDataTable(records, -1);
        }

        /// <summary>
        /// Create a data table containing at most maxRecords (-1 is unlimitted)
        /// </summary>
        /// <param name="records">Records to add to datatable</param>
        /// <param name="maxRecords">Maximum number of records (-1 is all)</param>
        /// <returns>Datatable based on record</returns>
        public DataTable RecordsToDataTable(ICollection records, int maxRecords)
        {
            DataTable res = CreateEmptyDataTable();

            res.BeginLoadData();

            res.MinimumCapacity = records.Count;

            if (maxRecords == -1) {
                foreach (var r in records)
                    res.Rows.Add(RecordToValues(r));
            }
            else {
                int i = 0;
                foreach (var r in records) {
                    if (i == maxRecords)
                        break;

                    res.Rows.Add(RecordToValues(r));
                    i++;
                }
            }

            res.EndLoadData();
            return res;
        }

        /// <summary>
        /// Create an empty datatable based upon the record layout
        /// </summary>
        /// <returns>Datatable defined based on the record definition</returns>
        public DataTable CreateEmptyDataTable()
        {
            var res = new DataTable();

            foreach (var f in mRecordInfo.Fields) {
                DataColumn column1;
                if (f.IsNullableType) {
                    column1 = res.Columns.Add(f.FieldInfo.Name, Nullable.GetUnderlyingType(f.FieldInfo.FieldType));
                    column1.AllowDBNull = true;
                }
                else
                    column1 = res.Columns.Add(f.FieldInfo.Name, f.FieldInfo.FieldType);

                column1.ReadOnly = true;
            }
            return res;
        }

        #endregion

        #region "  Lightweight code generation (NET 2.0)  "

        // Create on first usage
        private CreateAndAssignDelegate mCreateHandler;
        private AssignDelegate mAssignHandler;
        private CreateObjectDelegate mFastConstructor;
        private ObjectToValuesDelegate mObjectToValuesHandler;

        /// <summary>
        /// Function to take and instance and return an array of objects
        /// Dynamically created when first used
        /// </summary>
        private ObjectToValuesDelegate ObjectToValuesHandler
        {
            get
            {
                return mObjectToValuesHandler ??
                       (mObjectToValuesHandler =
                           ReflectionHelper.ObjectToValuesMethod(mRecordInfo.RecordType, GetFieldInfoArray()));
            }
        }

        /// <summary>
        /// function to create the object and assign the values to that object
        /// </summary>
        private CreateAndAssignDelegate CreateHandler
        {
            get
            {
                if (mCreateHandler == null) {
                    mCreateHandler = ReflectionHelper.CreateAndAssignValuesMethod(mRecordInfo.RecordType,
                        GetFieldInfoArray());
                }
                return mCreateHandler;
            }
        }

        /// <summary>
        /// First time through create a dynamic method to assign the data to the class object based on the fields
        /// </summary>
        private AssignDelegate AssignHandler
        {
            get
            {
                if (mAssignHandler == null)
                    mAssignHandler = ReflectionHelper.AssignValuesMethod(mRecordInfo.RecordType, GetFieldInfoArray());
                return mAssignHandler;
            }
        }

        /// <summary>
        /// Create an instance of the object function
        /// </summary>
        internal CreateObjectDelegate CreateRecordHandler
        {
            get
            {
                if (mFastConstructor == null)
                    mFastConstructor = ReflectionHelper.CreateFastConstructor(mRecordInfo.RecordType);
                return mFastConstructor;
            }
        }

        #endregion

        /// <summary>
        /// Extract the fieldinfo from the record info
        /// </summary>
        /// <returns>array of fieldInfo</returns>
        private FieldInfo[] GetFieldInfoArray()
        {
            var res = new FieldInfo[mRecordInfo.Fields.Length];

            for (int i = 0; i < mRecordInfo.Fields.Length; i++)
                res[i] = mRecordInfo.Fields[i].FieldInfo;
            return res;
        }

        /// <summary>
        /// Copy one object to another based on field list
        /// </summary>
        /// <param name="ri">Record layout instance</param>
        /// <returns>Copy of the handlers class is using</returns>
        public RecordOperations Clone(RecordInfo ri)
        {
            var res = new RecordOperations(ri)
            {
                mCreateHandler = mCreateHandler,
                mFastConstructor = mFastConstructor,
                mObjectToValuesHandler = mObjectToValuesHandler
            };
            return res;
        }

        /// <summary>
        /// Determines whether the beginning of this string instance matches the specified string ignoring white spaces at the start.
        /// </summary>
        /// <param name="source">source string.</param>
        /// <param name="value">The string to compare.</param>
        /// <param name="comparisonType">string comparison type.</param>
        /// <returns></returns>
        public static bool StartsWithIgnoringWhiteSpaces(string source, string value, StringComparison comparisonType)
        {
            if (value == null)
            {
                return false;
            }
            // find lower bound
            int i = 0;
            int sz = source.Length;
            while (i < sz && char.IsWhiteSpace(source[i]))
            {
                i++;
            }
            // adjust upper bound
            sz = sz - i;
            if (sz < value.Length)
                return false;
            sz = value.Length;
            // search
            return source.IndexOf(value, i, sz, comparisonType) == i;
        }
    }
}
