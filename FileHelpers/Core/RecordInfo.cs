using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace FileHelpers
{


    /// <summary>An internal class used to store information about the Record Type.</summary>
    /// <remarks>Is public to provide extensibility of DataSorage from outside the library.</remarks>
    internal sealed class RecordInfo : IRecordInfo
    {
        #region "  Internal Fields  "
        // Cache of all the fields that must be used for a Type
        // More info at:  http://www.filehelpers.com/forums/viewtopic.php?t=387
        // Thanks Brian for the report, research and fix
        private static readonly Dictionary<Type, List<FieldInfo>> mCachedRecordFields =
            new Dictionary<Type, List<FieldInfo>>();
        public Regex mConditionRegEx;
        public int mSizeHint = 32;
        private IRecordOperations mOperations;
        #endregion

        // --------------------------------------
        // Constructor and Init Methods

        #region IRecordInfo Members

        public int SizeHint
        {
            get { return mSizeHint; }
        }

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

        public Regex RecordConditionRegEx
        {
            get { return mConditionRegEx; }
        }

        public string RecordConditionSelector { get; set; }

        public bool IsDelimited
        {
            get { return Fields[0] is DelimitedField; }
        }
        #endregion

        #region "  Constructor "

        private RecordInfo(Type recordType)
        {
            RecordConditionSelector = String.Empty;
            RecordCondition = RecordCondition.None;
            CommentAnyPlace = true;
            RecordType = recordType;
            InitRecordFields();
            mOperations = new RecordOperations(this);
        }

        private void InitRecordFields()
        {
            //Debug.Assert(false, "TODO: Add RecordFilter to the engine.");
            var recordAttribute = Attributes.GetFirstInherited<TypedRecordAttribute>(RecordType);

            if (recordAttribute == null)
                throw new BadUsageException(Messages.Errors.ClassWithOutRecordAttribute
                                                .ClassName(RecordType.Name)
                                                .Text);


            if (ReflectionHelper.GetDefaultConstructor(RecordType) == null)
                throw new BadUsageException(Messages.Errors.ClassWithOutDefaultConstructor
                                                .ClassName(RecordType.Name)
                                                .Text);
            
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
                    fields = new List<FieldInfo>(ReflectionHelper.RecursiveGetFields(RecordType));
                    mCachedRecordFields.Add(RecordType, fields);
                }
            }

            Fields = CreateCoreFields(fields, recordAttribute);
            FieldCount = Fields.Length;

            if (FieldCount == 0)
                throw new BadUsageException(Messages.Errors.ClassWithOutFields
                                                .ClassName(RecordType.Name)
                                                .Text);

            if (recordAttribute is FixedLengthRecordAttribute)
            {
                // Defines the initial size of the StringBuilder
                mSizeHint = 0;
                for (int i = 0; i < FieldCount; i++)
                    mSizeHint += ((FixedLengthField) Fields[i]).mFieldLength;
            }
        }

        #endregion


        #region "  CreateFields  "
        private static FieldBase[] CreateCoreFields(IList<FieldInfo> fields, TypedRecordAttribute recordAttribute)
        {
            var resFields = new List<FieldBase>();

            var automaticFields = 0;
            var genericFields = 0;
            for (int i = 0; i < fields.Count; i++)
            {
                FieldBase currentField = FieldBase.CreateField(fields[i], recordAttribute);
                if (currentField == null) 
                    continue;

                if (currentField.mFieldInfo.IsDefined(typeof(CompilerGeneratedAttribute), false))
                    automaticFields++;
                else
                    genericFields++;

                // Add to the result
                resFields.Add(currentField);
                
                if (resFields.Count > 1)
                {
                    CheckForOrderProblems(currentField, resFields);
                }

            }

            if (automaticFields > 0 && genericFields > 0)
            {
                throw new BadUsageException(Messages.Errors.MixOfStandardAndAutoPropertiesFields
                    .ClassName(resFields[0].mFieldInfo.DeclaringType.Name)
                    .Text);
            }

            SortFieldsByOrder(resFields);

            if (resFields.Count > 0)
            {
                resFields[0].mIsFirst = true;
                resFields[resFields.Count - 1].mIsLast = true;
            }

            CheckForOptionalAndArrayProblems(resFields);

            return resFields.ToArray();
        }

        private static void CheckForOptionalAndArrayProblems(List<FieldBase> resFields)
        {
            for (int i = 0; i < resFields.Count; i++)
            {
                var currentField = resFields[i];

                // Dont check the first field
                if (i < 1)
                    continue;

                FieldBase prevField = resFields[i - 1];

                prevField.mNextIsOptional = currentField.mIsOptional;

                // Check for optional problems
                if (prevField.mIsOptional && currentField.mIsOptional == false)
                    throw new BadUsageException(Messages.Errors.ExpectingFieldOptional
                                                    .FieldName(prevField.mFieldInfo.Name)
                                                    .Text);

                // Check for array problems
                if (prevField.mIsArray)
                {
                    if (prevField.mArrayMinLength == Int32.MinValue)
                        throw new BadUsageException(Messages.Errors.MissingFieldArrayLenghtInNotLastField
                                                        .FieldName(prevField.mFieldInfo.Name)
                                                        .Text);

                    if (prevField.mArrayMinLength != prevField.mArrayMaxLength)
                        throw new BadUsageException(Messages.Errors.SameMinMaxLengthForArrayNotLastField
                                                        .FieldName(prevField.mFieldInfo.Name)
                                                        .Text);
                }

            }
        }

        private static void SortFieldsByOrder(List<FieldBase> resFields)
        {
            if (resFields.FindAll(x => x.mFieldOrder.HasValue).Count > 0)
                resFields.Sort( (x,y) => x.mFieldOrder.Value.CompareTo(y.mFieldOrder.Value));
        }

        private static void CheckForOrderProblems(FieldBase currentField, List<FieldBase> resFields)
        {
            if (currentField.mFieldOrder.HasValue)
            {
                var othersWithoutOrder = resFields.FindAll(x => x.mFieldOrder.HasValue == false);
                if (othersWithoutOrder.Count > 0)
                    throw new BadUsageException(Messages.Errors.PartialFieldOrder
                                                    .FieldName(othersWithoutOrder[0].mFieldInfo.Name)
                                                    .Text);

                // Same Number
                var otherWithSameOrder =
                    resFields.FindAll(x => x != currentField && x.mFieldOrder == currentField.mFieldOrder);

                if (otherWithSameOrder.Count > 0)
                    throw new BadUsageException(Messages.Errors.SameFieldOrder
                                                    .FieldName1(currentField.mFieldInfo.Name)
                                                    .FieldName2(otherWithSameOrder[0].mFieldInfo.Name)
                                                    .Text);


            }
            else
            {
                var othersWithOrder = resFields.FindAll(x => x.mFieldOrder.HasValue).Count;
                if (othersWithOrder > 0)
                    throw new BadUsageException(Messages.Errors.PartialFieldOrder
                                                    .FieldName(currentField.mFieldInfo.Name)
                                                    .Text);

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
                throw new BadUsageException(Messages.Errors.FieldNotFound
                                                .FieldName(fieldName)
                                                .ClassName(RecordType.Name)
                                                .Text);

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

        public IRecordOperations Operations
        {
            get
            {
                return mOperations;
            }
        }

        #endregion

        public static IRecordInfo Resolve(Type type)
        {
            // TODO: CACHE !!!
            return new RecordInfo(type);

            // return Container.Resolve<IRecordInfo>(mMasterType);
        }
    }




























        internal sealed class RecordOperations : IRecordOperations
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
                        if (values[i] != null && !RecordInfo.Fields[i].mFieldTypeInternal.IsInstanceOfType(values[i]))
                            throw new ConvertException(null,
                                                       RecordInfo.Fields[i].mFieldTypeInternal,
                                                       RecordInfo.Fields[i].mFieldInfo.Name,
                                                       line.mReader.LineNumber,
                                                       -1,
                                                       Messages.Errors.WrongConverter
                                                           .FieldName(RecordInfo.Fields[i].mFieldInfo.Name)
                                                           .ConverterReturnedType(values[i].GetType().Name)
                                                           .FieldType(RecordInfo.Fields[i].mFieldInfo.FieldType.Name)
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
                    if (RecordInfo.Fields[i].mFieldTypeInternal == typeof(DateTime) && values[i] is double)
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

            public CreateObjectDelegate CreateRecordHandler
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
                    res[i] = RecordInfo.Fields[i].mFieldInfo;
                }
                return res;
            }


        }

}