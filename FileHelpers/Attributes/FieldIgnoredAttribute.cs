using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace FileHelpers
{
    /// <summary>Hides the field to the library. Obsolete: You must use [FieldNotInFile]</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
    /// <seealso href="quick_start.html">Quick Start Guide</seealso>
    /// <seealso href="examples.html">Examples of Use</seealso>
    [AttributeUsage(AttributeTargets.Field)]
    [Obsolete("You must use [FieldNotInFile] instead", false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class FieldIgnoredAttribute
        : FieldAttribute {}
}