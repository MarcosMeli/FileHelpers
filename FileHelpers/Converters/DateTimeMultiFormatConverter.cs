using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Convert a value to a date time value
    /// </summary>
    internal sealed class DateTimeMultiFormatConverter : ConverterBase
    {
        private readonly string[] mFormats;

        /// <summary>
        /// Convert a value to a date time value using multiple formats
        /// </summary>
        public DateTimeMultiFormatConverter(string format1, string format2)
            : this(new[] { format1, format2 }) { }

        /// <summary>
        /// Convert a value to a date time value using multiple formats
        /// </summary>
        public DateTimeMultiFormatConverter(string format1, string format2, string format3)
            : this(new[] { format1, format2, format3 }) { }

        /// <summary>
        /// Convert a date time value to a string
        /// </summary>
        /// <param name="formats">list of formats to try</param>
        private DateTimeMultiFormatConverter(string[] formats)
        {
            for (int i = 0; i < formats.Length; i++)
            {
                if (formats[i] == null ||
                    formats[i] == String.Empty)
                    throw new BadUsageException("The format of the DateTime Converter can be null or empty.");

                try
                {
                    DateTime.Now.ToString(formats[i]);
                }
                catch
                {
                    throw new BadUsageException("The format: '" + formats[i] +
                                                " is invalid for the DateTime Converter.");
                }
            }

            mFormats = formats;
        }

        /// <summary>
        /// Convert a date time value to a string
        /// </summary>
        /// <param name="from">DateTime value to convert</param>
        /// <returns>string DateTime value</returns>
        public override object StringToField(string from)
        {
            if (@from == null)
                @from = String.Empty;

            DateTime val;
            if (!DateTime.TryParseExact(@from.Trim(), mFormats, null, DateTimeStyles.None, out val))
            {
                string extra = " does not match any of the given formats: " + CreateFormats();
                throw new ConvertException(@from, typeof(DateTime), extra);
            }
            return val;
        }

        /// <summary>
        /// Create a list of formats to pass to the DateTime tryparse function
        /// </summary>
        /// <returns>string DateTime value</returns>
        private string CreateFormats()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < mFormats.Length; i++)
            {
                if (i == 0)
                    sb.Append("'" + mFormats[i] + "'");
                else
                    sb.Append(", '" + mFormats[i] + "'");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Convert a date time value to a string (uses first format for output
        /// </summary>
        /// <param name="from">DateTime value to convert</param>
        /// <returns>string DateTime value</returns>
        public override string FieldToString(object from)
        {
            if (@from == null)
                return String.Empty;

            return Convert.ToDateTime(@from).ToString(mFormats[0]);
        }
    }
}