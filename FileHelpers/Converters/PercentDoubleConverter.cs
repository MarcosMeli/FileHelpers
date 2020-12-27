using System;
using System.Globalization;

namespace FileHelpers.Converters
{
    /// <summary>
    /// This Class is specialized version of the Double Converter
    /// The main difference being that it can handle % sign at the end of the number
    /// It gives a value which is basically number / 100.
    /// </summary>
    internal sealed class PercentDoubleConverter : CultureConverter
    {
        /// <summary>
        /// Convert a value to a floating point from a percentage
        /// </summary>
        public PercentDoubleConverter()
            : this(ConvertHelpers.DefaultDecimalSep) { }

        /// <summary>
        /// Convert a value to a floating point from a percentage
        /// </summary>
        /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
        public PercentDoubleConverter(string decimalSepOrCultureName)
            : base(typeof(double), decimalSepOrCultureName) { }

        /// <summary>
        /// Convert a string to an floating point from percentage
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>floating point value</returns>
        public override object StringToField(string from)
        {
            double res;
            var blanksRemoved = ConvertHelpers.RemoveBlanks(from);
            if (blanksRemoved.EndsWith("%"))
            {
                if (
                    !double.TryParse(blanksRemoved,
                        NumberStyles.Number | NumberStyles.AllowExponent,
                        mCulture,
                        out res))
                    throw new ConvertException(from, mType);
                return res / 100.0;
            }
            else
            {
                if (
                    !double.TryParse(blanksRemoved,
                        NumberStyles.Number | NumberStyles.AllowExponent,
                        mCulture,
                        out res))
                    throw new ConvertException(from, mType);
                return res;
            }
        }
    }
}