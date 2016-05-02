using System;
using System.ComponentModel;
using FileHelpers.Interfaces;

namespace FileHelpers
{
    /// <summary>Base class for custom user validation attributes and <see cref="FieldValidateIsNotEmptyAttribute"/>.</summary>
    /// <remarks>See the <a href="http://www.filehelpers.net/mustread">complete attributes list</a> for more information and examples of each one.</remarks>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class FieldValidateAttribute : Attribute, IFieldValidate
    {
        /// <summary>Message used when validation fails and a <see cref="ConvertException"/> is thrown.</summary>
        protected internal string Message { get; set; }

        /// <summary>Indicates if validation should be done on null / empty string values of the field.  Defaults to false.</summary>
        protected internal bool ValidateNullValue { get; set; }

        /// <summary>
        /// Used to determine whether a field's raw string value is valid or not.  If false, the engine will throw a <see cref="ConvertException"/>.
        /// </summary>
        /// <returns>Boolean value indicating if the field's value validated properly or not.</returns>
        public abstract bool Validate(string value);

        /// <summary>Abstract class, see inheritors.</summary>
        protected FieldValidateAttribute()
        {
            Message = "Field value is invalid.";
            ValidateNullValue = false;
        }

        // IFieldValidate Support
        bool IFieldValidate.Validate(string value)
        {
            return Validate(value);
        }

        string IFieldValidate.Message
        {
            get
            {
                return Message;
            }
        }

        bool IFieldValidate.ValidateNullValue
        {
            get
            {
                return ValidateNullValue;
            }
        }
    }
}