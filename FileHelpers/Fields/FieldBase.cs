#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Reflection;

namespace FileHelpers
{


	/// <summary>Base class for all Field Types. Implements all the basic functionality of a field in a typed file.</summary>
	internal abstract class FieldBase
	{
		#region "  Private & Internal Fields  "

		private static Type strType = typeof (string);

		internal Type mFieldType;
		internal bool mIsStringField;
		internal FieldInfo mFieldInfo;

		internal TrimMode mTrimMode = TrimMode.None;
		internal Char[] mTrimChars = null;
		internal bool mIsOptional = false;
		internal bool mNextIsOptional = false;
		internal bool mInNewLine = false;

		internal bool mIsFirst = false;
		internal bool mIsLast = false;
		internal bool mTrailingArray = false;

		internal FixedMode mFixedMode = FixedMode.ExactLength;

		internal object mNullValue = null;
		//internal bool mNullValueOnWrite = false;

		#endregion

		#region "  Constructor  " 

		protected FieldBase(FieldInfo fi)
		{
			mFieldInfo = fi;
			mFieldType = mFieldInfo.FieldType;
			mIsStringField = mFieldType == strType;

			object[] attribs = fi.GetCustomAttributes(typeof (FieldConverterAttribute), true);

			if (attribs.Length > 0)
				mConvertProvider = ((FieldConverterAttribute) attribs[0]).Converter;
			else
				mConvertProvider = ConvertHelpers.GetDefaultConverter(fi.Name, mFieldType);

			if (mConvertProvider != null)
				mConvertProvider.mDestinationType = fi.FieldType;

			attribs = fi.GetCustomAttributes(typeof (FieldNullValueAttribute), true);

			if (attribs.Length > 0)
			{
				mNullValue = ((FieldNullValueAttribute) attribs[0]).NullValue;
//				mNullValueOnWrite = ((FieldNullValueAttribute) attribs[0]).NullValueOnWrite;

				if (mNullValue != null)
				{
					if (! mFieldType.IsAssignableFrom(mNullValue.GetType()))
						throw new BadUsageException("The NullValue is of type: " + mNullValue.GetType().Name +
						                            " that is not asignable to the field " + mFieldInfo.Name + " of type: " +
						                            mFieldType.Name);
				}
			}
		}

		#endregion

		private static char[] WhitespaceChars = new char[] 
			 { 
				 '\t', '\n', '\v', '\f', '\r', ' ', '\x00a0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', 
				 '\u2009', '\u200a', '\u200b', '\u3000', '\ufeff'
			 };

		#region "  MustOverride (String Handling)  " 

		protected abstract ExtractedInfo ExtractFieldString(LineInfo line);

		protected virtual string CreateFieldString(object record)
		{
			object fieldValue = mFieldInfo.GetValue(record);
			// Default implementation

			string res;
			if (mConvertProvider == null)
			{
				if (fieldValue == null)
					res = string.Empty;
//				else if (mNullValueOnWrite && fieldValue.Equals(mNullValue))
//					res = string.Empty;
				else
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

		internal ConverterBase mConvertProvider;

		#region "  ExtractValue  " 

// object[] values, int index, ForwardReader reader
		internal object ExtractValue(LineInfo line)
		{
			//-> extract only what I need

			if (this.mInNewLine == true)
			{
				if (line.EmptyFromPos() == false)
					throw new BadUsageException("Text '" + line.CurrentString +
					                            "' found before the new line of the field: " + mFieldInfo.Name +
					                            " (this is not allowed when you use [FieldInNewLine])");

				line.ReLoad(line.mReader.ReadNextLine());

				if (line.mLine == null)
					throw new BadUsageException("End of stream found parsing the field " + mFieldInfo.Name +
					                            ". Please check the class record.");
			}

			ExtractedInfo info = ExtractFieldString(line);

			object val = AssignFromString(info);

			//-> discard the part that I use


			//TODO: Uncoment this for Quoted Handling
//			if (info.NewRestOfLine != null)
//			{
//				if (info.NewRestOfLine.Length < CharsToDiscard())
//					return info.NewRestOfLine;
//				else
//					return info.NewRestOfLine.Substring(CharsToDiscard());
//			}
//			else
//			{
				int total;
				if (info.CharsRemoved >= line.mLine.Length)
					total = line.mLine.Length;
				else
					total = info.CharsRemoved + CharsToDiscard();

				line.mCurrentPos += total;
				//return buffer.Substring(total);
//			}


			return val;
		}

		#region "  AssignFromString  " 

		internal object AssignFromString(ExtractedInfo fieldString)
		{
			object val;

			
				switch (mTrimMode)
				{
					case TrimMode.None:
						break;

					case TrimMode.Both:
						fieldString.TrimBoth(mTrimChars);
						break;

					case TrimMode.Left:
						fieldString.TrimStart(mTrimChars);
						break;

					case TrimMode.Right:
						fieldString.TrimEnd(mTrimChars);
						break;
				}

			if (mConvertProvider == null)
			{
				if (mIsStringField)
					val = fieldString.ExtractedString();
				else
				{
					// Trim it to use Convert.ChangeType
					fieldString.TrimBoth(WhitespaceChars);

					if (fieldString.Length == 0)
					{
						// Empty stand for null
						val = GetNullValue();
					}
					else
					{
						val = Convert.ChangeType(fieldString, mFieldType, null);
					}
				}
			}

			else
			{
				if (mConvertProvider.CustomNullHandling == false && 
				    fieldString.HasOnlyThisChars(WhitespaceChars))
				{
					val = GetNullValue();
				}
				else
				{
					try
					{
						val = mConvertProvider.StringToField(fieldString.ExtractedString());

						if (val == null)
							val = GetNullValue();
					}
					catch (ConvertException ex)
					{
						ex.FieldName = this.mFieldInfo.Name;
						throw ex;
					}
				}
			}

			return val;
			//mFieldInfo.SetValue(record, val);
		}

		private object GetNullValue()
		{
			object val;
			if (mNullValue == null)
			{
				if (mFieldType.IsValueType)
					throw new BadUsageException("Null Value found for the field '" + mFieldInfo.Name + "' in the class '" +
					                            mFieldInfo.DeclaringType.Name +
					                            "'. You must specify a FieldNullValueAttribute because this is a ValueType and can´t be null.");
				else
					val = null;
			}
			else
				val = mNullValue;
			return val;
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
						throw new BadUsageException("Null Value found. You must specify a NullValueAttribute in the " + mFieldInfo.Name +
						                            " field of type " + mFieldInfo.FieldType.Name + ", because this is a ValueType.");
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

//		private string ApplyTrim(string fieldString)
//		{
////			if (fieldString == null)
////				return string.Empty;
//
//			switch (mTrimMode)
//			{
//				case TrimMode.Both:
//					return fieldString.Trim(mTrimChars);
//
//				case TrimMode.Left:
//					return fieldString.TrimStart(mTrimChars);
//
//				case TrimMode.Right:
//					return fieldString.TrimEnd(mTrimChars);
//			}
//			return fieldString;
//		}

		#endregion

		#region "  AssignToString  " 

		internal string AssignToString(object record)
		{
			string fieldString = string.Empty;

			if (this.mInNewLine == true)
				fieldString = StringHelper.NewLine;

			fieldString += CreateFieldString(record);

			return fieldString;
		}

		#endregion
	}
}
