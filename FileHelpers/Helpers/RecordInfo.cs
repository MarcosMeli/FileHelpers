#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

#if ! MINI
using System.Data;
using System.Text.RegularExpressions;
using System.Threading;
#endif

namespace FileHelpers
{
	
	

	/// <summary>An internal class used to store information about the Record Type.</summary>
	/// <remarks>Is public to provide extensibility of DataSorage from outside the library.</remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal sealed class RecordInfo
	{
		#region Internal Fields
		
		internal Type mRecordType;
		internal FieldBase[] mFields;
		internal int mIgnoreFirst = 0;
		internal int mIgnoreLast = 0;
		public bool mIgnoreEmptyLines = false;
		private bool mIgnoreEmptySpaces = false;
		
		private string mCommentString = null;
		private bool mCommentAtFirstColumn = true;

		private RecordCondition mCondition = RecordCondition.None;
		private string mConditionSelector = null;
		
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
		
		#region Constructor
		/// <summary>The unique constructor for this class. It needs the subyacent record class.</summary>
		/// <param name="recordType">The Type of the record class.</param>
		internal RecordInfo(Type recordType)
		{
			mRecordType = recordType;
			InitFields();
			mValues = new object[mFieldCount];
			
#if NET_2_0
			CreateAssingMethods();
#endif
		}
		#endregion

		#region "  CreateAssingMethods  "

#if NET_2_0

        private delegate object CreateAndAssign(object[] values);
        private CreateAndAssign mCreateHandler;

		private void CreateAssingMethods()
		{
				DynamicMethod dm = new DynamicMethod("_CreateAndAssing_FH_RT_", MethodAttributes.Static | MethodAttributes.Public, CallingConventions.Standard, typeof(object), new Type[] { typeof(object[]) }, mRecordType, true);
				//dm.InitLocals = false;

			
    ILGenerator generator = dm.GetILGenerator();
    
    generator.DeclareLocal(mRecordType);
    generator.DeclareLocal(typeof(object));
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
            generator.Emit(OpCodes.Unbox, field.mFieldType);
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
    generator.Emit(OpCodes.Stloc_1);

    Label l = generator.DefineLabel();
    generator.Emit(OpCodes.Br_S, l);
    generator.MarkLabel(l);
    generator.Emit(OpCodes.Ldloc_1);
    generator.Emit(OpCodes.Ret);

    mCreateHandler = (CreateAndAssign)dm.CreateDelegate(typeof(CreateAndAssign));

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
					mCommentString = ignoreComments.mCommentMarker;
					mCommentAtFirstColumn = ignoreComments.mAtFirstColumn;
			}

			if (mRecordType.IsDefined(typeof (ConditionalRecordAttribute), false))
			{
				ConditionalRecordAttribute conditional =
					(ConditionalRecordAttribute) mRecordType.GetCustomAttributes(typeof (ConditionalRecordAttribute), false)[0];

				mCondition = conditional.mCondition;
				mConditionSelector = conditional.mConditionSelector;

				#if ! MINI

				if (mCondition == RecordCondition.ExcludeIfMatchRegex ||
					mCondition == RecordCondition.IncludeIfMatchRegex)
				{
					mConditionRegEx = new Regex(mConditionSelector, RegexOptions.Compiled | RegexOptions.CultureInvariant |RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
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

			FieldInfo[] fields;
			fields = mRecordType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

			// Add the inherited fields at the end 
			ArrayList curFields = new ArrayList(fields.Length);
			ArrayList inheritFields = new ArrayList(fields.Length);
			for(int i = 0; i < fields.Length; i++)
				if (fields[i].DeclaringType == mRecordType)
					curFields.Add(fields[i]);
				else
					inheritFields.Add(fields[i]);
			
			inheritFields.AddRange(curFields);

			fields = (FieldInfo[]) inheritFields.ToArray(typeof (FieldInfo));
			
			mFields = CreateFields(fields, recordAttribute);
			mFieldCount = mFields.Length;
			
			if (recordAttribute is FixedLengthRecordAttribute)
			{
				//Defines the initial size of the StringBuilder
				mSizeHint = 0;
				for(int i = 0; i < mFieldCount; i++)
					mSizeHint += ((FixedLengthField) mFields[i]).mFieldLength;
			}
					

			if (mFieldCount == 0)
				throw new BadUsageException("The record class " + mRecordType.Name + " don't contains any field.");

		}
		#endregion

		#region CreateFields

		private static FieldBase[] CreateFields(FieldInfo[] fields, TypedRecordAttribute recordAttribute)
		{
			FieldBase curField;
			ArrayList arr = new ArrayList();
			bool someOptional = false;
			for (int i = 0; i < fields.Length; i++)
			{
				FieldInfo fieldInfo = fields[i];

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
		//TODO: Profile and optimize it !!!
		//      Something like to catch the constructor and use a delagate or something like Emit.
		internal object CreateRecordObject()
		{
			return mRecordConstructor.Invoke(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, RecordInfo.mEmptyObjectArr, null);
		}
		#endregion


		object[] mValues;
		
		#region StringToRecord
		internal object StringToRecord(LineInfo line)
		{
			if (MustIgnoreLine(line.mLineStr))
				return null;

			// array that holds the fields values
			
			for (int i = 0; i < mFieldCount; i++)
			{
				mValues[i] = mFields[i].ExtractValue(line);
			}

			
#if NET_1_1
			object record = CreateRecordObject();
			TypedReference tr = __makeref(record);
			for (int i = 0; i < mFieldCount; i++)
			{
				mFields[i].mFieldInfo.SetValueDirect(tr, mValues[i]);
			//	mFields[i].mFieldInfo.SetValue(record, mValues[i]);
			}
			
			return record;
#else
			// Asign all values via dinamic method that creates an object and assign values
			return mCreateHandler(mValues);;
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
		
		
			if (mCommentString != null)
				if ( (mCommentAtFirstColumn == false && line.TrimStart().StartsWith(mCommentString)) ||
					line.StartsWith(mCommentString))
					return true;
			


			if (mCondition != RecordCondition.None)
			{
				switch (mCondition)
				{
					case RecordCondition.ExcludeIfBegins:
						return ConditionHelper.BeginsWith(line, mConditionSelector);
					case RecordCondition.IncludeIfBegins:
						return ! ConditionHelper.BeginsWith(line, mConditionSelector);

					case RecordCondition.ExcludeIfContains:
						return ConditionHelper.Contains(line, mConditionSelector);
					case RecordCondition.IncludeIfContains:
						return ConditionHelper.Contains(line, mConditionSelector);


					case RecordCondition.ExcludeIfEnclosed:
						return ConditionHelper.Enclosed(line, mConditionSelector);
					case RecordCondition.IncludeIfEnclosed:
						return ! ConditionHelper.Enclosed(line, mConditionSelector);
					
					case RecordCondition.ExcludeIfEnds:
						return ConditionHelper.EndsWith(line, mConditionSelector);
					case RecordCondition.IncludeIfEnds:
						return ! ConditionHelper.EndsWith(line, mConditionSelector);

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

			for (int f = 0; f < mFieldCount; f++)
			{
				sb.Append(mFields[f].AssignToString(record));
			}

			//writer.WriteLine();
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
			object record = mRecordConstructor.Invoke(RecordInfo.mEmptyObjectArr);

			for (int i = 0; i < mFieldCount; i++)
			{
				if (mFields[i].mFieldType == typeof(DateTime) && values[i] is double)
					mFields[i].AssignFromValue(DoubleToDate((int)(double)values[i]), record);
				else
					mFields[i].AssignFromValue(values[i], record);
			}

			return record;
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
			object[] res = new object[mFieldCount];

			for (int i = 0; i < mFieldCount; i++)
			{
				res[i] = mFields[i].mFieldInfo.GetValue(record);
			}

			return res;
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
			DataTable res = new DataTable();
			res.BeginLoadData();

			foreach (FieldBase f in mFields)
			{
				DataColumn column1;

				column1 = res.Columns.Add(f.mFieldInfo.Name, f.mFieldInfo.FieldType);
				column1.ReadOnly = true;
			}

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
		
		#endregion

	#endif


	}
	

}
