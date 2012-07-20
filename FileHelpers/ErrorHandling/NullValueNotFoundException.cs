using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
    /// <summary>Indicates the wrong usage of the library.</summary>
    [Serializable]
    public class NullValueNotFoundException : BadUsageException
    {
        /// <summary>Creates an instance of an NullValueNotFoundException.</summary>
        /// <param name="message">The exception Message</param>
        protected internal NullValueNotFoundException(string message)
            : base(message)
        {
        }
        /// <summary>Creates an instance of an NullValueNotFoundException.</summary>
        /// <param name="message">The exception Message</param>
        /// <param name="line">The line number where the problem was found</param>
        /// <param name="column">The column number where the problem was found</param>
        protected internal NullValueNotFoundException(int line, int column, string message)
            : base(line, column, message)
        {
        }
        /// <summary>Creates an instance of an NullValueNotFoundException.</summary>
        /// <param name="message">The exception Message</param>
        /// <param name="line">Line to display in message</param>
        internal NullValueNotFoundException(LineInfo line, string message)
            : base(line, message)
        {
        }
    }
}