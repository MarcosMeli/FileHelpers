#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

#if ! MINI
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection.Emit;
#endif

namespace FileHelpers
{
    
    /// <summary>An internal class used to store information about the Record Type.</summary>
    /// <remarks>Is public to provide extensibility of DataSorage from outside the library.</remarks>
    internal sealed class RecordInfo
    {

        // --------------------------------------
        // Constructor and Init Methods

        #region "  Internal Fields  "

        // Cache of all the fields that must be used for a Type
        // More info at:  http://www.filehelpers.com/forums/viewtopic.php?t=387
        // Thanks Brian for the report, research and fix
        private static readonly Dictionary<Type, List<FieldInfo>> mCachedRecordFields = new Dictionary<Type, List<FieldInfo>>();

        internal Type mRecordType;
        internal FieldBase[] mFields;
        internal int mIgnoreFirst = 0;
        internal int mIgnoreLast = 0;
        internal bool mIgnoreEmptyLines = false;
        internal bool mIgnoreEmptySpaces = false;

        internal string mCommentMarker = null;
        internal bool mCommentAnyPlace = true;

        internal RecordCondition mRecordCondition = RecordCondition.None;
        internal string mRecordConditionSelector = string.Empty;

#if ! MINI
        internal bool mNotifyRead;
        internal bool mNotifyWrite;
        private Regex mConditionRegEx = null;
#endif
        internal int mFieldCount;

        private ConstructorInfo mRecordConstructor;

        private static readonly object[] mEmptyObjectArr = new object[] { };
        private static readonly Type[] mEmptyTypeArr = new Type[] { };

        #endregion

        #region "  Constructor  "

        /// <summary>The unique constructor for this class. It needs the subyacent record class.</summary>
        /// <param name="recordType">The Type of the record class.</param>
        internal RecordInfo(Type recordType)
        {
            mRecordType = recordType;
            InitRecordFields();

        }

        internal bool IsDelimited
        {
            get { return mFields[0] is DelimitedField; }
        }

        #endregion
        
        #region "  InitRecordFields  "

        private void InitRecordFields()
        {
            //-> Checked by the AttributeTargets
            //new BadUsageException("Structures are not supported in the FileHelperEngine only classes are allowed.");

            TypedRecordAttribute recordAttribute;

            if (mRecordType.IsDefined(typeof(TypedRecordAttribute), true) == false)
                throw new BadUsageException("The class " + mRecordType.Name + " must be marked with the [DelimitedRecord] or [FixedLengthRecord] Attribute.");
            else
            {
                object[] attbs = mRecordType.GetCustomAttributes(typeof(TypedRecordAttribute), true);
                recordAttribute = (TypedRecordAttribute)attbs[0];
            }

            if (mRecordType.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic, null, mEmptyTypeArr, new ParameterModifier[] { }) == null)
                throw new BadUsageException("The record class " + mRecordType.Name + " need a constructor with no args (public or private)");

            if (mRecordType.IsDefined(typeof(IgnoreFirstAttribute), false))
                mIgnoreFirst = ((IgnoreFirstAttribute)mRecordType.GetCustomAttributes(typeof(IgnoreFirstAttribute), false)[0]).NumberOfLines;

            if (mRecordType.IsDefined(typeof(IgnoreLastAttribute), false))
                mIgnoreLast = ((IgnoreLastAttribute)mRecordType.GetCustomAttributes(typeof(IgnoreLastAttribute), false)[0]).NumberOfLines;

            if (mRecordType.IsDefined(typeof(IgnoreEmptyLinesAttribute), false))
            {
                mIgnoreEmptyLines = true;
                mIgnoreEmptySpaces = ((IgnoreEmptyLinesAttribute)mRecordType.GetCustomAttributes(typeof(IgnoreEmptyLinesAttribute), false)[0]).
                        mIgnoreSpaces;
            }

            if (mRecordType.IsDefined(typeof(IgnoreCommentedLinesAttribute), false))
            {
                IgnoreCommentedLinesAttribute ignoreComments =
                    (IgnoreCommentedLinesAttribute)mRecordType.GetCustomAttributes(typeof(IgnoreCommentedLinesAttribute), false)[0];
                mCommentMarker = ignoreComments.mCommentMarker;
                mCommentAnyPlace = ignoreComments.mAnyPlace;
            }

