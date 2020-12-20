using System;
using System.Globalization;
using FileHelpers.Helpers;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Convert a value to a single floating point
    /// </summary>
    internal sealed class SingleConverter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a single floating point
        /// </summary>
        public SingleConverter()
            : this(ConvertHelpers.DefaultDecimalSep) { }

        /// <summary>
        /// Convert a value to a single floating point
        /// </summary>
        /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
        public SingleConverter(string decimalSepOrCultureName)
            : base(typeof(float), decimalSepOrCultureName) { }

        /// <summary>
        /// Convert a string to an single precision floating point
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>Single floating point value</returns>
        protected override object ParseString(string from)
        {
            float res;
            if (
                !Single.TryParse(StringHelper.RemoveBlanks(@from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    mCulture,
                    out res))
                throw new ConvertException(@from, mType);
            return res;
        }
    }
}