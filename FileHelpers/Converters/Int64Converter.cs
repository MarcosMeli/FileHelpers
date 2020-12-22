using System;
using System.Globalization;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Convert a value to a long integer
    /// </summary>
    internal sealed class Int64Converter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a long integer
        /// </summary>
        public Int64Converter()
            : this(ConvertHelpers.DefaultDecimalSep) { }

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
        public override object StringToField(string from)
        {
            long res;
            if (
                !Int64.TryParse(ConvertHelpers.RemoveBlanks(@from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    mCulture,
                    out res))
                throw new ConvertException(@from, mType);
            return res;
        }
    }
}