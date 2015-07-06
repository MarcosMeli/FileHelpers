using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>
    /// The number of lines to be ignored at the end of the file.
    /// </summary>
    /// <remarks>
    /// This is useful to discard trailer records from an incoming file.
    /// <para/>
    /// See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class IgnoreLastAttribute : Attribute
    {
        /// <summary> The number of lines to be ignored at end of the file. </summary>
        public int NumberOfLines { get; private set; }

        /// <summary>Indicates that the last line of the file will be ignored.</summary>
        public IgnoreLastAttribute()
            : this(1) {}

        /// <summary>Indicates the number of lines at end of the file that will be ignored.</summary>
        /// <param name="numberOfLines">The number of lines to be ignored at end of the file.</param>
        public IgnoreLastAttribute(int numberOfLines)
        {
            NumberOfLines = numberOfLines;
        }
    }
}