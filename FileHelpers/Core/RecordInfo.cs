using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using FileHelpers.Events;

namespace FileHelpers
{
    /// <summary>A class used to store information about the Record Type.</summary>
    public sealed partial class RecordInfo
        : IRecordInfo
    {
        // --------------------------------------
        // Constructor and Init Methods

        #region IRecordInfo Members

        /// <summary>
        /// Hint about record size
        /// </summary>
        public int SizeHint { get; private set; }

        /// <summary>
        /// Class we are defining
        /// </summary>
        public Type RecordType { get; private set; }

        /// <summary>
        /// Do we skip empty lines?
        /// </summary>
        public bool IgnoreEmptyLines { get; set; }

        /// <summary>
        /// Do we skip lines with completely blank lines
        /// </summary>
        public bool IgnoreEmptySpaces { get; private set; }

        /// <summary>
        /// Comment prefix
        /// </summary>
        public string CommentMarker { get; set; }

        /// <summary>
        /// Number of fields we are processing
        /// </summary>
        public int FieldCount => Fields.Length;

        /// <summary>
        /// List of fields and the extraction details
        /// </summary>
        public FieldBase[] Fields { get; private set; }

        /// <summary>
        /// Number of lines to skip at beginning of file
        /// </summary>
        public int IgnoreFirst { get; set; }

        /// <summary>
        /// Number of lines to skip at end of file
        /// </summary>
        public int IgnoreLast { get; set; }

        /// <summary>
        /// DO we need to issue a Notify read
        /// </summary>
        public bool NotifyRead { get; private set; }

        /// <summary>
        /// Do we need to issue a Notify Write
        /// </summary>
        public bool NotifyWrite { get; private set; }

        /// <summary>
        /// Can the comment prefix have leading whitespace
        /// </summary>
        public bool CommentAnyPlace { get; set; }

        /// <summary>
        /// Include or skip a record based upon a defined RecordCondition interface
        /// </summary>
        public RecordCondition RecordCondition { get; set; }

        /// <summary>
        /// Skip or include a record based upon a regular expression
        /// </summary>
        public Regex RecordConditionRegEx { get; private set; }

        /// <summary>
        /// Include or exclude a record based upon presence of a string
        /// </summary>
        public string RecordConditionSelector { get; set; }

        /// <summary>
        /// Operations are the functions that perform creation or extraction of data from objects
        /// these are created dynamically from the record conditions
        /// </summary>
        public RecordOperations Operations { get; private set; }

        private Dictionary<string, int> mMapFieldIndex;

        /// <summary>
        /// Is this record layout delimited
        /// </summary>
        public bool IsDelimited => Fields[0] is DelimitedField;

        #endregion

        #region "  Constructor "

        private RecordInfo() {}

        /// <summary>
        /// Read the attributes of the class and create an array
        /// of how to process the file
        /// </summary>
        /// <param name="recordType">Class we are analysing</param>
        private RecordInfo(Type recordType)
        {
            SizeHint = 32;
            RecordConditionSelector = String.Empty;
            RecordCondition = RecordCondition.None;
            CommentAnyPlace = true;
            RecordType = recordType;
            InitRecordFields();
            Operations = new RecordOperations(this);
        }

        /// <summary>
        /// Create a list of fields we are extracting and set
        /// the size hint for record I/O
        /// </summary>
        private void InitRecordFields()
        {
            var recordAttribute = Attributes.GetFirstInherited<TypedRecordAttribute>(RecordType);

            if (recordAttribute == null) {
                throw new BadUsageException(Messages.Errors.ClassWithOutRecordAttribute
                    .ClassName(RecordType.Name)
                    .Text);
            }

            if (ReflectionHelper.GetDefaultConstructor(RecordType) == null) {
                throw new BadUsageException(Messages.Errors.ClassWithOutDefaultConstructor
                    .ClassName(RecordType.Name)
                    .Text);
            }

            Attributes.WorkWithFirst<IgnoreFirstAttribute>(
                RecordType,
                a => IgnoreFirst = a.NumberOfLines);

            Attributes.WorkWithFirst<IgnoreLastAttribute>(
                RecordType,
                a => IgnoreLast = a.NumberOfLines);

            Attributes.WorkWithFirst<IgnoreEmptyLinesAttribute>(
                RecordType,
                a => {
                    IgnoreEmptyLines = true;
                    IgnoreEmptySpaces = a.IgnoreSpaces;
                });

#pragma warning disable CS0618 // Type or member is obsolete
            Attributes.WorkWithFirst<IgnoreCommentedLinesAttribute>(
#pragma warning restore CS0618 // Type or member is obsolete
                RecordType,
                a => {
                    IgnoreEmptyLines = true;
                    CommentMarker = a.CommentMarker;
                    CommentAnyPlace = a.AnyPlace;
                });

            Attributes.WorkWithFirst<ConditionalRecordAttribute>(
                RecordType,
                a => {
                    RecordCondition = a.Condition;
                    RecordConditionSelector = a.ConditionSelector;

                    if (RecordCondition == RecordCondition.ExcludeIfMatchRegex ||
                        RecordCondition == RecordCondition.IncludeIfMatchRegex) {
                        RecordConditionRegEx = new Regex(RecordConditionSelector,
                            RegexOptions.Compiled | RegexOptions.IgnoreCase |
                            RegexOptions.ExplicitCapture);
                    }
                });

            if (CheckInterface(RecordType, typeof (INotifyRead)))
                NotifyRead = true;

            if (CheckInterface(RecordType, typeof (INotifyWrite)))
                NotifyWrite = true;

            // Create fields
            // Search for cached fields
            var fields = new List<FieldInfo>(ReflectionHelper.RecursiveGetFields(RecordType));

            Fields = CreateCoreFields(fields, recordAttribute);

            if (FieldCount == 0) {
                throw new BadUsageException(Messages.Errors.ClassWithOutFields
                    .ClassName(RecordType.Name)
                    .Text);
            }

            if (recordAttribute is FixedLengthRecordAttribute) {
                // Defines the initial size of the StringBuilder
                SizeHint = 0;
                for (int i = 0; i < FieldCount; i++)
                    SizeHint += ((FixedLengthField) Fields[i]).FieldLength;
            }
        }

        #endregion

        #region "  CreateFields  "

        /// <summary>
        /// Parse the attributes on the class and create an ordered list of
        /// fields we are extracting from the record
        /// </summary>
        /// <param name="fields">Complete list of fields in class</param>
        /// <param name="recordAttribute">Type of record, fixed or delimited</param>
        /// <returns>List of fields we are extracting</returns>
        private static FieldBase[] CreateCoreFields(IList<FieldInfo> fields, TypedRecordAttribute recordAttribute)
        {
            var resFields = new List<FieldBase>();

            // count of Properties
            var automaticFields = 0;

            // count of normal fields
            var genericFields = 0;
            for (int i = 0; i < fields.Count; i++) {
                FieldBase currentField = FieldBase.CreateField(fields[i], recordAttribute);
                if (currentField == null)
                    continue;

                if (currentField.FieldInfo.IsDefined(typeof (CompilerGeneratedAttribute), false))
                    automaticFields++;
                else
                    genericFields++;

                // Add to the result
                resFields.Add(currentField);

                if (resFields.Count > 1)
                    CheckForOrderProblems(currentField, resFields);
            }

            if (automaticFields > 0 &&
                genericFields > 0 && SumOrder(resFields) == 0) {
                throw new BadUsageException(Messages.Errors.MixOfStandardAndAutoPropertiesFields
                    .ClassName(resFields[0].FieldInfo.DeclaringType.Name)
                    .Text);
            }

            SortFieldsByOrder(resFields);

            CheckForOptionalAndArrayProblems(resFields);

            return resFields.ToArray();
        }

        private static int SumOrder(List<FieldBase> fields)
        {
            int res = 0;
            foreach (var field in fields)
            {
                res += field.FieldOrder ?? 0;
            }
            return res;
        }

        /// <summary>
        /// Check that once one field is optional all following fields are optional
        /// <para/>
        /// Check that arrays in the middle of a record are of fixed length
        /// </summary>
        /// <param name="resFields">List of fields to extract</param>
        private static void CheckForOptionalAndArrayProblems(List<FieldBase> resFields)
        {
            for (int i = 0; i < resFields.Count; i++) {
                var currentField = resFields[i];

                // Dont check the first field
                if (i < 1)
                    continue;

                FieldBase prevField = resFields[i - 1];

                // Check for optional problems.  Previous is optional but current is not
                if (prevField.IsOptional
                    &&
                    currentField.IsOptional == false
                    &&
                    currentField.InNewLine == false) {
                    throw new BadUsageException(Messages.Errors.ExpectingFieldOptional
                        .FieldName(prevField.FieldInfo.Name)
                        .Text);
                }

                // Check for an array array in the middle of a record that is not a fixed length
                if (prevField.IsArray) {
                    if (prevField.ArrayMinLength == Int32.MinValue) {
                        throw new BadUsageException(Messages.Errors.MissingFieldArrayLenghtInNotLastField
                            .FieldName(prevField.FieldInfo.Name)
                            .Text);
                    }

                    if (prevField.ArrayMinLength != prevField.ArrayMaxLength) {
                        throw new BadUsageException(Messages.Errors.SameMinMaxLengthForArrayNotLastField
                            .FieldName(prevField.FieldInfo.Name)
                            .Text);
                    }
                }
            }
        }

        /// <summary>
        /// Sort fields by the order if supplied
        /// </summary>
        /// <param name="resFields">List of fields to use</param>
        private static void SortFieldsByOrder(List<FieldBase> resFields)
        {
            if (resFields.FindAll(x => x.FieldOrder.HasValue).Count > 0)
                resFields.Sort((x, y) => x.FieldOrder.Value.CompareTo(y.FieldOrder.Value));
        }

        /// <summary>
        /// Confirm all fields are either ordered or unordered
        /// </summary>
        /// <param name="currentField">Newest field</param>
        /// <param name="resFields">Other fields we have found</param>
        private static void CheckForOrderProblems(FieldBase currentField, List<FieldBase> resFields)
        {
            if (currentField.FieldOrder.HasValue) {
                // If one field has order number set, all others must also have an order number
                var fieldWithoutOrder = resFields.Find(x => x.FieldOrder.HasValue == false);
                if (fieldWithoutOrder != null) {
                    throw new BadUsageException(Messages.Errors.PartialFieldOrder
                        .FieldName(fieldWithoutOrder.FieldInfo.Name)
                        .Text);
                }

                // No other field should have the same order number
                var fieldWithSameOrder =
                    resFields.Find(x => x != currentField && x.FieldOrder == currentField.FieldOrder);

                if (fieldWithSameOrder != null)
                {
                    throw new BadUsageException(Messages.Errors.SameFieldOrder
                        .FieldName1(currentField.FieldInfo.Name)
                        .FieldName2(fieldWithSameOrder.FieldInfo.Name)
                        .Text);
                }
            }
            else {
                // No other field should have order number set
                var fieldWithOrder = resFields.Find(x => x.FieldOrder.HasValue);
                if (fieldWithOrder != null)
                {
                    var autoPropertyName = FieldBase.AutoPropertyName(currentField.FieldInfo);

                    if (string.IsNullOrEmpty(autoPropertyName))
                        throw new BadUsageException(Messages.Errors.PartialFieldOrder
                            .FieldName(currentField.FieldInfo.Name)
                            .Text);
                    else
                        throw new BadUsageException(Messages.Errors.PartialFieldOrderInAutoProperty
                            .PropertyName(autoPropertyName)
                            .Text);
                }
            }
        }

        #endregion

        #region " FieldIndexes  "

        /// <summary>
        /// Get the index number of the fieldname in the field list
        /// </summary>
        /// <param name="fieldName">Fieldname to search for</param>
        /// <returns>Index in field list</returns>
        public int GetFieldIndex(string fieldName)
        {
            if (mMapFieldIndex == null) {
                // Initialize field index map
                mMapFieldIndex = new Dictionary<string, int>(FieldCount, StringComparer.Ordinal);
                for (int i = 0; i < FieldCount; i++)
                {
                    mMapFieldIndex.Add(Fields[i].FieldInfo.Name, i);
                    if (Fields[i].FieldInfo.Name != Fields[i].FieldFriendlyName)
                        mMapFieldIndex.Add(Fields[i].FieldFriendlyName, i);
                }
            }

            int res;
            if (!mMapFieldIndex.TryGetValue(fieldName, out res)) {
                throw new BadUsageException(Messages.Errors.FieldNotFound
                    .FieldName(fieldName)
                    .ClassName(RecordType.Name)
                    .Text);
            }

            return res;
        }

        #endregion

        #region "  GetFieldInfo  "

        /// <summary>
        /// Get field information base on name
        /// </summary>
        /// <param name="name">name to find details for</param>
        /// <returns>Field information</returns>
        public FieldInfo GetFieldInfo(string name)
        {
            foreach (var field in Fields) {
                if (field.FieldInfo.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return field.FieldInfo;
            }

            return null;
        }

        #endregion

        /// <summary>
        /// Return the record type information for the record
        /// </summary>
        /// <param name="type">Type of object to create</param>
        /// <returns>Record info for that type</returns>
        public static IRecordInfo Resolve(Type type)
        {
            return RecordInfoFactory.Resolve(type);
        }

        /// <summary>
        /// Create an new instance of the record information
        /// </summary>
        /// <returns>Deep copy of the RecordInfo class</returns>
        private IRecordInfo Clone()
        {
            var res = new RecordInfo
            {
                CommentAnyPlace = CommentAnyPlace,
                CommentMarker = CommentMarker,
                IgnoreEmptyLines = IgnoreEmptyLines,
                IgnoreEmptySpaces = IgnoreEmptySpaces,
                IgnoreFirst = IgnoreFirst,
                IgnoreLast = IgnoreLast,
                NotifyRead = NotifyRead,
                NotifyWrite = NotifyWrite,
                RecordCondition = RecordCondition,
                RecordConditionRegEx = RecordConditionRegEx,
                RecordConditionSelector = RecordConditionSelector,
                RecordType = RecordType,
                SizeHint = SizeHint
            };

            res.Operations = Operations.Clone(res);
            
            res.Fields = new FieldBase[Fields.Length];
            for (int i = 0; i < Fields.Length; i++)
                res.Fields[i] = Fields[i].Clone();

            return res;
        }

        /// <summary>
        /// Check whether the type implements the INotifyRead or INotifyWrite interfaces
        /// </summary>
        /// <param name="type">Type to check interface</param>
        /// <param name="interfaceType">Interface generic type we are checking for eg INotifyRead&lt;&gt;</param>
        /// <returns>Whether we found interface</returns>
        private static bool CheckInterface(Type type, Type interfaceType)
        {
            return type.GetInterface(interfaceType.FullName) != null;
        }
    }
}