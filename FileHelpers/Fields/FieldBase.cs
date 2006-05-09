#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Reflection;

namespace FileHelpers
{

	#region  "  ExtractInfo Class  "

	internal struct ExtractedInfo
			  {
				  public int CharsRemoved;
				  public string ExtractedString;
				  public int ExtraLines;
		 		  public string NewRestOfLine;
		//public string TrailString;

				  public ExtractedInfo(string extracted)
				  {
					  ExtractedString = extracted;
					  CharsRemoved = extracted.Length;
					  ExtraLines = 0;
					  NewRestOfLine = null;
				  }

				  public ExtractedInfo(string extracted, int charsRem, int lines)
				  {
					  ExtractedString = extracted;
					  CharsRemoved = charsRem;
					  ExtraLines = lines;
					  NewRestOfLine = null;
				  }

				  internal static readonly ExtractedInfo Empty = new ExtractedInfo(string.Empty);
			  }

	#endregion

	/// <summary>Base class for all Field Types. Implements all the basic functionality of a field in a typed file.</summary>
	internal abstract class FieldBase
	{
		#region "  Private & Internal Fields  "

		private static Type strType = typeof (string);

		internal Type mFieldType;
		internal FieldInfo mFieldInfo;

		internal TrimMode mTrimMode = TrimMode.None;
		internal Char[] mTrimChars = null;
		internal bool mIsOptional = false;
		internal bool mNextIsOptional = false;
		internal bool mInNewLine = false;

		internal bool mIsLast = false;
		internal bool mTrailingArray = false;

		internal object mNullValue = null;

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

		protected abstract ExtractedInfo ExtractFieldString(string from, ForwardReader reader); 

		protected virtual string CreateFieldString(object record)
		{
			object fieldValue = mFieldInfo.GetValue(record);
			// Default implementation

			string res;
			if (mConvertProvider == null)
			{
				if (fieldValue == null)
					res = string.Empty;
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

//		#region "  Convert Handlers  " 

		internal ConverterBase mConvertProvider;

//		public ConverterBase ConvertProvider
//		{
//			get { return mConvertProvider; }
//			set { mConvertProvider = value; }
//		}
//
//		#endregion



//		#region "  TrimMode  " 
//
//		public TrimMode TrimMode
//		{
//			get { return mTrimMode; }
//		}
//
//		#endregion
//
//		#region "  TrimChars  " 
//
//		public char[] TrimChars
//		{
//			get { return mTrimChars; }
//		}
//
//		#endregion
//
//		#region "  IsOptional  " 
//
//		public bool IsOptional
//		{
//			get { return mIsOptional; }
//		}
//
//		#endregion
//
//		#region "  NextIsOptional  " 
//
//		internal bool NextIsOptional
//		{
//			get { return mNextIsOptional; }
//			set { mNextIsOptional = value; }
//		}
//
//		#endregion

//		#region "  InNewLine  " 

//		public bool InNewLine
//		{
//			get { return mInNewLine; }
//		}
//
//		#endregion
//
//

//		#region "  FieldType  " 
//
//		public Type FieldType
//		{
//			get { return mFieldType; }
//		}
//
//		#endregion

		#region "  ExtractAndAssignFromString  " 

		internal string ExtractAndAssignFromString(string buffer, object record, ForwardReader reader)
		{
			//-> extract only what I need

			if (this.mInNewLine == true)
			{
				if (buffer.Trim() != String.Empty)
					throw new BadUsageException("Text '" + buffer +"' found before the new line of the field: " + mFieldInfo.Name + " (this is not allowed when you use [FieldInNewLine])");

				buffer = reader.ReadNextLine();

				if (buffer == null)
					throw new BadUsageException("End of stream found parsing the field " + mFieldInfo.Name + ". Please check the class record.");
			}

			ExtractedInfo info = ExtractFieldString(buffer, reader);

			AssignFromString(info.ExtractedString, record);

			//-> discard the part that I use
			

			if (info.NewRestOfLine != null)
			{
				if (info.NewRestOfLine.Length < CharsToDiscard())
					return info.NewRestOfLine;
				else
					return info.NewRestOfLine.Substring(CharsToDiscard());
			}
			else
			{
				int total;
				if (info.CharsRemoved >= buffer.Length)
					total = buffer.Length;
				else
					total = info.CharsRemoved + CharsToDiscard();

				return buffer.Substring(total);
			}

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
				if (mConvertProvider != null)
				{
					if (mConvertProvider.CustomNullHandling == false && fieldString.Trim() == String.Empty )
					{
						val = GetNullValue();
					}
					else
					{
						val = mConvertProvider.StringToField(fieldString);

						if (val == null)
							val = GetNullValue();
					}
				}
				else 
				{
					// Trim it to use Convert.ChangeType
					fieldString = fieldString.Trim();

					if (fieldString == String.Empty)
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
			
			mFieldInfo.SetValue(record, val);
			
		}

		private object GetNullValue()
		{
			object val;
			if (mNullValue == null)
			{
				if (mFieldType.IsValueType)
					throw new BadUsageException("Null Value found for the field " + mFieldInfo.Name + " in the class " + mFieldInfo.DeclaringType.Name + ". You must specify a FieldNullValueAttribute because this is a ValueType and can´t be null.");
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
						throw new BadUsageException("Null Value found. You must specify a NullValueAttribute in the " + mFieldInfo.Name + " field of type " + mFieldInfo.FieldType.Name + ", because this is a ValueType.");
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

			string fieldString = string.Empty;

			if (this.mInNewLine == true)
				fieldString = StringHelper.NewLine;

			fieldString += CreateFieldString(record);

			return fieldString;
		}

		#endregion
	}
}