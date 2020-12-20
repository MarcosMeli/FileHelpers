using System;
using System.Globalization;
using FileHelpers.Helpers;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Convert a value to a decimal value
    /// </summary>
    internal sealed class DecimalConverter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a decimal value
        /// </summary>
        public DecimalConverter()
            : this(ConvertHelpers.DefaultDecimalSep) { }

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
                !Decimal.TryParse(StringHelper.RemoveBlanks(@from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    mCulture,
                    out res))
                throw new ConvertException(@from, mType);
            return res;
        }
    }
}