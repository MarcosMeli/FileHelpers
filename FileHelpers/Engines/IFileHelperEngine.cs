using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Text;
using FileHelpers.Events;
using FileHelpers.Options;

namespace FileHelpers
{
    public interface IFileHelperEngine<T> where T : class
    {
        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadFile/*'/>
        T[] ReadFile(string fileName);

        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadFile/*'/>
        /// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
        T[] ReadFile(string fileName, int maxRecords);

        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadStream/*'/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        T[] ReadStream(TextReader reader);

        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadStream/*'/>
        /// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        T[] ReadStream(TextReader reader, int maxRecords);

        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadString/*'/>
        T[] ReadString(string source);

        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadString/*'/>
        /// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
        T[] ReadString(string source, int maxRecords);

        /// <include file='FileHelperEngine.docs.xml' path='doc/WriteFile/*'/>
        void WriteFile(string fileName, IEnumerable<T> records);

        /// <include file='FileHelperEngine.docs.xml' path='doc/WriteFile2/*'/>
        void WriteFile(string fileName, IEnumerable<T> records, int maxRecords);

        /// <include file='FileHelperEngine.docs.xml' path='doc/WriteStream/*'/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void WriteStream(TextWriter writer, IEnumerable<T> records);

        /// <include file='FileHelperEngine.docs.xml' path='doc/WriteStream2/*'/>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        void WriteStream(TextWriter writer, IEnumerable<T> records, int maxRecords);

        /// <include file='FileHelperEngine.docs.xml' path='doc/WriteString/*'/>
        string WriteString(IEnumerable<T> records);

        /// <include file='FileHelperEngine.docs.xml' path='doc/WriteString2/*'/>
        string WriteString(IEnumerable<T> records, int maxRecords);

        /// <include file='FileHelperEngine.docs.xml' path='doc/AppendToFile1/*'/>
        void AppendToFile(string fileName, T record);

        /// <include file='FileHelperEngine.docs.xml' path='doc/AppendToFile2/*'/>
        void AppendToFile(string fileName, IEnumerable<T> records);

        /// <summary>
        /// Read the records of the file and fill a DataTable with them
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <returns>The DataTable with the read records.</returns>
        DataTable ReadFileAsDT(string fileName);

        /// <summary>
        /// Read the records of the file and fill a DataTable with them
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
        /// <returns>The DataTable with the read records.</returns>
        DataTable ReadFileAsDT(string fileName, int maxRecords);

        /// <summary>
        /// Read the records of a string and fill a DataTable with them.
        /// </summary>
        /// <param name="source">The source string with the records.</param>
        /// <returns>The DataTable with the read records.</returns>
        DataTable ReadStringAsDT(string source);

        /// <summary>
        /// Read the records of a string and fill a DataTable with them.
        /// </summary>
        /// <param name="source">The source string with the records.</param>
        /// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
        /// <returns>The DataTable with the read records.</returns>
        DataTable ReadStringAsDT(string source, int maxRecords);

        /// <summary>
        /// Read the records of the stream and fill a DataTable with them
        /// </summary>
        /// <param name="reader">The stream with the source records.</param>
        /// <returns>The DataTable with the read records.</returns>
        DataTable ReadStreamAsDT(TextReader reader);

        /// <summary>
        /// Read the records of the stream and fill a DataTable with them
        /// </summary>
        /// <param name="reader">The stream with the source records.</param>
        /// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
        /// <returns>The DataTable with the read records.</returns>
        DataTable ReadStreamAsDT(TextReader reader, int maxRecords);

        /// <summary>Called in read operations just before the record string is translated to a record.</summary>
        event BeforeReadHandler<T> BeforeReadRecord;

        /// <summary>Called in read operations just after the record was created from a record string.</summary>
        event AfterReadHandler<T> AfterReadRecord;

        /// <summary>Called in write operations just before the record is converted to a string to write it.</summary>
        event BeforeWriteHandler<T> BeforeWriteRecord;

        /// <summary>Called in write operations just after the record was converted to a string.</summary>
        event AfterWriteHandler<T> AfterWriteRecord;

        /// <summary>
        /// Allows to change some record layout options at runtime
        /// </summary>
        RecordOptions Options { get; }

        /// <include file='FileHelperEngine.docs.xml' path='doc/LineNum/*'/>
        int LineNumber { get; }

        /// <include file='FileHelperEngine.docs.xml' path='doc/TotalRecords/*'/>
        int TotalRecords { get; }

        /// <include file='FileHelperEngine.docs.xml' path='doc/RecordType/*'/>
        Type RecordType { get; }

        /// <summary>The read header in the last read operation. If any.</summary>
        string HeaderText { get; set; }

        /// <summary>The read footer in the last read operation. If any.</summary>
        string FooterText { get; set; }

        /// <summary>The encoding to Read and Write the streams. Default is the system's current ANSI code page.</summary>
        /// <value>Default is the system's current ANSI code page.</value>
        Encoding Encoding { get; set; }

        /// <summary>This is a common class that manage the errors of the library.</summary>
        /// <remarks>You can, for example, get the errors, their number, Save them to a file, etc.</remarks>
        ErrorManager ErrorManager { get; }

        /// <summary>Indicates the behavior of the engine when it found an error. (shortcut for ErrorManager.ErrorMode)</summary>
        ErrorMode ErrorMode { get; set; }

        /// <summary>Called to notify progress.</summary>
        event EventHandler<ProgressEventArgs> Progress;
    }
}