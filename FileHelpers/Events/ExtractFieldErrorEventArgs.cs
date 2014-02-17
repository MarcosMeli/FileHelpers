using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Events
{
    /// <summary>Arguments for the <see cref="ExtractFieldErrorEventArgs"/></summary>
    public sealed class ExtractFieldErrorEventArgs : EventArgs
    {
        /// <summary>
        /// The current record that contains the field
        /// </summary>
        public string Line { get; set; }
        /// <summary>
        /// The line number
        /// </summary>
        public int LineNumber { get; set; }
        /// <summary>
        /// The field that failed
        /// </summary>
        public FieldBase Field { get; set; }
        /// <summary>
        /// The exception that occured
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// After an exception occurs when extracting the value of a field from the line
        /// </summary>
        /// <param name="line">Record that was analysed</param>
        /// <param name="lineNumber">Record number read</param>
        /// <param name="field">Was it changed before</param>
        /// <param name="exception">Object created</param>
        internal ExtractFieldErrorEventArgs(string line, int lineNumber, FieldBase field, Exception exception)
            : base()
		{
            this.Line = line;
            this.LineNumber = lineNumber;
            this.Field = field;
            this.Exception = exception;
		}
	}
}
