using System;
using System.Globalization;
using System.Linq;
using System.Text;
using FileHelpers.Helpers;

namespace FileHelpers
{
    /// <summary>
    /// Class that provides static methods that returns a default 
    /// <see cref="ConverterBase">Converter</see> to the basic types.
    /// </summary>
    /// <remarks>
    ///     Used by the <see cref="FileHelpers.FieldConverterAttribute"/>.
    /// </remarks>
    internal static class ConvertHelpers
    {
        private const string DefaultDecimalSep = ".";

        /// <summary>
        /// Array of all allowed decimal separators
        /// </summary>
        private static readonly string[] mAllowedDecimalSeparators = { ".", "," };

        #region "  CreateCulture  "

        /// <summary>
        /// Return culture information for with comma decimal separator or comma decimal separator
        /// </summary>
        /// <param name="decimalSepOrCultureName">Decimal separator string or culture name</param>
        /// <returns>Cultural information based on separator</returns>
        private static CultureInfo CreateCulture(string decimalSepOrCultureName)
        {
            CultureInfo ci;

            if (mAllowedDecimalSeparators.Contains(decimalSepOrCultureName))
            {
                ci = new CultureInfo(CultureInfo.CurrentCulture.LCID);

                if (decimalSepOrCultureName == ".")
                {
                    ci.NumberFormat.NumberDecimalSeparator = ".";
                    ci.NumberFormat.NumberGroupSeparator = ",";
                }
                else if (decimalSepOrCultureName == ",")
                {
                    ci.NumberFormat.NumberDecimalSeparator = ",";
                    ci.NumberFormat.NumberGroupSeparator = ".";
                }
                else
                    throw new BadUsageException("You can only use '.' or ',' as decimal or group separators");
            }
            else
            {
                ci = CultureInfo.GetCultureInfo(decimalSepOrCultureName);
            }

            return ci;
        }

        #endregion

        #region "  GetDefaultConverter  "

