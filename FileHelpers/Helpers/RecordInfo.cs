#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

#if ! MINI
using System.Data;
#endif

namespace FileHelpers
{
	/// <summary>An internal class used to store information about the Record Type.</summary>
	/// <remarks>Is public to provide extensibility of DataSorage from outside the library.</remarks>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class RecordInfo
	{
		internal Type mRecordType;

		/// <summary>The unique constructor for this class. It needs the subyacent record class.</summary>
		/// <param name="recordType">The Type of the record class.</param>
		internal RecordInfo(Type recordType)
		{
			mRecordType = recordType;
			InitFields();
		}

//		#region " TESTING "
//
//		internal RecordInfo()
//		{
//		}
//
//		internal void AddFields(FieldBase[] fields)
//		{
//			mFields = fields;
//		}
//
//		#endregion

		private void InitFields()
		{
			//-> Checked by the AttributeTargets
			//new BadUsageException("Structures are not supported in the FileHelperEngine only classes are allowed.");

			TypedRecordAttribute recordAttribute = null;

			if (mRecordType.IsDefined(typeof (TypedRecordAttribute), true) == false)
				throw new BadUsageException("The class " + mRecordType.Name + " must be marked with the DelimitedRecord or FixedLengthRecord Attribute.");
			else
			{
				object[] attbs = mRecordType.GetCustomAttributes(typeof (TypedRecordAttribute), true);
				recordAttribute = (TypedRecordAttribute) attbs[0];
			}

			if (mRecordType.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic, null, mEmptyTypeArr, new ParameterModifier[] {}) == null)
				throw new BadUsageException("The record class don't have a default constructor.");

			if (mRecordType.IsDefined(typeof (IgnoreFirstAttribute), false))
				mIgnoreFirst = ((IgnoreFirstAttribute) mRecordType.GetCustomAttributes(typeof (IgnoreFirstAttribute), false)[0]).NumberOfLines;

			if (mRecordType.IsDefined(typeof (IgnoreLastAttribute), false))
				mIgnoreLast = ((IgnoreLastAttribute) mRecordType.GetCustomAttributes(typeof (IgnoreLastAttribute), false)[0]).NumberOfLines;

			if (mRecordType.IsDefined(typeof (IgnoreEmptyLinesAttribute), false))
				mIgnoreEmptyLines = true;

			mRecordConstructor = mRecordType.GetConstructor(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic, null, mEmptyTypeArr, new ParameterModifier[] {});


// Create fields

			FieldInfo[] fields;
			fields = mRecordType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

			mFields = CreateFields(fields, recordAttribute);
			mFieldCount = mFields.Length;

			if (mFieldCount == 0)
				throw new BadUsageException("The record class don't contains any field.");

		}


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
				((FieldBase) arr[arr.Count - 1]).mIsLast = true;

			return (FieldBase[]) arr.ToArray(typeof (FieldBase));

		}

		internal FieldBase[] mFields;
		private int mFieldCount;
		internal int mIgnoreFirst = 0;
		internal int mIgnoreLast = 0;
		internal bool mIgnoreEmptyLines = false;

		internal static readonly object[] mEmptyObjectArr = new object[] {};
		internal static readonly Type[] mEmptyTypeArr = new Type[] {};
		ConstructorInfo mRecordConstructor;

		/// <summary>The Number of no ignored fields in the record class.</summary>
		public int FieldCount
		{
			get { return mFieldCount; }
		}


		internal FieldInfo GetFieldInfo(string name)
		{
			foreach (FieldBase field in mFields)
			{
				if (field.mFieldInfo.Name.ToLower() == name.ToLower())
					return field.mFieldInfo;
			}

			return null;
		}


		internal object StringToRecord(string line, ForwardReader reader)
		{
			if (line == string.Empty && mIgnoreEmptyLines)
				return null;

			object record = CreateRecordObject();

			for (int i = 0; i < mFieldCount; i++)
			{
				line = mFields[i].ExtractAndAssignFromString(line, record, reader);
			}

			return record;
		}

		internal object CreateRecordObject()
		{
			return mRecordConstructor.Invoke(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, RecordInfo.mEmptyObjectArr, null);
		}

		/// <summary>Internal.</summary>
		/// <param name="record"></param>
		/// <returns></returns>
		internal string RecordToString(object record)
		{
			string res = String.Empty;

			for (int f = 0; f < mFieldCount; f++)
			{
				res += mFields[f].AssignToString(record);
			}
			return res;

		}

		/// <summary>Returns a record formed with the passed values.</summary>
		/// <param name="values">The source Values.</param>
		/// <returns>A record formed with the passed values.</returns>
		public object ValuesToRecord(object[] values)
		{
			object record = mRecordConstructor.Invoke(RecordInfo.mEmptyObjectArr);

			for (int i = 0; i < mFieldCount; i++)
			{
				mFields[i].AssignFromValue(values[i], record);
			}

			return record;
		}

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

		/// <summary>Indicates if the Record Info has fields of type Date</summary>
		/// <remarks>This is used externally by the ExcelStorage.</remarks>
		public bool HasDateFields
		{
			get
			{
				foreach (FieldBase field in mFields)
				{
					if (field.mFieldType == typeof (DateTime))
						return true;
				}
				return false;
			}
		}


		
	#if ! MINI

		internal DataTable RecordsToDataTable(ICollection records)
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

			foreach (object r in records)
				res.Rows.Add(RecordToValues(r));

			res.EndLoadData();
			return res;
		}

		#endif

	}


}