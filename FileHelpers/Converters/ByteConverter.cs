using System;
using System.Globalization;
using FileHelpers.Helpers;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Convert a string into a byte value
    /// </summary>
    internal sealed class ByteConverter : CultureConverter
    {
        /// <summary>
        /// Convert a string to a byte value using the default decimal separator
        /// </summary>
        public ByteConverter()
            : this(ConvertHelpers.DefaultDecimalSep) { }

        /// <summary>
        /// Convert a string to a byte
        /// </summary>
        /// <param name="decimalSepOrCultureName">decimal separator to use '.' or ','</param>
        public ByteConverter(string decimalSepOrCultureName)
            : base(typeof(byte), decimalSepOrCultureName) { }

        /// <summary>
        /// Convert a string to a byte value
        /// </summary>
        /// <param name="from">string to parse</param>
        /// <returns>byte value</returns>
        public override object StringToField(string from)
        {
            byte res;
            if (!Byte.TryParse(StringHelper.RemoveBlanks(@from), NumberStyles.Number, mCulture, out res))
                throw new ConvertException(@from, mType);
            return res;
        }
    }
}