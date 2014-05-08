using FileHelpers.Resources;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpers
{
	/// <summary>
	/// Indicates that a string value can't be converted to a dest type.
	/// </summary>
    [Serializable]
    public sealed class ConvertException : FileHelpersException
    {
        #region "  Fields & Property  "

	    /// <summary>The destination type.</summary>
	    public Type FieldType { get; private set; }

	    /// <summary>The value that can't be converted. (null for unknown)</summary>
	    public string FieldStringValue { get; private set; }

	    /// <summary>Extra info about the error.</summary>
	    public string MessageExtra { get; private set; }

	    /// <summary>The message without the Line, Column and FieldName.</summary>
	    public string MessageOriginal { get; private set; }

	    /// <summary>The name of the field related to the exception. (null for unknown)</summary>
	    public string FieldName { get; internal set; }

	    /// <summary>The line where the error was found. (-1 is unknown)</summary>
	    public int LineNumber { get; internal set; }

	    /// <summary>The estimate column where the error was found. (-1 is unknown)</summary>
	    public int ColumnNumber { get; internal set; }

	    #endregion

        #region "  Constructors  "

        /// <summary>
        /// Create a new ConvertException object
        /// </summary>
        /// <param name="origValue">The value to convert.</param>
        /// <param name="destType">The destination Type.</param>
        public ConvertException(string origValue, Type destType)
            : this(origValue, destType, string.Empty)
        {
        }


        /// <summary>
        /// Create a new ConvertException object
        /// </summary>
        /// <param name="origValue">The value to convert.</param>
        /// <param name="destType">The destination Type.</param>
        /// <param name="extraInfo">Additional info of the error.</param>
        public ConvertException(string origValue, Type destType, string extraInfo)
            : this(origValue, destType, string.Empty, -1, -1, extraInfo, null)
        {
        }

        /// <summary>
        /// Create a new ConvertException object
        /// </summary>
        /// <param name="origValue">The value to convert.</param>
        /// <param name="destType">The destination Type.</param>
        /// <param name="extraInfo">Additional info of the error.</param>
        /// <param name="columnNumber">The estimated column number.</param>
        /// <param name="lineNumber">The line where the error was found.</param>
        /// <param name="fieldName">The name of the field with the error</param>
        /// <param name="innerEx">The Inner Exception</param>
        public ConvertException(string origValue, Type destType, string fieldName, int lineNumber, int columnNumber, string extraInfo, Exception innerEx)
            : base(MessageBuilder(origValue, destType, fieldName, lineNumber, columnNumber, extraInfo), innerEx)
        {
            MessageOriginal = string.Empty;
            FieldStringValue = origValue;
            FieldType = destType;
            LineNumber = lineNumber;
            ColumnNumber = columnNumber;
            FieldName = fieldName;
            MessageExtra = extraInfo;

            if (origValue != null && destType != null)
                MessageOriginal = String.Format(Errors.ValueError, origValue, destType.Name);

        }

        private static string MessageBuilder(string origValue, Type destType, string fieldName, int lineNumber, int columnNumber, string extraInfo)
        {
            var res = string.Empty;
            if (lineNumber >= 0)
                res += String.Format(Errors.LineError, lineNumber);

            if (columnNumber >= 0)
                res += String.Format(Errors.ColumnError, columnNumber);

            if (!string.IsNullOrEmpty(fieldName))
                res += String.Format(Errors.FieldError, fieldName);

            if (origValue != null && destType != null)
                res += String.Format(Errors.ValueError, origValue, destType.Name);

            res += extraInfo;

            return res;
        }

        #endregion
    }
}