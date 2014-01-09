using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FileHelpers.Options;

namespace FileHelpers
{
    /// <summary>
    /// This version of the <see cref="FileHelperEngine"/> is exclusively
    /// for delimited records. It allows you to change the delimiter and
    /// other options at runtime
    /// </summary>
    /// <remarks>
    /// Useful when you need to export or import the same info with 2 or
    /// more different delimiters or slightly different options.
    /// </remarks>
    [DebuggerDisplay(
        "DelimitedFileEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}"
        )]
    public sealed class DelimitedFileEngine
        : FileHelperEngine
    {
        /// <summary>
        /// Create a version of the <see cref="FileHelperEngine"/> exclusively
        /// for delimited records. It allows you to change the delimiter and
        /// other options at runtime
        /// </summary>
        /// <remarks>
        /// Useful when you need to export or import the same info with 2 or
        /// more different delimiters or slightly different options.
        /// </remarks>
        /// <param name="recordType">The record mapping class.</param>
        public DelimitedFileEngine(Type recordType)
            : base(recordType)
        {
            if (!RecordInfo.IsDelimited) {
                throw new BadUsageException(
                    "The Delimited Engine only accepts record types marked with DelimitedRecordAttribute");
            }
        }

        /// <summary>
        /// Create a delimited engine of record type and encoding
        /// </summary>
        /// <param name="recordType">Type of record to read</param>
        /// <param name="encoding">Encoding of each record</param>
        public DelimitedFileEngine(Type recordType, Encoding encoding)
            : this(recordType)
        {
            Encoding = encoding;
        }

        /// <summary>
        /// Allow changes in the record layout like delimiters and others
        /// common settings.
        /// </summary>
        public new DelimitedRecordOptions Options
        {
            get { return (DelimitedRecordOptions) base.Options; }
        }
    }


    /// <summary>
    /// A version of the <see cref="FileHelperEngine"/> exclusively for 
    /// delimited records. It allows you to change the delimiter and other
    /// options at runtime
    /// </summary>
    /// <remarks>
    /// Useful when you need to export or import the same info with 2 or more
    /// more different delimiters or slightly different options.
    /// </remarks>
    [DebuggerDisplay(
        "DelimitedFileEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}"
        )]
    public sealed class DelimitedFileEngine<T>
        : FileHelperEngine<T>
        where T : class
    {
        #region "  Constructor  "

        /// <summary>
        /// Create a version of the <see cref="FileHelperEngine"/> exclusively
        /// for delimited records. It allows you to change the delimiter and
        /// other options at runtime
        /// </summary>
        /// <remarks>
        /// Useful when you need to export or import the same info with 2 or
        /// more different delimiters or slightly different options.
        /// </remarks>
        public DelimitedFileEngine()
        {
            if (!RecordInfo.IsDelimited) {
                throw new BadUsageException(
                    "The Delimited Engine only accepts Record Types marked with DelimitedRecordAttribute");
            }
        }

        /// <summary>
        /// Create a Delimited engine with file type of Encoding
        /// </summary>
        /// <param name="encoding">Type of encoding on file</param>
        public DelimitedFileEngine(Encoding encoding)
            : this()
        {
            Encoding = encoding;
        }

        #endregion

        /// <summary>
        /// Allows changes in the record layout like delimiters and others
        /// common settings.
        /// </summary>
        public new DelimitedRecordOptions Options
        {
            get { return (DelimitedRecordOptions) base.Options; }
        }
    }
}