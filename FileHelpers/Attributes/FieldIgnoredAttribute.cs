using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace FileHelpers
{
    /// <summary>Hides the field to the library. Obsolete: You must use [FieldHidden]</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    [AttributeUsage(AttributeTargets.Field)]
    [Obsolete("You must use [FieldHidden] instead", false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class FieldIgnoredAttribute
        : FieldAttribute {}
}