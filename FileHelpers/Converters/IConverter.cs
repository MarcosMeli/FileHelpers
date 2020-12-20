namespace FileHelpers.Converters
{
    /// <summary>
    /// Can be used both from Attributes as with the fluent configuration.
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// Convert a string in the file to a field value.
        /// </summary>
        /// <param name="from">The string to convert.</param>
        /// <returns>The Field value.</returns>
        object StringToField(string from);

        /// <summary>
        /// Convert a field value to an string to write this to the file.
        /// </summary>
        /// <remarks>The basic implementation just returns  from.ToString();</remarks>
        /// <param name="from">The field values to convert, can be null</param>
        /// <returns>The string representing the field value, must return a string, can be string.empty</returns>
        string FieldToString(object from);
    }
}