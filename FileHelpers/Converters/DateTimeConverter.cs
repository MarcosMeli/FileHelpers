using System;
using System.Globalization;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Convert a value to a date time value
    /// </summary>
    internal sealed class DateTimeConverter : ConverterBase
    {
        private readonly string mFormat;
        private readonly CultureInfo mCulture;

        /// <summary>
        /// Convert a value to a date time value
        /// </summary>
        public DateTimeConverter()
            : this(DefaultDateTimeFormat) { }

        /// <summary>
        /// Convert a value to a date time value
        /// </summary>
        /// <param name="format">date format see .Net documentation</param>
        public DateTimeConverter(string format)
            : this(format, null)
        {
        }

        /// <summary>
        /// Convert a value to a date time value
        /// </summary>
        /// <param name="format">date format see .Net documentation</param>
        /// <param name="culture">The culture used to parse the Dates</param>
        public DateTimeConverter(string format, string culture)
        {
            if (String.IsNullOrEmpty(format))
                throw new BadUsageException("The format of the DateTime Converter cannot be null or empty.");

            try
            {
                DateTime.Now.ToString(format);
            }
            catch
            {
                throw new BadUsageException("The format: '" + format + " is invalid for the DateTime Converter.");
            }

            mFormat = format;
            if (culture != null)
                mCulture = CultureInfo.GetCultureInfo(culture);
        }
        /// <summary>
        /// Convert a string to a date time value
        /// </summary>
        /// <param name="from">String value to convert</param>
        /// <returns>DateTime value</returns>
        public override object StringToField(string from)
        {
            if (@from == null)
                @from = String.Empty;

            DateTime val;
            if (!DateTime.TryParseExact(@from.Trim(), mFormat, mCulture, DateTimeStyles.None, out val))
            {
                string extra;

                if (@from.Length > mFormat.Length)
                    extra = " There are more chars in the Input String than in the Format string: '" + mFormat + "'";
                else if (@from.Length < mFormat.Length)
                    extra = " There are less chars in the Input String than in the Format string: '" + mFormat + "'";
                else
                    extra = " Using the format: '" + mFormat + "'";


                throw new ConvertException(@from, typeof(DateTime), extra);
            }
            return val;
        }

        /// <summary>
        /// Convert a date time value to a string
        /// </summary>
        /// <param name="from">DateTime value to convert</param>
        /// <returns>string DateTime value</returns>
        public override string FieldToString(object from)
        {
            if (@from == null)
                return String.Empty;

            return Convert.ToDateTime(@from).ToString(mFormat, mCulture);
        }
    }
}