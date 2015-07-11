using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>
    /// Indicates that the target field might be on the source file.
    /// If it is not present then the value will be null (TODO: Check null)
    /// This attribute is used for read.
    /// </summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/must_read">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FieldOptionalAttribute : Attribute {}
}