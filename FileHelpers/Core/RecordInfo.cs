using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace FileHelpers
{


    /// <summary>An internal class used to store information about the Record Type.</summary>
    /// <remarks>Is public to provide extensibility of DataSorage from outside the library.</remarks>
    internal sealed partial class RecordInfo 
        : IRecordInfo
    {

        // --------------------------------------
        // Constructor and Init Methods

        #region IRecordInfo Members

        public int SizeHint { get; private set; }
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
        public Regex RecordConditionRegEx { get; private set; }
        public string RecordConditionSelector { get; set; }

        public RecordOperations Operations { get; private set; }

        private Dictionary<string, int> mMapFieldIndex;

        public bool IsDelimited
        {
            get { return Fields[0] is DelimitedField; }
        }
        #endregion

        #region "  Constructor "

        private RecordInfo()
        {
        }

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
                                                      RecordConditionRegEx = new Regex(RecordConditionSelector,
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

            fields = new List<FieldInfo>(ReflectionHelper.RecursiveGetFields(RecordType));

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

                // Check for optional problems
                if (prevField.IsOptional && currentField.IsOptional == false)
                    throw new BadUsageException(Messages.Errors.ExpectingFieldOptional
                                                    .FieldName(prevField.FieldInfo.Name)
                                                    .Text);

                // Check for array problems
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

        private static void SortFieldsByOrder(List<FieldBase> resFields)
        {
            if (resFields.FindAll(x => x.FieldOrder.HasValue).Count > 0)
                resFields.Sort( (x,y) => x.FieldOrder.Value.CompareTo(y.FieldOrder.Value));
        }

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

        public static IRecordInfo Resolve(Type type)
        {
            return RecordInfoFactory.Resolve(type);
        }

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
    }
}