        /// <summary>
        /// Check the type of the field and then return a converter for that particular type
        /// </summary>
        /// <param name="fieldName">Field name to check</param>
        /// <param name="fieldType">Type of the field to check</param>
        /// <param name="defaultCultureName">Default culture name used for each properties if no converter is specified otherwise. If null, the default decimal separator (".") will be used.</param>
        /// <returns>Converter for this particular field</returns>
        internal static ConverterBase GetDefaultConverter(string fieldName, Type fieldType, string defaultCultureName=null)
        {
            if (fieldType.IsArray)
            {

                if (fieldType.GetArrayRank() != 1)
                {
                    throw new BadUsageException("The array field: '" + fieldName +
                                                "' has more than one dimension and is not supported by the library.");
                }

                fieldType = fieldType.GetElementType();

                if (fieldType.IsArray)
                {
                    throw new BadUsageException("The array field: '" + fieldName +
                                                "' is a jagged array and is not supported by the library.");
                }
            }

            if (fieldType.IsValueType &&
                fieldType.IsGenericType &&
                fieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
                fieldType = fieldType.GetGenericArguments()[0];


            // Try to assign a default Converter
            if (fieldType == typeof(string))
                return null;
            if (fieldType == typeof(short))
                return new Int16Converter(defaultCultureName ?? DefaultDecimalSep);

            if (fieldType == typeof(int))
                return new Int32Converter(defaultCultureName ?? DefaultDecimalSep);

            if (fieldType == typeof(long))
                return new Int64Converter(defaultCultureName ?? DefaultDecimalSep);

            if (fieldType == typeof(sbyte))
                return new SByteConverter(defaultCultureName ?? DefaultDecimalSep);

            if (fieldType == typeof(ushort))
                return new UInt16Converter(defaultCultureName ?? DefaultDecimalSep);

            if (fieldType == typeof(uint))
                return new UInt32Converter(defaultCultureName ?? DefaultDecimalSep);

            if (fieldType == typeof(ulong))
                return new UInt64Converter(defaultCultureName ?? DefaultDecimalSep);

            if (fieldType == typeof(byte))
                return new ByteConverter(defaultCultureName ?? DefaultDecimalSep);

            if (fieldType == typeof(decimal))
                return new DecimalConverter(defaultCultureName ?? DefaultDecimalSep);

            if (fieldType == typeof(double))
                return new DoubleConverter(defaultCultureName ?? DefaultDecimalSep);

            if (fieldType == typeof(float))
                return new SingleConverter(defaultCultureName ?? DefaultDecimalSep);

            if (fieldType == typeof(DateTime))
                return new DateTimeConverter(ConverterBase.DefaultDateTimeFormat, defaultCultureName);

            if (fieldType == typeof(bool))
                return new BooleanConverter();

            // Added by Alexander Obolonkov 2007.11.08 (the next three)
            if (fieldType == typeof(char))
                return new CharConverter();
            if (fieldType == typeof(Guid))
                return new GuidConverter();
            if (fieldType.IsEnum)
                return new EnumConverter(fieldType);

            throw new BadUsageException("The field: '" + fieldName + "' has the type: " + fieldType.Name +
                                        " which is not a system type, so this field needs a CustomConverter (please check the docs for more info).");
        }

        #endregion

        /// <summary>
        /// Convert a numeric value with separators into a value
        /// </summary>
        internal abstract class CultureConverter
            : ConverterBase
        {
            /// <summary>
            /// Culture information based on the separator
            /// </summary>
            protected CultureInfo mCulture;

            /// <summary>
            /// Type fo field being converted
            /// </summary>
            protected Type mType;

            /// <summary>
            /// Convert to a type given a decimal separator
            /// </summary>
            /// <param name="T">type we are converting</param>
            /// <param name="decimalSepOrCultureName">Separator or culture name (eg. 'en-US', 'fr-FR'...)</param>
            protected CultureConverter(Type T, string decimalSepOrCultureName)
            {
                mCulture = CreateCulture(decimalSepOrCultureName);
                mType = T;
            }

            /// <summary>
            /// Convert the field to a string representation
            /// </summary>
            /// <param name="from">Object to convert</param>
            /// <returns>string representation</returns>
            public override sealed string FieldToString(object from)
            {
                if (from == null)
                    return string.Empty;

                return ((IConvertible)from).ToString(mCulture);
            }

            /// <summary>
            /// Convert a string to the object type
            /// </summary>
            /// <param name="from">String to convert</param>
            /// <returns>Object converted to</returns>
            public override sealed object StringToField(string from)
            {
                return ParseString(from);
            }

            /// <summary>
            /// Convert a string into the return object required
            /// </summary>
            /// <param name="from">Value to convert (string)</param>
            /// <returns>Converted object</returns>
            protected abstract object ParseString(string from);
        }

        /// <summary>
        /// COnvert a string into a byte value
        /// </summary>
        internal sealed class ByteConverter : CultureConverter
        {
            /// <summary>
            /// Convert a string to a byte value using the default decimal separator
            /// </summary>
            public ByteConverter()
                : this(DefaultDecimalSep) { }

            /// <summary>
            /// Convert a string to a byte
            /// </summary>
            /// <param name="decimalSepOrCultureName">decimal separator to use '.' or ','</param>
            public ByteConverter(string decimalSepOrCultureName)
                : base(typeof(byte), decimalSepOrCultureName) { }

            /// <summary>
            /// Convert a string to a byte value
            /// </summary>
            /// <param name="from">string to parse</param>
            /// <returns>byte value</returns>
            protected override object ParseString(string from)
            {
                byte res;
                if (!byte.TryParse(StringHelper.RemoveBlanks(from), NumberStyles.Number, mCulture, out res))
                    throw new ConvertException(from, mType);
                return res;
            }
        }

        /// <summary>
        /// Convert a string to a short integer
        /// </summary>
        internal sealed class UInt16Converter : CultureConverter
        {
            /// <summary>
            /// Convert a number to a short integer
            /// </summary>
            public UInt16Converter()
                : this(DefaultDecimalSep) { }

            /// <summary>
            /// Convert a number to a short integer
            /// </summary>
            /// <param name="decimalSepOrCultureName">Decimal separator</param>
            public UInt16Converter(string decimalSepOrCultureName)
                : base(typeof(ushort), decimalSepOrCultureName) { }

            /// <summary>
            /// Parse a string to a short integer
            /// </summary>
            /// <param name="from">string representing short integer</param>
            /// <returns>short integer value</returns>
            protected override object ParseString(string from)
            {
                ushort res;
                if (
                    !ushort.TryParse(StringHelper.RemoveBlanks(from),
                        NumberStyles.Number | NumberStyles.AllowExponent,
                        mCulture,
                        out res))
                    throw new ConvertException(from, mType);
                return res;
            }
        }

        /// <summary>
        /// Unsigned integer converter
        /// </summary>
        internal sealed class UInt32Converter : CultureConverter
        {
            /// <summary>
            /// Unsigned integer converter
            /// </summary>
            public UInt32Converter()
                : this(DefaultDecimalSep) { }

            /// <summary>
            /// Unsigned integer converter with a decimal separator
            /// </summary>
            /// <param name="decimalSepOrCultureName">dot or comma for to separate decimal</param>
            public UInt32Converter(string decimalSepOrCultureName)
                : base(typeof(uint), decimalSepOrCultureName) { }

            /// <summary>
            /// Convert a string to a unsigned integer value
            /// </summary>
            /// <param name="from">String value to parse</param>
            /// <returns>Unsigned integer object</returns>
            protected override object ParseString(string from)
            {
                uint res;
                if (
                    !uint.TryParse(StringHelper.RemoveBlanks(from),
                        NumberStyles.Number | NumberStyles.AllowExponent,
                        mCulture,
                        out res))
                    throw new ConvertException(from, mType);
                return res;
            }
        }

        /// <summary>
        /// Unsigned long converter
        /// </summary>
        internal sealed class UInt64Converter : CultureConverter
        {
            /// <summary>
            /// Unsigned long converter
            /// </summary>
            public UInt64Converter()
                : this(DefaultDecimalSep) { }

            /// <summary>
            /// Unsigned long with decimal separator
            /// </summary>
            /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
            public UInt64Converter(string decimalSepOrCultureName)
                : base(typeof(ulong), decimalSepOrCultureName) { }

            /// <summary>
            /// Convert a string to an unsigned integer long
            /// </summary>
            /// <param name="from">String value to convert</param>
            /// <returns>Unsigned long value</returns>
            protected override object ParseString(string from)
            {
                ulong res;
                if (
                    !ulong.TryParse(StringHelper.RemoveBlanks(from),
                        NumberStyles.Number | NumberStyles.AllowExponent,
                        mCulture,
                        out res))
                    throw new ConvertException(from, mType);
                return res;
            }
        }

        #region "  Int16, Int32, Int64 Converters  "

        #region "  Convert Classes  "

        /// <summary>
        /// Signed byte converter (8 bit signed integer)
        /// </summary>
        internal sealed class SByteConverter : CultureConverter
        {
            /// <summary>
            /// Signed byte converter (8 bit signed integer)
            /// </summary>
            public SByteConverter()
                : this(DefaultDecimalSep) { }

            /// <summary>
            /// Signed byte converter (8 bit signed integer)
            /// </summary>
            /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
            public SByteConverter(string decimalSepOrCultureName)
                : base(typeof(sbyte), decimalSepOrCultureName) { }

            /// <summary>
            /// Convert a string to an signed byte
            /// </summary>
            /// <param name="from">String value to convert</param>
            /// <returns>Signed byte value</returns>
            protected override object ParseString(string from)
            {
                sbyte res;
                if (!sbyte.TryParse(StringHelper.RemoveBlanks(from), NumberStyles.Number, mCulture, out res))
                    throw new ConvertException(from, mType);
                return res;
            }
        }

        /// <summary>
        /// Convert a value to a short integer
        /// </summary>
        internal sealed class Int16Converter : CultureConverter
        {
            /// <summary>
            /// Convert a value to a short integer
            /// </summary>
            public Int16Converter()
                : this(DefaultDecimalSep) { }

            /// <summary>
            /// Convert a value to a short integer
            /// </summary>
            /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
            public Int16Converter(string decimalSepOrCultureName)
                : base(typeof(short), decimalSepOrCultureName) { }

            /// <summary>
            /// Convert a string to an short integer
            /// </summary>
            /// <param name="from">String value to convert</param>
            /// <returns>Short signed value</returns>
            protected override object ParseString(string from)
            {
                short res;
                if (
                    !short.TryParse(StringHelper.RemoveBlanks(from),
                        NumberStyles.Number | NumberStyles.AllowExponent,
                        mCulture,
                        out res))
                    throw new ConvertException(from, mType);
                return res;
            }
        }

        /// <summary>
        /// Convert a value to a integer
        /// </summary>
        internal sealed class Int32Converter : CultureConverter
        {
            /// <summary>
            /// Convert a value to a integer
            /// </summary>
            public Int32Converter()
                : this(DefaultDecimalSep) { }

            /// <summary>
            /// Convert a value to a integer
            /// </summary>
            /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
            public Int32Converter(string decimalSepOrCultureName)
                : base(typeof(int), decimalSepOrCultureName) { }

            /// <summary>
            /// Convert a string to an integer
            /// </summary>
            /// <param name="from">String value to convert</param>
            /// <returns>integer value</returns>
            protected override object ParseString(string from)
            {
                int res;
                if (
                    !int.TryParse(StringHelper.RemoveBlanks(from),
                        NumberStyles.Number | NumberStyles.AllowExponent,
                        mCulture,
                        out res))
                    throw new ConvertException(from, mType);

                return res;
            }
        }

        /// <summary>
        /// Convert a value to a long integer
        /// </summary>
        internal sealed class Int64Converter : CultureConverter
        {
            /// <summary>
            /// Convert a value to a long integer
            /// </summary>
            public Int64Converter()
                : this(DefaultDecimalSep) { }

            /// <summary>
            /// Convert a value to a long integer
            /// </summary>
            /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
            public Int64Converter(string decimalSepOrCultureName)
                : base(typeof(long), decimalSepOrCultureName) { }

            /// <summary>
            /// Convert a string to an integer long
            /// </summary>
            /// <param name="from">String value to convert</param>
            /// <returns>Long value</returns>
            protected override object ParseString(string from)
            {
                long res;
                if (
                    !long.TryParse(StringHelper.RemoveBlanks(from),
                        NumberStyles.Number | NumberStyles.AllowExponent,
                        mCulture,
                        out res))
                    throw new ConvertException(from, mType);
                return res;
            }
        }

        #endregion

        #endregion

        #region "  Single, Double, DecimalConverters  "

        #region "  Convert Classes  "

        /// <summary>
        /// Convert a value to a decimal value
        /// </summary>
        internal sealed class DecimalConverter : CultureConverter
        {
            /// <summary>
            /// Convert a value to a decimal value
            /// </summary>
            public DecimalConverter()
                : this(DefaultDecimalSep) { }

            /// <summary>
            /// Convert a value to a decimal value
            /// </summary>
            /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
            public DecimalConverter(string decimalSepOrCultureName)
                : base(typeof(decimal), decimalSepOrCultureName) { }

            /// <summary>
            /// Convert a string to a decimal
            /// </summary>
            /// <param name="from">String value to convert</param>
            /// <returns>decimal value</returns>
            protected override object ParseString(string from)
            {
                decimal res;
                if (
                    !decimal.TryParse(StringHelper.RemoveBlanks(from),
                        NumberStyles.Number | NumberStyles.AllowExponent,
                        mCulture,
                        out res))
                    throw new ConvertException(from, mType);
                return res;
            }
        }

        /// <summary>
        /// Convert a value to a single floating point
        /// </summary>
        internal sealed class SingleConverter : CultureConverter
        {
            /// <summary>
            /// Convert a value to a single floating point
            /// </summary>
            public SingleConverter()
                : this(DefaultDecimalSep) { }

            /// <summary>
            /// Convert a value to a single floating point
            /// </summary>
            /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
            public SingleConverter(string decimalSepOrCultureName)
                : base(typeof(float), decimalSepOrCultureName) { }

            /// <summary>
            /// Convert a string to an single precision floating point
            /// </summary>
            /// <param name="from">String value to convert</param>
            /// <returns>Single floating point value</returns>
            protected override object ParseString(string from)
            {
                float res;
                if (
                    !float.TryParse(StringHelper.RemoveBlanks(from),
                        NumberStyles.Number | NumberStyles.AllowExponent,
                        mCulture,
                        out res))
                    throw new ConvertException(from, mType);
                return res;
            }
        }

        /// <summary>
        /// Convert a value to a single floating point
        /// </summary>
        internal sealed class DoubleConverter : CultureConverter
        {
            /// <summary>
            /// Convert a value to a floating point
            /// </summary>
            public DoubleConverter()
                : this(DefaultDecimalSep) { }

            /// <summary>
            /// Convert a value to a floating point
            /// </summary>
            /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
            public DoubleConverter(string decimalSepOrCultureName)
                : base(typeof(double), decimalSepOrCultureName) { }

            /// <summary>
            /// Convert a string to an floating point
            /// </summary>
            /// <param name="from">String value to convert</param>
            /// <returns>Floating point value</returns>
            protected override object ParseString(string from)
            {
                double res;
                if (
                    !double.TryParse(StringHelper.RemoveBlanks(from),
                        NumberStyles.Number | NumberStyles.AllowExponent,
                        mCulture,
                        out res))
                    throw new ConvertException(from, mType);
                return res;
            }
        }

        /// <summary>
        /// This Class is specialized version of the Double Converter
        /// The main difference being that it can handle % sign at the end of the number
        /// It gives a value which is basically number / 100.
        /// </summary>
        /// <remarks>Edited : Shreyas Narasimhan (17 March 2010) </remarks>
        internal sealed class PercentDoubleConverter : CultureConverter
        {
            /// <summary>
            /// Convert a value to a floating point from a percentage
            /// </summary>
            public PercentDoubleConverter()
                : this(DefaultDecimalSep) { }

            /// <summary>
            /// Convert a value to a floating point from a percentage
            /// </summary>
            /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
            public PercentDoubleConverter(string decimalSepOrCultureName)
                : base(typeof(double), decimalSepOrCultureName) { }

            /// <summary>
            /// Convert a string to an floating point from percentage
            /// </summary>
            /// <param name="from">String value to convert</param>
            /// <returns>floating point value</returns>
            protected override object ParseString(string from)
            {
                double res;
                var blanksRemoved = StringHelper.RemoveBlanks(from);
                if (blanksRemoved.EndsWith("%"))
                {
                    if (
                        !double.TryParse(blanksRemoved,
                            NumberStyles.Number | NumberStyles.AllowExponent,
                            mCulture,
                            out res))
                        throw new ConvertException(from, mType);
                    return res / 100.0;
                }
                else
                {
                    if (
                        !double.TryParse(blanksRemoved,
                            NumberStyles.Number | NumberStyles.AllowExponent,
                            mCulture,
                            out res))
                        throw new ConvertException(from, mType);
                    return res;
                }
            }
        }

        #endregion

        #endregion

        #region "  Date Converters  "

        #region "  Convert Classes  "

        /// <summary>
        /// Convert a value to a date time value
        /// </summary>
        internal sealed class DateTimeConverter : ConverterBase
        {
            private readonly string mFormat;
            private readonly CultureInfo mCulture;

            /// <summary>
            /// Convert a value to a date time value
            /// </summary>
            public DateTimeConverter()
                : this(DefaultDateTimeFormat) { }

            /// <summary>
            /// Convert a value to a date time value
            /// </summary>
            /// <param name="format">date format see .Net documentation</param>
            public DateTimeConverter(string format)
                : this(format, null)
            {
            }

            /// <summary>
            /// Convert a value to a date time value
            /// </summary>
            /// <param name="format">date format see .Net documentation</param>
            /// <param name="culture">The culture used to parse the Dates</param>
            public DateTimeConverter(string format, string culture)
            {
                if (string.IsNullOrEmpty(format))
                    throw new BadUsageException("The format of the DateTime Converter cannot be null or empty.");

                try
                {
                    DateTime.Now.ToString(format);
                }
                catch
                {
                    throw new BadUsageException("The format: '" + format + " is invalid for the DateTime Converter.");
                }

                mFormat = format;
                if (culture != null)
                    mCulture = CultureInfo.GetCultureInfo(culture);
            }
            /// <summary>
            /// Convert a string to a date time value
            /// </summary>
            /// <param name="from">String value to convert</param>
            /// <returns>DateTime value</returns>
            public override object StringToField(string from)
            {
                if (from == null)
                    from = string.Empty;

                DateTime val;
                if (!DateTime.TryParseExact(from.Trim(), mFormat, mCulture, DateTimeStyles.None, out val))
                {
                    string extra;

                    if (from.Length > mFormat.Length)
                        extra = " There are more chars in the Input String than in the Format string: '" + mFormat + "'";
                    else if (from.Length < mFormat.Length)
                        extra = " There are less chars in the Input String than in the Format string: '" + mFormat + "'";
                    else
                        extra = " Using the format: '" + mFormat + "'";


                    throw new ConvertException(from, typeof(DateTime), extra);
                }
                return val;
            }

            /// <summary>
            /// Convert a date time value to a string
            /// </summary>
            /// <param name="from">DateTime value to convert</param>
            /// <returns>string DateTime value</returns>
            public override string FieldToString(object from)
            {
                if (from == null)
                    return string.Empty;

                return Convert.ToDateTime(from).ToString(mFormat, mCulture);
            }
        }

        #endregion

        #region "  Convert Classes  "

        /// <summary>
        /// Convert a value to a date time value
        /// </summary>
        internal sealed class DateTimeMultiFormatConverter : ConverterBase
        {
            private readonly string[] mFormats;


            /// <summary>
            /// Convert a value to a date time value using multiple formats
            /// </summary>
            public DateTimeMultiFormatConverter(string format1, string format2)
                : this(new[] { format1, format2 }) { }

            /// <summary>
            /// Convert a value to a date time value using multiple formats
            /// </summary>
            public DateTimeMultiFormatConverter(string format1, string format2, string format3)
                : this(new[] { format1, format2, format3 }) { }

            /// <summary>
            /// Convert a date time value to a string
            /// </summary>
            /// <param name="formats">list of formats to try</param>
            private DateTimeMultiFormatConverter(string[] formats)
            {
                for (int i = 0; i < formats.Length; i++)
                {
                    if (formats[i] == null ||
                        formats[i] == string.Empty)
                        throw new BadUsageException("The format of the DateTime Converter can be null or empty.");

                    try
                    {
                        DateTime.Now.ToString(formats[i]);
                    }
                    catch
                    {
                        throw new BadUsageException("The format: '" + formats[i] +
                                                    " is invalid for the DateTime Converter.");
                    }
                }

                mFormats = formats;
            }

            /// <summary>
            /// Convert a date time value to a string
            /// </summary>
            /// <param name="from">DateTime value to convert</param>
            /// <returns>string DateTime value</returns>
            public override object StringToField(string from)
            {
                if (from == null)
                    from = string.Empty;

                DateTime val;
                if (!DateTime.TryParseExact(from.Trim(), mFormats, null, DateTimeStyles.None, out val))
                {
                    string extra = " does not match any of the given formats: " + CreateFormats();
                    throw new ConvertException(from, typeof(DateTime), extra);
                }
                return val;
            }

            /// <summary>
            /// Create a list of formats to pass to the DateTime tryparse function
            /// </summary>
            /// <returns>string DateTime value</returns>
            private string CreateFormats()
            {
                var sb = new StringBuilder();

                for (int i = 0; i < mFormats.Length; i++)
                {
                    if (i == 0)
                        sb.Append("'" + mFormats[i] + "'");
                    else
                        sb.Append(", '" + mFormats[i] + "'");
                }

                return sb.ToString();
            }

            /// <summary>
            /// Convert a date time value to a string (uses first format for output
            /// </summary>
            /// <param name="from">DateTime value to convert</param>
            /// <returns>string DateTime value</returns>
            public override string FieldToString(object from)
            {
                if (from == null)
                    return string.Empty;

                return Convert.ToDateTime(from).ToString(mFormats[0]);
            }
        }

        #endregion

        #endregion

        #region "  Boolean Converters  "

        #region "  Convert Classes  "

        /// <summary>
        /// Convert an input value to a boolean,  allows for true false values
        /// </summary>
        internal sealed class BooleanConverter : ConverterBase
        {
            private readonly string mTrueString = null;
            private readonly string mFalseString = null;
            private readonly string mTrueStringLower = null;
            private readonly string mFalseStringLower = null;

            /// <summary>
            /// Simple boolean converter
            /// </summary>
            public BooleanConverter() { }

            /// <summary>
            /// Boolean converter with true false values
            /// </summary>
            /// <param name="trueStr">True string</param>
            /// <param name="falseStr">False string</param>
            public BooleanConverter(string trueStr, string falseStr)
            {
                mTrueString = trueStr;
                mFalseString = falseStr;
                mTrueStringLower = trueStr.ToLower();
                mFalseStringLower = falseStr.ToLower();
            }

            /// <summary>
            /// convert a string to a boolean value
            /// </summary>
            /// <param name="from">string to convert</param>
            /// <returns>boolean value</returns>
            public override object StringToField(string from)
            {
                object val;
                string testTo = from.ToLower();

                if (mTrueString == null)
                {
                    testTo = testTo.Trim();
                    switch (testTo)
                    {
                        case "true":
                        case "1":
                        case "y":
                        case "t":
                            val = true;
                            break;

                        case "false":
                        case "0":
                        case "n":
                        case "f":

                        // I don't think that this case is possible without overriding the CustomNullHandling
                        // and it is possible that defaulting empty fields to be false is not correct
                        case "":
                            val = false;
                            break;

                        default:
                            throw new ConvertException(from,
                                typeof(bool),
                                "The string: " + from
                                + " can't be recognized as boolean using default true/false values.");
                    }
                }
                else
                {
                    //  Most of the time the strings should match exactly.  To improve performance
                    //  we skip the trim if the exact match is true
                    if (testTo == mTrueStringLower)
                        val = true;
                    else if (testTo == mFalseStringLower)
                        val = false;
                    else
                    {
                        testTo = testTo.Trim();
                        if (testTo == mTrueStringLower)
                            val = true;
                        else if (testTo == mFalseStringLower)
                            val = false;
                        else
                        {
                            throw new ConvertException(from,
                                typeof(bool),
                                "The string: " + from
                                + " can't be recognized as boolean using the true/false values: " + mTrueString + "/" +
                                mFalseString);
                        }
                    }
                }

                return val;
            }

            /// <summary>
            /// Convert to a true false string
            /// </summary>
            /// <param name="from"></param>
            /// <returns></returns>
            public override string FieldToString(object from)
            {
                bool b = Convert.ToBoolean(from);
                if (b)
                {
                    if (mTrueString == null)
                        return "True";
                    else
                        return mTrueString;
                }
                else if (mFalseString == null)
                    return "False";
                else
                    return mFalseString;
            }
        }

        #endregion

        #endregion

        #region "  GUID, Char, String, Padding Converters  "

        #region "  Convert Classes  "

        /// <summary>
        /// Allow characters to be converted to upper and lower case automatically.
        /// </summary>
        internal sealed class CharConverter : ConverterBase
        {
            /// <summary>
            /// whether we upper or lower case the character on input
            /// </summary>
            private enum CharFormat
            {
                /// <summary>
                /// Don't change the case
                /// </summary>
                NoChange = 0,

                /// <summary>
                /// Change to lower case
                /// </summary>
                Lower,

                /// <summary>
                /// change to upper case
                /// </summary>
                Upper,
            }

            /// <summary>
            /// default to not upper or lower case
            /// </summary>
            private readonly CharFormat mFormat = CharFormat.NoChange;


            /// <summary>
            /// Create a single character converter that does not upper or lower case result
            /// </summary>
            public CharConverter()
                : this("") // default,  no upper or lower case conversion
            { }

            /// <summary>
            /// Single character converter that optionally makes it upper (X) or lower case (x)
            /// </summary>
            /// <param name="format"> empty string for no upper or lower,  x for lower case,  X for Upper case</param>
            public CharConverter(string format)
            {
                switch (format.Trim())
                {
                    case "x":
                    case "lower":
                        mFormat = CharFormat.Lower;
                        break;

                    case "X":
                    case "upper":
                        mFormat = CharFormat.Upper;
                        break;

                    case "":
                        mFormat = CharFormat.NoChange;
                        break;

                    default:
                        throw new BadUsageException(
                            "The format of the Char Converter must be \"\", \"x\" or \"lower\" for lower case, \"X\" or \"upper\" for upper case");
                }
            }

            /// <summary>
            /// Extract the first character with optional upper or lower case
            /// </summary>
            /// <param name="from">String contents</param>
            /// <returns>Character (may be upper or lower case)</returns>
            public override object StringToField(string from)
            {
                if (string.IsNullOrEmpty(from))
                    return char.MinValue;

                try
                {
                    switch (mFormat)
                    {
                        case CharFormat.NoChange:
                            return from[0];

                        case CharFormat.Lower:
                            return char.ToLower(from[0]);

                        case CharFormat.Upper:
                            return char.ToUpper(from[0]);

                        default:
                            throw new ConvertException(from,
                                typeof(char),
                                "Unknown char convert flag " + mFormat.ToString());
                    }
                }
                catch
                {
                    throw new ConvertException(from, typeof(char), "Upper or lower case of input string failed");
                }
            }

            /// <summary>
            /// Convert from a character to a string for output
            /// </summary>
            /// <param name="from">Character to convert from</param>
            /// <returns>String containing the character</returns>
            public override string FieldToString(object from)
            {
                switch (mFormat)
                {
                    case CharFormat.NoChange:
                        return Convert.ToChar(from).ToString();

                    case CharFormat.Lower:
                        return char.ToLower(Convert.ToChar(from)).ToString();

                    case CharFormat.Upper:
                        return char.ToUpper(Convert.ToChar(from)).ToString();

                    default:
                        throw new ConvertException("", typeof(char), "Unknown char convert flag " + mFormat.ToString());
                }
            }
        }

        /// <summary>
        ///  Convert a GUID to and from a field value
        /// </summary>
        internal sealed class GuidConverter : ConverterBase
        {
            /// <summary>
            /// D or N or B or P (default is D: see Guid.ToString(string format))
            /// </summary>
            private readonly string mFormat;

            /// <summary>
            /// Create a GUID converter with the default format code "D"
            /// </summary>
            public GuidConverter()
                : this("D") // D or N or B or P (default is D: see Guid.ToString(string format))
            { }

            /// <summary>
            /// Create a GUID converter with formats as defined for GUID
            /// N, D, B or P
            /// </summary>
            /// <param name="format">Format code for GUID</param>
            public GuidConverter(string format)
            {
                if (string.IsNullOrEmpty(format))
                    format = "D";

                format = format.Trim().ToUpper();

                if (!(format == "N" || format == "D" || format == "B" || format == "P"))
                    throw new BadUsageException("The format of the Guid Converter must be N, D, B or P.");

                mFormat = format;
            }

            /// <summary>
            /// Convert a GUID string to a GUID object for the record object
            /// </summary>
            /// <param name="from">String representation of the GUID</param>
            /// <returns>GUID object or GUID empty</returns>
            public override object StringToField(string from)
            {
                if (string.IsNullOrEmpty(from))
                    return Guid.Empty;

                try
                {
                    return new Guid(from);
                }
                catch
                {
                    throw new ConvertException(from, typeof(Guid));
                }
            }

            /// <summary>
            /// Output GUID as a string field
            /// </summary>
            /// <param name="from">Guid object</param>
            /// <returns>GUID as a string depending on format</returns>
            public override string FieldToString(object from)
            {
                if (from == null)
                    return string.Empty;
                return ((Guid)from).ToString(mFormat);
            }
        }

        //// Added by Alexander Obolonkov 2007.11.08
        //internal sealed class StringConverter : ConverterBase
        //{
        //    string mFormat;

        //    public StringConverter()
        //        : this(null)
        //    {
        //    }

        //    public StringConverter(string format)
        //    {
        //            //throw new BadUsageException("The format of the String Converter can be null or empty.");

        //        if (String.IsNullOrEmpty(format))
        //            mFormat = null;
        //        else
        //        {
        //            mFormat = format;

        //            try
        //            {
        //                string tmp = String.Format(format, "Any String");
        //            }
        //            catch
        //            {
        //                throw new BadUsageException(
        //                    String.Format("The format: '{0}' is invalid for the String Converter.", format));
        //            }
        //        }
        //    }

        //    public override object StringToField(string from)
        //    {
        //        if (from == null)
        //            return string.Empty;
        //        if (from.Length == 0)
        //            return string.Empty;

        //        try
        //        {
        //            if (mFormat == null)
        //                return from;
        //            else
        //                return String.Format(mFormat, from);

        //            //if (m_intMaxLength > 0)
        //            //    strRet = strRet.Substring(0, m_intMaxLength);
        //        }
        //        catch
        //        {
        //            throw new ConvertException(from, typeof(String), "TODO Extra Info");
        //        }

        //    }

        //    public override string FieldToString(object from)
        //    {
        //        if (from == null)
        //            return string.Empty;
        //        else
        //            return String.Format(mFormat, from);
        //    }
        //}

        /// <summary>
        /// Convert a numeric value with separators into a value
        /// </summary>
        internal sealed class PaddingConverter
            : ConverterBase
        {
            /// <summary>
            /// Total length of the field when written
            /// </summary>
            private readonly int totalLength;

            /// <summary>
            /// Padding to left or right
            /// </summary>
            private readonly PaddingMode paddingMode;

            /// <summary>
            /// Character to pad with
            /// </summary>
            private readonly Char paddingChar;

            /// <summary>
            /// Padding Converter with Length and option Padding Mode and Padding character
            /// </summary>
            /// <param name="TotalLength">Total length of the field when written</param>
            /// <param name="PaddingMode">Whether to pad left or right</param>
            /// /// <param name="PaddingCharacter">Character to pad with</param>
            public PaddingConverter(Int32 TotalLength, PaddingMode PaddingMode = PaddingMode.Left, char PaddingCharacter = ' ')
            {
                totalLength = TotalLength;
                paddingMode = PaddingMode;
                paddingChar = PaddingCharacter;
            }

            /// <summary>
            /// Convert a string to object for the record object
            /// </summary>
            /// <param name="from">String representation of the object</param>
            /// <returns>object or empty</returns>
            public override object StringToField(string from)
            {
                if (String.IsNullOrEmpty(from))
                    return string.Empty;

                try
                {
                    return from;
                }
                catch
                {
                    throw new ConvertException(from, typeof(string));
                }
            }

            /// <summary>
            /// Convert a object to padded string
            /// </summary>
            /// <param name="from">Object to convert</param>
            /// <returns>String converted to</returns>
            public override string FieldToString(object from)
            {
                string paddedString = "";
                try
                {
                    paddedString = from.ToString();

                    if (paddedString.Length < totalLength)
                    {
                        int paddingLength = totalLength - paddedString.Length;
                        string strPadding = new string(paddingChar, paddingLength);
                        if (paddingMode == PaddingMode.Right)
                        {
                            return string.Concat(paddedString, strPadding);
                        }
                        else
                        {
                            return string.Concat(strPadding, paddedString);
                        }
                    }

                    return paddedString;
                }
                catch
                {

                    throw new ConvertException(paddedString, from.GetType(), "Padding failed");
                }
                
            }

        }

        #endregion

        #endregion
    }
}