using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Hides the field to the library, the library does not use the
    /// target field at all. It is your responsibility to set the value.
    /// <para/>
    /// Note: If the field is in the source file but you want to discard the values in that position use <see
    /// cref="FieldValueDiscardedAttribute"/></summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
    /// <seealso href="quick_start.html">Quick Start Guide</seealso>
    /// <seealso href="examples.html">Examples of Use</seealso>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FieldNotInFileAttribute : FieldAttribute
    {
    }
}