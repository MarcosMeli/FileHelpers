using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Indicates the length of a FixedLength field.</summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FieldFixedLengthAttribute : FieldAttribute
    {
        /// <summary>Length of this Fixed Length field.</summary>
        public int Length { get; private set; }

        /// <summary>Indicates the length of a Fixed Length field.</summary>
        /// <param name="length">The length of the field.</param>
        public FieldFixedLengthAttribute(int length)
        {
            if (length > 0)
                this.Length = length;
            else
                throw new BadUsageException("The FieldFixedLength attribute must be > 0");
        }
    }
}