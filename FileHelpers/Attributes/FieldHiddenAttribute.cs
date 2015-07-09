using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Hides the field to the library, the library does not use the
    /// target field at all. Nor for read and write
    /// <para/>
    /// Note: If the field is in the record structure but you want to discard the values in that position use <see
    /// cref="FieldValueDiscardedAttribute"/></summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FieldHiddenAttribute
        : FieldAttribute { }
}