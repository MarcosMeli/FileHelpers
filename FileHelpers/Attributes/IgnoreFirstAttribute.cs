using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Indicates the number of lines at beginning of the file to be ignored.</summary>
    /// <remarks>
    /// Useful to ignore header records that you are not interested in.
    /// <para/>
    /// See the <a href="http://www.filehelpers.net/must_read">complete attributes list</a> for more
    /// information and examples of each one.
    /// </remarks>

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class IgnoreFirstAttribute : Attribute
    {
        /// <summary>The number of lines at beginning of the file to be ignored.</summary>
        public int NumberOfLines { get; private set; }

        /// <summary>Indicates that the first line of the file is ignored.</summary>
        public IgnoreFirstAttribute()
            : this(1) {}

        /// <summary>Indicates the number of lines at beginning of the file to be ignored.</summary>
        /// <param name="numberOfLines">The number of lines to be ignored.</param>
        public IgnoreFirstAttribute(int numberOfLines)
        {
            NumberOfLines = numberOfLines;
        }
    }
}