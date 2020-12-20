using System;
using System.Globalization;
using FileHelpers.Helpers;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Convert a string to a short integer
    /// </summary>
    internal sealed class UInt16Converter : CultureConverter
    {
        /// <summary>
        /// Convert a number to a short integer
        /// </summary>
        public UInt16Converter()
            : this(ConvertHelpers.DefaultDecimalSep) { }

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
                !UInt16.TryParse(StringHelper.RemoveBlanks(@from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    mCulture,
                    out res))
                throw new ConvertException(@from, mType);
            return res;
        }
    }
}