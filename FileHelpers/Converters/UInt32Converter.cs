using System;
using System.Globalization;
using FileHelpers.Helpers;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Unsigned integer converter
    /// </summary>
    internal sealed class UInt32Converter : CultureConverter
    {
        /// <summary>
        /// Unsigned integer converter
        /// </summary>
        public UInt32Converter()
            : this(ConvertHelpers.DefaultDecimalSep) { }

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
                !UInt32.TryParse(StringHelper.RemoveBlanks(@from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    mCulture,
                    out res))
                throw new ConvertException(@from, mType);
            return res;
        }
    }
}