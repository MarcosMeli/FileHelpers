

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
        : ICloneable
    {

        #region "  Private & Internal Fields  "

        internal Type FieldType { get; private set; }

        internal int CharsToDiscard { get; set; }
        internal ConverterBase ConvertProvider { get; private set; }
        internal Type FieldTypeInternal { get; set; }
        internal bool IsArray { get; set; }
        internal Type ArrayType { get; set; }
        internal int ArrayMinLength { get; set; }
        internal int ArrayMaxLength { get; set; }
        internal bool IsFirst { get; set; }
        internal bool IsLast { get; set; }
        internal bool TrailingArray { get; set; }
        internal object NullValue { get; set; }
        internal bool IsStringField { get; set; }
        internal FieldInfo FieldInfo { get; set; }
        internal TrimMode TrimMode { get; set; }
        internal char[] TrimChars { get; set; }
        internal bool IsOptional { get; set; }
        internal bool NextIsOptional { get; set; }
        internal bool InNewLine { get; set; }
        internal int? FieldOrder { get; set; }
        internal bool IsNullableType { get; private set; }

        internal string FieldName
        {
            get { return FieldInfo.Name; }
        }

        // For performance add it here
        private static readonly char[] mWhitespaceChars = new[] 
			 { 
				 '\t', '\n', '\v', '\f', '\r', ' ', '\x00a0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005', '\u2006', '\u2007', '\u2008', 
				 '\u2009', '\u200a', '\u200b', '\u3000', '\ufeff'
			 };

        #endregion


        #region "  CreateField  "

        public static FieldBase CreateField(FieldInfo fi, TypedRecordAttribute recordAttribute)
        {
            // If ignored, return null
            if (fi.IsDefined(typeof(FieldIgnoredAttribute), true))
                return null;

            FieldBase res = null;

            var attributes = (FieldAttribute[])fi.GetCustomAttributes(typeof(FieldAttribute), true);

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
                FieldAttribute fieldAttb = attributes[0];

                if (fieldAttb is FieldFixedLengthAttribute)
                {
                    // Fixed Field
                    if (recordAttribute is DelimitedRecordAttribute)
                        throw new BadUsageException("The field: '" + fi.Name + "' can't be marked with FieldFixedLength attribute, it is only for the FixedLengthRecords not for delimited ones.");

                    var attbFixedLength = (FieldFixedLengthAttribute)fieldAttb;
                    var attbAlign = Attributes.GetFirst<FieldAlignAttribute>(fi);

                    res = new FixedLengthField(fi, attbFixedLength.Length, attbAlign);
                    ((FixedLengthField)res).FixedMode = ((FixedLengthRecordAttribute)recordAttribute).FixedMode;
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
                                                     res.TrimMode = x.TrimMode;
                                                     res.TrimChars = x.TrimChars;
                                                 });

                // FieldQuoted
                Attributes.WorkWithFirst<FieldQuotedAttribute>(fi, (x) =>
                                                                       {
                                                                           if (res is FixedLengthField)
                                                                               throw new BadUsageException(
                                                                                   "The field: '" + fi.Name +
                                                                                   "' can't be marked with FieldQuoted attribute, it is only for the delimited records.");

                                                                           ((DelimitedField)res).QuoteChar =
                                                                               x.QuoteChar;
                                                                           ((DelimitedField)res).QuoteMode =
                                                                               x.QuoteMode;
                                                                           ((DelimitedField)res).QuoteMultiline =
                                                                               x.QuoteMultiline;
                                                                       });



                // FieldOrder
                Attributes.WorkWithFirst<FieldOrderAttribute>(fi, (x) =>
                {
                    res.FieldOrder = x.Order;
                });


                // FieldOptional
                res.IsOptional = fi.IsDefined(typeof(FieldOptionalAttribute), false);

                // FieldInNewLine
                res.InNewLine = fi.IsDefined(typeof(FieldInNewLineAttribute), false);

                // FieldArrayLength
                if (fi.FieldType.IsArray)
                {
                    res.IsArray = true;
                    res.ArrayType = fi.FieldType.GetElementType();

                    // MinValue indicates that there is no FieldArrayLength in the array
                    res.ArrayMinLength = int.MinValue;
                    res.ArrayMaxLength = int.MaxValue;

                    Attributes.WorkWithFirst<FieldArrayLengthAttribute>(fi, (x) =>
                    {
                        res.ArrayMinLength = x.mMinLength;
                        res.ArrayMaxLength = x.mMaxLength;

                        if (res.ArrayMaxLength < res.ArrayMinLength ||
                            res.ArrayMinLength < 0 ||
                            res.ArrayMaxLength <= 0)
                            throw new BadUsageException("The field: " + fi.Name + " has invalid length values in the [FieldArrayLength] attribute.");
                    });
                }

            }

            return res;
        }


        #endregion


        #region "  Constructor  "

        internal FieldBase()
        {
          
        }

        internal FieldBase(FieldInfo fi)
        {
            IsNullableType = false;
            TrimMode = TrimMode.None;
            FieldOrder = null;
            InNewLine = false;
            NextIsOptional = false;
            IsOptional = false;
            TrimChars = null;
            NullValue = null;
            TrailingArray = false;
            IsLast = false;
            IsFirst = false;
            IsArray = false;
            CharsToDiscard = 0;
            FieldInfo = fi;
            FieldType = FieldInfo.FieldType;

            if (FieldType.IsArray)
                FieldTypeInternal = FieldType.GetElementType();
            else
                FieldTypeInternal = FieldType;

            IsStringField = FieldTypeInternal == typeof(string);

            object[] attribs = fi.GetCustomAttributes(typeof(FieldConverterAttribute), true);

            if (attribs.Length > 0)
            {
                FieldConverterAttribute conv = (FieldConverterAttribute)attribs[0];
                ConvertProvider = conv.Converter;
                conv.ValidateTypes(FieldInfo);
            }
            else
                ConvertProvider = ConvertHelpers.GetDefaultConverter(fi.Name, FieldType);

            if (ConvertProvider != null)
                ConvertProvider.mDestinationType = FieldTypeInternal;

            attribs = fi.GetCustomAttributes(typeof(FieldNullValueAttribute), true);

            if (attribs.Length > 0)
            {
                NullValue = ((FieldNullValueAttribute)attribs[0]).NullValue;
                //				mNullValueOnWrite = ((FieldNullValueAttribute) attribs[0]).NullValueOnWrite;

                if (NullValue != null)
                {
                    if (!FieldTypeInternal.IsAssignableFrom(NullValue.GetType()))
                        throw new BadUsageException("The NullValue is of type: " + NullValue.GetType().Name +
                                                    " that is not asignable to the field " + FieldInfo.Name + " of type: " +
                                                    FieldTypeInternal.Name);
                }
            }


            IsNullableType = FieldTypeInternal.IsValueType &&
                                    FieldTypeInternal.IsGenericType &&
                                    FieldTypeInternal.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        #endregion




        #region "  MustOverride (String Handling)  "

        internal abstract ExtractedInfo ExtractFieldString(LineInfo line);

        internal abstract void CreateFieldString(StringBuilder sb, object fieldValue);

        internal string CreateFieldString(object fieldValue)
        {
            if (ConvertProvider == null)
            {
                if (fieldValue == null)
                    return string.Empty;
                else
                    return fieldValue.ToString();

            }
            else
            {
                return ConvertProvider.FieldToString(fieldValue);
            }
        }

        #endregion

        #region "  ExtractValue  "

        internal object ExtractFieldValue(LineInfo line)
        {
            //-> extract only what I need

            if (InNewLine)
            {
                if (line.EmptyFromPos() == false)
                    throw new BadUsageException(line, "Text '" + line.CurrentString +
                                                "' found before the new line of the field: " + FieldInfo.Name +
                                                " (this is not allowed when you use [FieldInNewLine])");

                line.ReLoad(line.mReader.ReadNextLine());

                if (line.mLineStr == null)
                    throw new BadUsageException(line, "End of stream found parsing the field " + FieldInfo.Name +
                                                ". Please check the class record.");
            }

            if (IsArray == false)
            {
                ExtractedInfo info = ExtractFieldString(line);
                if (info.mCustomExtractedString == null)
                    line.mCurrentPos = info.ExtractedTo + 1;

                line.mCurrentPos += CharsToDiscard; //total;

                return AssignFromString(info, line);
            }
            else
            {
                if (ArrayMinLength <= 0)
                    ArrayMinLength = 0;

                int i = 0;

                var res = new ArrayList(Math.Max(ArrayMinLength, 10));

                while (line.mCurrentPos - CharsToDiscard < line.mLine.Length && i < ArrayMaxLength)
                {
                    ExtractedInfo info = ExtractFieldString(line);
                    if (info.mCustomExtractedString == null)
                        line.mCurrentPos = info.ExtractedTo + 1;

                    line.mCurrentPos += CharsToDiscard;

                    res.Add(AssignFromString(info, line));
                    i++;
                }

                if (res.Count < ArrayMinLength)
                    throw new InvalidOperationException(string.Format("Line: {0} Column: {1} Field: {2}. The array has only {3} values, less than the minimum length of {4}", line.mReader.LineNumber.ToString(), line.mCurrentPos.ToString(), FieldInfo.Name, res.Count, ArrayMinLength));

                else if (IsLast && line.IsEOL() == false)
                    throw new InvalidOperationException(string.Format("Line: {0} Column: {1} Field: {2}. The array has more values than the maximum length of {3}", line.mReader.LineNumber, line.mCurrentPos, FieldInfo.Name, ArrayMaxLength));

                return res.ToArray(ArrayType);

            }

        }

        #region "  AssignFromString  "

        internal object AssignFromString(ExtractedInfo fieldString, LineInfo line)
        {
            object val;

            var extractedString = fieldString.ExtractedString();
            var trimmedBoth = false;
            switch (TrimMode)
            {
                case TrimMode.None:
                    break;

                case TrimMode.Both:
                    extractedString = extractedString.Trim();
                    trimmedBoth = true;
                    //fieldString.TrimBoth(TrimChars);
                    break;

                case TrimMode.Left:
                    extractedString = extractedString.TrimStart();
                    //fieldString.TrimStart(TrimChars);
                    break;

                case TrimMode.Right:
                    extractedString = extractedString.TrimEnd();
                    //fieldString.TrimEnd(TrimChars);
                    break;
            }

            
            try
            {
                if (ConvertProvider == null)
                {
                    if (IsStringField)
                        val = extractedString;
                    else
                    {
                        // Trim it to use Convert.ChangeType
                        if (trimmedBoth == false)
                            extractedString = extractedString.Trim();

                        if (extractedString.Length == 0)
                        {
                            // Empty stand for null
                            val = GetNullValue(line);
                        }
                        else
                        {
                            val = Convert.ChangeType(extractedString, FieldTypeInternal, null);
                        }
                    }
                }
                else
                {
                    var trimmedString = extractedString;
                    if (trimmedBoth == false)
                    {
                        trimmedString = extractedString.Trim();
                    }

                    if (ConvertProvider.CustomNullHandling == false &&
                        trimmedString.Length == 0)
                    {
                        val = GetNullValue(line);
                    }
                    else
                    {
                        string from = extractedString;
                        val = ConvertProvider.StringToField(from);

                        if (val == null)
                            val = GetNullValue(line);

                    }
                }

                return val;
            }
            catch (ConvertException ex)
            {
                throw ConvertException.ReThrowException(ex, FieldInfo.Name, line.mReader.LineNumber, fieldString.ExtractedFrom + 1);
            }
            catch (BadUsageException)
            {
                throw;
            }
            catch (Exception ex)
            {
                if (ConvertProvider == null || ConvertProvider.GetType().Assembly == typeof(FieldBase).Assembly)
                    throw new ConvertException(extractedString, FieldTypeInternal, FieldInfo.Name, line.mReader.LineNumber, fieldString.ExtractedFrom + 1, ex.Message, ex);
                else
                    throw new ConvertException(extractedString, FieldTypeInternal, FieldInfo.Name, line.mReader.LineNumber, fieldString.ExtractedFrom + 1, "Your custom converter: " + ConvertProvider.GetType().Name + " throws an " + ex.GetType().Name + " with the message: " + ex.Message, ex);
            }
        }

        private object GetNullValue(LineInfo line)
        {
            if (NullValue == null)
            {
                if (FieldTypeInternal.IsValueType)
                {

                    if (IsNullableType)
                        return null;

                    string msg = "Not value found for the value type field: '" + FieldInfo.Name + "' Class: '" +
                                 FieldInfo.DeclaringType.Name + "'. " + Environment.NewLine
                                 +
                                 "You must use the [FieldNullValue] attribute because this is a value type and can´t be null or use a Nullable Type instead of the current type.";

                    throw new BadUsageException(line, msg);

                }
                else
                    return null;
            }
            else
                return NullValue;
        }

        #endregion

        #region "  CreateValueForField  "

        public object CreateValueForField(object fieldValue)
        {
            object val = null;

            if (fieldValue == null)
            {
                if (NullValue == null)
                {
                    if (FieldTypeInternal.IsValueType)
                        throw new BadUsageException("Null Value found. You must specify a NullValueAttribute in the " + FieldInfo.Name +
                                                    " field of type " + FieldTypeInternal.Name + ", because this is a ValueType.");
                    else
                        val = null;
                }
                else
                {
                    val = NullValue;
                }
            }
            else if (FieldTypeInternal == fieldValue.GetType())
                val = fieldValue;
            else
            {
                if (ConvertProvider == null)
                    val = Convert.ChangeType(fieldValue, FieldTypeInternal, null);
                else
                {
                    try
                    {
                        val = Convert.ChangeType(fieldValue, FieldTypeInternal, null);
                    }
                    catch
                    {
                        val = ConvertProvider.StringToField(fieldValue.ToString());
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
            if (this.InNewLine == true)
                sb.Append(StringHelper.NewLine);

            if (IsArray)
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

        public object Clone()
        {
            var res = CreateClone();

            res.FieldType = FieldType;
            res.CharsToDiscard = CharsToDiscard;
            res.ConvertProvider = ConvertProvider;
            res.FieldTypeInternal = FieldTypeInternal;
            res.IsArray = IsArray;
            res.ArrayType = ArrayType;
            res.ArrayMinLength = ArrayMinLength;
            res.ArrayMaxLength = ArrayMaxLength;
            res.IsFirst = IsFirst;
            res.IsLast = IsLast;
            res.TrailingArray = TrailingArray;
            res.NullValue = NullValue;
            res.IsStringField = IsStringField;
            res.FieldInfo = FieldInfo;
            res.TrimMode = TrimMode;
            res.TrimChars = TrimChars;
            res.IsOptional = IsOptional;
            res.NextIsOptional = NextIsOptional;
            res.InNewLine = InNewLine;
            res.FieldOrder = FieldOrder;
            res.IsNullableType = IsNullableType;

            return res;
        }

        protected abstract FieldBase CreateClone();
    }
}
