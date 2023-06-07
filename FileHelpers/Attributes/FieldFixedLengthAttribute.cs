using System;
using FileHelpers.Enums;

namespace FileHelpers
{
    /// <summary>Indicates the length of a FixedLength field.</summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldFixedLengthAttribute : FieldAttribute
    {
        /// <summary>Length of this Fixed Length field.</summary>
        public int Length { get; private set; }

        /// <summary>
        /// Length to offset the field by.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Overflow behavior for this Fixed Length field.
        /// By default uses <code>OverflowMode.DiscardEnd</code>.
        /// </summary>
        public OverflowMode OverflowMode { get; set; } = OverflowMode.DiscardEnd;

        /// <summary>Indicates the length of a Fixed Length field.</summary>
        /// <param name="length">The length of the field.</param>
        public FieldFixedLengthAttribute(int length)
        {
            if (length > 0)
                Length = length;
            else
                throw new BadUsageException("The FieldFixedLength attribute must be > 0");
        }
    }
}