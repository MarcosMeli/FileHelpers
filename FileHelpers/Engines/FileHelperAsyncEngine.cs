


using System;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using FileHelpers.Events;
using FileHelpers.Options;

namespace FileHelpers
{
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
    [DebuggerDisplay("FileHelperAsyncEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
    /// <typeparam name="T">The record type.</typeparam>
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

        protected FileHelperAsyncEngine(Type type)
            : base(type)
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
        public FileHelperAsyncEngine(Type type, Encoding encoding)
            : base(type, encoding)
        {
        }

    

        #endregion

        #region "  Readers and Writters  "

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ForwardReader mAsyncReader;
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        TextWriter mAsyncWriter;

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
                    mLastRecordValues = new object[mRecordInfo.FieldCount];

                if (value == null)
                {
                    if (mRecordInfo.Fields[fieldIndex].FieldType.IsValueType)
                        throw new BadUsageException("You can't assing null to a value type.");

                    mLastRecordValues[fieldIndex] = null;
                }
                else
                {
                    if (!mRecordInfo.Fields[fieldIndex].FieldType.IsInstanceOfType(value))
                        throw new BadUsageException(string.Format("Invalid type: {0}. Expected: {1}", value.GetType().Name, mRecordInfo.Fields[fieldIndex].FieldType.Name));

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

                int index = mRecordInfo.GetFieldIndex(fieldName);
                return mLastRecordValues[index];
            }
            set
            {
                int index = mRecordInfo.GetFieldIndex(fieldName);
                this[index] = value;
            }
        }



        #endregion

        #region "  BeginReadStream"

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginReadStream/*'/>
        public IDisposable BeginReadStream(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader", "The TextReader can´t be null.");

            if (mAsyncWriter != null)
                throw new BadUsageException("You can't start to read while you are writing.");

            var recordReader = new NewLineDelimitedRecordReader(reader);

            ResetFields();
            mHeaderText = String.Empty;
            mFooterText = String.Empty;

            if (mRecordInfo.IgnoreFirst > 0)
            {
                for (int i = 0; i < mRecordInfo.IgnoreFirst; i++)
                {
                    string temp = recordReader.ReadRecord();
                    mLineNumber++;
                    if (temp != null)
                        mHeaderText += temp + StringHelper.NewLine;
                    else
                        break;
                }
            }

            mAsyncReader = new ForwardReader(recordReader, mRecordInfo.IgnoreLast, mLineNumber);
            mAsyncReader.DiscardForward = true;
            mState = EngineState.Reading;
            mStreamInfo = new StreamInfoProvider(reader);
            mCurrentRecord = 0;

#if ! MINI
            OnProgress(new ProgressEventArgs(0, -1, mStreamInfo.Position, mStreamInfo.TotalBytes));
#endif
            return this;
        }

        #endregion

        #region "  BeginReadFile  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginReadFile/*'/>
        public IDisposable BeginReadFile(string fileName)
        {
            BeginReadStream(new StreamReader(fileName, mEncoding, true));
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


            LineInfo line = new LineInfo(string.Empty);
            line.mReader = mAsyncReader;

            if (mLastRecordValues == null)
                mLastRecordValues = new object[mRecordInfo.FieldCount];

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

#if ! MINI

                            BeforeReadRecordEventArgs<T> e = new BeforeReadRecordEventArgs<T>(currentLine, LineNumber);
                        skip = OnBeforeReadRecord(e);
                        if (e.RecordLineChanged)
                            line.ReLoad(e.RecordLine);


                        OnProgress(new ProgressEventArgs(mCurrentRecord, -1, mStreamInfo.Position, mStreamInfo.TotalBytes));


#endif
                        if (skip == false)
                        {
                            mLastRecord = (T)mRecordInfo.Operations.StringToRecord(line, mLastRecordValues);

#if ! MINI

    						skip = OnAfterReadRecord(currentLine, (T) mLastRecord, e.RecordLineChanged);
#endif

                            if (skip == false && mLastRecord != null)
                            {
                                byPass = true;
                                return;
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
                                ErrorInfo err = new ErrorInfo();
                                err.mLineNumber = mAsyncReader.LineNumber;
                                err.mExceptionInfo = ex;
                                //							err.mColumnNumber = mColumnNum;
                                err.mRecordString = currentLine;

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
                    mLastRecordValues = null;

					mLastRecord = default(T);


                    if (mRecordInfo.IgnoreLast > 0)
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
                    arr.Add(mLastRecord);
                else
                    break;
            }
			return arr.ToArray();
        }

        #endregion

        #region "  Close  "

        /// <summary>
        /// Save all the buffered data for write to the disk. 
        /// Useful to opened async engines that wants to save pending values to disk or for engines used for logging.
        /// </summary>
        public void Flush()
        {
            if (mAsyncWriter != null)
                mAsyncWriter.Flush();
        }

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/Close/*'/>
        public void Close()
        {
            mState = EngineState.Closed;

            try
            {
                mLastRecordValues = null;
			mLastRecord = default(T);

                if (mAsyncReader != null)
                    mAsyncReader.Close();

                mAsyncReader = null;
            }
            catch
            { }

            try
            {
                if (mAsyncWriter != null)
                {
                    if (mFooterText != null && mFooterText != string.Empty)
                        if (mFooterText.EndsWith(StringHelper.NewLine))
                            mAsyncWriter.Write(mFooterText);
                        else
                            mAsyncWriter.WriteLine(mFooterText);

                    mAsyncWriter.Close();
                    mAsyncWriter = null;


                }

            }
            catch
            { }
        }

        #endregion

        #region "  BeginWriteStream"

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginWriteStream/*'/>
        public IDisposable BeginWriteStream(TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentException("writer", "The TextWriter can´t be null.");

            if (mAsyncReader != null)
                throw new BadUsageException("You can't start to write while you are reading.");


            mState = EngineState.Writing;
            ResetFields();
            mAsyncWriter = writer;
            WriteHeader();
            mStreamInfo = new StreamInfoProvider(mAsyncWriter);
            mCurrentRecord = 0;

#if ! MINI
            OnProgress(new ProgressEventArgs(0, -1, mStreamInfo.Position, mStreamInfo.TotalBytes));
#endif

            return this;
        }

        private void WriteHeader()
        {
            if (mHeaderText != null && mHeaderText != string.Empty)
                if (mHeaderText.EndsWith(StringHelper.NewLine))
                    mAsyncWriter.Write(mHeaderText);
                else
                    mAsyncWriter.WriteLine(mHeaderText);
        }

        #endregion

        #region "  BeginWriteFile  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginWriteFile/*'/>
        public IDisposable BeginWriteFile(string fileName)
        {
            BeginWriteStream(new StreamWriter(fileName, false, mEncoding));

            return this;
        }

        #endregion


        #region "  BeginAppendToFile  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginAppendToFile/*'/>
        public IDisposable BeginAppendToFile(string fileName)
        {
            if (mAsyncReader != null)
                throw new BadUsageException("You can't start to write while you are reading.");

            mAsyncWriter = StreamHelper.CreateFileAppender(fileName, mEncoding, false);
            mHeaderText = String.Empty;
            mFooterText = String.Empty;
            mState = EngineState.Writing;
            mStreamInfo = new StreamInfoProvider(mAsyncWriter);
            mCurrentRecord = 0;

#if ! MINI
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
                throw new BadUsageException("The record to write can´t be null.");

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
                skip = OnBeforeWriteRecord(record);

                OnProgress(new ProgressEventArgs(mCurrentRecord, -1, mStreamInfo.Position, mStreamInfo.TotalBytes));

#endif

                if (skip == false)
                {
                    currentLine = mRecordInfo.Operations.RecordToString(record);
#if !MINI
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
                        ErrorInfo err = new ErrorInfo();
                        err.mLineNumber = mLineNumber;
                        err.mExceptionInfo = ex;
                        //							err.mColumnNumber = mColumnNum;
                        err.mRecordString = currentLine;
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
                throw new ArgumentNullException("The record to write can´t be null.");

            bool first = true;


			foreach (T rec in records)
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
        /// Write the current record values in the buffer. You can use engine[0] or engine["YourField"] to set the values.
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

                currentLine = mRecordInfo.Operations.RecordValuesToString(this.mLastRecordValues);
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
                        ErrorInfo err = new ErrorInfo();
                        err.mLineNumber = mLineNumber;
                        err.mExceptionInfo = ex;
                        //							err.mColumnNumber = mColumnNum;
                        err.mRecordString = currentLine;
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

			FileHelperAsyncEngine<T> mEngine;
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
