using System.Globalization;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Convert a string to a short integer
    /// </summary>
    public sealed class UInt16Converter : CultureConverter
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
        public override object StringToField(string from)
        {
            ushort res;
            if (
                !ushort.TryParse(ConvertHelpers.RemoveBlanks(from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    mCulture,
                    out res))
                throw new ConvertException(from, mType);
            return res;
        }
    }
}