using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace FileHelpers
{


    /// <summary>An internal class used to store information about the Record Type.</summary>
    /// <remarks>Is public to provide extensibility of DataSorage from outside the library.</remarks>
    internal sealed class RecordInfo : IRecordInfo
    {
        private readonly IFieldInfoCacheManipulator mFieldInfoCacheManipulator;

        #region "  Internal Fields  "
        // Cache of all the fields that must be used for a Type
        // More info at:  http://www.filehelpers.com/forums/viewtopic.php?t=387
        // Thanks Brian for the report, research and fix
        private static readonly Dictionary<Type, List<FieldInfo>> mCachedRecordFields =
            new Dictionary<Type, List<FieldInfo>>();
        private Regex mConditionRegEx;
        #endregion

        // --------------------------------------
        // Constructor and Init Methods

        #region IRecordInfo Members
        public Type RecordType { get; private set; }
        public bool IgnoreEmptyLines { get; set; }
        public bool IgnoreEmptySpaces { get; private set; }
        public string CommentMarker { get; set; }
        public int FieldCount { get; private set; }
        public FieldBase[] Fields { get; private set; }
        public int IgnoreFirst { get; set; }
        public int IgnoreLast { get; set; }
        public bool NotifyRead { get; private set; }
        public bool NotifyWrite { get; private set; }
        public bool CommentAnyPlace { get; set; }
        public RecordCondition RecordCondition { get; set; }
        public string RecordConditionSelector { get; set; }

        public bool IsDelimited
        {
            get { return Fields[0] is DelimitedField; }
        }
        #endregion

        #region "  Constructor "

        public RecordInfo(Type recordType, IFieldInfoCacheManipulator fieldInfoCacheManipulator)
        {
            this.mFieldInfoCacheManipulator = fieldInfoCacheManipulator;
            RecordConditionSelector = String.Empty;
            RecordCondition = RecordCondition.None;
            CommentAnyPlace = true;
            RecordType = recordType;
            InitRecordFields();
        }

        private void InitRecordFields()
        {
            //Debug.Assert(false, "TODO: Add RecordFilter to the engine.");
            var recordAttribute = Attributes.GetFirstInherited<TypedRecordAttribute>(RecordType);

            if (recordAttribute == null)
                throw new BadUsageException("The class " + RecordType.Name +
                                            " must be marked with the [DelimitedRecord] or [FixedLengthRecord] Attribute.");


            if (ReflectionHelper.GetDefaultConstructor(RecordType) == null)
                throw new BadUsageException("The record class " + RecordType.Name +
                                            " need a constructor with no args (public or private)");

            
            Attributes.WorkWithFirst<IgnoreFirstAttribute>(RecordType, 
                a => IgnoreFirst = a.NumberOfLines);
            
            Attributes.WorkWithFirst<IgnoreLastAttribute>(RecordType,
                a => IgnoreLast = a.NumberOfLines);

            Attributes.WorkWithFirst<IgnoreEmptyLinesAttribute>(RecordType,
                                                                  (a) =>
                                                                      {
                                                                          IgnoreEmptyLines = true;
                                                                          IgnoreEmptySpaces = a.mIgnoreSpaces;
                                                                      });


            Attributes.WorkWithFirst<IgnoreCommentedLinesAttribute>(RecordType,
                                                          (a) =>
                                                          {
                                                              IgnoreEmptyLines = true;
                                                              CommentMarker = a.mCommentMarker;
                                                              CommentAnyPlace = a.mAnyPlace;
                                                          });


            Attributes.WorkWithFirst<ConditionalRecordAttribute>(RecordType,
                                              (a) =>
                                              {
                                                  RecordCondition = a.Condition;
                                                  RecordConditionSelector = a.ConditionSelector;

                                                  if (RecordCondition == RecordCondition.ExcludeIfMatchRegex ||
                                                      RecordCondition == RecordCondition.IncludeIfMatchRegex)
                                                  {
                                                      mConditionRegEx = new Regex(RecordConditionSelector,
                                                                                  RegexOptions.Compiled | RegexOptions.IgnoreCase |
                                                                                  RegexOptions.ExplicitCapture);
                                                  }

                                              });


           
            if (typeof (INotifyRead).IsAssignableFrom(RecordType))
                NotifyRead = true;

            if (typeof (INotifyWrite).IsAssignableFrom(RecordType))
                NotifyWrite = true;

            // Create fields
            // Search for cached fields
            List<FieldInfo> fields;

            lock (mCachedRecordFields)
            {
                if (!mCachedRecordFields.TryGetValue(RecordType, out fields))
                {
                    fields = new List<FieldInfo>(RecursiveGetFields(RecordType));
                    mCachedRecordFields.Add(RecordType, fields);
                }
            }

            Fields = CreateCoreFields(fields, recordAttribute);
            FieldCount = Fields.Length;

            if (FieldCount == 0)
                throw new BadUsageException("The record class " + RecordType.Name + " don't contains any field.");

            if (recordAttribute is FixedLengthRecordAttribute)
            {
                // Defines the initial size of the StringBuilder
                mSizeHint = 0;
                for (int i = 0; i < FieldCount; i++)
                    mSizeHint += ((FixedLengthField) Fields[i]).mFieldLength;
            }
        }

        private IEnumerable<FieldInfo> RecursiveGetFields(Type currentType)
        {
            if (currentType.BaseType != null && !currentType.IsDefined(typeof (IgnoreInheritedClassAttribute), false))
                foreach (FieldInfo item in RecursiveGetFields(currentType.BaseType)) yield return item;

            if (currentType == typeof (object))
                yield break;

            mFieldInfoCacheManipulator.ResetFieldInfoCache(currentType);
            
            foreach (FieldInfo fi in currentType.GetFields(BindingFlags.Public |
                                                           BindingFlags.NonPublic |
                                                           BindingFlags.Instance |
                                                           BindingFlags.DeclaredOnly))
            {
                if (!(typeof (Delegate)).IsAssignableFrom(fi.FieldType))
                    yield return fi;
            }
        }
        #endregion

        #region "  CreateFields  "
        private static FieldBase[] CreateCoreFields(IList<FieldInfo> fields, TypedRecordAttribute recordAttribute)
        {
            var resFields = new List<FieldBase>();

            for (int i = 0; i < fields.Count; i++)
            {
                FieldBase currentField = FieldBase.CreateField(fields[i], recordAttribute);

                if (currentField == null) continue;

                // Add to the result
                resFields.Add(currentField);

                // Check some differences with the previous field
                if (resFields.Count <= 1) continue;

                FieldBase prevField = resFields[resFields.Count - 2];

                prevField.mNextIsOptional = currentField.mIsOptional;

                // Check for optional problems
                if (prevField.mIsOptional && currentField.mIsOptional == false)
                    throw new BadUsageException("The field: " + prevField.mFieldInfo.Name +
                                                " must be marked as optional bacause after a field with FieldOptional, the next fields must be marked with the same attribute. ( Try adding [FieldOptional] to " +
                                                currentField.mFieldInfo.Name + " )");

                // Check for array problems
                if (prevField.mIsArray)
                {
                    if (prevField.mArrayMinLength == Int32.MinValue)
                        throw new BadUsageException("The field: " + prevField.mFieldInfo.Name +
                                                    " is an array and must contain a [FieldArrayLength] attribute because not is the last field.");

                    if (prevField.mArrayMinLength != prevField.mArrayMaxLength)
                        throw new BadUsageException("The array field: " + prevField.mFieldInfo.Name +
                                                    " must contain a fixed length, i.e. the min and max length of the [FieldArrayLength] attribute must be the same because not is the last field.");
                }
            }

            if (resFields.Count > 0)
            {
                resFields[0].mIsFirst = true;
                resFields[resFields.Count - 1].mIsLast = true;
            }

            return resFields.ToArray();
        }
        #endregion

        #region "  StringToRecord  "
        public object StringToRecord(LineInfo line, object[] values)
        {
            if (MustIgnoreLine(line.mLineStr))
                return null;

            for (int i = 0; i < FieldCount; i++)
            {
                values[i] = Fields[i].ExtractFieldValue(line);
            }

            try
            {
                // Asign all values via dinamic method that creates an object and assign values
                return CreateHandler(values);
            }
            catch (InvalidCastException)
            {
                // Occurrs when the a custom converter returns an invalid value for the field.
                for (int i = 0; i < FieldCount; i++)
                {
                    if (values[i] != null && !Fields[i].mFieldTypeInternal.IsInstanceOfType(values[i]))
                        throw new ConvertException(null,
                                                   Fields[i].mFieldTypeInternal,
                                                   Fields[i].mFieldInfo.Name,
                                                   line.mReader.LineNumber,
                                                   -1,
                                                   "The converter for the field: " + Fields[i].mFieldInfo.Name +
                                                   " returns an object of Type: " + values[i].GetType().Name +
                                                   " and the field is of type: " + Fields[i].mFieldTypeInternal.Name,
                                                   null);
                }
                return null;
            }
        }

        private bool MustIgnoreLine(string line)
        {
            if (IgnoreEmptyLines)
                if ((IgnoreEmptySpaces && line.TrimStart().Length == 0) ||
                    line.Length == 0)
                    return true;

            if (!String.IsNullOrEmpty(CommentMarker))
                if ((CommentAnyPlace && line.TrimStart().StartsWith(CommentMarker)) ||
                    line.StartsWith(CommentMarker))
                    return true;

            if (RecordCondition != RecordCondition.None)
            {
                switch (RecordCondition)
                {
                    case RecordCondition.ExcludeIfBegins:
                        return ConditionHelper.BeginsWith(line, RecordConditionSelector);
                    case RecordCondition.IncludeIfBegins:
                        return ! ConditionHelper.BeginsWith(line, RecordConditionSelector);

                    case RecordCondition.ExcludeIfContains:
                        return ConditionHelper.Contains(line, RecordConditionSelector);
                    case RecordCondition.IncludeIfContains:
                        return ! ConditionHelper.Contains(line, RecordConditionSelector);

                    case RecordCondition.ExcludeIfEnclosed:
                        return ConditionHelper.Enclosed(line, RecordConditionSelector);
                    case RecordCondition.IncludeIfEnclosed:
                        return ! ConditionHelper.Enclosed(line, RecordConditionSelector);

                    case RecordCondition.ExcludeIfEnds:
                        return ConditionHelper.EndsWith(line, RecordConditionSelector);
                    case RecordCondition.IncludeIfEnds:
                        return ! ConditionHelper.EndsWith(line, RecordConditionSelector);

                    case RecordCondition.ExcludeIfMatchRegex:
                        return mConditionRegEx.IsMatch(line);
                    case RecordCondition.IncludeIfMatchRegex:
                        return ! mConditionRegEx.IsMatch(line);
                }
            }

            return false;
        }
        #endregion

        #region "  RecordToString  "
        private int mSizeHint = 32;

        public string RecordToString(object record)
        {
            var sb = new StringBuilder(mSizeHint);

            object[] mValues = ObjectToValuesHandler(record);

            for (int f = 0; f < FieldCount; f++)
            {
                Fields[f].AssignToString(sb, mValues[f]);
            }

            return sb.ToString();
        }

        public string RecordValuesToString(object[] recordValues)
        {
            var sb = new StringBuilder(mSizeHint);

            for (int f = 0; f < FieldCount; f++)
            {
                Fields[f].AssignToString(sb, recordValues[f]);
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
            for (int i = 0; i < FieldCount; i++)
            {
                if (Fields[i].mFieldTypeInternal == typeof (DateTime) && values[i] is double)
                    values[i] = DoubleToDate((int) (double) values[i]);

                values[i] = Fields[i].CreateValueForField(values[i]);
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

            return new DateTime((serialNumber + 693593)*(10000000L*24*3600));
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

            foreach (FieldBase f in Fields)
            {
                DataColumn column1 = res.Columns.Add(f.mFieldInfo.Name, f.mFieldInfo.FieldType);
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
                    mObjectToValuesHandler = ReflectionHelper.ObjectToValuesMethod(RecordType, GetFieldInfoArray());
                return mObjectToValuesHandler;
            }
        }


        private ValuesToObjectDelegate CreateHandler
        {
            get
            {
                if (mCreateHandler == null)
                    mCreateHandler = ReflectionHelper.ValuesToObjectMethod(RecordType, GetFieldInfoArray());
                return mCreateHandler;
            }
        }
        public CreateObjectDelegate CreateRecordHandler
        {
            get
            {
                if (mFastConstructor == null)
                    mFastConstructor = ReflectionHelper.CreateFastConstructor(RecordType);
                return mFastConstructor;
            }
        }


        #endregion

        #region " FieldIndexes  "
        private Dictionary<string, int> mMapFieldIndex;

        public int GetFieldIndex(string fieldName)
        {
            if (mMapFieldIndex == null)
            {
                mMapFieldIndex = new Dictionary<string, int>(FieldCount);
                for (int i = 0; i < FieldCount; i++)
                {
                    mMapFieldIndex.Add(Fields[i].mFieldInfo.Name, i);
                }
            }

            int res;
            if (!mMapFieldIndex.TryGetValue(fieldName, out res))
                throw new BadUsageException("The field: " + fieldName + " was not found in the class: " +
                                            RecordType.Name + ". Remember that this option is case sensitive.");

            return res;
        }
        #endregion

        #region "  GetFieldInfo  "
        public FieldInfo GetFieldInfo(string name)
        {
            foreach (FieldBase field in Fields)
            {
                if (field.mFieldInfo.Name.ToLower() == name.ToLower())
                    return field.mFieldInfo;
            }

            return null;
        }

        private FieldInfo[] GetFieldInfoArray()
        {
            var res = new FieldInfo[Fields.Length];

            for (int i = 0; i < Fields.Length; i++)
            {
                res[i] = Fields[i].mFieldInfo;
            }
            return res;
        }

        #endregion
    }
}