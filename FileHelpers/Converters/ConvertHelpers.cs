using System;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Class that provides static methods that returns a default 
    /// <see cref="ConverterBase">Converter</see> to the basic types.
    /// </summary>
    /// <remarks>
    ///     Used by the <see cref="FieldConverterAttribute"/>.
    /// </remarks>
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

            if (fieldType == typeof(char))
                return new CharConverter();
            if (fieldType == typeof(Guid))
                return new GuidConverter();
            if (fieldType.IsEnum)
                return new EnumConverter(fieldType);

            throw new BadUsageException("The field: '" + fieldName + "' has the type: " + fieldType.Name +
                                        " which is not a system type, so this field needs a CustomConverter (please check the docs for more info).");
        }
    }
}