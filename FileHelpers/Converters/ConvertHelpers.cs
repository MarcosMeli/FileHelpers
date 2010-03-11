

using System;
using System.Globalization;
using System.Text;

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

        
        #region "  CreateCulture  "

        static CultureInfo CreateCulture(string decimalSep)
        {
            CultureInfo ci = new CultureInfo(CultureInfo.CurrentCulture.LCID);

            if (decimalSep == ".")
            {
                ci.NumberFormat.NumberDecimalSeparator = ".";
                ci.NumberFormat.NumberGroupSeparator = ",";
            }
            else if (decimalSep == ",")
            {
                ci.NumberFormat.NumberDecimalSeparator = ",";
                ci.NumberFormat.NumberGroupSeparator = ".";
            }
            else
                throw new BadUsageException("You can only use '.' or ',' as decimal or group separators");

            return ci;
        }

        #endregion

        #region "  GetDefaultConverter  "

        internal static ConverterBase GetDefaultConverter(string fieldName, Type fieldType)
        {
            if (fieldType.IsArray)
            {
#if !MINI
                if (fieldType.GetArrayRank() != 1)
                    throw new BadUsageException("The array field: '" + fieldName + "' has more than one dimension and is not supported by the library.");
#endif

                fieldType = fieldType.GetElementType();

                if (fieldType.IsArray)
                    throw new BadUsageException("The array field: '" + fieldName + "' is a jagged array and is not supported by the library.");

            }

            if (fieldType.IsValueType &&
                  fieldType.IsGenericType &&
                    fieldType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                fieldType = fieldType.GetGenericArguments()[0];
            }


            // Try to assign a default Converter
            if (fieldType == typeof(string))
                return null;
            if (fieldType == typeof(Int16))
                return new Int16Converter();

            if (fieldType == typeof(Int32))
                return new Int32Converter();

            if (fieldType == typeof(Int64))
                return new Int64Converter();

            if (fieldType == typeof(SByte))
                return new SByteConverter();

            if (fieldType == typeof(UInt16))
                return new UInt16Converter();

            if (fieldType == typeof(UInt32))
                return new UInt32Converter();

            if (fieldType == typeof(UInt64))
                return new UInt64Converter();

            if (fieldType == typeof(Byte))
                return new ByteConverter();

            if (fieldType == typeof(Decimal))
                return new DecimalConverter();

            if (fieldType == typeof(Double))
                return new DoubleConverter();

            if (fieldType == typeof(Single))
                return new SingleConverter();

            if (fieldType == typeof(DateTime))
                return new DateTimeConverter();

            if (fieldType == typeof(Boolean))
                return new BooleanConverter();

            // Added by Alexander Obolonkov 2007.11.08 (the next three)
            if (fieldType == typeof(Char))
                return new CharConverter();
            if (fieldType == typeof(Guid))
                return new GuidConverter();
#if ! MINI
            if (fieldType.IsEnum)
                return new EnumConverter(fieldType);
#endif

            throw new BadUsageException("The field: '" + fieldName + "' has the type: " + fieldType.Name + " that is not a system type, so this field need a CustomConverter ( Please Check the docs for more Info).");
        }

        #endregion

        internal abstract class CultureConverter
            : ConverterBase
        {
            protected CultureInfo mCulture;
            protected Type mType;
            public CultureConverter(Type T, string decimalSep)
            {
                mCulture = CreateCulture(decimalSep);
                mType = T;
            }

            public sealed override string FieldToString(object from)
            {
                if (from == null)
                    return string.Empty;

                return ((IConvertible)from).ToString(mCulture);
            }

            public sealed override object StringToField(string from)
            {
                object val;

                try
                {
                    val = ParseString(from);
                }
                catch
                {
                    throw new ConvertException(from, mType);
                }

                return val;
            }

            protected abstract object ParseString(string from);

        }

        internal sealed class ByteConverter : CultureConverter
        {
            public ByteConverter()
                : this(DefaultDecimalSep)
            { }

            public ByteConverter(string decimalSep)
                : base(typeof(Byte), decimalSep)
            { }

            protected override object ParseString(string from)
            {
                return Byte.Parse(StringHelper.RemoveBlanks(from), NumberStyles.Number, mCulture);
            }
        }


        internal sealed class UInt16Converter : CultureConverter
        {
            public UInt16Converter()
                : this(DefaultDecimalSep)
            { }

            public UInt16Converter(string decimalSep)
                : base(typeof(UInt16), decimalSep)
            { }

            protected override object ParseString(string from)
            {
                return UInt16.Parse(StringHelper.RemoveBlanks(from), NumberStyles.Number | NumberStyles.AllowExponent, mCulture);
            }
        }


        internal sealed class UInt32Converter : CultureConverter
        {
            public UInt32Converter()
                : this(DefaultDecimalSep)
            { }

            public UInt32Converter(string decimalSep)
                : base(typeof(UInt32), decimalSep)
            { }

            protected override object ParseString(string from)
            {
                return UInt32.Parse(StringHelper.RemoveBlanks(from), NumberStyles.Number | NumberStyles.AllowExponent, mCulture);
            }
        }



        internal sealed class UInt64Converter : CultureConverter
        {
            public UInt64Converter()
                : this(DefaultDecimalSep)
            { }

            public UInt64Converter(string decimalSep)
                : base(typeof(UInt64), decimalSep)
            { }

            protected override object ParseString(string from)
            {
                return UInt64.Parse(StringHelper.RemoveBlanks(from), NumberStyles.Number | NumberStyles.AllowExponent, mCulture);
            }
        }

        #region "  Int16, Int32, Int64 Converters  "

        #region "  Convert Classes  "

        internal sealed class SByteConverter : CultureConverter
        {
            public SByteConverter()
                : this(DefaultDecimalSep)
            { }

            public SByteConverter(string decimalSep)
                : base(typeof(SByte), decimalSep)
            { }

            protected override object ParseString(string from)
            {
                return SByte.Parse(StringHelper.RemoveBlanks(from), NumberStyles.Number, mCulture);
            }
        }

        internal sealed class Int16Converter : CultureConverter
        {
            public Int16Converter()
                : this(DefaultDecimalSep)
            { }

            public Int16Converter(string decimalSep)
                : base(typeof(Int16), decimalSep)
            { }

            protected override object ParseString(string from)
            {
                return Int16.Parse(StringHelper.RemoveBlanks(from), NumberStyles.Number | NumberStyles.AllowExponent, mCulture);
            }
        }

        internal sealed class Int32Converter : CultureConverter
        {
            public Int32Converter()
                : this(DefaultDecimalSep)
            { }


            public Int32Converter(string decimalSep)
                : base(typeof(Int32), decimalSep)
            { }

            protected override object ParseString(string from)
            {
                return Int32.Parse(StringHelper.RemoveBlanks(from), NumberStyles.Number | NumberStyles.AllowExponent, mCulture);
            }
        }

        internal sealed class Int64Converter : CultureConverter
        {
            public Int64Converter()
                : this(DefaultDecimalSep)
            { }
            public Int64Converter(string decimalSep)
                : base(typeof(Int64), decimalSep)
            { }

            protected override object ParseString(string from)
            {
                return Int64.Parse(StringHelper.RemoveBlanks(from), NumberStyles.Number | NumberStyles.AllowExponent, mCulture);
            }
        }

        #endregion

        #endregion

        #region "  Single, Double, DecimalConverters  "

        #region "  Convert Classes  "

        internal sealed class DecimalConverter : CultureConverter
        {
            public DecimalConverter()
                : this(DefaultDecimalSep)
            { }

            public DecimalConverter(string decimalSep)
                : base(typeof(Decimal), decimalSep)
            {
            }

            protected override object ParseString(string from)
            {
                return Decimal.Parse(StringHelper.RemoveBlanks(from), NumberStyles.Number | NumberStyles.AllowExponent, mCulture);
            }
        }

        internal sealed class SingleConverter : CultureConverter
        {
            public SingleConverter()
                : this(DefaultDecimalSep)
            { }

            public SingleConverter(string decimalSep)
                : base(typeof(Single), decimalSep)
            { }

            protected override object ParseString(string from)
            {
                return Single.Parse(StringHelper.RemoveBlanks(from), NumberStyles.Number | NumberStyles.AllowExponent, mCulture);
            }
        }


        internal sealed class DoubleConverter : CultureConverter
        {
            public DoubleConverter()
                : this(DefaultDecimalSep)
            { }

            public DoubleConverter(string decimalSep)
                : base(typeof(Double), decimalSep)
            {
            }

            protected override object ParseString(string from)
            {
                return Double.Parse(StringHelper.RemoveBlanks(from), NumberStyles.Number | NumberStyles.AllowExponent, mCulture);
            }
        }

        #endregion

        #endregion

        #region "  Date Converters  "

        #region "  Convert Classes  "

        internal sealed class DateTimeConverter : ConverterBase
        {
            readonly string mFormat;

            public DateTimeConverter()
                : this(DefaultDateTimeFormat)
            {
            }

            public DateTimeConverter(string format)
            {
                if (format == null || format == String.Empty)
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
            }

            public override object StringToField(string from)
            {
                if (from == null) from = string.Empty;

                object val;
                try
                {
                    val = DateTime.ParseExact(from.Trim(), mFormat, null);
                }
                catch
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

            public override string FieldToString(object from)
            {
                if (from == null)
                    return string.Empty;

                return Convert.ToDateTime(from).ToString(mFormat);
            }
        }

        #endregion

        #region "  Convert Classes  "

        internal sealed class DateTimeMultiFormatConverter : ConverterBase
        {
            readonly string[] mFormats;


            public DateTimeMultiFormatConverter(string format1, string format2)
                : this(new string[] { format1, format2 })
            {
            }

            public DateTimeMultiFormatConverter(string format1, string format2, string format3)
                : this(new string[] { format1, format2, format3 })
            {
            }

            private DateTimeMultiFormatConverter(string[] formats)
            {
                for (int i = 0; i < formats.Length; i++)
                {
                    if (formats[i] == null || formats[i] == String.Empty)
                        throw new BadUsageException("The format of the DateTime Converter can be null or empty.");

                    try
                    {
                        DateTime.Now.ToString(formats[i]);
                    }
                    catch
                    {
                        throw new BadUsageException("The format: '" + formats[i] + " is invalid for the DateTime Converter.");
                    }
                }

                mFormats = formats;
            }

            public override object StringToField(string from)
            {
                if (from == null) from = string.Empty;

                object val;
                try
                {
                    val = DateTime.ParseExact(from.Trim(), mFormats, null, DateTimeStyles.None);
                }
                catch
                {
                    string extra;
                    extra = " Not matching any of the given formats: " + CreateFormats();
                    throw new ConvertException(from, typeof(DateTime), extra);
                }
                return val;
            }

            private string CreateFormats()
            {
                StringBuilder sb = new StringBuilder();

                for (int i = 0; i < mFormats.Length; i++)
                {
                    if (i == 0)
                        sb.Append("'" + mFormats[i] + "'");
                    else
                        sb.Append(", '" + mFormats[i] + "'");
                }

                return sb.ToString();
            }

            public override string FieldToString(object from)
            {
                return Convert.ToDateTime(from).ToString(mFormats[0]);
            }
        }

        #endregion


        #endregion

        #region "  Boolean Converters  "

        #region "  Convert Classes  "

        internal sealed class BooleanConverter : ConverterBase
        {
            private readonly string mTrueString = null;
            private readonly string mFalseString = null;
            private readonly string mTrueStringLower = null;
            private readonly string mFalseStringLower = null;

            public BooleanConverter()
            {
            }

            public BooleanConverter(string trueStr, string falseStr)
            {
                mTrueString = trueStr;
                mFalseString = falseStr;
                mTrueStringLower = trueStr.ToLower();
                mFalseStringLower = falseStr.ToLower();
            }

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

                        // I dont thing that this case is possible without overriding the CustomNullHandling
                        // and maybe It is not good to allow empty fields to be false
                        case "":
                            val = false;
                            break;

                        default:
                            throw new ConvertException(from, typeof(bool), "The string: " + from + " can't be recognized as boolean using default true/false values.");
                    }
                }
                else
                {
                    // The trim in the or part is for performance enhancement as we dont want to unnecessarily trim.
                    if (testTo == mTrueStringLower || testTo.Trim() == mTrueStringLower)
                        val = true;
                    else if (testTo == mFalseStringLower || testTo.Trim() == mFalseStringLower)
                        val = false;
                    else
                        throw new ConvertException(from, typeof(bool), "The string: " + from + " can't be recognized as boolean using the true/false values: " + mTrueString + "/" + mFalseString);
                }

                return val;
            }

            public override string FieldToString(object from)
            {
                bool b = Convert.ToBoolean(from);
                if (b)
                    if (mTrueString == null)
                        return "True";
                    else
                        return mTrueString;
                else
                    if (mFalseString == null)
                        return "False";
                    else
                        return mFalseString;

            }
        }

        #endregion

        #endregion

        #region "  GUID, Char, String Converters  "
        // Added by Alexander Obolonkov 2007.11.08

        #region "  Convert Classes  "
        // Added by Alexander Obolonkov 2007.11.08
        internal sealed class CharConverter : ConverterBase
        {
            int mFormat = 0; //1 Lower, 2 Upper

            public CharConverter()
                : this(" ") // xX
            {
            }

            public CharConverter(string format)
            {
                format = format.Trim();

                if (format == "x")
                    mFormat = 1;
                else if (format == "X")
                    mFormat = 2;
                else if (string.IsNullOrEmpty(format))
                    mFormat = 0;
                else
                    throw new BadUsageException("The format of the Char Converter must be \"\", \"x\" for lower or \"X\" for upper");
            }

            public override object StringToField(string from)
            {
                if (from == null) from = string.Empty;
                if (from.Length == 0)
                    return Char.MinValue;

                try
                {
                    char res = from[0];

                    if (mFormat == 1)
                        res = char.ToLower(res);
                    else if (mFormat == 2)
                        res = char.ToUpper(res);

                    return res;
                }
                catch
                {
                    throw new ConvertException(from, typeof(Char), "TODO Extra Info");
                }
            }

            public override string FieldToString(object from)
            {
                return Convert.ToChar(from).ToString();
            }
        }

        // Added by Alexander Obolonkov 2007.11.08
        internal sealed class GuidConverter : ConverterBase
        {
            string mFormat;

            public GuidConverter()
                : this("D") // D or N or B or P (default is D: see Guid.ToString(string format))
            {
            }

            public GuidConverter(string format)
            {
                if (String.IsNullOrEmpty(format))
                    format = "D";

                format = format.Trim().ToUpper();

                if (!(format == "N" || format == "D" || format == "B" || format == "P"))
                    throw new BadUsageException("The format of the Guid Converter must be N, D, B or P.");

                mFormat = format;
            }

            public override object StringToField(string from)
            {
                if (String.IsNullOrEmpty(from))
                    return Guid.Empty;

                try
                {
                    return new Guid(from);
                }
                catch
                {
                    throw new ConvertException(from, typeof(Guid), "TODO Extra Info");
                }
            }

            public override string FieldToString(object from)
            {
                if (from == null) return String.Empty; 
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

        #endregion

        #endregion
    }
}

