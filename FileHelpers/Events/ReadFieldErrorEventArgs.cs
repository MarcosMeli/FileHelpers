using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Events
{
    /// <summary>Arguments for the <see cref="ReadFieldErrorEventArgs"/></summary>
    public sealed class ReadFieldErrorEventArgs : EventArgs
    {
        /// <summary>
        /// The error that occured
        /// </summary>
        public ErrorInfo Error { get; set; }
        /// <summary>
        /// The field that failed
        /// </summary>
        public FieldBase Field { get; set; }

        /// <summary>
        /// After an exception occurs when extracting the value of a field from the line
        /// </summary>
        /// <param name="error">The error that occured</param>
        /// <param name="field">Was it changed before</param>
        internal ReadFieldErrorEventArgs(ErrorInfo error, FieldBase field)
            : base()
        {
            this.Error = error;
            this.Field = field;
        }
    }
}
