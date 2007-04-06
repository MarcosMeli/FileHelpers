#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Collections;
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
		#region "  Internal Fields  "
		
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

		private static readonly object[] mEmptyObjectArr = new object[] {};
		private static readonly Type[] mEmptyTypeArr = new Type[] {};
		
		#endregion
		
		#region "  Constructor  "

		/// <summary>The unique constructor for this class. It needs the subyacent record class.</summary>
		/// <param name="recordType">The Type of the record class.</param>
		internal RecordInfo(Type recordType)
		{
			mRecordType = recordType;
			InitFields();
		
		}

		internal bool IsDelimited
		{
			get { return mFields[0] is DelimitedField; }
		}

		#endregion

		#region "  CreateAssingMethods  "

#if NET_2_0

        private delegate object[] GetAllValuesCallback(object record);
        private GetAllValuesCallback mGetAllValuesHandler;

        private void CreateGetAllMethod()
		{
			if (mGetAllValuesHandler != null)
				return;

				DynamicMethod dm = new DynamicMethod("_GetAllValues_FH_RT_", MethodAttributes.Static | MethodAttributes.Public,	CallingConventions.Standard, typeof(object[]), new Type[] { typeof(object) }, mRecordType, true);

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


        if (field.mFieldType.IsValueType)
        {
            generator.Emit(OpCodes.Box, field.mFieldType);
        }

        generator.Emit(OpCodes.Stelem_Ref);
        //generator.EmitCall();

    }

    // return the value
    generator.Emit(OpCodes.Ldloc_0);
    generator.Emit(OpCodes.Ret);

    mGetAllValuesHandler = (GetAllValuesCallback)dm.CreateDelegate(typeof(GetAllValuesCallback));

		}








        private delegate object CreateAndAssignCallback(object[] values);
        private CreateAndAssignCallback mCreateHandler;




		private void CreateAssingMethods()
		{
			if (mCreateHandler != null)
				return;

				DynamicMethod dm = new DynamicMethod("_CreateAndAssing_FH_RT_", MethodAttributes.Static | MethodAttributes.Public,							CallingConventions.Standard, typeof(object), new Type[] { typeof(object[]) }, mRecordType, true);
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


        if (field.mFieldType.IsValueType)
        {
            generator.Emit(OpCodes.Unbox_Any, field.mFieldType);
        }
        else
        {
            generator.Emit(OpCodes.Castclass, field.mFieldType);
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

			DynamicMethod dm = new DynamicMethod("_CreateRecordFast_FH_RT_", MethodAttributes.Static | MethodAttributes.Public,							CallingConventions.Standard, typeof(object), new Type[] { typeof(object[]) }, mRecordType, true);
			
		    ILGenerator generator = dm.GetILGenerator();
    
//			generator.DeclareLocal(mRecordType);
		    generator.Emit(OpCodes.Newobj, mRecordConstructor);
			generator.Emit(OpCodes.Ret);

			mFastConstructor = (CreateNewObject)dm.CreateDelegate(typeof(CreateNewObject));

		}


	#endif
		#endregion

		
		#region InitFields

		private void InitFields()
		{
			//-> Checked by the AttributeTargets
			//new BadUsageException("Structures are not supported in the FileHelperEngine only classes are allowed.");

			TypedRecordAttribute recordAttribute = null;

			if (mRecordType.IsDefined(typeof (TypedRecordAttribute), true) == false)
				throw new BadUsageException("The class " + mRecordType.Name + " must be marked with the [DelimitedRecord] or [FixedLengthRecord] Attribute.");
			else
			{
				object[] attbs = mRecordType.GetCustomAttributes(typeof (TypedRecordAttribute), true);
				recordAttribute = (TypedRecordAttribute) attbs[0];
			}

			if (mRecordType.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic, null, mEmptyTypeArr, new ParameterModifier[] {}) == null)
				throw new BadUsageException("The record class " + mRecordType.Name + " need a constructor with no args (public or private)");

			if (mRecordType.IsDefined(typeof (IgnoreFirstAttribute), false))
				mIgnoreFirst = ((IgnoreFirstAttribute) mRecordType.GetCustomAttributes(typeof (IgnoreFirstAttribute), false)[0]).NumberOfLines;

			if (mRecordType.IsDefined(typeof (IgnoreLastAttribute), false))
				mIgnoreLast = ((IgnoreLastAttribute) mRecordType.GetCustomAttributes(typeof (IgnoreLastAttribute), false)[0]).NumberOfLines;

			if (mRecordType.IsDefined(typeof (IgnoreEmptyLinesAttribute), false))
			{
				mIgnoreEmptyLines = true;
				mIgnoreEmptySpaces = ((IgnoreEmptyLinesAttribute) mRecordType.GetCustomAttributes(typeof (IgnoreEmptyLinesAttribute), false)[0]).
						mIgnoreSpaces;
			}

			if (mRecordType.IsDefined(typeof (IgnoreCommentedLinesAttribute), false))
			{
				IgnoreCommentedLinesAttribute ignoreComments =
					(IgnoreCommentedLinesAttribute) mRecordType.GetCustomAttributes(typeof (IgnoreCommentedLinesAttribute), false)[0];
					mCommentMarker = ignoreComments.mCommentMarker;
					mCommentAnyPlace = ignoreComments.mAnyPlace;
			}

			if (mRecordType.IsDefined(typeof (ConditionalRecordAttribute), false))
			{
				ConditionalRecordAttribute conditional =
					(ConditionalRecordAttribute) mRecordType.GetCustomAttributes(typeof (ConditionalRecordAttribute), false)[0];

				mRecordCondition = conditional.mCondition;
				mRecordConditionSelector = conditional.mConditionSelector;

				#if ! MINI

				if (mRecordCondition == RecordCondition.ExcludeIfMatchRegex ||
					mRecordCondition == RecordCondition.IncludeIfMatchRegex)
				{
					mConditionRegEx = new Regex(mRecordConditionSelector, RegexOptions.Compiled | RegexOptions.CultureInvariant |RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
				}
				#endif
			}



#if ! MINI
			if (typeof(INotifyRead).IsAssignableFrom(mRecordType))
				mNotifyRead = true;

			if (typeof(INotifyWrite).IsAssignableFrom(mRecordType))
				mNotifyWrite = true;
#endif


			mRecordConstructor = mRecordType.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic, null, mEmptyTypeArr, new ParameterModifier[] {});

			// Create fields

			ArrayList fields = new ArrayList();
			RecursiveGetFields(fields, mRecordType, recordAttribute);

			mFields = CreateCoreFields(fields, recordAttribute);
			mFieldCount = mFields.Length;
			
			if (recordAttribute is FixedLengthRecordAttribute)
			{
				// Defines the initial size of the StringBuilder
				mSizeHint = 0;
				for(int i = 0; i < mFieldCount; i++)
					mSizeHint += ((FixedLengthField) mFields[i]).mFieldLength;
			}
					

			if (mFieldCount == 0)
				throw new BadUsageException("The record class " + mRecordType.Name + " don't contains any field.");

		}

		private void RecursiveGetFields(ArrayList fields, Type currentType, TypedRecordAttribute recordAttribute)
		{
			if (currentType.BaseType != null)
				RecursiveGetFields(fields, currentType.BaseType, recordAttribute);

			fields.AddRange(currentType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly));
		}

		#endregion

		#region CreateFields

		private static FieldBase[] CreateCoreFields(ArrayList fields, TypedRecordAttribute recordAttribute)
		{
			FieldBase curField;
			ArrayList arr = new ArrayList();
			bool someOptional = false;
			for (int i = 0; i < fields.Count; i++)
			{
				FieldInfo fieldInfo = (FieldInfo) fields[i];

				curField = FieldFactory.CreateField(fieldInfo, recordAttribute, someOptional);

				if (curField != null)
				{
					someOptional = curField.mIsOptional;

					arr.Add(curField);
					if (arr.Count > 1)
						((FieldBase)arr[arr.Count-2]).mNextIsOptional = ((FieldBase)arr[arr.Count-1]).mIsOptional;
				}
			}

			if (arr.Count > 0)
			{
				((FieldBase) arr[0]).mIsFirst = true;
				((FieldBase) arr[arr.Count - 1]).mIsLast = true;

			}

			return (FieldBase[]) arr.ToArray(typeof (FieldBase));

		}
		#endregion

		#region GetFieldInfo
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

		#region CreateRecordObject
		internal object CreateRecordObject()
		{
#if NET_2_0
			CreateFastConstructor();

			if (mFastConstructor == null)
				CreateFastConstructor();

			return mFastConstructor();
#else
			return mRecordConstructor.Invoke(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, RecordInfo.mEmptyObjectArr, null);
#endif
		}
		#endregion


		#region StringToRecord
		internal object StringToRecord(LineInfo line)
		{
			if (MustIgnoreLine(line.mLineStr))
				return null;


			object[] mValues = new object[mFieldCount];

			// array that holds the fields values
			
			for (int i = 0; i < mFieldCount; i++)
			{
				mValues[i] = mFields[i].ExtractValue(line);
			}

#if NET_1_1 || MINI
			object record = CreateRecordObject();
			for (int i = 0; i < mFieldCount; i++)
			{
				mFields[i].mFieldInfo.SetValue(record, mValues[i]);
			}
			
			return record;
#else
			CreateAssingMethods();

            try
            {
                // Asign all values via dinamic method that creates an object and assign values
               return mCreateHandler(mValues);
            }
            catch (InvalidCastException)
            {
                // Occurrs when the a custom converter returns an invalid value for the field.
                for (int i = 0; i < mFieldCount; i++)
                {
                    if (mValues[i] != null && ! mFields[i].mFieldType.IsInstanceOfType(mValues[i]))
                        throw new ConvertException(null, mFields[i].mFieldType, mFields[i].mFieldInfo.Name, line.mReader.LineNumber, -1, "The converter for the field: " + mFields[i].mFieldInfo.Name + " returns an object of Type: " + mValues[i].GetType().Name + " and the field is of type: " + mFields[i].mFieldType.Name);
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
				if ( (mCommentAnyPlace && line.TrimStart().StartsWith(mCommentMarker)) ||
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
						return ConditionHelper.Contains(line, mRecordConditionSelector);


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

		#region RecordToString

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
		#endregion

		#region ValuesToRecord
		/// <summary>Returns a record formed with the passed values.</summary>
		/// <param name="values">The source Values.</param>
		/// <returns>A record formed with the passed values.</returns>
		public object ValuesToRecord(object[] values)
		{
			for (int i = 0; i < mFieldCount; i++)
			{
				if (mFields[i].mFieldType == typeof(DateTime) && values[i] is double)
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

		private DateTime DoubleToDate(int serialNumber)
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

		#region RecordToValues
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

	
	#if ! MINI

		#region RecordsToDataTable

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

		#endregion

	#endif


		#if NET_2_0

		public static GetFieldValueCallback CreateGetFieldMethod(FieldInfo fi)
		{
				DynamicMethod dm = new DynamicMethod("_GetValue"+ fi.Name + "_FH_RT_", MethodAttributes.Static | MethodAttributes.Public,	CallingConventions.Standard, typeof(object), new Type[] { typeof(object) }, fi.DeclaringType, true);

				ILGenerator generator = dm.GetILGenerator();
    
				generator.Emit(OpCodes.Ldarg_0);
				generator.Emit(OpCodes.Castclass, fi.DeclaringType);
				generator.Emit(OpCodes.Ldfld, fi);
				generator.Emit(OpCodes.Ret);

				return (GetFieldValueCallback)dm.CreateDelegate(typeof(GetFieldValueCallback));

		}
	
		#endif
	}
	
}
