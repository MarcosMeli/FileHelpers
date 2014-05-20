using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>
    /// Indicates the relative order of the current field.
    /// Note: If you use this property for one field you
    /// must to use it for all fields.
    /// </summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
    /// <seealso href="quick_start.html">Quick Start Guide</seealso>
    /// <seealso href="examples.html">Examples of Use</seealso>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property )]
    public sealed class FieldOrderAttribute : Attribute
    {
        /// <summary>The relative position order of this field.</summary>
        public int Order { get; private set; }

        /// <summary>
        /// Indicates the relative order of the current field.
        /// Note:If you use this property for one field you must to use it for
        /// all fields.
        /// </summary>
        /// <param name="order">Indicates the relative order of the current field</param>
        public FieldOrderAttribute(int order)
        {
            Order = order;
        }
    }
}