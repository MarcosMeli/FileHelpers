using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace FileHelpers
{
    /// <summary>
    /// Base class for all Field Types.
    /// Implements all the basic functionality of a field in a typed file.
    /// </summary>
    public abstract class FieldBase
        : ICloneable
    {
        #region "  Private & Internal Fields  "

        // --------------------------------------------------------------
        // WARNING !!!
        //    Remember to add each of these fields to the clone method !!
        // --------------------------------------------------------------

        /// <summary>
        /// type of object to be created,  eg DateTime
        /// </summary>
        public Type FieldType { get; private set; }

        /// <summary>
        /// Provider to convert to and from text
        /// </summary>
        public ConverterBase Converter { get; private set; }

        /// <summary>
        /// Number of extra characters used,  delimiters and quote characters
        /// </summary>
        internal int CharsToDiscard { get; set; }

        /// <summary>
        /// Field type of an array or it is just fieldType.
        /// What actual object will be created
        /// </summary>
        internal Type FieldTypeInternal { get; set; }

        /// <summary>
        /// Is this field an array?
        /// </summary>
        public bool IsArray { get; private set; }

        /// <summary>
        /// Array must have this many entries
        /// </summary>
        public int ArrayMinLength { get; set; }

        /// <summary>
        /// Array may have this many entries,  if equal to ArrayMinLength then
        /// it is a fixed length array
        /// </summary>
        public int ArrayMaxLength { get; set; }

        /// <summary>
        /// Seems to be duplicate of FieldTypeInternal except it is ONLY set
        /// for an array
        /// </summary>
        internal Type ArrayType { get; set; }


        /// <summary>
        /// Am I the first field in an array list
        /// </summary>
        internal bool IsFirst { get; set; }

        /// <summary>
        /// Am I the last field in the array list
        /// </summary>
        internal bool IsLast { get; set; }

        /// <summary>
        /// Do we process this field but not store the value
        /// </summary>
        public bool Discarded { get; set; }

        /// <summary>
        /// Unused!
        /// </summary>
        internal bool TrailingArray { get; set; }

        /// <summary>
        /// Value to use if input is null or empty
        /// </summary>
        internal object NullValue { get; set; }

        /// <summary>
        /// Are we a simple string field we can just assign to
        /// </summary>
        internal bool IsStringField { get; set; }

        /// <summary>
        /// Details about the extraction criteria
        /// </summary>
        internal FieldInfo FieldInfo { get; set; }

        /// <summary>
        /// indicates whether we trim leading and/or trailing whitespace
        /// </summary>
        public TrimMode TrimMode { get; set; }

        /// <summary>
        /// Character to chop off front and / rear of the string
        /// </summary>
        internal char[] TrimChars { get; set; }

        /// <summary>
        /// The field may not be present on the input data (line not long enough)
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// The next field along is optional,  optimise processing next records
        /// </summary>
        internal bool NextIsOptional { get; set; }

        /// <summary>
        /// Set from the FieldInNewLIneAtribute.  This field begins on a new
        /// line of the file
        /// </summary>
        internal bool InNewLine { get; set; }

        /// <summary>
        /// Order of the field in the file layout
        /// </summary>
        internal int? FieldOrder { get; set; }

        /// <summary>
        /// Can null be assigned to this value type, for example not int or
        /// DateTime
        /// </summary>
        internal bool IsNullableType { get; private set; }

        /// <summary>
        /// Name of the field without extra characters (eg property)
        /// </summary>
        internal string FieldFriendlyName { get; set; }

        /// <summary>
        /// The field must be not be empty
        /// </summary>
        public bool IsNotEmpty { get; set; }

        // --------------------------------------------------------------
        // WARNING !!!
        //    Remember to add each of these fields to the clone method !!
        // --------------------------------------------------------------

        /// <summary>
        /// Fieldname of the field we are storing
        /// </summary>
        internal string FieldName
        {
            get { return FieldInfo.Name; }
        }

        // For performance add it here
        /// <summary>
        /// List the various whitespace characters in Unicode
        /// </summary>
        private static readonly char[] mWhitespaceChars = new[] {
            '\t', '\n', '\v', '\f', '\r', ' ', '\x00a0', '\u2000', '\u2001', '\u2002', '\u2003', '\u2004', '\u2005',
            '\u2006', '\u2007', '\u2008',
            '\u2009', '\u200a', '\u200b', '\u3000', '\ufeff'
        };

        #endregion

        #region "  CreateField  "

        /// <summary>
        /// Check the Attributes on the field and return a structure containing
        /// the settings for this file.
        /// </summary>
        /// <param name="fi">Information about this field</param>
        /// <param name="recordAttribute">Type of record we are reading</param>
        /// <returns>Null if not used</returns>
        public static FieldBase CreateField(FieldInfo fi, TypedRecordAttribute recordAttribute)
        {
            // If ignored, return null
#pragma warning disable 612,618 // disable obsole warning
            if (fi.IsDefined(typeof (FieldNotInFileAttribute), true) ||
                fi.IsDefined(typeof (FieldIgnoredAttribute), true) ||
                fi.IsDefined(typeof (FieldHiddenAttribute), true))
#pragma warning restore 612,618
                return null;

            FieldBase res = null;

            var attributes = (FieldAttribute[]) fi.GetCustomAttributes(typeof (FieldAttribute), true);

            // CHECK USAGE ERRORS !!!

            // Fixed length record and no attributes at all
            if (recordAttribute is FixedLengthRecordAttribute &&
                attributes.Length == 0) {
                throw new BadUsageException("The field: '" + fi.Name +
                                            "' must be marked the FieldFixedLength attribute because the record class is marked with FixedLengthRecord.");
            }

            if (attributes.Length > 1) {
                throw new BadUsageException("The field: '" + fi.Name +
                                            "' has a FieldFixedLength and a FieldDelimiter attribute.");
            }

            if (recordAttribute is DelimitedRecordAttribute &&
                fi.IsDefined(typeof (FieldAlignAttribute), false)) {
                throw new BadUsageException("The field: '" + fi.Name +
                                            "' can't be marked with FieldAlign attribute, it is only valid for fixed length records and are used only for write purpose.");
            }

            if (fi.FieldType.IsArray == false &&
                fi.IsDefined(typeof (FieldArrayLengthAttribute), false)) {
                throw new BadUsageException("The field: '" + fi.Name +
                                            "' can't be marked with FieldArrayLength attribute is only valid for array fields.");
            }

            // PROCESS IN NORMAL CONDITIONS
            if (attributes.Length > 0) {
                FieldAttribute fieldAttb = attributes[0];

                if (fieldAttb is FieldFixedLengthAttribute) {
                    // Fixed Field
                    if (recordAttribute is DelimitedRecordAttribute) {
                        throw new BadUsageException("The field: '" + fi.Name +
                                                    "' can't be marked with FieldFixedLength attribute, it is only for the FixedLengthRecords not for delimited ones.");
                    }

                    var attbFixedLength = (FieldFixedLengthAttribute) fieldAttb;
                    var attbAlign = Attributes.GetFirst<FieldAlignAttribute>(fi);

                    res = new FixedLengthField(fi, attbFixedLength.Length, attbAlign);
                    ((FixedLengthField) res).FixedMode = ((FixedLengthRecordAttribute) recordAttribute).FixedMode;
                }
                else if (fieldAttb is FieldDelimiterAttribute) {
                    // Delimited Field
                    if (recordAttribute is FixedLengthRecordAttribute) {
                        throw new BadUsageException("The field: '" + fi.Name +
                                                    "' can't be marked with FieldDelimiter attribute, it is only for DelimitedRecords not for fixed ones.");
                    }

                    res = new DelimitedField(fi, ((FieldDelimiterAttribute) fieldAttb).Delimiter);
                }
                else {
                    throw new BadUsageException(
                        "Custom field attributes are not currently supported. Unknown attribute: " +
                        fieldAttb.GetType().Name + " on field: " + fi.Name);
                }
            }
            else // attributes.Length == 0
            {
                var delimitedRecordAttribute = recordAttribute as DelimitedRecordAttribute;

                if (delimitedRecordAttribute != null)
                    res = new DelimitedField(fi, delimitedRecordAttribute.Separator);
            }

            if (res != null) {
                // FieldDiscarded
                res.Discarded = fi.IsDefined(typeof (FieldValueDiscardedAttribute), false);

                // FieldTrim
                Attributes.WorkWithFirst<FieldTrimAttribute>(fi,
                    (x) => {
                        res.TrimMode = x.TrimMode;
                        res.TrimChars = x.TrimChars;
                    });

                // FieldQuoted
                Attributes.WorkWithFirst<FieldQuotedAttribute>(fi,
                    (x) => {
                        if (res is FixedLengthField) {
                            throw new BadUsageException(
                                "The field: '" + fi.Name +
                                "' can't be marked with FieldQuoted attribute, it is only for the delimited records.");
                        }

                        ((DelimitedField) res).QuoteChar =
                            x.QuoteChar;
                        ((DelimitedField) res).QuoteMode =
                            x.QuoteMode;
                        ((DelimitedField) res).QuoteMultiline =
                            x.QuoteMultiline;
                    });

                // FieldOrder
                Attributes.WorkWithFirst<FieldOrderAttribute>(fi, x => res.FieldOrder = x.Order);

                // FieldOptional
                res.IsOptional = fi.IsDefined(typeof(FieldOptionalAttribute), false);

                // FieldInNewLine
                res.InNewLine = fi.IsDefined(typeof(FieldInNewLineAttribute), false);

                // FieldNotEmpty
                res.IsNotEmpty = fi.IsDefined(typeof(FieldNotEmptyAttribute), false);

                // FieldArrayLength
                if (fi.FieldType.IsArray) {
                    res.IsArray = true;
                    res.ArrayType = fi.FieldType.GetElementType();

                    // MinValue indicates that there is no FieldArrayLength in the array
                    res.ArrayMinLength = int.MinValue;
                    res.ArrayMaxLength = int.MaxValue;

                    Attributes.WorkWithFirst<FieldArrayLengthAttribute>(fi,
                        (x) => {
                            res.ArrayMinLength = x.MinLength;
                            res.ArrayMaxLength = x.MaxLength;

                            if (res.ArrayMaxLength < res.ArrayMinLength ||
                                res.ArrayMinLength < 0 ||
                                res.ArrayMaxLength <= 0) {
                                throw new BadUsageException("The field: " + fi.Name +
                                                            " has invalid length values in the [FieldArrayLength] attribute.");
                            }
                        });
                }
            }

            if (fi.IsDefined(typeof (CompilerGeneratedAttribute), false))
            {
                if (fi.Name.EndsWith("__BackingField") &&
                    fi.Name.StartsWith("<") &&
                    fi.Name.Contains(">"))

                res.FieldFriendlyName = fi.Name.Substring(1, fi.Name.IndexOf(">") - 1);
                res.IsAutoProperty = true;

                var prop = fi.DeclaringType.GetProperty(res.FieldFriendlyName);
                if (prop != null)
                {
                    Attributes.WorkWithFirst<FieldOrderAttribute>(prop, x => res.FieldOrder = x.Order);
                }
            }

            if (string.IsNullOrEmpty(res.FieldFriendlyName))
                res.FieldFriendlyName = res.FieldName;

            return res;
        }

        internal static string AutoPropertyName(FieldInfo fi)
        {
            if (fi.IsDefined(typeof(CompilerGeneratedAttribute), false))
            {
                if (fi.Name.EndsWith("__BackingField") &&
                    fi.Name.StartsWith("<") &&
                    fi.Name.Contains(">"))
                    return fi.Name.Substring(1, fi.Name.IndexOf(">") - 1);
                
            }
            return "";
        }

        internal bool IsAutoProperty { get; set; }

        #endregion

        #region "  Constructor  "

        /// <summary>
        /// Create a field base without any configuration
        /// </summary>
        internal FieldBase()
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
            IsNotEmpty = false;
        }

        /// <summary>
        /// Create a field base from a fieldinfo object
        /// Verify the settings against the actual field to ensure it will work.
        /// </summary>
        /// <param name="fi">Field Info Object</param>
        internal FieldBase(FieldInfo fi)
            : this()
        {
         

            FieldInfo = fi;
            FieldType = FieldInfo.FieldType;

            if (FieldType.IsArray)
                FieldTypeInternal = FieldType.GetElementType();
            else
                FieldTypeInternal = FieldType;

            IsStringField = FieldTypeInternal == typeof (string);

            object[] attribs = fi.GetCustomAttributes(typeof (FieldConverterAttribute), true);

            if (attribs.Length > 0) {
                var conv = (FieldConverterAttribute) attribs[0];
                this.Converter = conv.Converter;
                conv.ValidateTypes(FieldInfo);
            }
            else
                this.Converter = ConvertHelpers.GetDefaultConverter(fi.Name, FieldType);

            if (this.Converter != null)
                this.Converter.mDestinationType = FieldTypeInternal;

            attribs = fi.GetCustomAttributes(typeof (FieldNullValueAttribute), true);

            if (attribs.Length > 0) {
                NullValue = ((FieldNullValueAttribute) attribs[0]).NullValue;
                //				mNullValueOnWrite = ((FieldNullValueAttribute) attribs[0]).NullValueOnWrite;

                if (NullValue != null) {
                    if (!FieldTypeInternal.IsAssignableFrom(NullValue.GetType())) {
                        throw new BadUsageException("The NullValue is of type: " + NullValue.GetType().Name +
                                                    " that is not asignable to the field " + FieldInfo.Name +
                                                    " of type: " +
                                                    FieldTypeInternal.Name);
                    }
                }
            }

            IsNullableType = FieldTypeInternal.IsValueType &&
                             FieldTypeInternal.IsGenericType &&
                             FieldTypeInternal.GetGenericTypeDefinition() == typeof (Nullable<>);
        }

        #endregion

        #region "  MustOverride (String Handling)  "

        /// <summary>
        /// Extract the string from the underlying data, removes quotes
        /// characters for example
        /// </summary>
        /// <param name="line">Line to parse data from</param>
        /// <returns>Slightly processed string from the data</returns>
        internal abstract ExtractedInfo ExtractFieldString(LineInfo line);

        /// <summary>
        /// Create a text block containing the field from definition
        /// </summary>
        /// <param name="sb">Append string to output</param>
        /// <param name="fieldValue">Field we are adding</param>
        /// <param name="isLast">Indicates if we are processing last field</param>
        internal abstract void CreateFieldString(StringBuilder sb, object fieldValue, bool isLast);

        /// <summary>
        /// Convert a field value to a string representation
        /// </summary>
        /// <param name="fieldValue">Object containing data</param>
        /// <returns>String representation of field</returns>
        internal string CreateFieldString(object fieldValue)
        {
            if (this.Converter == null) {
                if (fieldValue == null)
                    return string.Empty;
                else
                    return fieldValue.ToString();
            }
            else
                return this.Converter.FieldToString(fieldValue);
        }

        #endregion

        #region "  ExtractValue  "

        /// <summary>
        /// Get the data out of the records
        /// </summary>
        /// <param name="line">Line handler containing text</param>
        /// <returns></returns>
        internal object ExtractFieldValue(LineInfo line)
        {
            //-> extract only what I need

            if (InNewLine) {
                // Any trailing characters, terminate
                if (line.EmptyFromPos() == false) {
                    throw new BadUsageException(line,
                        "Text '" + line.CurrentString +
                        "' found before the new line of the field: " + FieldInfo.Name +
                        " (this is not allowed when you use [FieldInNewLine])");
                }

                line.ReLoad(line.mReader.ReadNextLine());

                if (line.mLineStr == null) {
                    throw new BadUsageException(line,
                        "End of stream found parsing the field " + FieldInfo.Name +
                        ". Please check the class record.");
                }
            }

            if (IsArray == false) {
                ExtractedInfo info = ExtractFieldString(line);
                if (info.mCustomExtractedString == null)
                    line.mCurrentPos = info.ExtractedTo + 1;

                line.mCurrentPos += CharsToDiscard; //total;

                if (Discarded)
                    return GetDiscardedNullValue();
                else
                    return AssignFromString(info, line).Value;
            }
            else {
                if (ArrayMinLength <= 0)
                    ArrayMinLength = 0;

                int i = 0;

                var res = new ArrayList(Math.Max(ArrayMinLength, 10));

                while (line.mCurrentPos - CharsToDiscard < line.mLineStr.Length &&
                       i < ArrayMaxLength) {
                    ExtractedInfo info = ExtractFieldString(line);
                    if (info.mCustomExtractedString == null)
                        line.mCurrentPos = info.ExtractedTo + 1;

                    line.mCurrentPos += CharsToDiscard;

                    try {
                        var value = AssignFromString(info, line);

                        if (value.NullValueUsed &&
                            i == 0 &&
                            line.IsEOL())
                            break;

                        res.Add(value.Value);
                    }
                    catch (NullValueNotFoundException) {
                        if (i == 0)
                            break;
                        else
                            throw;
                    }
                    i++;
                }

                if (res.Count < ArrayMinLength) {
                    throw new InvalidOperationException(
                        string.Format(
                            "Line: {0} Column: {1} Field: {2}. The array has only {3} values, less than the minimum length of {4}",
                            line.mReader.LineNumber.ToString(),
                            line.mCurrentPos.ToString(),
                            FieldInfo.Name,
                            res.Count,
                            ArrayMinLength));
                }
                else if (IsLast && line.IsEOL() == false) {
                    throw new InvalidOperationException(
                        string.Format(
                            "Line: {0} Column: {1} Field: {2}. The array has more values than the maximum length of {3}",
                            line.mReader.LineNumber,
                            line.mCurrentPos,
                            FieldInfo.Name,
                            ArrayMaxLength));
                }

                // TODO:   is there a reason we go through all the array processing then discard it
                if (Discarded)
                    return null;
                else
                    return res.ToArray(ArrayType);
            }
        }

        #region "  AssignFromString  "

        private struct AssignResult
        {
            public object Value;
            public bool NullValueUsed;
        }

        /// <summary>
        /// Create field object after extracting the string from the underlying
        /// input data
        /// </summary>
        /// <param name="fieldString">Information extracted?</param>
        /// <param name="line">Underlying input data</param>
        /// <returns>Object to assign to field</returns>
        private AssignResult AssignFromString(ExtractedInfo fieldString, LineInfo line)
        {
            object val;

            var extractedString = fieldString.ExtractedString();

            try {
                if (IsNotEmpty && String.IsNullOrEmpty(extractedString)) {
                    throw new InvalidOperationException("The value is empty and must be populated.");
                } else if (this.Converter == null) {
                    if (IsStringField)
                        val = TrimString(extractedString);
                    else {
                        extractedString = extractedString.Trim();

                        if (extractedString.Length == 0) {
                            return new AssignResult {
                                Value = GetNullValue(line),
                                NullValueUsed = true
                            };
                        }
                        else
                            val = Convert.ChangeType(extractedString, FieldTypeInternal, null);
                    }
                }
                else {
                    var trimmedString = extractedString.Trim();

                    if (this.Converter.CustomNullHandling == false &&
                        trimmedString.Length == 0) {
                        return new AssignResult {
                            Value = GetNullValue(line),
                            NullValueUsed = true
                        };
                    }
                    else {
                        if (TrimMode == TrimMode.Both)
                            val = this.Converter.StringToField(trimmedString);
                        else
                            val = this.Converter.StringToField(TrimString(extractedString));

                        if (val == null) {
                            return new AssignResult {
                                Value = GetNullValue(line),
                                NullValueUsed = true
                            };
                        }
                    }
                }

                return new AssignResult {
                    Value = val
                };
            }
            catch (ConvertException ex) {
                ex.FieldName = FieldInfo.Name;
                ex.LineNumber = line.mReader.LineNumber;
                ex.ColumnNumber = fieldString.ExtractedFrom + 1;
                throw;
            }
            catch (BadUsageException) {
                throw;
            }
            catch (Exception ex) {
                if (this.Converter == null ||
                    this.Converter.GetType().Assembly == typeof (FieldBase).Assembly) {
                    throw new ConvertException(extractedString,
                        FieldTypeInternal,
                        FieldInfo.Name,
                        line.mReader.LineNumber,
                        fieldString.ExtractedFrom + 1,
                        ex.Message,
                        ex);
                }
                else {
                    throw new ConvertException(extractedString,
                        FieldTypeInternal,
                        FieldInfo.Name,
                        line.mReader.LineNumber,
                        fieldString.ExtractedFrom + 1,
                        "Your custom converter: " + this.Converter.GetType().Name + " throws an " + ex.GetType().Name +
                        " with the message: " + ex.Message,
                        ex);
                }
            }
        }

        private String TrimString(string extractedString)
        {
            switch (TrimMode) {
                case TrimMode.None:
                    return extractedString;

                case TrimMode.Both:
                    return extractedString.Trim();

                case TrimMode.Left:
                    return extractedString.TrimStart();

                case TrimMode.Right:
                    return extractedString.TrimEnd();

                default:
                    throw new Exception("Trim mode invalid in FieldBase.TrimString -> " + TrimMode.ToString());
            }
        }

        /// <summary>
        /// Convert a null value into a representation,
        /// allows for a null value override
        /// </summary>
        /// <param name="line">input line to read, used for error messages</param>
        /// <returns>Null value for object</returns>
        private object GetNullValue(LineInfo line)
        {
            if (NullValue == null) {
                if (FieldTypeInternal.IsValueType) {
                    if (IsNullableType)
                        return null;

                    string msg = "Not value found for the value type field: '" + FieldInfo.Name + "' Class: '" +
                                 FieldInfo.DeclaringType.Name + "'. " + Environment.NewLine
                                 +
                                 "You must use the [FieldNullValue] attribute because this is a value type and can't be null or use a Nullable Type instead of the current type.";

                    throw new NullValueNotFoundException(line, msg);
                }
                else
                    return null;
            }
            else
                return NullValue;
        }

        /// <summary>
        /// Get the null value that represent a discarded value
        /// </summary>
        /// <returns>null value of discard?</returns>
        private object GetDiscardedNullValue()
        {
            if (NullValue == null) {
                if (FieldTypeInternal.IsValueType) {
                    if (IsNullableType)
                        return null;


                    string msg = "The field: '" + FieldInfo.Name + "' Class: '" +
                                 FieldInfo.DeclaringType.Name +
                                 "' is from a value type: " + FieldInfo.FieldType.Name +
                                 " and is discarded (null) you must provide a [FieldNullValue] attribute.";

                    throw new BadUsageException(msg);
                }
                else
                    return null;
            }
            else
                return NullValue;
        }

        #endregion

        #region "  CreateValueForField  "

        /// <summary>
        /// Convert a field value into a write able value
        /// </summary>
        /// <param name="fieldValue">object value to convert</param>
        /// <returns>converted value</returns>
        public object CreateValueForField(object fieldValue)
        {
            object val = null;

            if (fieldValue == null) {
                if (NullValue == null) {
                    if (FieldTypeInternal.IsValueType &&
                        Nullable.GetUnderlyingType(FieldTypeInternal) == null) {
                        throw new BadUsageException(
                            "Null Value found. You must specify a FieldNullValueAttribute in the " + FieldInfo.Name +
                            " field of type " + FieldTypeInternal.Name + ", because this is a ValueType.");
                    }
                    else
                        val = null;
                }
                else
                    val = NullValue;
            }
            else if (FieldTypeInternal == fieldValue.GetType())
                val = fieldValue;
            else {
                if (this.Converter == null)
                    val = Convert.ChangeType(fieldValue, FieldTypeInternal, null);
                else {
                    try {
                        if (Nullable.GetUnderlyingType(FieldTypeInternal) != null &&
                            Nullable.GetUnderlyingType(FieldTypeInternal) == fieldValue.GetType())
                            val = fieldValue;
                        else
                            val = Convert.ChangeType(fieldValue, FieldTypeInternal, null);
                    }
                    catch {
                        val = Converter.StringToField(fieldValue.ToString());
                    }
                }
            }

            return val;
        }

        #endregion

        #endregion

        #region "  AssignToString  "

        /// <summary>
        /// convert field to string value and assign to a string builder
        /// buffer for output
        /// </summary>
        /// <param name="sb">buffer to collect record</param>
        /// <param name="fieldValue">value to convert</param>
        internal void AssignToString(StringBuilder sb, object fieldValue)
        {
            if (this.InNewLine == true)
                sb.Append(StringHelper.NewLine);

            if (IsArray) {
                if (fieldValue == null) {
                    if (0 < this.ArrayMinLength) {
                        throw new InvalidOperationException(
                            string.Format("Field: {0}. The array is null, but the minimum length is {1}",
                                FieldInfo.Name,
                                ArrayMinLength));
                    }

                    return;
                }

                var array = (IList) fieldValue;

                if (array.Count < this.ArrayMinLength) {
                    throw new InvalidOperationException(
                        string.Format("Field: {0}. The array has {1} values, but the minimum length is {2}",
                            FieldInfo.Name,
                            array.Count,
                            ArrayMinLength));
                }

                if (array.Count > this.ArrayMaxLength) {
                    throw new InvalidOperationException(
                        string.Format("Field: {0}. The array has {1} values, but the maximum length is {2}",
                            FieldInfo.Name,
                            array.Count,
                            ArrayMaxLength));
                }

                for (int i = 0; i < array.Count; i++) {
                    object val = array[i];
                    CreateFieldString(sb, val, IsLast && i == array.Count - 1);
                }
            }
            else
                CreateFieldString(sb, fieldValue, IsLast);
        }

        #endregion

        /// <summary>
        /// Copy the field object
        /// </summary>
        /// <returns>a complete copy of the Field object</returns>
        public object Clone()
        {
            var res = CreateClone();

            res.FieldType = FieldType;
            res.CharsToDiscard = CharsToDiscard;
            res.Converter = this.Converter;
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
            res.Discarded = Discarded;
            res.FieldFriendlyName = FieldFriendlyName;
            res.IsNotEmpty = IsNotEmpty;

            return res;
        }

        /// <summary>
        /// Add the extra details that derived classes create
        /// </summary>
        /// <returns>field clone of right type</returns>
        protected abstract FieldBase CreateClone();
    }
}
