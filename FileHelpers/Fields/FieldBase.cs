#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Reflection;

namespace FileHelpers.Fields
{
	/// <summary>Base class for all Field Types. Implements all the basic functionality of a field in a typed file.</summary>
	internal abstract class FieldBase
	{
		#region  "  ExtractInfo Class  "

		protected struct ExtractInfo
		{
			public int CharsRemoved;
			public string ExtractedString;
			//public string TrailString;

			public ExtractInfo(string extracted)
			{
				ExtractedString = extracted;
				CharsRemoved = extracted.Length;
			}

			public ExtractInfo(string extracted, int charsRem)
			{
				ExtractedString = extracted;
				CharsRemoved = charsRem;
			}

		}

		#endregion

		#region "  Private & Internal Fields  "

		private static Type strType = typeof (string);

		private Type mFieldType;
		private FieldInfo mFieldInfo;

		internal TrimMode mTrimMode = TrimMode.None;
		internal Char[] mTrimChars = null;

		internal bool mIsLast = false;

		private object mNullValue = null;

		#endregion

		#region "  Constructor  " 

		protected FieldBase(FieldInfo fi)
		{
			mFieldInfo = fi;
			mFieldType = mFieldInfo.FieldType;

			object[] attribs = fi.GetCustomAttributes(typeof (FieldConverterAttribute), true);
			if (attribs.Length > 0)
				mConvertProvider = ((FieldConverterAttribute) attribs[0]).Converter;
			else
				mConvertProvider = ConvertHelpers.GetDefaultConverter(fi.Name, mFieldType);

			attribs = fi.GetCustomAttributes(typeof (FieldNullValueAttribute), true);

			if (attribs.Length > 0)
			{
				mNullValue = ((FieldNullValueAttribute) attribs[0]).NullValue;
				if (mNullValue != null)
				{
					if (! mFieldType.IsAssignableFrom(mNullValue.GetType()))
						throw new BadUsageException("The NullValue is of type: " + mNullValue.GetType().Name + " that is not asignable to the field " + mFieldInfo.Name + " of type: " + mFieldType.Name);
				}
			}

		}

		#endregion

		#region "  MustOverride (String Handling)  " 

		protected abstract ExtractInfo ExtractFieldString(string from);

		protected virtual string CreateFieldString(object record)
		{
			object fieldValue = mFieldInfo.GetValue(record);
			// Default implementation

			string res;
			if (mConvertProvider == null)
			{
				res = fieldValue.ToString();
			}
			else
			{
				res = mConvertProvider.FieldToString(fieldValue);
			}

			return res;
		}

		protected virtual int CharsToDiscard()
		{
			return 0;
		}

		#endregion

		#region "  Convert Handlers  " 

		private ConverterBase mConvertProvider;

		public ConverterBase ConvertProvider
		{
			get { return mConvertProvider; }
			set { mConvertProvider = value; }
		}

		#endregion

		#region "  FieldInfo  " 

		public FieldInfo FieldInfo
		{
			get { return mFieldInfo; }
		}

		#endregion

		#region "  TrimMode  " 

		public TrimMode TrimMode
		{
			get { return mTrimMode; }
		}

		#endregion

		#region "  TrimChars  " 

		public char[] TrimChars
		{
			get { return mTrimChars; }
		}

		#endregion

		#region "  FieldType" 

		public Type FieldType
		{
			get { return mFieldType; }
		}

		#endregion

		#region "  ExtractAndAssignFromString  " 

		internal string ExtractAndAssignFromString(string buffer, object record)
		{
			//-> Extraigo la que me corresponde

			ExtractInfo info = ExtractFieldString(buffer);

			string fieldString;
			int discardFrom;

			fieldString = info.ExtractedString;
			discardFrom = info.CharsRemoved;

			AssignFromString(fieldString, record);

			//-> Descarto lo leído
			return buffer.Substring(discardFrom + CharsToDiscard());

		}

		#region "  AssignFromString  " 

		internal void AssignFromString(string fieldString, object record)
		{
			object val = null;

			fieldString = ApplyTrim(fieldString);

			if (mFieldType == strType)
				val = fieldString;
			else
			{
				fieldString = fieldString.Trim();
				if (fieldString == String.Empty)
				{
					if (mNullValue == null)
					{
						if (mFieldType.IsValueType)
							throw new NullValueException(mFieldInfo);
						else
							val = null;
					}
					else
					{
						val = mNullValue;
					}
				}
				else
				{
					if (mConvertProvider == null)
						val = Convert.ChangeType(fieldString, mFieldType, null);
					else
						val = mConvertProvider.StringToField(fieldString);
				}
			}

			mFieldInfo.SetValue(record, val);

		}

		#endregion

		#region "  AssignFromValue  " 

		internal void AssignFromValue(object fieldValue, object record)
		{
			object val = null;

			//fieldString = ApplyTrim(fieldString);

			if (fieldValue == null)
			{
				if (mNullValue == null)
				{
					if (mFieldType.IsValueType)
						throw new NullValueException(mFieldInfo);
					else
						val = null;
				}
				else
				{
					val = mNullValue;
				}
			}
			else if (mFieldType == fieldValue)
				val = fieldValue;
			else
			{
				if (mConvertProvider == null)
					val = Convert.ChangeType(fieldValue, mFieldType, null);
				else
				{
					try
					{
						val = Convert.ChangeType(fieldValue, mFieldType, null);
					}
					catch
					{
						val = mConvertProvider.StringToField(fieldValue.ToString());
					}
				}
			}

			mFieldInfo.SetValue(record, val);

		}

		#endregion

		private string ApplyTrim(string fieldString)
		{
			if (fieldString == null)
				return string.Empty;

			switch (mTrimMode)
			{
				case TrimMode.None:
					return fieldString;

				case TrimMode.Both:
					return fieldString.Trim(mTrimChars);

				case TrimMode.Left:
					return fieldString.TrimStart(mTrimChars);

				case TrimMode.Right:
					return fieldString.TrimEnd(mTrimChars);
			}
			return fieldString;
		}

		#endregion

		#region "  AssignToString  " 

		internal string AssignToString(object record)
		{
//			switch(TrimMode)
//			{
//				case TrimMode.None:
//					break;
//
//				case TrimMode.Both:
//					fieldString = fieldString.Trim(TrimChars);
//					break;
//
//				case TrimMode.Left:
//					fieldString = fieldString.TrimStart(TrimChars);
//					break;
//
//				case TrimMode.Right:
//					fieldString = fieldString.TrimEnd(TrimChars);
//					break;
//			}

			string fieldString = CreateFieldString(record);

//			if (ConvertHandler == null)
//			{
//				if (mFieldType == strType)
//					val = fieldString;
//				else
//					val = Convert.ChangeType(fieldString, mFieldType);
//			}
//			else
//			{
//				val = ConvertHandler(fieldString);
//			}

			//mFieldInfo.SetValue(record, val);

			//-> Descarto lo leído
			return fieldString;
		}


		#endregion

	}
}