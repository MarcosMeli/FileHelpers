using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Indicates the <see cref="AlignMode"/> used for <b>write</b> operations.</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes list</seealso>
    /// <seealso href="quick_start.html">Quick start guide</seealso>
    /// <seealso href="examples.html">Examples of use</seealso>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FieldAlignAttribute : Attribute
    {
        /// <summary>The position of the alignment.</summary>
        public AlignMode Align { get; private set; }

        /// <summary>The character used to align.</summary>
        public char AlignChar { get; private set; }

        #region "  Constructors  "

        /// <summary>Uses the ' ' char to align.</summary>
        /// <param name="align">The position of the alignment.</param>
        public FieldAlignAttribute(AlignMode align)
            : this(align, ' ') {}

        /// <summary>You can indicate the align character.</summary>
        /// <param name="align">The position of the alignment.</param>
        /// <param name="alignChar">The character used to align.</param>
        public FieldAlignAttribute(AlignMode align, char alignChar)
        {
            Align = align;
            AlignChar = alignChar;
        }

        #endregion
    }
}