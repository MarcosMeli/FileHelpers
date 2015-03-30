using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.Interfaces
{
    /// <summary>
    /// Defines methodology for validating fields through FieldValidateAttribute or adding directly to Engine.Options.Fields.Validators.
    /// </summary>
    public interface IFieldValidate
    {
        /// <summary>
        /// Message used when validation fails and a <see cref="ConvertException"/> is thrown.
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Indicates if validation should be done on null / empty string values of the field.  Defaults to false.
        /// </summary>
        bool ValidateNullValue { get; }

        /// <summary>
        /// Used to determine whether a field's raw string value is valid or not.  If false, the engine will throw a <see cref="ConvertException"/>.
        /// </summary>
        /// <returns>Boolean value indicating if the field's value validated properly or not.</returns>
        bool Validate(string value); 
    }
}