using System;

namespace FileHelpers
{
    /// <summary>Indicates a different caption for this field, which overrides FieldFriendlyName when calling GetFileHeader. </summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldCaptionAttribute : Attribute
    {
        /// <summary>
        /// Gets the caption for this field
        /// </summary>
        public string Caption { get; private set; }

        /// <summary>Indicates a different caption for this field. </summary>
        /// <param name="caption">The string used for the field in the header row.</param>
        public FieldCaptionAttribute(string caption)
        {
            Caption = caption;
        }
    }
}