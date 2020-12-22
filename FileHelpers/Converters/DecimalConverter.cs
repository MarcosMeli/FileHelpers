using System;
using System.Globalization;

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
        public override object StringToField(string from)
        {
            decimal res;
            if (
                !Decimal.TryParse(ConvertHelpers.RemoveBlanks(@from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    mCulture,
                    out res))
                throw new ConvertException(@from, mType);
            return res;
        }
    }
}