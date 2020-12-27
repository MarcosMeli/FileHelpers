using System;
using System.Text;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Class that provides static methods that returns a default 
    /// <see cref="ConverterBase">Converter</see> to the basic types.
    /// </summary>
    internal static class ConvertHelpers
    {
        internal const string DefaultDecimalSep = ".";

        /// <summary>
        /// Check the type of the field and then return a converter for that particular type
        /// </summary>
        /// <param name="fieldName">Field name to check</param>
        /// <param name="fieldType">Type of the field to check</param>
        /// <param name="defaultCultureName">Default culture name used for each properties if no converter is specified otherwise. If null, the default decimal separator (".") will be used.</param>
        /// <returns>Converter for this particular field</returns>
        internal static IConverter GetDefaultConverter(string fieldName, Type fieldType, string defaultCultureName=null)
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

            if (fieldType == typeof(char))
                return new CharConverter();
            if (fieldType == typeof(Guid))
                return new GuidConverter();
            if (fieldType.IsEnum)
                return new EnumConverter(fieldType);

            throw new BadUsageException("The field: '" + fieldName + "' has the type: " + fieldType.Name +
                                        " which is not a system type, so this field needs a CustomConverter (please check the docs for more info).");
        }

        /// <summary>
        /// Remove leading blanks and blanks after the plus or minus sign from a string
        /// to allow it to be parsed by ToInt or other converters
        /// </summary>
        /// <param name="source">source to trim</param>
        /// <returns>String without blanks</returns>
        /// <remarks>
        /// This logic is used to handle strings line " +  21 " from
        /// input data (returns "+21 "). The integer convert would fail
        /// because of the extra blanks so this logic trims them
        /// </remarks>
        internal static string RemoveBlanks(string source)
        {
            int i = 0;

            while (i < source.Length &&
                   char.IsWhiteSpace(source[i]))
                i++;

            // Only whitespace return an empty string
            if (i >= source.Length)
                return string.Empty;

            // we are looking for a gap after the sign, if not found then
            // trim off the front of the string and return
            if (source[i] == '+' ||
                source[i] == '-')
            {
                i++;
                if (!char.IsWhiteSpace(source[i]))
                    return source; //  sign is followed by text so just return it

                // start out with the sign
                var sb = new StringBuilder(source[i - 1].ToString(), source.Length - i);

                i++; // I am on whitespace so skip it
                while (i < source.Length &&
                       char.IsWhiteSpace(source[i]))
                    i++;
                if (i < source.Length)
                    sb.Append(source.Substring(i));

                return sb.ToString();
            }
            else // No sign, just return string
                return source;
        }
    }
}