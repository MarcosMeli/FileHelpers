using System.Globalization;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Unsigned integer converter
    /// </summary>
    public sealed class UInt32Converter : CultureConverter
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
        public override object StringToField(string from)
        {
            uint res;
            if (
                !uint.TryParse(ConvertHelpers.RemoveBlanks(from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    mCulture,
                    out res))
                throw new ConvertException(from, mType);
            return res;
        }
    }
}