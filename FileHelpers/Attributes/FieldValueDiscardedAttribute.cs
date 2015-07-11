using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Discards the values for the target field.
    /// Note: If the field <b>is not</b> on the source file you must use <see cref="FieldHiddenAttribute"/></summary>
    /// <remarks>
    /// <para/>
    /// See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more
    /// information and examples of each one.
    /// </remarks>

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FieldValueDiscardedAttribute : Attribute {}
}