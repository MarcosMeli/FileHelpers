using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using FileHelpers.Events;
using FileHelpers.MasterDetail;
using FileHelpers.Options;

namespace FileHelpers
{

    #region "  Delegate  "

    /// <summary>
    /// Delegate thats determines the Type of the current record (Master, Detail, Skip)
    /// </summary>
    /// <param name="recordString">The string of the current record.</param>
    /// <param name="engine">The engine that calls the selector.</param>
    /// <returns>the action used for the current record (Master, Detail, Skip)</returns>
    public delegate Type RecordTypeSelector(MultiRecordEngine engine, string recordString);

    #endregion

    /// <summary>
    /// <para>This engine allows you to parse and write files that contain
    /// records of different types and that are in a linear relationship</para>
    /// <para>(for Master-Detail check the <see cref="MasterDetailEngine"/>)</para>
    /// </summary>
    [DebuggerDisplay(
        "MultiRecordEngine for types: {ListTypes()}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}"
        )]
    public sealed class MultiRecordEngine
        :
            EventEngineBase<object>,
            IEnumerable,
            IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IRecordInfo[] mMultiRecordInfo;
        private readonly RecordOptions[] mMultiRecordOptions;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Hashtable mRecordInfoHash;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private RecordTypeSelector mRecordSelector;

