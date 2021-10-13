using System;
using System.Globalization;
using System.Linq;

namespace FileHelpers.Converters
{
    /// <summary>
    /// Convert a numeric value with separators into a value
    /// </summary>
    internal abstract class CultureConverter
        : ConverterBase
    {
        /// <summary>
        /// Culture information based on the separator
        /// </summary>
        protected CultureInfo mCulture;

        /// <summary>
        /// Type for field being converted
        /// </summary>
        protected Type mType;

        /// <summary>
        /// Convert to a type given a decimal separator
        /// </summary>
        /// <param name="T">type we are converting</param>
        /// <param name="decimalSepOrCultureName">Separator or culture name (eg. 'en-US', 'fr-FR'...)</param>
        protected CultureConverter(Type T, string decimalSepOrCultureName)
        {
            mCulture = CreateCulture(decimalSepOrCultureName);
            mType = T;
        }

        /// <summary>
        /// Convert the field to a string representation
        /// </summary>
        /// <param name="from">Object to convert</param>
        /// <returns>string representation</returns>
        public sealed override string FieldToString(object from)
        {
            if (from == null)
                return string.Empty;

            return ((IConvertible)from).ToString(mCulture);
        }

        /// <summary>
        /// Return culture information for with comma decimal separator or comma decimal separator
        /// </summary>
        /// <param name="decimalSepOrCultureName">Decimal separator string or culture name</param>
        /// <returns>Cultural information based on separator</returns>
        private static CultureInfo CreateCulture(string decimalSepOrCultureName)
        {
            // Array of all allowed decimal separators
            string[] allowedDecimalSeparators = { ".", "," };

            CultureInfo ci;

            if (allowedDecimalSeparators.Contains(decimalSepOrCultureName))
            {
                ci = new CultureInfo(CultureInfo.CurrentCulture.Name);

                if (decimalSepOrCultureName == ".")
                {
                    ci.NumberFormat.NumberDecimalSeparator = ".";
                    ci.NumberFormat.NumberGroupSeparator = ",";
                }
                else if (decimalSepOrCultureName == ",")
                {
                    ci.NumberFormat.NumberDecimalSeparator = ",";
                    ci.NumberFormat.NumberGroupSeparator = ".";
                }
                else
                    throw new BadUsageException("You can only use '.' or ',' as decimal or group separators");
            }
            else
            {
                ci = CultureInfo.GetCultureInfo(decimalSepOrCultureName);
            }

            return ci;
        }
    }
}