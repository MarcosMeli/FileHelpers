using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers.Events
{
    /// <summary>Arguments for the <see cref="ReadLineErrorEventArgs"/></summary>
    public sealed class ReadLineErrorEventArgs : EventArgs
    {
        /// <summary>
        /// The current record that contains the errors
        /// </summary>
        public string Line { get; private set; }
        /// <summary>
        /// The line number
        /// </summary>
        public int LineNumber { get; private set; }
        /// <summary>
        /// The type of record that contains the field
        /// </summary>
        public Type RecordType { get; private set; }
        /// <summary>
        /// The errors that have occured
        /// </summary>
        public List<ErrorInfo> Errors { get; private set; }

        /// <summary>
        /// Returns all errors in the line
        /// </summary>
        /// <param name="line">Record that was analysed</param>
        /// <param name="lineNumber">Record number read</param>
        /// <param name="recordType">Type of record</param>
        /// <param name="errors">The errors that have occured</param>
        internal ReadLineErrorEventArgs(string line, int lineNumber, Type recordType, List<ErrorInfo> errors)
            : base()
        {
            this.Line = line;
            this.LineNumber = lineNumber;
            this.Errors = errors;
            this.RecordType = recordType;
        }
    }
}
