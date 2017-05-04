using System;
using System.ComponentModel;

namespace FileHelpers
{
    /// <summary>Base class for the record types..</summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>
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