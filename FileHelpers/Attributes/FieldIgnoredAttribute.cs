using System;
using System.ComponentModel;

namespace FileHelpers
{
    /// <summary>Hides the field to the library. Obsolete: You must use [FieldHidden]</summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>
    [AttributeUsage(AttributeTargets.Field)]
    [Obsolete("You must use [FieldHidden] instead", false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class FieldIgnoredAttribute
        : FieldAttribute {}
}