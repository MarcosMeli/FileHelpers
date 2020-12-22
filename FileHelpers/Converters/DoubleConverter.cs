using System;
using System.Globalization;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Convert a value to a single floating point
    /// </summary>
    internal sealed class DoubleConverter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a floating point
        /// </summary>
        public DoubleConverter()
            : this(ConvertHelpers.DefaultDecimalSep) { }

        /// <summary>
        /// Convert a value to a floating point
        /// </summary>
        /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
        public DoubleConverter(string decimalSepOrCultureName)
            : base(typeof(double), decimalSepOrCultureName) { }

        /// <summary>
        /// Convert a string to an floating point
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>Floating point value</returns>
        public override object StringToField(string from)
        {
            double res;
            if (
                !Double.TryParse(ConvertHelpers.RemoveBlanks(@from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    mCulture,
                    out res))
                throw new ConvertException(@from, mType);
            return res;
        }
    }
}