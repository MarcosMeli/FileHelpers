using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>
    /// Indicates that the target field cannot contain an empty string value.
    /// This attribute is used for read.
    /// </summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FieldValidateIsNotEmptyAttribute : FieldValidateAttribute
    {
        /// <summary>Indicates that the target field cannot contain an empty string value.</summary>
        public FieldValidateIsNotEmptyAttribute()
        {
            this.Message = "The value is empty and must be populated.";
            this.ValidateNullValue = true;
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
