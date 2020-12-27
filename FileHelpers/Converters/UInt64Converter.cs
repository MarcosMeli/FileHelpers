using System.Globalization;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Unsigned long converter
    /// </summary>
    public sealed class UInt64Converter : CultureConverter
    {
        /// <summary>
        /// Unsigned long converter
        /// </summary>
        public UInt64Converter()
            : this(ConvertHelpers.DefaultDecimalSep) { }

        /// <summary>
        /// Unsigned long with decimal separator
        /// </summary>
        /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
        public UInt64Converter(string decimalSepOrCultureName)
            : base(typeof(ulong), decimalSepOrCultureName) { }

        /// <summary>
        /// Convert a string to an unsigned integer long
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>Unsigned long value</returns>
        public override object StringToField(string from)
        {
            ulong res;
            if (
                !ulong.TryParse(ConvertHelpers.RemoveBlanks(from),
                    NumberStyles.Number | NumberStyles.AllowExponent,
                    mCulture,
                    out res))
                throw new ConvertException(from, mType);
            return res;
        }
    }
}