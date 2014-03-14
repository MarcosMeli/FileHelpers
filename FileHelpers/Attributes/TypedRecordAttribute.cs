using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace FileHelpers
{
    /// <summary>Base class for the record types..</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes list</seealso>
    /// <seealso href="quick_start.html">Quick start guide</seealso>
    /// <seealso href="examples.html">Examples of use</seealso>
    [AttributeUsage(AttributeTargets.Class)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class TypedRecordAttribute
        : Attribute
    {
        #region "  Constructors  "

        /// <summary>Abstract class, see inheritors.</summary>
        protected TypedRecordAttribute() {}

        #endregion
    }
}