using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Indicates that this class represents a delimited record. </summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DelimitedRecordAttribute : TypedRecordAttribute
    {
        /// <summary>The string used as a field separator.</summary>
        public string Separator { get; private set; }

        /// <summary>Indicates that this class represents a delimited record. </summary>
        /// <param name="delimiter">The separator string used to split the fields of the record.</param>
        public DelimitedRecordAttribute(string delimiter)
        {
            if (Separator != String.Empty)
                this.Separator = delimiter;
            else
                throw new ArgumentException("Given delimiter cannot be <> \"\"", "delimiter");
        }
    }
}