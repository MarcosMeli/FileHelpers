using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>
    /// Indicates that the target field cannot contain an empty string value.
    /// This attribute is used for read.
    /// </summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FieldNotEmptyAttribute : Attribute {}
}