            if (mRecordType.IsDefined(typeof(ConditionalRecordAttribute), false))
            {
                ConditionalRecordAttribute conditional =
                    (ConditionalRecordAttribute)mRecordType.GetCustomAttributes(typeof(ConditionalRecordAttribute), false)[0];

                mRecordCondition = conditional.mCondition;
                mRecordConditionSelector = conditional.mConditionSelector;

#if ! MINI

                if (mRecordCondition == RecordCondition.ExcludeIfMatchRegex ||
                    mRecordCondition == RecordCondition.IncludeIfMatchRegex)
                {
                    mConditionRegEx = new Regex(mRecordConditionSelector, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
                }
#endif
            }

			
#if ! MINI
            if (typeof(INotifyRead).IsAssignableFrom(mRecordType))
                mNotifyRead = true;

            if (typeof(INotifyWrite).IsAssignableFrom(mRecordType))
                mNotifyWrite = true;
#endif

            // Create fields
            // Search for cached fields
            List<FieldInfo> fields; ;

            if (! mCachedRecordFields.TryGetValue(mRecordType, out fields))
            {
                fields = new List<FieldInfo>();
                RecursiveGetFields(fields, mRecordType, recordAttribute);
                mCachedRecordFields.Add(mRecordType, fields);
            }

            mRecordConstructor = mRecordType.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic, null, mEmptyTypeArr, new ParameterModifier[] { });

            mFields = CreateCoreFields(fields, recordAttribute);
            mFieldCount = mFields.Length;

            if (recordAttribute is FixedLengthRecordAttribute)
            {
                // Defines the initial size of the StringBuilder
                mSizeHint = 0; 
                for (int i = 0; i < mFieldCount; i++)
                    mSizeHint += ((FixedLengthField)mFields[i]).mFieldLength;
            }

            if (mFieldCount == 0)
                throw new BadUsageException("The record class " + mRecordType.Name + " don't contains any field.");


        }

