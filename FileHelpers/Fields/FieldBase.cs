#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace FileHelpers
{

    /// <summary>Base class for all Field Types. Implements all the basic functionality of a field in a typed file.</summary>
    internal abstract class FieldBase
    {

        #region "  CreateField  "

        public static FieldBase CreateField(FieldInfo fi, TypedRecordAttribute recordAttribute)
        {
            // If ignored, return null
            if (fi.IsDefined(typeof(FieldIgnoredAttribute), true))
                return null;

            FieldBase res = null;

            FieldAttribute[] attributes;
            FieldAttribute fieldAttb;

            attributes = (FieldAttribute[])fi.GetCustomAttributes(typeof(FieldAttribute), true);

            // CHECK USAGE ERRORS !!!

            if (recordAttribute is FixedLengthRecordAttribute && attributes.Length == 0)
                throw new BadUsageException("The field: '" + fi.Name + "' must be marked the FieldFixedLength attribute because the record class is marked with FixedLengthRecord.");

            if (attributes.Length > 1)
                throw new BadUsageException("The field: '" + fi.Name + "' has a FieldFixedLength and a FieldDelimiter attribute.");

            if (recordAttribute is DelimitedRecordAttribute && fi.IsDefined(typeof(FieldAlignAttribute), false))
                throw new BadUsageException("The field: '" + fi.Name + "' cant be marked with FieldAlign attribute, it is only valid for fixed length records and are used only for write purpouse.");

            if (fi.FieldType.IsArray == false && fi.IsDefined(typeof(FieldArrayLengthAttribute), false))
                throw new BadUsageException("The field: '" + fi.Name + "' cant be marked with FieldArrayLength attribute is only valid for array fields.");


            // PROCESS IN NORMAL CONDITIONS

            if (attributes.Length > 0)
            {
                fieldAttb = attributes[0];

                if (fieldAttb is FieldFixedLengthAttribute)
                {
                    // Fixed Field
                    if (recordAttribute is DelimitedRecordAttribute)
                        throw new BadUsageException("The field: '" + fi.Name + "' cant be marked with FieldFixedLength attribute, it is only for the FixedLengthRecords not for delimited ones.");

                    FieldFixedLengthAttribute attb = ((FieldFixedLengthAttribute)fieldAttb);

                    FieldAlignAttribute[] alignAttbs = (FieldAlignAttribute[])fi.GetCustomAttributes(typeof(FieldAlignAttribute), false);
                    FieldAlignAttribute align = null;

                    if (alignAttbs.Length > 0)
                        align = alignAttbs[0];

                    res = new FixedLengthField(fi, attb.Length, align);
                    ((FixedLengthField)res).mFixedMode = ((FixedLengthRecordAttribute)recordAttribute).mFixedMode;
                }
                else if (fieldAttb is FieldDelimiterAttribute)
                {
                    // Delimited Field
                    if (recordAttribute is FixedLengthRecordAttribute)
                        throw new BadUsageException("The field: '" + fi.Name + "' cant be marked with FieldDelimiter attribute, it is only for DelimitedRecords not for fixed ones.");

                    res = new DelimitedField(fi, ((FieldDelimiterAttribute)fieldAttb).mSeparator);

                }
                else
                    throw new BadUsageException("Custom Record Types are not currently supported. And sure will never be :P (You must not be here)");
            }
            else // attributes.Length == 0
            {
                if (recordAttribute is DelimitedRecordAttribute)
                    res = new DelimitedField(fi, ((DelimitedRecordAttribute)recordAttribute).Separator);
            }

            if (res != null)
            {

                // Trim Related
                FieldTrimAttribute[] trim = (FieldTrimAttribute[])fi.GetCustomAttributes(typeof(FieldTrimAttribute), false);
                if (trim.Length > 0)
                {
                    res.mTrimMode = trim[0].TrimMode;
                    res.mTrimChars = trim[0].TrimChars;
                }

                // Quote Related
                FieldQuotedAttribute[] quotedAttributes = (FieldQuotedAttribute[])fi.GetCustomAttributes(typeof(FieldQuotedAttribute), false);
                if (quotedAttributes.Length > 0)
                {
                    if (res is FixedLengthField)
                        throw new BadUsageException("The field: '" + fi.Name + "' cant be marked with FieldQuoted attribute, it is only for the delimited records.");

                    ((DelimitedField)res).mQuoteChar = quotedAttributes[0].QuoteChar;
                    ((DelimitedField)res).mQuoteMode = quotedAttributes[0].QuoteMode;
                    ((DelimitedField)res).mQuoteMultiline = quotedAttributes[0].QuoteMultiline;
                }

                // Optional Related
                FieldOptionalAttribute[] optionalAttribs = (FieldOptionalAttribute[])fi.GetCustomAttributes(typeof(FieldOptionalAttribute), false);

                if (optionalAttribs.Length > 0)
                    res.mIsOptional = true;

                // NewLine Related
                res.mInNewLine = fi.IsDefined(typeof(FieldInNewLineAttribute), true);

                // Array Related
                if (fi.FieldType.IsArray)
                {
                    res.mIsArray = true;
                    res.mArrayType = fi.FieldType.GetElementType();

                    FieldArrayLengthAttribute[] arrayAttribs = (FieldArrayLengthAttribute[])fi.GetCustomAttributes(typeof(FieldArrayLengthAttribute), false);

                    if (arrayAttribs.Length > 0)
                    {
                        res.mArrayMinLength = arrayAttribs[0].mMinLength;
                        res.mArrayMaxLength = arrayAttribs[0].mMaxLength;

                        if (res.mArrayMaxLength < res.mArrayMinLength ||
                            res.mArrayMinLength < 0 ||
                            res.mArrayMaxLength <= 0)
                            throw new BadUsageException("The field: " + fi.Name + " has invalid length values in the [FieldArrayLength] attribute.");
                    }
                    else
                    {
                        // MinValue indicates that there is no FieldArrayLength in the array
                        res.mArrayMinLength = int.MinValue;
                        res.mArrayMaxLength = int.MaxValue;
                    }
                }

            }

            return res;
        }


        #endregion

        public string FielName
        {
            get { return mFieldInfo.Name; }
        }

        public Type FieldType
        {
            get { return mFieldType; }
        }

        #region "  Private & Internal Fields  "

        private static Type strType = typeof(string);

        private Type mFieldType;
        internal Type mFieldTypeInternal;
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

        internal bool mIsArray = false;
        internal Type mArrayType;
        internal int mArrayMinLength;
        internal int mArrayMaxLength;

        internal object mNullValue = null;
        //internal bool mNullValueOnWrite = false;

#if NET_2_0
        private bool mIsNullableType = false;
#endif
        #endregion

        #region "  Constructor  "

        internal FieldBase(FieldInfo fi)
        {
            mFieldInfo = fi;
            mFieldType = mFieldInfo.FieldType;

            if (mFieldType.IsArray)
                mFieldTypeInternal = mFieldType.GetElementType();
            else
                mFieldTypeInternal = mFieldType;

            mIsStringField = mFieldTypeInternal == strType;

            object[] attribs = fi.GetCustomAttributes(typeof(FieldConverterAttribute), true);

            if (attribs.Length > 0)
            {
                FieldConverterAttribute conv = (FieldConverterAttribute)attribs[0];
                mConvertProvider = conv.Converter;
                conv.ValidateTypes(mFieldInfo);
            }
            else
                mConvertProvider = ConvertHelpers.GetDefaultConverter(fi.Name, mFieldType);

            if (mConvertProvider != null)
                mConvertProvider.mDestinationType = mFieldTypeInternal;

            attribs = fi.GetCustomAttributes(typeof(FieldNullValueAttribute), true);

            if (attribs.Length > 0)
            {
                mNullValue = ((FieldNullValueAttribute)attribs[0]).NullValue;
                //				mNullValueOnWrite = ((FieldNullValueAttribute) attribs[0]).NullValueOnWrite;

                if (mNullValue != null)
                {
                    if (!mFieldTypeInternal.IsAssignableFrom(mNullValue.GetType()))
                        throw new BadUsageException("The NullValue is of type: " + mNullValue.GetType().Name +
                                                    " that is not asignable to the field " + mFieldInfo.Name + " of type: " +
                                                    mFieldTypeInternal.Name);
                }
            }


#if NET_2_0
            mIsNullableType = mFieldTypeInternal.IsValueType &&
                                    mFieldTypeInternal.IsGenericType &&
                                    mFieldTypeInternal.GetGenericTypeDefinition() == typeof(Nullable<>);
#endif
        }

        #endregion


        private static char[] WhitespaceChars = new char[] 
			 { 
				 '\t', '\n', '\v', '\f', '\r', ' ', '\x00a0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', 
				 '\u2009', '\u200a', '\u200b', '\u3000', '\ufeff'
			 };


        #region "  MustOverride (String Handling)  "

        internal abstract ExtractedInfo ExtractFieldString(LineInfo line);

        internal abstract void CreateFieldString(StringBuilder sb, object fieldValue);

        internal string CreateFieldString(object fieldValue)
        {
            if (mConvertProvider == null)
            {
                if (fieldValue == null)
                    return string.Empty;
                else
                    return fieldValue.ToString();

            }
            else
            {
                return mConvertProvider.FieldToString(fieldValue);
            }
        }

        internal int mCharsToDiscard = 0;

        #endregion

        internal ConverterBase mConvertProvider;

        #region "  ExtractValue  "

        internal object ExtractFieldValue(LineInfo line)
        {
            //-> extract only what I need

            if (mInNewLine)
            {
                if (line.EmptyFromPos() == false)
                    throw new BadUsageException(line, "Text '" + line.CurrentString +
                                                "' found before the new line of the field: " + mFieldInfo.Name +
                                                " (this is not allowed when you use [FieldInNewLine])");

                line.ReLoad(line.mReader.ReadNextLine());

                if (line.mLineStr == null)
                    throw new BadUsageException(line, "End of stream found parsing the field " + mFieldInfo.Name +
                                                ". Please check the class record.");
            }

            if (mIsArray == false)
            {
                ExtractedInfo info = ExtractFieldString(line);
                if (info.mCustomExtractedString == null)
                    line.mCurrentPos = info.ExtractedTo + 1;

                line.mCurrentPos += mCharsToDiscard; //total;

                return AssignFromString(info, line);
            }
            else
            {
                if (mArrayMinLength <= 0)
                    mArrayMinLength = 0;

                int i = 0;

                ArrayList res = new ArrayList(Math.Max(mArrayMinLength, 10));

                while (line.mCurrentPos - mCharsToDiscard < line.mLine.Length && i < mArrayMaxLength)
                {
                    ExtractedInfo info = ExtractFieldString(line);
                    if (info.mCustomExtractedString == null)
                        line.mCurrentPos = info.ExtractedTo + 1;

                    line.mCurrentPos += mCharsToDiscard;

                    res.Add(AssignFromString(info, line));
                    i++;
                }

                if (res.Count < mArrayMinLength)
                    throw new InvalidOperationException(string.Format("Line: {0} Column: {1} Field: {2}. The array has only {3} values, less than the minimum length of {4}", line.mReader.LineNumber.ToString(), line.mCurrentPos.ToString(), mFieldInfo.Name, res.Count, mArrayMinLength));

                else if (mIsLast && line.IsEOL() == false)
                    throw new InvalidOperationException(string.Format("Line: {0} Column: {1} Field: {2}. The array has more values than the maximum length of {3}", line.mReader.LineNumber, line.mCurrentPos, mFieldInfo.Name, mArrayMaxLength));

                return res.ToArray(mArrayType);

            }

        }

        #region "  AssignFromString  "

        internal object AssignFromString(ExtractedInfo fieldString, LineInfo line)
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

            try
            {

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
                            val = GetNullValue(line);
                        }
                        else
                        {
                            val = Convert.ChangeType(fieldString.ExtractedString(), mFieldTypeInternal, null);
                        }
                    }
                }
                else
                {
                    if (mConvertProvider.CustomNullHandling == false &&
                        fieldString.HasOnlyThisChars(WhitespaceChars))
                    {
                        val = GetNullValue(line);
                    }
                    else
                    {
                        string from = fieldString.ExtractedString();
                        val = mConvertProvider.StringToField(from);

                        if (val == null)
                            val = GetNullValue(line);

                    }
                }

                return val;
            }
            catch (ConvertException ex)
            {
                throw ConvertException.ReThrowException(ex, mFieldInfo.Name, line.mReader.LineNumber, fieldString.ExtractedFrom + 1);
            }
            catch (BadUsageException)
            {
                throw;
            }
            catch (Exception ex)
            {
                if (mConvertProvider == null || mConvertProvider.GetType().Assembly == typeof(FieldBase).Assembly)
                    throw new ConvertException(fieldString.ExtractedString(), mFieldTypeInternal, mFieldInfo.Name, line.mReader.LineNumber, fieldString.ExtractedFrom + 1, ex.Message, ex);
                else
                    throw new ConvertException(fieldString.ExtractedString(), mFieldTypeInternal, mFieldInfo.Name, line.mReader.LineNumber, fieldString.ExtractedFrom + 1, "Your custom converter: " + mConvertProvider.GetType().Name + " throws an " + ex.GetType().Name + " with the message: " + ex.Message, ex);
            }
        }

        private object GetNullValue(LineInfo line)
        {
            if (mNullValue == null)
            {
                if (mFieldTypeInternal.IsValueType)
                {

#if NET_2_0
                    if (mIsNullableType)
                        return null;
#endif

                    string msg = "Empty value found for the Field: '" + mFieldInfo.Name + "' Class: '" + mFieldInfo.DeclaringType.Name + "'. ";

#if NET_2_0
                    throw new BadUsageException(line, msg + "You must use the FieldNullValue attribute because this is a ValueType and can´t be null or you can use the Nullable Types feature of the .NET framework.");
#else
					throw new BadUsageException(line, msg + "You must use the FieldNullValue attribute because this is a ValueType and can´t be null.");
#endif

                }
                else
                    return null;
            }
            else
                return mNullValue;
        }

        #endregion

        #region "  CreateValueForField  "

        public object CreateValueForField(object fieldValue)
        {
            object val = null;

            if (fieldValue == null)
            {
                if (mNullValue == null)
                {
                    if (mFieldTypeInternal.IsValueType)
                        throw new BadUsageException("Null Value found. You must specify a NullValueAttribute in the " + mFieldInfo.Name +
                                                    " field of type " + mFieldTypeInternal.Name + ", because this is a ValueType.");
                    else
                        val = null;
                }
                else
                {
                    val = mNullValue;
                }
            }
            else if (mFieldTypeInternal == fieldValue.GetType())
                val = fieldValue;
            else
            {
                if (mConvertProvider == null)
                    val = Convert.ChangeType(fieldValue, mFieldTypeInternal, null);
                else
                {
                    try
                    {
                        val = Convert.ChangeType(fieldValue, mFieldTypeInternal, null);
                    }
                    catch
                    {
                        val = mConvertProvider.StringToField(fieldValue.ToString());
                    }
                }
            }

            return val;
        }

        #endregion


        #endregion

        #region "  AssignToString  "

        internal void AssignToString(StringBuilder sb, object fieldValue)
        {
            if (this.mInNewLine == true)
                sb.Append(StringHelper.NewLine);

            if (mIsArray)
            {
                if (fieldValue == null)
                    return;

                foreach (object val in (Array)fieldValue)
                {
                    CreateFieldString(sb, val);
                }
            }
            else
                CreateFieldString(sb, fieldValue);
        }

        #endregion



    }
}
