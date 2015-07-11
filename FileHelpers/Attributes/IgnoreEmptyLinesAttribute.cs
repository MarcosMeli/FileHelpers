using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Indicates that the engine will ignore the empty lines while reading.</summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class IgnoreEmptyLinesAttribute : Attribute
    {
        /// <summary>Ignore lines consisting of only whitespace.</summary>
        public bool IgnoreSpaces { get; private set; }

        /// <summary>Indicates that the engine will ignore the empty lines while reading.</summary>
        public IgnoreEmptyLinesAttribute()
        {
            IgnoreSpaces = false;
        }

        /// <summary>Indicates that the engine will ignore the empty lines while reading.</summary>
        /// <param name="ignoreSpaces">Ignore lines consisting of only whitespace.</param>
        public IgnoreEmptyLinesAttribute(bool ignoreSpaces)
        {
            IgnoreSpaces = ignoreSpaces;
        }
    }
}