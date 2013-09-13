using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using FileHelpers.Events;

namespace FileHelpers
{
    /// <summary>
    /// Async engine,  reads records from file in background,
    /// returns them record by record in foreground
    /// </summary>
    public sealed class FileHelperAsyncEngine :
        FileHelperAsyncEngine<object>
    {
        #region "  Constructor  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngineCtr/*'/>
        public FileHelperAsyncEngine(Type recordType)
            : base(recordType)
        {
        }

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngineCtr/*'/>
        /// <param name="encoding">The encoding used by the Engine.</param>
        public FileHelperAsyncEngine(Type recordType, Encoding encoding)
            : base(recordType, encoding)
        {
        }

        #endregion
        
    }

    /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngine/*'/>
    /// <include file='Examples.xml' path='doc/examples/FileHelperAsyncEngine/*'/>
    /// <typeparam name="T">The record type.</typeparam>
    [DebuggerDisplay("FileHelperAsyncEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
    public class FileHelperAsyncEngine<T> :
        EventEngineBase<T>,
        IFileHelperAsyncEngine<T>
        where T: class
    {

        #region "  Constructor  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngineCtr/*'/>
		public FileHelperAsyncEngine() 
			: base(typeof(T))
        {
        }

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngineCtr/*'/>
        /// <param name="recordType">Type of object to be handled</param>
        protected FileHelperAsyncEngine(Type recordType)
            : base(recordType)
        {
        }

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngineCtr/*'/>
        /// <param name="encoding">The encoding used by the Engine.</param>
		public FileHelperAsyncEngine(Encoding encoding)
			: base(typeof(T), encoding)
        {
        }

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngineCtr/*'/>
        /// <param name="encoding">The encoding used by the Engine.</param>
        /// <param name="recordType">Type of record to read</param>
        protected FileHelperAsyncEngine(Type recordType, Encoding encoding)
            : base(recordType, encoding)
        {
        }

    

        #endregion

        #region "  Readers and Writters  "

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ForwardReader mAsyncReader;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        TextWriter mAsyncWriter;

        public bool mEof = false;
        /// <summary>
        /// Indicates if End of File was reached.
        /// </summary>
        /// <value>The EOF flag.</value>
        public bool Eof
        {
            get { return mEof; }
        }

        #endregion

        #region "  LastRecord  "

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private T mLastRecord;

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/LastRecord/*'/>
		public T LastRecord
		{
			get { return mLastRecord; }
		}

        private object[] mLastRecordValues;

        /// <summary>
        /// An array with the values of each field of the current record
        /// </summary>
        public object[] LastRecordValues
        {
            get { return mLastRecordValues; }
        }


        /// <summary>
        /// Get a field value of the current records.
        /// </summary>
        /// <param name="fieldIndex" >The index of the field.</param>
        public object this[int fieldIndex]
        {
            get
            {
                if (mLastRecordValues == null)
                    throw new BadUsageException("You must be reading something to access this property. Try calling BeginReadFile first.");

                return mLastRecordValues[fieldIndex];
            }
            set
            {
                if (mAsyncWriter == null)
                    throw new BadUsageException("You must be writting something to set a record value. Try calling BeginWriteFile first.");

                if (mLastRecordValues == null)
                    mLastRecordValues = new object[RecordInfo.FieldCount];

                if (value == null)
                {
                    if (RecordInfo.Fields[fieldIndex].FieldType.IsValueType)
                        throw new BadUsageException("You can't assing null to a value type.");

                    mLastRecordValues[fieldIndex] = null;
                }
                else
                {
                    if (!RecordInfo.Fields[fieldIndex].FieldType.IsInstanceOfType(value))
                        throw new BadUsageException(string.Format("Invalid type: {0}. Expected: {1}", value.GetType().Name, RecordInfo.Fields[fieldIndex].FieldType.Name));

                    mLastRecordValues[fieldIndex] = value;
                }
            }

        }

        /// <summary>
        /// Get a field value of the current records.
        /// </summary>
        /// <param name="fieldName" >The name of the field (case sensitive)</param>
        public object this[string fieldName]
        {
            get
            {
                if (mLastRecordValues == null)
                    throw new BadUsageException("You must be reading something to access this property. Try calling BeginReadFile first.");

                int index = RecordInfo.GetFieldIndex(fieldName);
                return mLastRecordValues[index];
            }
            set
            {
                int index = RecordInfo.GetFieldIndex(fieldName);
                this[index] = value;
            }
        }

        #endregion

        #region "  BeginReadStream"

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginReadStream/*'/>
        public IDisposable BeginReadStream(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader", "The TextReader can�t be null.");

            if (mAsyncWriter != null)
                throw new BadUsageException("You can't start to read while you are writing.");

            var recordReader = new NewLineDelimitedRecordReader(reader);

            ResetFields();
            mHeaderText = String.Empty;
            mFooterText = String.Empty;

            if (RecordInfo.IgnoreFirst > 0)
            {
                for (int i = 0; i < RecordInfo.IgnoreFirst; i++)
                {
                    string temp = recordReader.ReadRecordString();
                    mLineNumber++;
                    if (temp != null)
                        mHeaderText += temp + StringHelper.NewLine;
                    else
                        break;
                }
            }

            mAsyncReader = new ForwardReader(recordReader, RecordInfo.IgnoreLast, mLineNumber)
                {
                    DiscardForward = true
                };
            mState = EngineState.Reading;
            mStreamInfo = new StreamInfoProvider(reader);
            mCurrentRecord = 0;

#if ! MINI
            if (MustNotifyProgress) // Avoid object creation
                OnProgress(new ProgressEventArgs(0, -1, mStreamInfo.Position, mStreamInfo.TotalBytes));
#endif
            return this;
        }

        #endregion

        #region "  BeginReadFile  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginReadFile/*'/>
        public IDisposable BeginReadFile(string fileName)
        {
            BeginReadFile(fileName, DefaultReadBufferSize);
            return this;
        }

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginReadFile/*'/>
        /// <param name="bufferSize">Buffer size to read</param>
        public IDisposable BeginReadFile(string fileName, int bufferSize)
        {
            BeginReadStream(new InternalStreamReader(fileName, mEncoding, true, bufferSize));
            return this;
        }
        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginReadString/*'/>
        public IDisposable BeginReadString(string sourceData)
        {
            if (sourceData == null)
                sourceData = String.Empty;

            BeginReadStream(new InternalStringReader(sourceData));
            return this;
        }

        #endregion

        #region "  ReadNext  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/ReadNext/*'/>
		public T ReadNext()
        {
            if (mAsyncReader == null)
                throw new BadUsageException("Before call ReadNext you must call BeginReadFile or BeginReadStream.");

            ReadNextRecord();

            return mLastRecord;
        }

        private int mCurrentRecord = 0;

        private void ReadNextRecord()
        {
            string currentLine = mAsyncReader.ReadNextLine();
            mLineNumber++;

            bool byPass = false;

            mLastRecord = default(T); 


            var line = new LineInfo(string.Empty)
                {
                    mReader = mAsyncReader
                };

            if (mLastRecordValues == null)
                mLastRecordValues = new object[RecordInfo.FieldCount];

            while (true)
            {
                if (currentLine != null)
                {
                    try
                    {
                        mTotalRecords++;
                        mCurrentRecord++;
                        line.ReLoad(currentLine);

                        bool skip = false;

                        mLastRecord = (T) RecordInfo.Operations.CreateRecordHandler();
#if ! MINI
                        if (MustNotifyProgress) // Avoid object creation
                            OnProgress(new ProgressEventArgs(mCurrentRecord, -1, mStreamInfo.Position, mStreamInfo.TotalBytes));

                        BeforeReadEventArgs<T> e = null;
                        if (MustNotifyRead) // Avoid object creation
                        {
                            e = new BeforeReadEventArgs<T>(this, mLastRecord, currentLine, LineNumber);
                            skip = OnBeforeReadRecord(e);
                            if (e.RecordLineChanged)
                                line.ReLoad(e.RecordLine);
                        }



#endif
                        if (skip == false)
                        {
                            if (RecordInfo.Operations.StringToRecord(mLastRecord, line, mLastRecordValues))
                            {
#if ! MINI
                                if (MustNotifyRead) // Avoid object creation
                                    skip = OnAfterReadRecord(currentLine, mLastRecord, e.RecordLineChanged, LineNumber);
#endif
                                if (skip == false)
                                {
                                    byPass = true;
                                    return;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        switch (mErrorManager.ErrorMode)
                        {
                            case ErrorMode.ThrowException:
                                byPass = true;
                                throw;
                            case ErrorMode.IgnoreAndContinue:
                                break;
                            case ErrorMode.SaveAndContinue:
                                var err = new ErrorInfo
                                    {
                                        mLineNumber = mAsyncReader.LineNumber,
                                        mExceptionInfo = ex,
                                        mRecordString = currentLine
                                    };
                                //							err.mColumnNumber = mColumnNum;

                                mErrorManager.AddError(err);
                                break;
                        }
                    }
                    finally
                    {
                        if (byPass == false)
                        {
                            currentLine = mAsyncReader.ReadNextLine();
                            mLineNumber = mAsyncReader.LineNumber;
                        }
                    }
                }
                else
                {
					// mark end of file
                    mEof = true;
                    mLastRecordValues = null;

					mLastRecord = default(T);


                    if (RecordInfo.IgnoreLast > 0)
                        mFooterText = mAsyncReader.RemainingText;

                    try
                    {
                        mAsyncReader.Close();
                        //mAsyncReader = null;
                    }
                    catch
                    {
                    }

                    return;
                }
            }
        }

        /// <summary>
        /// Return array of object for all data to end of the file
        /// </summary>
        /// <returns>Array of objects created from data on file</returns>
        public T[] ReadToEnd()
        {
            return ReadNexts(int.MaxValue);
        }

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/ReadNexts/*'/>
		public T[] ReadNexts(int numberOfRecords)
        {
            if (mAsyncReader == null)
                throw new BadUsageException("Before call ReadNext you must call BeginReadFile or BeginReadStream.");

            var arr = new List<T>(numberOfRecords);

            for (int i = 0; i < numberOfRecords; i++)
            {
                ReadNextRecord();
                if (mLastRecord != null)
                {
                    arr.Add(mLastRecord);                    
                }
                else
                {
                    // mark end of file
                    mEof = true;
                    break;
                }
            }
			return arr.ToArray();
        }

        #endregion

        #region "  Close  "

        /// <summary>
        /// Save all the buffered data for write to the disk. 
        /// Useful to opened async engines that wants to save pending values to
        /// disk or for engines used for logging.
        /// </summary>
        public void Flush()
        {
            if (mAsyncWriter != null)
                mAsyncWriter.Flush();
        }

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/Close/*'/>
        public void Close()
        {
            lock (this)
            {
                mState = EngineState.Closed;

                try
                {
                    mLastRecordValues = null;
                    mLastRecord = default(T);

                    var reader = mAsyncReader;
                    if (reader != null)
                    {
                        reader.Close();
                        mAsyncReader = null;
                    }
                }
                catch
                {
                }

                try
                {
                    using (var writer = mAsyncWriter)
                    {
                        mAsyncWriter = null;
                        if (writer != null)
                        {
                            if (!string.IsNullOrEmpty (mFooterText))
                                if (mFooterText.EndsWith (StringHelper.NewLine, StringComparison.Ordinal))
                                    writer.Write (mFooterText);
                                else
                                    writer.WriteLine (mFooterText);

                            writer.Close ();                            
                        }
                    }
                }
                catch
                {
                }

            }
        }

        #endregion

        #region "  BeginWriteStream"

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginWriteStream/*'/>
        public IDisposable BeginWriteStream(TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentException("writer", "The TextWriter can�t be null.");

            if (mAsyncReader != null)
                throw new BadUsageException("You can't start to write while you are reading.");


            mState = EngineState.Writing;
            ResetFields();
            mAsyncWriter = writer;
            WriteHeader();
            mStreamInfo = new StreamInfoProvider(mAsyncWriter);
            mCurrentRecord = 0;

#if ! MINI
            if (MustNotifyProgress) // Avoid object creation
                OnProgress(new ProgressEventArgs(0, -1, mStreamInfo.Position, mStreamInfo.TotalBytes));
#endif

            return this;
        }

        private void WriteHeader()
        {
            if (!string.IsNullOrEmpty(mHeaderText))
                if (mHeaderText.EndsWith (StringHelper.NewLine, StringComparison.Ordinal))
                    mAsyncWriter.Write(mHeaderText);
                else
                    mAsyncWriter.WriteLine(mHeaderText);
        }

        #endregion

        #region "  BeginWriteFile  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginWriteFile/*'/>
        public IDisposable BeginWriteFile(string fileName)
        {
            return BeginWriteFile(fileName, DefaultWriteBufferSize);
        }

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginWriteFile/*'/>
        /// <param name="bufferSize">Size of the write buffer</param>
        public IDisposable BeginWriteFile(string fileName, int bufferSize)
        {
            BeginWriteStream(new StreamWriter(fileName, false, mEncoding, bufferSize));

            return this;
        }

        #endregion


        #region "  BeginAppendToFile  "

        /// <summary>
        /// Begin the append to an existing file
        /// </summary>
        /// <param name="fileName">Filename to append to</param>
        /// <returns>Object to append  TODO:  ???</returns>
        public IDisposable BeginAppendToFile(string fileName)
        {
            return BeginAppendToFile(fileName, DefaultWriteBufferSize);
        }

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginAppendToFile/*'/>
        /// <param name="bufferSize">Size of the buffer for writing</param>
        public IDisposable BeginAppendToFile(string fileName, int bufferSize)
        {
            if (mAsyncReader != null)
                throw new BadUsageException("You can't start to write while you are reading.");

            mAsyncWriter = StreamHelper.CreateFileAppender(fileName, mEncoding, false, true, bufferSize);
            mHeaderText = String.Empty;
            mFooterText = String.Empty;
            mState = EngineState.Writing;
            mStreamInfo = new StreamInfoProvider(mAsyncWriter);
            mCurrentRecord = 0;

#if ! MINI
            if (MustNotifyProgress) // Avoid object creation
                OnProgress(new ProgressEventArgs(0, -1, mStreamInfo.Position, mStreamInfo.TotalBytes));
#endif

            return this;
        }

        #endregion

        #region "  WriteNext  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/WriteNext/*'/>
		public void WriteNext(T record)
        {
            if (mAsyncWriter == null)
                throw new BadUsageException("Before call WriteNext you must call BeginWriteFile or BeginWriteStream.");

            if (record == null)
                throw new BadUsageException("The record to write can�t be null.");

            if (RecordType.IsAssignableFrom(record.GetType()) == false)
                throw new BadUsageException("The record must be of type: " + RecordType.Name);

            WriteRecord(record);
        }

		private void WriteRecord(T record)
        {
            string currentLine = null;

            try
            {
                mLineNumber++;
                mTotalRecords++;
                mCurrentRecord++;

                bool skip = false;
#if !MINI
                
                if (MustNotifyProgress) // Avoid object creation
                    OnProgress(new ProgressEventArgs(mCurrentRecord, -1, mStreamInfo.Position, mStreamInfo.TotalBytes));

                if (MustNotifyWrite)
                    skip = OnBeforeWriteRecord(record, LineNumber);

#endif

                if (skip == false)
                {
                    currentLine = RecordInfo.Operations.RecordToString(record);
#if !MINI
                    if (MustNotifyWrite)
                        currentLine = OnAfterWriteRecord(currentLine, record);
#endif
                    mAsyncWriter.WriteLine(currentLine);
                }
            }
            catch (Exception ex)
            {
                switch (mErrorManager.ErrorMode)
                {
                    case ErrorMode.ThrowException:
                        throw;
                    case ErrorMode.IgnoreAndContinue:
                        break;
                    case ErrorMode.SaveAndContinue:
                        var err = new ErrorInfo
                            {
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

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/WriteNexts/*'/>
		public void WriteNexts(IEnumerable<T> records)
        {
            if (mAsyncWriter == null)
                throw new BadUsageException("Before call WriteNext you must call BeginWriteFile or BeginWriteStream.");

            if (records == null)
                throw new ArgumentNullException("records", "The record to write can�t be null.");

            bool first = true;


			foreach (var rec in records)
            {
                if (first)
                {
                    if (RecordType.IsAssignableFrom(rec.GetType()) == false)
                        throw new BadUsageException("The record must be of type: " + RecordType.Name);
                    first = false;
                }

                WriteRecord(rec);
            }

        }

        #endregion

        #region "  WriteNext for LastRecordValues  "

        /// <summary>
        /// Write the current record values in the buffer. You can use
        /// engine[0] or engine["YourField"] to set the values.
        /// </summary>
        public void WriteNextValues()
        {
            if (mAsyncWriter == null)
                throw new BadUsageException("Before call WriteNext you must call BeginWriteFile or BeginWriteStream.");

            if (mLastRecordValues == null)
                throw new BadUsageException("You must set some values of the record before call this method, or use the overload that has a record as argument.");

            string currentLine = null;

            try
            {
                mLineNumber++;
                mTotalRecords++;

                currentLine = RecordInfo.Operations.RecordValuesToString(this.mLastRecordValues);
                mAsyncWriter.WriteLine(currentLine);
            }
            catch (Exception ex)
            {
                switch (mErrorManager.ErrorMode)
                {
                    case ErrorMode.ThrowException:
                        throw;
                    case ErrorMode.IgnoreAndContinue:
                        break;
                    case ErrorMode.SaveAndContinue:
                        var err = new ErrorInfo
                            {
                                mLineNumber = mLineNumber,
                                mExceptionInfo = ex,
                                mRecordString = currentLine
                            };
                        //							err.mColumnNumber = mColumnNum;
                        mErrorManager.AddError(err);
                        break;
                }
            }
            finally
            {
                mLastRecordValues = null;
            }
        }

        #endregion


        #region "  IEnumerable implementation  "

        /// <summary>Allows to loop record by record in the engine</summary>
        /// <returns>The enumerator</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            if (mAsyncReader == null)
                throw new FileHelpersException("You must call BeginRead before use the engine in a for each loop.");
            return new AsyncEnumerator(this);
        }

        ///<summary>
        ///Returns an enumerator that iterates through a collection.
        ///</summary>
        ///
        ///<returns>
        ///An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            if (mAsyncReader == null)
                throw new FileHelpersException("You must call BeginRead before use the engine in a for each loop.");
            return new AsyncEnumerator(this);
        }


		private class AsyncEnumerator : IEnumerator<T>
		{
            T IEnumerator<T>.Current
            {
                get { return mEngine.mLastRecord; }
            }

            void IDisposable.Dispose()
            {
                mEngine.Close();
            }

		    readonly FileHelperAsyncEngine<T> mEngine;
			public AsyncEnumerator(FileHelperAsyncEngine<T> engine)
            {
                mEngine = engine;
            }

            public bool MoveNext()
            {
                object res = mEngine.ReadNext();

                if (res == null)
                {
                    mEngine.Close();
                    return false;
                }

                return true;

            }

            public object Current
            {
                get
                {
                    return mEngine.mLastRecord;
                }
            }

            public void Reset()
            {
                // No needed
            }


        }


        #endregion

        #region "  IDisposable implementation  "

        /// <summary>Release Resources</summary>
        void IDisposable.Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }

        /// <summary>Destructor</summary>
        ~FileHelperAsyncEngine()
        {
            Close();
        }

        #endregion

        #region "  State  "

        private EngineState mState = EngineState.Closed;
        private StreamInfoProvider mStreamInfo;

        /// <summary>
        /// Indicates the current state of the engine.
        /// </summary>
        public EngineState State
        {
            get { return mState; }
            set { mState = value; }
        }

        #endregion

    }
}
