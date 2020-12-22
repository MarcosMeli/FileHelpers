using System.Globalization;

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
        public override object StringToField(string from)
        {
            float res;
            if (
                !float.TryParse(ConvertHelpers.RemoveBlanks(@from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    mCulture,
                    out res))
                throw new ConvertException(@from, mType);
            return res;
        }
    }
}