        /// <summary>
        /// The Selector used by the engine in Read operations to determine the Type to use.
        /// </summary>
        public RecordTypeSelector RecordSelector
        {
            get { return mRecordSelector; }
            set { mRecordSelector = value; }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type[] mTypes;

        #region "  Constructor  "

        /// <summary>Create a new instance of the MultiRecordEngine</summary>
        /// <param name="recordTypes">The Types of the records that this engine can handle.</param>
        public MultiRecordEngine(params Type[] recordTypes)
            : this(null, recordTypes) {}

        /// <summary>Create a new instance of the MultiRecordEngine</summary>
        /// <param name="recordTypes">The Types of the records that this engine can handle.</param>
        /// <param name="recordSelector">
        /// Used only in read operations. The selector indicates to the engine
        /// what Type to use in each read line.
        /// </param>
        public MultiRecordEngine(RecordTypeSelector recordSelector, params Type[] recordTypes)
            : base(GetFirstType(recordTypes))
        {
            mTypes = recordTypes;
            mMultiRecordInfo = new IRecordInfo[mTypes.Length];
            mRecordInfoHash = new Hashtable(mTypes.Length);
            mMultiRecordOptions = new RecordOptions[mTypes.Length];

            for (int i = 0; i < mTypes.Length; i++) {
                if (mTypes[i] == null)
                    throw new BadUsageException("The type at index " + i + " is null.");

                if (mRecordInfoHash.Contains(mTypes[i])) {
                    throw new BadUsageException("The type '" + mTypes[i].Name +
                                                " is already in the engine. You can't pass the same type twice to the constructor.");
                }

                mMultiRecordInfo[i] = FileHelpers.RecordInfo.Resolve(mTypes[i]);
                mMultiRecordOptions[i] = CreateRecordOptionsCore(mMultiRecordInfo[i]);

                mRecordInfoHash.Add(mTypes[i], mMultiRecordInfo[i]);

            }
            mRecordSelector = recordSelector;
        }

        #endregion

        #region "  ReadFile  "

        /// <summary>
        /// Read a File and returns the records.
        /// </summary>
        /// <param name="fileName">The file with the records.</param>
        /// <returns>The read records of different types all mixed.</returns>
        public object[] ReadFile(string fileName)
        {
            using (var fs = new StreamReader(fileName, mEncoding, true, DefaultReadBufferSize))
                return ReadStream(fs);
        }

        #endregion

        #region "  ReadStream  "

        /// <summary>
        /// Read an array of objects from a stream
        /// </summary>
        /// <param name="reader">Stream we are reading from</param>
        /// <returns>Array of objects</returns>
        public object[] ReadStream(TextReader reader)
        {
            return ReadStream(new NewLineDelimitedRecordReader(reader));
        }

        /// <include file='MultiRecordEngine.docs.xml' path='doc/ReadStream/*'/>
        public object[] ReadStream(IRecordReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader", "The reader of the Stream can´t be null");

            if (mRecordSelector == null) {
                throw new BadUsageException(
                    "The Recordselector can´t be null, please pass a not null Selector in the constructor.");
            }

            ResetFields();
            mHeaderText = String.Empty;
            mFooterText = String.Empty;

            var resArray = new ArrayList();

            using (var freader = new ForwardReader(reader, mMultiRecordInfo[0].IgnoreLast)) {
                freader.DiscardForward = true;

                mLineNumber = 1;

                var completeLine = freader.ReadNextLine();
                var currentLine = completeLine;

                if (MustNotifyProgress) // Avoid object creation
                    OnProgress(new ProgressEventArgs(0, -1));

                int currentRecord = 0;

                if (mMultiRecordInfo[0].IgnoreFirst > 0) {
                    for (int i = 0; i < mMultiRecordInfo[0].IgnoreFirst && currentLine != null; i++) {
                        mHeaderText += currentLine + StringHelper.NewLine;
                        currentLine = freader.ReadNextLine();
                        mLineNumber++;
                    }
                }

                bool byPass = false;

                var line = new LineInfo(currentLine) {
                    mReader = freader
                };

                while (currentLine != null) {
                    try {
                        mTotalRecords++;
                        currentRecord++;

                        line.ReLoad(currentLine);

                        var skip = false;
                        Type currType;
                        try {
                            currType = mRecordSelector(this, currentLine);
                        }
                        catch (Exception ex) {
                            throw new Exception("Selector failed to process correctly", ex);
                        }

                        if (currType != null) {
                            var info = (RecordInfo) mRecordInfoHash[currType];
                            if (info == null) {
                                throw new BadUsageException("A record is of type '" + currType.Name +
                                                            "' which this engine is not configured to handle. Try adding this type to the constructor.");
                            }

                            var record = info.Operations.CreateRecordHandler();


                            if (MustNotifyProgress) // Avoid object creation
                                OnProgress(new ProgressEventArgs(currentRecord, -1));

                            BeforeReadEventArgs<object> e = null;
                            if (MustNotifyRead) // Avoid object creation
                            {
                                e = new BeforeReadEventArgs<object>(this, record, currentLine, LineNumber);
                                skip = OnBeforeReadRecord(e);
                                if (e.RecordLineChanged)
                                    line.ReLoad(e.RecordLine);
                            }

                            if (skip == false) {
                                var values = new object[info.FieldCount];
                                if (info.Operations.StringToRecord(record, line, values)) {
                                    if (MustNotifyRead) // Avoid object creation
                                        skip = OnAfterReadRecord(currentLine, record, e.RecordLineChanged, LineNumber);

                                    if (skip == false)
                                        resArray.Add(record);
                                }
                            }
                        }
                    }
                    catch (Exception ex) {
                        switch (mErrorManager.ErrorMode) {
                            case ErrorMode.ThrowException:
                                byPass = true;
                                throw;
                            case ErrorMode.IgnoreAndContinue:
                                break;
                            case ErrorMode.SaveAndContinue:
                                var err = new ErrorInfo {
                                    mLineNumber = freader.LineNumber,
                                    mExceptionInfo = ex,
                                    mRecordString = completeLine
                                };

                                mErrorManager.AddError(err);
                                break;
                        }
                    }
                    finally {
                        if (byPass == false) {
                            currentLine = freader.ReadNextLine();
                            completeLine = currentLine;
                            mLineNumber = freader.LineNumber;
                        }
                    }
                }

                if (mMultiRecordInfo[0].IgnoreLast > 0)
                    mFooterText = freader.RemainingText;
            }
            return resArray.ToArray();
        }

        #endregion

        #region "  ReadString  "

        /// <include file='MultiRecordEngine.docs.xml' path='doc/ReadString/*'/>
        public object[] ReadString(string source)
        {
            var reader = new InternalStringReader(source);
            object[] res = ReadStream(reader);
            reader.Close();
            return res;
        }

        #endregion

        #region "  WriteFile  "

        /// <include file='MultiRecordEngine.docs.xml' path='doc/WriteFile/*'/>
        public void WriteFile(string fileName, IEnumerable records)
        {
            WriteFile(fileName, records, -1);
        }

        /// <include file='MultiRecordEngine.docs.xml' path='doc/WriteFile2/*'/>
        public void WriteFile(string fileName, IEnumerable records, int maxRecords)
        {
            using (var fs = new StreamWriter(fileName, false, mEncoding, DefaultWriteBufferSize)) {
                WriteStream(fs, records, maxRecords);
                fs.Close();
            }
        }

        #endregion

        #region "  WriteStream  "

        /// <summary>
        /// Write the records to a file
        /// </summary>
        /// <param name="writer">Where data is written</param>
        /// <param name="records">records to write to the file</param>
        public void WriteStream(TextWriter writer, IEnumerable records)
        {
            WriteStream(writer, records, -1);
        }

        /// <include file='MultiRecordEngine.docs.xml' path='doc/WriteStream2/*'/>
        public void WriteStream(TextWriter writer, IEnumerable records, int maxRecords)
        {
            if (writer == null)
                throw new ArgumentNullException("writer", "The writer of the Stream can be null");

            if (records == null)
                throw new ArgumentNullException("records", "The records can be null. Try with an empty array.");

            ResetFields();

            if (!string.IsNullOrEmpty(mHeaderText)) {
                if (mHeaderText.EndsWith(StringHelper.NewLine))
                    writer.Write(mHeaderText);
                else
                    writer.WriteLine(mHeaderText);
            }

            string currentLine = null;

            int max = maxRecords;

            if (records is IList) {
                max = Math.Min(max < 0
                    ? int.MaxValue
                    : max,
                    ((IList) records).Count);
            }


            if (MustNotifyProgress) // Avoid object creation
                OnProgress(new ProgressEventArgs(0, max));

            int recIndex = 0;

            foreach (var rec in records) {
                if (recIndex == maxRecords)
                    break;
                try {
                    if (rec == null)
                        throw new BadUsageException("The record at index " + recIndex + " is null.");

                    bool skip = false;

                    if (MustNotifyProgress) // Avoid object creation
                        OnProgress(new ProgressEventArgs(recIndex + 1, max));

                    if (MustNotifyWrite)
                        skip = OnBeforeWriteRecord(rec, LineNumber);

                    var info = (IRecordInfo) mRecordInfoHash[rec.GetType()];

                    if (info == null) {
                        throw new BadUsageException("The record at index " + recIndex + " is of type '" +
                                                    rec.GetType().Name +
                                                    "' and the engine dont handle this type. You can add it to the constructor.");
                    }

                    if (skip == false) {
                        currentLine = info.Operations.RecordToString(rec);

                        if (MustNotifyWrite)
                            currentLine = OnAfterWriteRecord(currentLine, rec);
                        writer.WriteLine(currentLine);
                    }
                }
                catch (Exception ex) {
                    switch (mErrorManager.ErrorMode) {
                        case ErrorMode.ThrowException:
                            throw;
                        case ErrorMode.IgnoreAndContinue:
                            break;
                        case ErrorMode.SaveAndContinue:
                            var err = new ErrorInfo {
                                mLineNumber = mLineNumber,
                                mExceptionInfo = ex,
                                mRecordString = currentLine
                            };
                            mErrorManager.AddError(err);
                            break;
                    }
                }
                recIndex++;
            }

            mTotalRecords = recIndex;

            if (!string.IsNullOrEmpty(mFooterText)) {
                if (mFooterText.EndsWith(StringHelper.NewLine))
                    writer.Write(mFooterText);
                else
                    writer.WriteLine(mFooterText);
            }
        }

        #endregion

        #region "  WriteString  "

        /// <include file='MultiRecordEngine.docs.xml' path='doc/WriteString/*'/>
        public string WriteString(IEnumerable records)
        {
            return WriteString(records, -1);
        }

        /// <include file='MultiRecordEngine.docs.xml' path='doc/WriteString2/*'/>
        public string WriteString(IEnumerable records, int maxRecords)
        {
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            WriteStream(writer, records, maxRecords);
            string res = writer.ToString();
            writer.Close();
            return res;
        }

        #endregion

        #region "  AppendToFile  "

        /// <include file='MultiRecordEngine.docs.xml' path='doc/AppendToFile1/*'/>
        public void AppendToFile(string fileName, object record)
        {
            AppendToFile(fileName, new[] {record});
        }

        /// <include file='MultiRecordEngine.docs.xml' path='doc/AppendToFile2/*'/>
        public void AppendToFile(string fileName, IEnumerable records)
        {
            using (
                TextWriter writer = StreamHelper.CreateFileAppender(fileName,
                    mEncoding,
                    true,
                    false,
                    DefaultWriteBufferSize)) {
                mHeaderText = String.Empty;
                mFooterText = String.Empty;

                WriteStream(writer, records);
                writer.Close();
            }
        }

        #endregion

        private static Type GetFirstType(Type[] types)
        {
            if (types == null)
                throw new BadUsageException("A null Type[] is not valid for the MultiRecordEngine.");
            if (types.Length == 0)
                throw new BadUsageException("An empty Type[] is not valid for the MultiRecordEngine.");
            if (types.Length == 1) {
                throw new BadUsageException(
                    "You only provided one type to the engine constructor. You need 2 or more types, for one type you can use the FileHelperEngine.");
            }
            return types[0];
        }

        // ASYNC METHODS --------------

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ForwardReader mAsyncReader;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private TextWriter mAsyncWriter;

        #region "  LastRecord  "

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private object mLastRecord;

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/LastRecord/*'/>
        public object LastRecord
        {
            get { return mLastRecord; }
        }

        #endregion

        #region "  BeginReadStream"

        /// <summary>
        /// Read a generic file as delimited by newlines
        /// </summary>
        /// <param name="reader">Text reader</param>
        public void BeginReadStream(TextReader reader)
        {
            BeginReadStream(new NewLineDelimitedRecordReader(reader));
        }

        /// <summary>
        /// Method used to use this engine in Async mode. Work together with
        /// <see cref="ReadNext"/>. (Remember to call Close after read the
        /// data)
        /// </summary>
        /// <param name="reader">The source Reader.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void BeginReadStream(IRecordReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("The TextReader can´t be null.");

            ResetFields();
            mHeaderText = String.Empty;
            mFooterText = String.Empty;

            if (RecordInfo.IgnoreFirst > 0) {
                for (int i = 0; i < RecordInfo.IgnoreFirst; i++) {
                    string temp = reader.ReadRecordString();
                    mLineNumber++;
                    if (temp != null)
                        mHeaderText += temp + StringHelper.NewLine;
                    else
                        break;
                }
            }

            mAsyncReader = new ForwardReader(reader, RecordInfo.IgnoreLast, mLineNumber) {
                DiscardForward = true
            };
        }

        #endregion

        #region "  BeginReadFile  "

        /// <summary>
        /// Method used to use this engine in Async mode. Work together with
        /// <see cref="ReadNext"/>. (Remember to call Close after read the
        /// data)
        /// </summary>
        /// <param name="fileName">The source file.</param>
        public void BeginReadFile(string fileName)
        {
            BeginReadStream(new StreamReader(fileName, mEncoding, true, DefaultReadBufferSize));
        }

        /// <summary>
        /// Method used to use this engine in Async mode. Work together with
        /// <see cref="ReadNext"/>. (Remember to call Close after read the
        /// data)
        /// </summary>
        /// <param name="sourceData">The source String</param>
        public void BeginReadString(string sourceData)
        {
            if (sourceData == null)
                sourceData = String.Empty;

            BeginReadStream(new InternalStringReader(sourceData));
        }

        #endregion

        /// <summary>
        /// Save all the buffered data for write to the disk. 
        /// Useful to long opened async engines that wants to save pending
        /// values or for engines used for logging.
        /// </summary>
        public void Flush()
        {
            if (mAsyncWriter != null)
                mAsyncWriter.Flush();
        }

        #region "  Close  "

        /// <summary>
        /// Close the underlining Readers and Writers. (if any)
        /// </summary>
        public void Close()
        {
            try {
                if (mAsyncReader != null)
                    mAsyncReader.Close();

                mAsyncReader = null;
            }
            catch {}

            try {
                if (mAsyncWriter != null) {
                    if (!string.IsNullOrEmpty(mFooterText)) {
                        if (mFooterText.EndsWith(StringHelper.NewLine))
                            mAsyncWriter.Write(mFooterText);
                        else
                            mAsyncWriter.WriteLine(mFooterText);
                    }

                    mAsyncWriter.Close();
                    mAsyncWriter = null;
                }
            }
            catch {}
        }

        #endregion

        // DIFFERENT FROM THE ASYNC ENGINE

        #region "  ReadNext  "
        /// <summary>
        /// Reads the next record from the source
        /// </summary>
        /// <returns>The record just read</returns>
        public object ReadNext()
        {
            if (mAsyncReader == null)
                throw new BadUsageException("Before call ReadNext you must call BeginReadFile or BeginReadStream.");

            ReadNextRecord();

            return mLastRecord;
        }

        private void ReadNextRecord()
        {
            string currentLine = mAsyncReader.ReadNextLine();
            mLineNumber++;

            bool byPass = false;

            mLastRecord = null;

            var line = new LineInfo(currentLine) {
                mReader = mAsyncReader
            };

            while (true) {
                if (currentLine != null) {
                    try {
                        mTotalRecords++;

                        Type currType = mRecordSelector(this, currentLine);

                        line.ReLoad(currentLine);

                        if (currType != null)
                        {
                            var info = (RecordInfo) mRecordInfoHash[currType];
                            if (info == null) {
                                throw new BadUsageException("A record is of type '" + currType.Name +
                                                            "' which this engine is not configured to handle. Try adding this type to the constructor.");
                            }
                            var values = new object[info.FieldCount];
                            mLastRecord = info.Operations.StringToRecord(line, values);

                            if (MustNotifyRead)
                            {
                                OnAfterReadRecord(currentLine, mLastRecord, false, LineNumber);
                            }

                            if (mLastRecord != null) {
                                byPass = true;
                                return;
                            }
                        }
                    }
                    catch (Exception ex) {
                        switch (mErrorManager.ErrorMode) {
                            case ErrorMode.ThrowException:
                                byPass = true;
                                throw;
                            case ErrorMode.IgnoreAndContinue:
                                break;
                            case ErrorMode.SaveAndContinue:
                                var err = new ErrorInfo {
                                    mLineNumber = mAsyncReader.LineNumber,
                                    mExceptionInfo = ex,
                                    mRecordString = currentLine
                                };

                                mErrorManager.AddError(err);
                                break;
                        }
                    }
                    finally {
                        if (byPass == false) {
                            currentLine = mAsyncReader.ReadNextLine();
                            mLineNumber = mAsyncReader.LineNumber;
                        }
                    }
                }
                else {
                    mLastRecord = null;

                    if (RecordInfo.IgnoreLast > 0)
                        mFooterText = mAsyncReader.RemainingText;

                    try {
                        mAsyncReader.Close();
                    }
                    catch {}

                    return;
                }
            }
        }
        /// <summary>
        /// Read a defined number of records from the source
        /// </summary>
        /// <param name="numberOfRecords">The count of records to read</param>
        /// <returns>An Array with all the read record objects</returns>
        public object[] ReadNexts(int numberOfRecords)
        {
            if (mAsyncReader == null)
                throw new BadUsageException("Before call ReadNext you must call BeginReadFile or BeginReadStream.");

            var arr = new ArrayList(numberOfRecords);

            for (int i = 0; i < numberOfRecords; i++) {
                ReadNextRecord();
                if (mLastRecord != null)
                    arr.Add(mLastRecord);
                else
                    break;
            }
            return arr.ToArray();
        }

        #endregion

        #region "  IEnumerable implementation  "

        /// <summary>Allows to loop record by record in the engine</summary>
        /// <returns>The Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            if (mAsyncReader == null)
                throw new FileHelpersException("You must call BeginRead before use the engine in a foreach loop.");

            return new AsyncEnumerator(this);
        }

        private class AsyncEnumerator : IEnumerator
        {
            private readonly MultiRecordEngine mEngine;

            public AsyncEnumerator(MultiRecordEngine engine)
            {
                mEngine = engine;
            }

            public bool MoveNext()
            {
                object res = mEngine.ReadNext();

                if (res == null) {
                    mEngine.Close();
                    return false;
                }

                return true;
            }

            public object Current
            {
                get { return mEngine.mLastRecord; }
            }

            public void Reset()
            {
                // No needed
            }
        }

        #endregion

        #region "  IDisposable implementation  "

        /// <summary>
        /// Release Resources
        /// </summary>
        void IDisposable.Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }

        /// <summary>Destructor</summary>
        ~MultiRecordEngine()
        {
            Close();
        }

        #endregion

        #region "  WriteNext  "

        /// <summary>
        /// Write the next record to a file or stream opened with
        /// <see cref="BeginWriteFile" />, <see cref="BeginWriteStream" /> or
        /// <see cref="BeginAppendToFile" /> method.
        /// </summary>
        /// <param name="record">The record to write.</param>
        public void WriteNext(object record)
        {
            if (mAsyncWriter == null)
                throw new BadUsageException("Before call WriteNext you must call BeginWriteFile or BeginWriteStream.");

            if (record == null)
                throw new BadUsageException("The record to write can´t be null.");

            WriteRecord(record);
        }

        /// <summary>
        /// Write the nexts records to a file or stream opened with
        /// <see cref="BeginWriteFile" />, <see cref="BeginWriteStream" /> or
        /// <see cref="BeginAppendToFile" /> method.
        /// </summary>
        /// <param name="records">The records to write (Can be an array, ArrayList, etc)</param>
        public void WriteNexts(IEnumerable records)
        {
            if (mAsyncWriter == null)
                throw new BadUsageException("Before call WriteNext you must call BeginWriteFile or BeginWriteStream.");

            if (records == null)
                throw new ArgumentNullException("records", "The record to write can´t be null.");

            int nro = 0;
            foreach (var rec in records) {
                nro++;

                if (rec == null)
                    throw new BadUsageException("The record at index " + nro + " is null.");

                WriteRecord(rec);
            }
        }

        private void WriteRecord(object record)
        {
            string currentLine = null;

            try {
                mLineNumber++;
                mTotalRecords++;

                var info = (IRecordInfo) mRecordInfoHash[record.GetType()];

                if (info == null) {
                    throw new BadUsageException("A record is of type '" + record.GetType().Name +
                                                "' and the engine dont handle this type. You can add it to the constructor.");
                }

                currentLine = info.Operations.RecordToString(record);
                mAsyncWriter.WriteLine(currentLine);
            }
            catch (Exception ex) {
                switch (mErrorManager.ErrorMode) {
                    case ErrorMode.ThrowException:
                        throw;
                    case ErrorMode.IgnoreAndContinue:
                        break;
                    case ErrorMode.SaveAndContinue:
                        var err = new ErrorInfo {
                            mLineNumber = mLineNumber,
                            mExceptionInfo = ex,
                            mRecordString = currentLine
                        };
                        //							err.mColumnNumber = mColumnNum;
                        mErrorManager.AddError(err);
                        break;
                }
            }
        }

        #endregion

        #region "  BeginWriteStream"

        ///	<summary>Set the stream to be used in the <see cref="WriteNext" /> operation.</summary>
        ///	<remarks>
        ///	<para>When you finish to write to the file you must call
        ///	<b><see cref="Close" /></b> method.</para>
        ///	</remarks>
        ///	<param name="writer">To stream to writes to.</param>
        public void BeginWriteStream(TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentException("The TextWriter can´t be null.", "writer");

            ResetFields();
            mAsyncWriter = writer;
            WriteHeader();
        }

        private void WriteHeader()
        {
            if (!string.IsNullOrEmpty(mHeaderText)) {
                if (mHeaderText.EndsWith(StringHelper.NewLine))
                    mAsyncWriter.Write(mHeaderText);
                else
                    mAsyncWriter.WriteLine(mHeaderText);
            }
        }

        #endregion

        #region "  BeginWriteFile  "

        ///	<summary>
        ///	Open a file for write operations. If exist the engine override it.
        ///	You can use <see cref="WriteNext"/> or <see cref="WriteNexts"/> to
        ///	write records.
        ///	</summary>
        ///	<remarks>
        ///	<para>When you finish to write to the file you must call
        ///	<b><see cref="Close" /></b> method.</para>
        ///	</remarks>
        /// <param name="fileName">The file path to be opened for write.</param>
        public void BeginWriteFile(string fileName)
        {
            BeginWriteStream(new StreamWriter(fileName, false, mEncoding, DefaultWriteBufferSize));
        }

        #endregion

        #region "  BeginappendToFile  "

        ///	<summary>Open a file to be appended at the end.</summary>
        ///	<remarks><para>This method open and seek to the end the file.</para>
        ///	<para>When you finish to append to the file you must call
        ///	<b><see cref="Close" /></b> method.</para></remarks>
        ///	<param name="fileName">The file path to be opened to write at the end.</param>
        public void BeginAppendToFile(string fileName)
        {
            mAsyncWriter = StreamHelper.CreateFileAppender(fileName, mEncoding, false, true, DefaultWriteBufferSize);
            mHeaderText = String.Empty;
            mFooterText = String.Empty;
        }

        #endregion
    }
}

//#endif
