using System;
using System.Globalization;
using FileHelpers.Helpers;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Signed byte converter (8 bit signed integer)
    /// </summary>
    internal sealed class SByteConverter : CultureConverter
    {
        /// <summary>
        /// Signed byte converter (8 bit signed integer)
        /// </summary>
        public SByteConverter()
            : this(ConvertHelpers.DefaultDecimalSep) { }

        /// <summary>
        /// Signed byte converter (8 bit signed integer)
        /// </summary>
        /// <param name="decimalSepOrCultureName">dot or comma for separator</param>
        public SByteConverter(string decimalSepOrCultureName)
            : base(typeof(sbyte), decimalSepOrCultureName) { }

        /// <summary>
        /// Convert a string to an signed byte
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>Signed byte value</returns>
        public override object StringToField(string from)
        {
            sbyte res;
            if (!SByte.TryParse(StringHelper.RemoveBlanks(@from), NumberStyles.Number, mCulture, out res))
                throw new ConvertException(@from, mType);
            return res;
        }
    }
}