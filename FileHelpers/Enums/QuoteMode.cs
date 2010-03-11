

using System;

namespace FileHelpers
{
	/// <summary>Indicates the behavior of quoted fields.</summary>
	public enum QuoteMode
	{
		/// <summary>
        /// The engines expect that the field must always be surrounded with quotes while reading and 
        /// always adds the quotes when writing.
        /// </summary>
		AlwaysQuoted = 0,
		/// <summary>
        /// The engine can handle a field even if it is not surrounded with quotes while reading 
        /// but it always adds the quotes when writing.
        /// </summary>
		OptionalForRead,
		/// <summary>
        /// The engine always expects a quote when read and adds the quotes 
        /// and it will adds the quotes when writing only if the field contains quotes, new lines or the separator char.
        /// </summary>
		OptionalForWrite,
		/// <summary>
        /// The engine can handle a field even if it is not surrounded with quotes while reading 
        /// and it will adds the quotes when writing only if the field contains quotes, new lines or the separator char.
        /// </summary>
		OptionalForBoth
	}
}