using System;
using System.ComponentModel;

namespace FileHelpers
{
    /// <summary>Base class of <see cref="FieldFixedLengthAttribute"/> and <see cref="FieldDelimiterAttribute"/></summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">Complete Attributes List</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class FieldAttribute : Attribute
    {
        /// <summary>Abstract class, see the derived classes.</summary>
        protected FieldAttribute() {}
    }
}