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
        /// The type of the record
        /// </summary>
        public Type RecordType { get; set; }
        /// <summary>
        /// Indicates whether to stop processing the file
        /// </summary>
        public bool Continue { get; set; }
        /// <summary>
        /// After an exception occurs when extracting the value of a field from the line
        /// </summary>
        /// <param name="error">The error that occured</param>
        /// <param name="field">The field that contains the error</param>
        /// <param name="recordType">The type of the record</param>
        internal ReadFieldErrorEventArgs(ErrorInfo error, FieldBase field, Type recordType)
            : base()
        {
            this.Error = error;
            this.Field = field;
            this.RecordType = recordType;
        }
    }
}
