using System;
using System.Globalization;
using FileHelpers.Helpers;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Convert a value to a short integer
    /// </summary>
    internal sealed class Int16Converter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a short integer
        /// </summary>
        public Int16Converter()
            : this(ConvertHelpers.DefaultDecimalSep) { }

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
        public override object StringToField(string from)
        {
            short res;
            if (
                !Int16.TryParse(StringHelper.RemoveBlanks(@from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    mCulture,
                    out res))
                throw new ConvertException(@from, mType);
            return res;
        }
    }
}