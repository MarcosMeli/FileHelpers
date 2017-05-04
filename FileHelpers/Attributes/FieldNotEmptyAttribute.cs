using System;

namespace FileHelpers
{
    /// <summary>
    /// Indicates that the target field cannot contain an empty string value.
    /// This attribute is used for read.
    /// </summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldNotEmptyAttribute : Attribute {}
}