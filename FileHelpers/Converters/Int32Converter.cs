using System;
using System.Globalization;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Convert a value to a integer
    /// </summary>
    internal sealed class Int32Converter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a integer
        /// </summary>
        public Int32Converter()
            : this(ConvertHelpers.DefaultDecimalSep) { }

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
        public override object StringToField(string from)
        {
            int res;
            if (
                !Int32.TryParse(ConvertHelpers.RemoveBlanks(@from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    mCulture,
                    out res))
                throw new ConvertException(@from, mType);

            return res;
        }
    }
}