using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FileHelpers.Events;
using FileHelpers.Options;

namespace FileHelpers
{
    public interface IFileHelperAsyncEngine<T> 
        : IEnumerable<T>, IDisposable
        where T : class
    {
        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/LastRecord/*'/>
        T LastRecord { get; }

        /// <summary>
        /// An array with the values of each field of the current record
        /// </summary>D:\
        object[] LastRecordValues { get; }

        /// <summary>Allows to change some record layout options at runtime</summary>
        RecordOptions Options { get; }

        /// <summary>
        /// Indicates the current state of the engine.
        /// </summary>
        EngineState State { get; set; }

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

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginReadStream/*'/>
        IDisposable BeginReadStream(TextReader reader);

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginReadFile/*'/>
        IDisposable BeginReadFile(string fileName);

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginReadString/*'/>
        IDisposable BeginReadString(string sourceData);

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/ReadNext/*'/>
        T ReadNext();

        T[] ReadToEnd();

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/ReadNexts/*'/>
        T[] ReadNexts(int numberOfRecords);

        /// <summary>
        /// Save all the buffered data for write to the disk. 
        /// Useful to opened async engines that wants to save pending values to disk or for engines used for logging.
        /// </summary>
        void Flush();

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/Close/*'/>
        void Close();

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginWriteStream/*'/>
        IDisposable BeginWriteStream(TextWriter writer);

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginWriteFile/*'/>
        IDisposable BeginWriteFile(string fileName);

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginAppendToFile/*'/>
        IDisposable BeginAppendToFile(string fileName);

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/WriteNext/*'/>
        void WriteNext(T record);

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/WriteNexts/*'/>
        void WriteNexts(IEnumerable<T> records);

        /// <summary>
        /// Write the current record values in the buffer. You can use engine[0] or engine["YourField"] to set the values.
        /// </summary>
        void WriteNextValues();

        /// <summary>Called in read operations just before the record string is translated to a record.</summary>
        event BeforeReadRecordHandler<T> BeforeReadRecord;

        /// <summary>Called in read operations just after the record was created from a record string.</summary>
        event AfterReadRecordHandler<T> AfterReadRecord;

        /// <summary>Called in write operations just before the record is converted to a string to write it.</summary>
        event BeforeWriteRecordHandler<T> BeforeWriteRecord;

        /// <summary>Called in write operations just after the record was converted to a string.</summary>
        event AfterWriteRecordHandler<T> AfterWriteRecord;

        /// <summary>Called to notify progress.</summary>
        event EventHandler<ProgressEventArgs> Progress;
    }
}