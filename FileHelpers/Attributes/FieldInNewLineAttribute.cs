using System;

namespace FileHelpers
{
    /// <summary>
    /// Indicates the target field has a new line before this value 
    /// i.e. indicates that the records have multiple lines, 
    /// and this field is in the beginning of a line.
    /// </summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldInNewLineAttribute : Attribute
    {
        /// <summary>
        /// Indicates the target field has a new line before this value 
        /// i.e. indicates that the records have multiple lines, 
        /// and this field is in the beginning of a line.
        /// </summary>
        public FieldInNewLineAttribute() {}
    }
}