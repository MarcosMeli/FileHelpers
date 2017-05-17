using System;

namespace FileHelpers
{
    /// <summary>Indicates a different delimiter for this field. </summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldDelimiterAttribute : FieldAttribute
    {
        /// <summary>
        /// Gets the Delimiter for this field
        /// </summary>
        public string Delimiter { get; private set; }

        /// <summary>Indicates a different delimiter for this field. </summary>
        /// <param name="separator">The separator string used to split the fields of the record.</param>
        public FieldDelimiterAttribute(string separator)
        {
            if (string.IsNullOrEmpty(separator)) {
                throw new BadUsageException(
                    "The separator parameter of the FieldDelimiter attribute can't be null or empty");
            }
            else
                this.Delimiter = separator;
        }
    }
}