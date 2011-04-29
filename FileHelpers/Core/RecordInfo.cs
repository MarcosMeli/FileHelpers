using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using FileHelpers.Events;

namespace FileHelpers
{

    /// <summary>An internal class used to store information about the Record Type.</summary>
    /// <remarks>Is public to provide extensibility of DataStorage from outside the library.</remarks>
    internal sealed partial class RecordInfo 
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
        public int FieldCount { get; private set; }

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
        public bool IsDelimited
        {
            get { return Fields[0] is DelimitedField; }
        }
        #endregion

        #region "  Constructor "

        private RecordInfo()
        {
        }

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

            Attributes.WorkWithFirst<IgnoreFirstAttribute>(
                RecordType,
                a => IgnoreFirst = a.NumberOfLines);

            Attributes.WorkWithFirst<IgnoreLastAttribute>(
                RecordType,
                a => IgnoreLast = a.NumberOfLines);

            Attributes.WorkWithFirst<IgnoreEmptyLinesAttribute>(
                RecordType,
                a =>
                    {
                        IgnoreEmptyLines = true;
                        IgnoreEmptySpaces = a.mIgnoreSpaces;
                    });


            Attributes.WorkWithFirst<IgnoreCommentedLinesAttribute>(
                RecordType,
                a =>
                    {
                        IgnoreEmptyLines = true;
                        CommentMarker = a.mCommentMarker;
                        CommentAnyPlace = a.mAnyPlace;
                    });


            Attributes.WorkWithFirst<ConditionalRecordAttribute>(
                RecordType,
                a =>
                    {
                        RecordCondition = a.Condition;
                        RecordConditionSelector = a.ConditionSelector;

                        if (RecordCondition == RecordCondition.ExcludeIfMatchRegex ||
                            RecordCondition == RecordCondition.IncludeIfMatchRegex)
                        {
                            RecordConditionRegEx = new Regex(RecordConditionSelector,
                                                             RegexOptions.Compiled | RegexOptions.IgnoreCase |
                                                             RegexOptions.ExplicitCapture);
                        }
                    });

            if (CheckGenericInterface(RecordType, typeof(INotifyRead<>), RecordType))
                NotifyRead = true;

            if (CheckGenericInterface(RecordType, typeof(INotifyWrite<>), RecordType))
                NotifyWrite = true;

            // Create fields
            // Search for cached fields

            var fields = new List<FieldInfo>(ReflectionHelper.RecursiveGetFields(RecordType));

            Fields = CreateCoreFields(fields, recordAttribute);
            FieldCount = Fields.Length;

            if (FieldCount == 0)
                throw new BadUsageException(Messages.Errors.ClassWithOutFields
                                                .ClassName(RecordType.Name)
                                                .Text);

            if (recordAttribute is FixedLengthRecordAttribute)
            {
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
            for (int i = 0; i < fields.Count; i++)
            {
                FieldBase currentField = FieldBase.CreateField(fields[i], recordAttribute);
                if (currentField == null) 
                    continue;

                if (currentField.FieldInfo.IsDefined(typeof(CompilerGeneratedAttribute), false))
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
                    .ClassName(resFields[0].FieldInfo.DeclaringType.Name)
                    .Text);
            }

            SortFieldsByOrder(resFields);

            if (resFields.Count > 0)
            {
                resFields[0].IsFirst = true;
                resFields[resFields.Count - 1].IsLast = true;
            }

            CheckForOptionalAndArrayProblems(resFields);

