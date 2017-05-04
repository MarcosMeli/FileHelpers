using System;

namespace FileHelpers
{
    /// <summary>
    /// Allows you to set the length or bounds that the target array field must have.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldArrayLengthAttribute : Attribute
    {
        #region "  Constructors  "

        /// <summary>
        /// Allows you to set the bounds that the target array field must have.
        /// </summary>
        /// <param name="minLength">The lower bound</param>
        /// <param name="maxLength">The upper bound</param>
        public FieldArrayLengthAttribute(int minLength, int maxLength)
        {
            MinLength = minLength;
            MaxLength = maxLength;
        }

        /// <summary>
        /// Allow you to set the exact length that the target array field must have.
        /// </summary>
        /// <param name="length">The exact length of the array field.</param>
        public FieldArrayLengthAttribute(int length)
            : this(length, length) {}

        #endregion

        /// <summary>Array lower bound.</summary>
        public int MinLength { get; private set; }

        /// <summary>Array upper bound.</summary>
        public int MaxLength { get; private set; }
    }
}