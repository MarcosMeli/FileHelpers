

using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace FileHelpers
{

    /// <summary>
    /// Base class for all Field Types. 
    /// Implements all the basic functionality of a field in a typed file.
    /// </summary>
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
                throw new BadUsageException("The field: '" + fi.Name + "' can't be marked with FieldAlign attribute, it is only valid for fixed length records and are used only for write purpouse.");

            if (fi.FieldType.IsArray == false && fi.IsDefined(typeof(FieldArrayLengthAttribute), false))
                throw new BadUsageException("The field: '" + fi.Name + "' can't be marked with FieldArrayLength attribute is only valid for array fields.");


            // PROCESS IN NORMAL CONDITIONS

            if (attributes.Length > 0)
            {
                fieldAttb = attributes[0];

                if (fieldAttb is FieldFixedLengthAttribute)
                {
                    // Fixed Field
                    if (recordAttribute is DelimitedRecordAttribute)
                        throw new BadUsageException("The field: '" + fi.Name + "' can't be marked with FieldFixedLength attribute, it is only for the FixedLengthRecords not for delimited ones.");

                    var attbFixedLength = (FieldFixedLengthAttribute)fieldAttb;
                    var attbAlign = Attributes.GetFirst<FieldAlignAttribute>(fi);

                    res = new FixedLengthField(fi, attbFixedLength.Length, attbAlign);
                    ((FixedLengthField)res).mFixedMode = ((FixedLengthRecordAttribute)recordAttribute).FixedMode;
                }
                else if (fieldAttb is FieldDelimiterAttribute)
                {
                    // Delimited Field
                    if (recordAttribute is FixedLengthRecordAttribute)
                        throw new BadUsageException("The field: '" + fi.Name + "' can't be marked with FieldDelimiter attribute, it is only for DelimitedRecords not for fixed ones.");

                    res = new DelimitedField(fi, ((FieldDelimiterAttribute)fieldAttb).Delimiter);

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

                // FieldTrim
                Attributes.WorkWithFirst<FieldTrimAttribute>(fi, (x) =>
                                                 {
                                                     res.mTrimMode = x.TrimMode;
                                                     res.mTrimChars = x.TrimChars;
                                                 });

                // FieldQuoted
                Attributes.WorkWithFirst<FieldQuotedAttribute>(fi, (x) =>
                                                                       {
                                                                           if (res is FixedLengthField)
                                                                               throw new BadUsageException(
                                                                                   "The field: '" + fi.Name +
                                                                                   "' can't be marked with FieldQuoted attribute, it is only for the delimited records.");

                                                                           ((DelimitedField) res).mQuoteChar =
                                                                               x.QuoteChar;
                                                                           ((DelimitedField) res).mQuoteMode =
                                                                               x.QuoteMode;
                                                                           ((DelimitedField) res).mQuoteMultiline =
                                                                               x.QuoteMultiline;
                                                                       });



                // FieldOrder
                Attributes.WorkWithFirst<FieldOrderAttribute>(fi, (x) =>
                {
                    res.mFieldOrder= x.Order;
                });


                // FieldOptional
                res.mIsOptional = fi.IsDefined(typeof(FieldOptionalAttribute), false);

                // FieldInNewLine
                res.mInNewLine = fi.IsDefined(typeof(FieldInNewLineAttribute), false);

                // FieldArrayLength
                if (fi.FieldType.IsArray)
                {
                    res.mIsArray = true;
                    res.mArrayType = fi.FieldType.GetElementType();

                    // MinValue indicates that there is no FieldArrayLength in the array
                    res.mArrayMinLength = int.MinValue;
                    res.mArrayMaxLength = int.MaxValue;

                    Attributes.WorkWithFirst<FieldArrayLengthAttribute>(fi, (x) =>
                    {
                        res.mArrayMinLength = x.mMinLength;
                        res.mArrayMaxLength = x.mMaxLength;

                        if (res.mArrayMaxLength < res.mArrayMinLength ||
                            res.mArrayMinLength < 0 ||
                            res.mArrayMaxLength <= 0)
                            throw new BadUsageException("The field: " + fi.Name + " has invalid length values in the [FieldArrayLength] attribute.");
                    });
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

        internal int? mFieldOrder = null;

        internal bool mIsFirst = false;
        internal bool mIsLast = false;
        internal bool mTrailingArray = false;

        internal bool mIsArray = false;
        internal Type mArrayType;
        internal int mArrayMinLength;
        internal int mArrayMaxLength;

        internal object mNullValue = null;
        //internal bool mNullValueOnWrite = false;


        private bool mIsNullableType = false;
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


            mIsNullableType = mFieldTypeInternal.IsValueType &&
                                    mFieldTypeInternal.IsGenericType &&
                                    mFieldTypeInternal.GetGenericTypeDefinition() == typeof(Nullable<>);
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

                    if (mIsNullableType)
                        return null;

                    string msg = "Not value found for the value type field: '" + mFieldInfo.Name + "' Class: '" +
                                 mFieldInfo.DeclaringType.Name + "'. " + Environment.NewLine
                                 +
                                 "You must use the [FieldNullValue] attribute because this is a value type and can´t be null or use a Nullable Type instead of the current type.";

                    throw new BadUsageException(line, msg);

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
