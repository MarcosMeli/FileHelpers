using System;

namespace FileHelpers
{
    /// <summary>Indicates the behavior of quoted fields.</summary>
    public enum QuoteMode
    {
        /// <summary>
        /// The engines expects that the field must always be surrounded with
        /// quotes when reading and always adds the quotes when writing.
        /// </summary>
        AlwaysQuoted = 0,
        /// <summary>
        /// The engine can handle a field even if it is not surrounded with
        /// quotes while reading but it always add the quotes when writing.
        /// </summary>
        OptionalForRead,
        /// <summary>
        /// The engine always expects a quote when read and it will only add
        /// the quotes when writing only if the field contains quotes, new
        /// lines or the separator char.
        /// </summary>
        OptionalForWrite,
        /// <summary>
        /// The engine can handle a field even if it is not surrounded with
        /// quotes while reading and it will only add the quotes when writing
        /// if the field contains quotes, new lines or the separator char.
        /// </summary>
        OptionalForBoth
    }
}