        private void RecursiveGetFields(List<FieldInfo> fields, Type currentType, TypedRecordAttribute recordAttribute)
        {
            if (currentType.BaseType != null && !currentType.IsDefined(typeof(IgnoreInheritedClassAttribute), false))
                RecursiveGetFields(fields, currentType.BaseType, recordAttribute);

            if (currentType == typeof(object))
                return;

#if ! MINI
            lock (mTypeCacheLock)
            {
                ClearFieldInfoCache();
#endif

                foreach (FieldInfo fi in currentType.GetFields(BindingFlags.Public |
                                                                    BindingFlags.NonPublic | 
                                                                    BindingFlags.Instance |
                                                                    BindingFlags.DeclaredOnly))
                {
                    if ((typeof (Delegate)).IsAssignableFrom(fi.FieldType))
                        continue;

                    fields.Add(fi);
                }
#if ! MINI
            }
#endif
            //currentType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            //currentType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }

#if ! MINI

        private static PropertyInfo mTypeCacheInfo;
        private static object mTypeCacheLock = new object();

        private static FieldInfo mFieldCachePointer;
        private void ClearFieldInfoCache()
        {
                if (mTypeCacheInfo == null)
                    mTypeCacheInfo = mRecordType.GetType().GetProperty("Cache", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.NonPublic);

                object cache = mTypeCacheInfo.GetValue(mRecordType, null);

                if (mFieldCachePointer == null)
                    mFieldCachePointer = cache.GetType().GetField("m_fieldInfoCache", BindingFlags.FlattenHierarchy | BindingFlags.Instance | BindingFlags.NonPublic);

                mFieldCachePointer.SetValue(cache, null);
            
        }

#endif 
        #endregion

        #region "  CreateFields  "
        
        private static FieldBase[] CreateCoreFields(List<FieldInfo> fields, TypedRecordAttribute recordAttribute)
        {
            List<FieldBase> resFields = new List<FieldBase>();

            for (int i = 0; i < fields.Count; i++)
            {
                FieldBase currentField = FieldBase.CreateField((FieldInfo)fields[i], recordAttribute);

                if (currentField != null)
                {
                    // Add to the result
                    resFields.Add(currentField);

                    // Check some differences with the previous field
                    if (resFields.Count > 1)
                    {
                        FieldBase prevField = (FieldBase)resFields[resFields.Count - 2];

                        prevField.mNextIsOptional = currentField.mIsOptional;

                        // Check for optional problems
                        if (prevField.mIsOptional && currentField.mIsOptional == false)
                            throw new BadUsageException("The field: " + prevField.mFieldInfo.Name + " must be marked as optional bacause after a field with FieldOptional, the next fields must be marked with the same attribute. ( Try adding [FieldOptional] to " + currentField.mFieldInfo.Name + " )");

                        // Check for array problems
                        if (prevField.mIsArray)
                        {
                            if (prevField.mArrayMinLength == int.MinValue)
                                throw new BadUsageException("The field: " + prevField.mFieldInfo.Name + " is an array and must contain a [FieldArrayLength] attribute because not is the last field.");

                            if (prevField.mArrayMinLength != prevField.mArrayMaxLength)
                                throw new BadUsageException("The array field: " + prevField.mFieldInfo.Name + " must contain a fixed length, i.e. the min and max length of the [FieldArrayLength] attribute must be the same because not is the last field.");
                        }
                    }

                }
            }

            if (resFields.Count > 0)
            {
                ((FieldBase)resFields[0]).mIsFirst = true;
                ((FieldBase)resFields[resFields.Count - 1]).mIsLast = true;

            }

            return resFields.ToArray();

        }
        #endregion

        // ----------------------------------------
        // String <--> Record <--> Values methods

        #region "  StringToRecord  "

        internal object StringToRecord(LineInfo line, object[] values)
        {
            if (MustIgnoreLine(line.mLineStr))
                return null;

            for (int i = 0; i < mFieldCount; i++)
            {
                values[i] = mFields[i].ExtractFieldValue(line);
            }

#if NET_1_1 || MINI
			try
			{
				object record = CreateRecordObject();
				for (int i = 0; i < mFieldCount; i++)
				{
					mFields[i].mFieldInfo.SetValue(record, values[i]);
				}
				return record;
			}
			catch (ArgumentException)
			{
				// Occurrs when the a custom converter returns an invalid value for the field.
				for (int i = 0; i < mFieldCount; i++)
				{
					if (values[i] != null && ! mFields[i].mFieldTypeInternal.IsInstanceOfType(values[i]))
						throw new ConvertException(null, mFields[i].mFieldTypeInternal, mFields[i].mFieldInfo.Name, line.mReader.LineNumber, -1, "The converter for the field: " + mFields[i].mFieldInfo.Name + " returns an object of Type: " + values[i].GetType().Name + " and the field is of type: " + mFields[i].mFieldTypeInternal.Name, null);
				}
				return null;
			}
#else
            CreateAssingMethods();

            try
            {
                // Asign all values via dinamic method that creates an object and assign values
                return mCreateHandler(values);
            }
            catch (InvalidCastException)
            {
                // Occurrs when the a custom converter returns an invalid value for the field.
                for (int i = 0; i < mFieldCount; i++)
                {
                    if (values[i] != null && !mFields[i].mFieldTypeInternal.IsInstanceOfType(values[i]))
                        throw new ConvertException(null, mFields[i].mFieldTypeInternal, mFields[i].mFieldInfo.Name, line.mReader.LineNumber, -1, "The converter for the field: " + mFields[i].mFieldInfo.Name + " returns an object of Type: " + values[i].GetType().Name + " and the field is of type: " + mFields[i].mFieldTypeInternal.Name, null);
                }
                return null;
            }
#endif
        }



        //		private static ErrorManager CreateAndAssign(object[] values)
        //		{
        //			ErrorManager record = new ErrorManager();
        //			record.mErrorMode = (ErrorMode) values[0];
        //			record.temp = (string) values[1];
        //			
        //			return record;
        //		}


        private bool MustIgnoreLine(string line)
        {
            if (mIgnoreEmptyLines)
                if ((mIgnoreEmptySpaces && line.TrimStart().Length == 0) ||
                    line.Length == 0)
                    return true;


            if (mCommentMarker != null && mCommentMarker.Length > 0)
                if ((mCommentAnyPlace && line.TrimStart().StartsWith(mCommentMarker)) ||
                    line.StartsWith(mCommentMarker))
                    return true;



            if (mRecordCondition != RecordCondition.None)
            {
                switch (mRecordCondition)
                {
                    case RecordCondition.ExcludeIfBegins:
                        return ConditionHelper.BeginsWith(line, mRecordConditionSelector);
                    case RecordCondition.IncludeIfBegins:
                        return ! ConditionHelper.BeginsWith(line, mRecordConditionSelector);

                    case RecordCondition.ExcludeIfContains:
                        return ConditionHelper.Contains(line, mRecordConditionSelector);
                    case RecordCondition.IncludeIfContains:
                        return ! ConditionHelper.Contains(line, mRecordConditionSelector);


                    case RecordCondition.ExcludeIfEnclosed:
                        return ConditionHelper.Enclosed(line, mRecordConditionSelector);
                    case RecordCondition.IncludeIfEnclosed:
                        return ! ConditionHelper.Enclosed(line, mRecordConditionSelector);

                    case RecordCondition.ExcludeIfEnds:
                        return ConditionHelper.EndsWith(line, mRecordConditionSelector);
                    case RecordCondition.IncludeIfEnds:
                        return ! ConditionHelper.EndsWith(line, mRecordConditionSelector);

#if ! MINI
                    case RecordCondition.ExcludeIfMatchRegex:
                        return mConditionRegEx.IsMatch(line);
                    case RecordCondition.IncludeIfMatchRegex:
                        return ! mConditionRegEx.IsMatch(line);
#endif

                }
            }

            return false;
        }

        #endregion

        #region "  RecordToString  "

        internal int mSizeHint = 32;

        internal string RecordToString(object record)
        {
            StringBuilder sb = new StringBuilder(mSizeHint);
            //string res = String.Empty;


#if NET_1_1 || MINI
			object[] mValues = new object[mFieldCount];
			for (int i = 0; i < mFieldCount; i++)
			{
				mValues[i] = mFields[i].mFieldInfo.GetValue(record);
			}

#else
            CreateGetAllMethod();

            object[] mValues = mGetAllValuesHandler(record);
#endif


            for (int f = 0; f < mFieldCount; f++)
            {
                mFields[f].AssignToString(sb, mValues[f]);
            }

            //_BigSize = Math.Max(_BigSize, sb.Length);

            return sb.ToString();
        }

        internal string RecordValuesToString(object[] recordValues)
        {
            StringBuilder sb = new StringBuilder(mSizeHint);
            //string res = String.Empty;

            for (int f = 0; f < mFieldCount; f++)
            {
                mFields[f].AssignToString(sb, recordValues[f]);
            }

            //_BigSize = Math.Max(_BigSize, sb.Length);

            return sb.ToString();
        }

        #endregion

        #region "  ValuesToRecord  "

        /// <summary>Returns a record formed with the passed values.</summary>
        /// <param name="values">The source Values.</param>
        /// <returns>A record formed with the passed values.</returns>
        public object ValuesToRecord(object[] values)
        {
            for (int i = 0; i < mFieldCount; i++)
            {
                if (mFields[i].mFieldTypeInternal == typeof(DateTime) && values[i] is double)
                    values[i] = DoubleToDate((int)(double)values[i]);

                values[i] = mFields[i].CreateValueForField(values[i]);
            }

#if NET_1_1 || MINI

			object record = mRecordConstructor.Invoke(RecordInfo.mEmptyObjectArr);

			for (int i = 0; i < mFieldCount; i++)
			{
				mFields[i].mFieldInfo.SetValue(record, values[i]);
			}

			return record;

#else
            CreateAssingMethods();

            // Asign all values via dinamic method that creates an object and assign values
            return mCreateHandler(values);
#endif

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
#if NET_1_1 || MINI
			object[] res = new object[mFieldCount];

			for (int i = 0; i < mFieldCount; i++)
			{
				res[i] = mFields[i].mFieldInfo.GetValue(record);
			}
			return res;

#else
            CreateGetAllMethod();

            return mGetAllValuesHandler(record);
#endif

        }
        #endregion

        #region "  RecordsToDataTable  "

        #if ! MINI

        internal DataTable RecordsToDataTable(ICollection records)
        {
            return RecordsToDataTable(records, -1);
        }

        internal DataTable RecordsToDataTable(ICollection records, int maxRecords)
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

        internal DataTable CreateEmptyDataTable()
        {
            DataTable res = new DataTable();

            foreach (FieldBase f in mFields)
            {
                DataColumn column1;

                column1 = res.Columns.Add(f.mFieldInfo.Name, f.mFieldInfo.FieldType);
                column1.ReadOnly = true;
            }
            return res;
        }

#endif

        #endregion

        #region "  CreateRecordObject  "

        internal object CreateRecordObject()
        {
            CreateFastConstructor();

            if (mFastConstructor == null)
                CreateFastConstructor();

            return mFastConstructor();
        }
        #endregion

        // ----------------------------------------
        // Lighweight code generation (NET 2.0)

        #region "  CreateAssingMethods  "



        private delegate object[] GetAllValuesCallback(object record);
        private GetAllValuesCallback mGetAllValuesHandler;

        private delegate object CreateAndAssignCallback(object[] values);
        private CreateAndAssignCallback mCreateHandler;

        private void CreateGetAllMethod()
        {
            if (mGetAllValuesHandler != null)
                return;

            DynamicMethod dm = new DynamicMethod("_GetAllValues_FH_RT_", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(object[]), new Type[] { typeof(object) }, mRecordType, true);

            ILGenerator generator = dm.GetILGenerator();

            generator.DeclareLocal(typeof(object[]));
            generator.DeclareLocal(mRecordType);

            generator.Emit(OpCodes.Ldc_I4, mFieldCount);
            generator.Emit(OpCodes.Newarr, typeof(object));
            generator.Emit(OpCodes.Stloc_0);

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Castclass, mRecordType);
            generator.Emit(OpCodes.Stloc_1);


            for (int i = 0; i < mFieldCount; i++)
            {
                FieldBase field = mFields[i];

                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Ldc_I4, i);
                generator.Emit(OpCodes.Ldloc_1);

                generator.Emit(OpCodes.Ldfld, field.mFieldInfo);


                if (field.FieldType.IsValueType)
                {
                    generator.Emit(OpCodes.Box, field.FieldType);
                }

                generator.Emit(OpCodes.Stelem_Ref);
                //generator.EmitCall();

            }

