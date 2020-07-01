using System;
using System.Diagnostics;
using System.Text;
using FileHelpers.Options;

namespace FileHelpers
{
    /// <summary>
    /// This version of the <see cref="FileHelperEngine"/> is exclusively for 
    /// fixed length records. It allows you to change options at runtime
    /// </summary>
    /// <remarks>
    /// Useful when you need to export or import the same info with slightly
    /// different options.
    /// </remarks>
    [DebuggerDisplay(
        "FixedFileEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}"
        )]
    public sealed class FixedFileEngine : FileHelperEngine
    {
        #region "  Constructor  "

        /// <summary>
        /// This version of the <see cref="FileHelperEngine"/> is exclusively for 
        /// fixed length records. It allows you to change options at runtime
        /// </summary>
        /// <remarks>
        /// Useful when you need to export or import the same info with
        /// slightly different options.
        /// </remarks>
        /// <param name="recordType">The record mapping class.</param>
        public FixedFileEngine(Type recordType)
            : base(recordType)
        {
            if (RecordInfo.IsDelimited) {
                throw new BadUsageException(
                    "The FixedFileEngine only accepts Record Types marked with the FixedLengthRecord attribute");
            }
        }

        /// <summary>
        /// Read a record with fixed length fields
        /// </summary>
        /// <param name="recordType">record type to read</param>
        /// <param name="encoding">Encoding to use</param>
        public FixedFileEngine(Type recordType, Encoding encoding)
            : this(recordType)
        {
            Encoding = encoding;
        }

        #endregion

        /// <summary>Allow changes some fixed length options and others common settings.</summary>
        public new FixedRecordOptions Options
        {
            get { return (FixedRecordOptions) base.Options; }
        }
    }


    /// <summary>
    /// This version of the <see cref="FileHelperEngine"/> is exclusively for 
    /// fixed length records. It allows you to change options at runtime
    /// </summary>
    /// <remarks>
    /// Useful when you need to export or import the same info with slightly
    /// different options.
    /// </remarks>
    [DebuggerDisplay(
        "FixedFileEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}"
        )]
    public sealed class FixedFileEngine<T>
        : FileHelperEngine<T>
        where T : class
    {
        #region "  Constructor  "

        /// <summary>
        /// Creates a version of the <see cref="FileHelperEngine"/> exclusively
        /// for fixed length records that allow you to change options at runtime
        /// </summary>
        /// <remarks>
        /// Useful when you need to export or import the same info with
        /// slightly different options.
        /// </remarks>
        public FixedFileEngine()
        {
            if (RecordInfo.IsDelimited) {
                throw new BadUsageException(
                    "The FixedFileEngine only accepts Record Types marked with the FixedLengthRecord attribute");
            }
        }

        /// <summary>
        /// Creates a version of the <see cref="FileHelperEngine"/> exclusively
        /// for fixed length records that allow you to change options at runtime
        /// </summary>
        /// <remarks>
        /// Useful when you need to export or import the same info with
        /// slightly different options.
        /// </remarks>
        /// <param name="encoding">Encoding of file to be read</param>
        public FixedFileEngine(Encoding encoding)
            : this()
        {
            Encoding = encoding;
        }

        #endregion

        /// <summary>Allow changes some fixed length options and others common settings.</summary>
        public new FixedRecordOptions Options
        {
            get { return (FixedRecordOptions) base.Options; }
        }
    }
}