            return resFields.ToArray();
        }

        /// <summary>
        /// Check that once one field is optional all following fields are optional
        /// <para/>
        /// Check that arrays in the middle of a record are of fixed length
        /// </summary>
        /// <param name="resFields">List of fields to extract</param>
        private static void CheckForOptionalAndArrayProblems(List<FieldBase> resFields)
        {
            for (int i = 0; i < resFields.Count; i++)
            {
                var currentField = resFields[i];

                // Dont check the first field
                if (i < 1)
                    continue;

                FieldBase prevField = resFields[i - 1];

                prevField.NextIsOptional = currentField.IsOptional;

                // Check for optional problems.  Previous is optional but current is not
                if (prevField.IsOptional && currentField.IsOptional == false)
                    throw new BadUsageException(Messages.Errors.ExpectingFieldOptional
                                                    .FieldName(prevField.FieldInfo.Name)
                                                    .Text);

                // Check for an array array in the middle of a record that is not a fixed length
                if (prevField.IsArray)
                {
                    if (prevField.ArrayMinLength == Int32.MinValue)
                        throw new BadUsageException(Messages.Errors.MissingFieldArrayLenghtInNotLastField
                                                        .FieldName(prevField.FieldInfo.Name)
                                                        .Text);

                    if (prevField.ArrayMinLength != prevField.ArrayMaxLength)
                        throw new BadUsageException(Messages.Errors.SameMinMaxLengthForArrayNotLastField
                                                        .FieldName(prevField.FieldInfo.Name)
                                                        .Text);
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
                resFields.Sort( (x,y) => x.FieldOrder.Value.CompareTo(y.FieldOrder.Value));
        }

        /// <summary>
        /// Confirm all fields are either ordered or unordered
        /// </summary>
        /// <param name="currentField">Newest field</param>
        /// <param name="resFields">Other fields we have found</param>
        private static void CheckForOrderProblems(FieldBase currentField, List<FieldBase> resFields)
        {
            if (currentField.FieldOrder.HasValue)
            {
                var othersWithoutOrder = resFields.FindAll(x => x.FieldOrder.HasValue == false);
                if (othersWithoutOrder.Count > 0)
                    throw new BadUsageException(Messages.Errors.PartialFieldOrder
                                                    .FieldName(othersWithoutOrder[0].FieldInfo.Name)
                                                    .Text);

                // Same Number
                var otherWithSameOrder =
                    resFields.FindAll(x => x != currentField && x.FieldOrder == currentField.FieldOrder);

                if (otherWithSameOrder.Count > 0)
                    throw new BadUsageException(Messages.Errors.SameFieldOrder
                                                    .FieldName1(currentField.FieldInfo.Name)
                                                    .FieldName2(otherWithSameOrder[0].FieldInfo.Name)
                                                    .Text);


            }
            else
            {
                var othersWithOrder = resFields.FindAll(x => x.FieldOrder.HasValue).Count;
                if (othersWithOrder > 0)
                    throw new BadUsageException(Messages.Errors.PartialFieldOrder
                                                    .FieldName(currentField.FieldInfo.Name)
                                                    .Text);

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
            if (mMapFieldIndex == null)
            {
                mMapFieldIndex = new Dictionary<string, int>(FieldCount);
                for (int i = 0; i < FieldCount; i++)
                {
                    mMapFieldIndex.Add(Fields[i].FieldInfo.Name, i);
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

        /// <summary>
        /// Get field information base on name
        /// </summary>
        /// <param name="name">name to find details for</param>
        /// <returns>Field information</returns>
        public FieldInfo GetFieldInfo(string name)
        {
            foreach (var field in Fields)
            {
                if (field.FieldInfo.Name.ToLower() == name.ToLower())
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
        public object Clone()
        {
            var res = new RecordInfo();

            res.CommentAnyPlace = CommentAnyPlace;
            res.CommentMarker = CommentMarker;
            res.FieldCount = FieldCount;
            res.IgnoreEmptyLines = IgnoreEmptyLines;
            res.IgnoreEmptySpaces = IgnoreEmptySpaces;
            res.IgnoreFirst = IgnoreFirst;
            res.IgnoreLast = IgnoreLast;
            res.NotifyRead = NotifyRead;
            res.NotifyWrite = NotifyWrite;
            res.Operations = Operations.Clone(res);

            res.RecordCondition = RecordCondition;
            res.RecordConditionRegEx = RecordConditionRegEx;
            res.RecordConditionSelector = RecordConditionSelector;
            res.RecordType = RecordType;
            res.SizeHint = SizeHint;

            res.Fields = new FieldBase[Fields.Length];
            for (int i = 0; i < Fields.Length; i++)
            {
                res.Fields[i] = (FieldBase) Fields[i].Clone();
            }

            return res;
        }

        /// <summary>
        /// Check whether the type implements the INotifyRead or INotifyWrite interfaces
        /// </summary>
        /// <param name="type">Type to check interface</param>
        /// <param name="interfaceType">Interface generic type we are checking for eg INotifyRead&lt;&gt;</param>
        /// <param name="genericsArgs">Arguments to pass</param>
        /// <returns>Whether we found interface</returns>
        public static bool CheckGenericInterface(Type type, Type interfaceType, params Type[] genericsArgs)
        {
            foreach (var inteImp in type.GetInterfaces())
            {
                if (inteImp.IsGenericType &&
                    inteImp.GetGenericTypeDefinition() == interfaceType)
                {
                    var args = inteImp.GetGenericArguments();
                    
                    if (args.Length == genericsArgs.Length)
                    {
                        bool fail = false;
                        for (int i = 0; i < args.Length; i++)
                        {
                            if (args[i] != genericsArgs[i])
                            {
                                fail = true;
                                break;
                            }
                        }
                        if (!fail)
                            return true;

                    }
                    throw new BadUsageException("The class: " + type.Name + " must implement the interface " +
                                                interfaceType.MakeGenericType(genericsArgs) + " and not " + inteImp);
                }
            }
            return false;
        }
    }
}