            // return the value
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ret);

            mGetAllValuesHandler = (GetAllValuesCallback)dm.CreateDelegate(typeof(GetAllValuesCallback));

        }


        private void CreateAssingMethods()
        {
            if (mCreateHandler != null)
                return;

            DynamicMethod dm = new DynamicMethod("_CreateAndAssing_FH_RT_", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(object), new Type[] { typeof(object[]) }, mRecordType, true);
            //dm.InitLocals = false;

            ILGenerator generator = dm.GetILGenerator();

            generator.DeclareLocal(mRecordType);
            //generator.DeclareLocal(typeof(object));
            generator.Emit(OpCodes.Newobj, mRecordConstructor);
            generator.Emit(OpCodes.Stloc_0);

            for (int i = 0; i < mFieldCount; i++)
            {
                FieldBase field = mFields[i];

                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldc_I4, i);
                generator.Emit(OpCodes.Ldelem_Ref);


                if (field.FieldType.IsValueType)
                {
                    generator.Emit(OpCodes.Unbox_Any, field.FieldType);
                }
                else
                {
                    generator.Emit(OpCodes.Castclass, field.FieldType);
                }

                generator.Emit(OpCodes.Stfld, field.mFieldInfo);
                //generator.EmitCall();

            }

            // return the value
            generator.Emit(OpCodes.Ldloc_0);
            generator.Emit(OpCodes.Ret);

            mCreateHandler = (CreateAndAssignCallback)dm.CreateDelegate(typeof(CreateAndAssignCallback));

        }

        private delegate object CreateNewObject();
        private CreateNewObject mFastConstructor;


        private void CreateFastConstructor()
        {
            if (mFastConstructor != null)
                return;

            DynamicMethod dm = new DynamicMethod("_CreateRecordFast_FH_RT_", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(object), new Type[] { typeof(object[]) }, mRecordType, true);

            ILGenerator generator = dm.GetILGenerator();

            //			generator.DeclareLocal(mRecordType);
            generator.Emit(OpCodes.Newobj, mRecordConstructor);
            generator.Emit(OpCodes.Ret);

            mFastConstructor = (CreateNewObject)dm.CreateDelegate(typeof(CreateNewObject));

        }


        public static GetFieldValueCallback CreateGetFieldMethod(FieldInfo fi)
        {
            DynamicMethod dm = new DynamicMethod("_GetValue" + fi.Name + "_FH_RT_", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(object), new Type[] { typeof(object) }, fi.DeclaringType, true);

            ILGenerator generator = dm.GetILGenerator();

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Castclass, fi.DeclaringType);
            generator.Emit(OpCodes.Ldfld, fi);
            generator.Emit(OpCodes.Ret);

            return (GetFieldValueCallback)dm.CreateDelegate(typeof(GetFieldValueCallback));

        }

        #endregion

        #region " FieldIndexes  "

        private Hashtable mMapFieldIndex;
        public int GetFieldIndex(string fieldName)
        {
            if (mMapFieldIndex == null)
            {
                mMapFieldIndex = new Hashtable(mFieldCount);
                for (int i = 0; i < mFieldCount; i++)
                {
                    mMapFieldIndex.Add(mFields[i].mFieldInfo.Name, i);
                }
            }

            object res = mMapFieldIndex[fieldName];
            if (res == null)
                throw new BadUsageException("The field: " + fieldName + " was not found in the class: " + mRecordType.Name + ". Remember that this option is case sensitive.");

            return (int)res;
        }

        #endregion

        #region "  GetFieldInfo  "

        internal FieldInfo GetFieldInfo(string name)
        {
            foreach (FieldBase field in mFields)
            {
                if (field.mFieldInfo.Name.ToLower() == name.ToLower())
                    return field.mFieldInfo;
            }

            return null;
        }
        #endregion

    }

}
