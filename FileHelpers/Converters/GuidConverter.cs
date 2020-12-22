using System;

namespace FileHelpers.Converters
{
    /// <summary>
    ///  Convert a GUID to and from a field value
    /// </summary>
    internal sealed class GuidConverter : ConverterBase
    {
        /// <summary>
        /// D or N or B or P (default is D: see Guid.ToString(string format))
        /// </summary>
        private readonly string mFormat;

        /// <summary>
        /// Create a GUID converter with the default format code "D"
        /// </summary>
        public GuidConverter()
            : this("D") // D or N or B or P (default is D: see Guid.ToString(string format))
        { }

        /// <summary>
        /// Create a GUID converter with formats as defined for GUID
        /// N, D, B or P
        /// </summary>
        /// <param name="format">Format code for GUID</param>
        public GuidConverter(string format)
        {
            if (string.IsNullOrEmpty(format))
                format = "D";

            format = format.Trim().ToUpper();

            if (!(format == "N" || format == "D" || format == "B" || format == "P"))
                throw new BadUsageException("The format of the Guid Converter must be N, D, B or P.");

            mFormat = format;
        }

        /// <summary>
        /// Convert a GUID string to a GUID object for the record object
        /// </summary>
        /// <param name="from">String representation of the GUID</param>
        /// <returns>GUID object or GUID empty</returns>
        public override object StringToField(string from)
        {
            if (string.IsNullOrEmpty(from))
                return Guid.Empty;

            try
            {
                return new Guid(from);
            }
            catch
            {
                throw new ConvertException(from, typeof(Guid));
            }
        }

        /// <summary>
        /// Output GUID as a string field
        /// </summary>
        /// <param name="from">Guid object</param>
        /// <returns>GUID as a string depending on format</returns>
        public override string FieldToString(object from)
        {
            if (from == null)
                return string.Empty;
            return ((Guid)from).ToString(mFormat);
        }
    }
}