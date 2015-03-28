using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>
    /// Indicates that the target field cannot contain an empty string value.
    /// This attribute is used for read.
    /// </summary>
    /// <remarks>See the <a href="attributes.html">complete attributes list</a> for more information and examples of each one.</remarks>
    /// <seealso href="attributes.html">Attributes List</seealso>
    /// <seealso href="quick_start.html">Quick Start Guide</seealso>
    /// <seealso href="examples.html">Examples of Use</seealso>
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class FieldValidateIsNotEmptyAttribute : FieldValidateAttribute
    {
        /// <summary>Indicates that the target field cannot contain an empty string value.</summary>
        public FieldValidateIsNotEmptyAttribute()
            : base(message:"The value is empty and must be populated.")
        {
        }

        /// <summary>
        /// Validates to ensure that the extracted string value is not empty.
        /// </summary>
        /// <param name="value">The extracted string value for the field.</param>
        /// <returns>Value indicating that the field is not empty.</returns>
        public override bool Validate(string value)
        {
            return !String.IsNullOrEmpty(value);
        }
    }
}
