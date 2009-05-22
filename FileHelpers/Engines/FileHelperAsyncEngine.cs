#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

//#undef GENERICS
#define GENERICS
#if NET_2_0

using System;
using System.Diagnostics;
using System.Collections;
using System.IO;
using System.Text;

#if GENERICS
using System.Collections.Generic;
#endif

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
#if NET_2_0
    [DebuggerDisplay("FileHelperAsyncEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
#endif
#if ! GENERICS
    public sealed class FileHelperAsyncEngine :
        EngineBase, IEnumerable, IDisposable
#else
    /// <typeparam name="T">The record type.</typeparam>
    public class FileHelperAsyncEngine<T> :
        EngineBase, IEnumerable<T>, IDisposable
#endif
    {

        #region "  Constructor  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngineCtr/*'/>
		public FileHelperAsyncEngine() 
			: base(typeof(T))
        {
            CreateRecordOptions();
        }

        protected FileHelperAsyncEngine(Type type)
            : base(type)
        {
            CreateRecordOptions();
        }

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngineCtr/*'/>
        /// <param name="encoding">The encoding used by the Engine.</param>
		public FileHelperAsyncEngine(Encoding encoding)
			: base(typeof(T), encoding)
        {
            CreateRecordOptions();
        }

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngineCtr/*'/>
        /// <param name="encoding">The encoding used by the Engine.</param>
        public FileHelperAsyncEngine(Type type, Encoding encoding)
            : base(type, encoding)
        {
            CreateRecordOptions();
        }

        private void CreateRecordOptions()
        {
            if (mRecordInfo.IsDelimited)
                mOptions = new DelimitedRecordOptions(mRecordInfo);
            else
                mOptions = new FixedRecordOptions(mRecordInfo);
        }

        #endregion

        #region "  Readers and Writters  "

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        ForwardReader mAsyncReader;
#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        TextWriter mAsyncWriter;

        #endregion

        #region "  LastRecord  "

#if ! GENERICS
#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private object mLastRecord;
        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/LastRecord/*'/>

        public object LastRecord
        {
            get { return mLastRecord; }
        }
#else
#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
		private T mLastRecord;

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/LastRecord/*'/>
		public T LastRecord
		{
			get { return mLastRecord; }
		}
#endif

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
                    mLastRecordValues = new object[mRecordInfo.mFieldCount];

                if (value == null)
                {
                    if (mRecordInfo.mFields[fieldIndex].FieldType.IsValueType)
                        throw new BadUsageException("You can't assing null to a value type.");

                    mLastRecordValues[fieldIndex] = null;
                }
                else
                {
                    if (!mRecordInfo.mFields[fieldIndex].FieldType.IsInstanceOfType(value))
                        throw new BadUsageException(string.Format("Invalid type: {0}. Expected: {1}", value.GetType().Name, mRecordInfo.mFields[fieldIndex].FieldType.Name));

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
        public void BeginReadStream(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader", "The TextReader can´t be null.");

            if (mAsyncWriter != null)
                throw new BadUsageException("You can't start to read while you are writing.");

            NewLineDelimitedRecordReader recordReader = new NewLineDelimitedRecordReader(reader);

            ResetFields();
            mHeaderText = String.Empty;
            mFooterText = String.Empty;

            if (mRecordInfo.mIgnoreFirst > 0)
            {
                for (int i = 0; i < mRecordInfo.mIgnoreFirst; i++)
                {
                    string temp = recordReader.ReadRecord();
                    mLineNumber++;
                    if (temp != null)
                        mHeaderText += temp + StringHelper.NewLine;
                    else
                        break;
                }
            }

            mAsyncReader = new ForwardReader(recordReader, mRecordInfo.mIgnoreLast, mLineNumber);
            mAsyncReader.DiscardForward = true;
            mState = EngineState.Reading;

        }

        #endregion

        #region "  BeginReadFile  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginReadFile/*'/>
        public void BeginReadFile(string fileName)
        {
            BeginReadStream(new StreamReader(fileName, mEncoding, true));
        }

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginReadString/*'/>
        public void BeginReadString(string sourceData)
        {
            if (sourceData == null)
                sourceData = String.Empty;

            BeginReadStream(new StringReader(sourceData));
        }

        #endregion

        #region "  ReadNext  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/ReadNext/*'/>
#if ! GENERICS
        public object ReadNext()
#else
		public T ReadNext()
#endif
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

#if ! GENERICS
            mLastRecord = null;
#else
			mLastRecord = default(T);
#endif


            LineInfo line = new LineInfo(string.Empty);
            line.mReader = mAsyncReader;

            if (mLastRecordValues == null)
                mLastRecordValues = new object[mRecordInfo.mFieldCount];

            while (true)
            {
                if (currentLine != null)
                {
                    try
                    {
                        mTotalRecords++;
                        line.ReLoad(currentLine);

                        bool skip = false;

#if ! MINI

    #if ! GENERICS 
                            BeforeReadRecordEventArgs e = new BeforeReadRecordEventArgs(currentLine, LineNumber);
    #else
                            BeforeReadRecordEventArgs<T> e = new BeforeReadRecordEventArgs<T>(currentLine, LineNumber);
    #endif
                        skip = OnBeforeReadRecord(e);
                        if (e.RecordLineChanged)
                            line.ReLoad(e.RecordLine);

#endif
                        if (skip == false)
                        {
#if ! GENERICS
                            mLastRecord = mRecordInfo.StringToRecord(line, mLastRecordValues);
#else
	    					mLastRecord = (T) mRecordInfo.StringToRecord(line, mLastRecordValues);
#endif

#if ! MINI

#if ! GENERICS
                            skip = OnAfterReadRecord(currentLine, mLastRecord);
#else
    						skip = OnAfterReadRecord(currentLine, (T) mLastRecord);
#endif
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

#if ! GENERICS
                    mLastRecord = null;
#else
					mLastRecord = default(T);
#endif


                    if (mRecordInfo.mIgnoreLast > 0)
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


        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/ReadNexts/*'/>
#if ! GENERICS
        public object[] ReadNexts(int numberOfRecords)
#else
		public T[] ReadNexts(int numberOfRecords)
#endif
        {
            if (mAsyncReader == null)
                throw new BadUsageException("Before call ReadNext you must call BeginReadFile or BeginReadStream.");

            ArrayList arr = new ArrayList(numberOfRecords);

            for (int i = 0; i < numberOfRecords; i++)
            {
                ReadNextRecord();
                if (mLastRecord != null)
                    arr.Add(mLastRecord);
                else
                    break;
            }
#if ! GENERICS
            return (object[])
#else
			return (T[])
#endif
arr.ToArray(RecordType);
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
#if ! GENERICS
                mLastRecord = null;
#else
			mLastRecord = default(T);
#endif

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
        public void BeginWriteStream(TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentException("writer", "The TextWriter can´t be null.");

            if (mAsyncReader != null)
                throw new BadUsageException("You can't start to write while you are reading.");


            mState = EngineState.Writing;
            ResetFields();
            mAsyncWriter = writer;
            WriteHeader();
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
        public void BeginWriteFile(string fileName)
        {
            BeginWriteStream(new StreamWriter(fileName, false, mEncoding));
        }

        #endregion


        #region "  BeginAppendToFile  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginAppendToFile/*'/>
        public void BeginAppendToFile(string fileName)
        {
            if (mAsyncReader != null)
                throw new BadUsageException("You can't start to write while you are reading.");

            mAsyncWriter = StreamHelper.CreateFileAppender(fileName, mEncoding, false);
            mHeaderText = String.Empty;
            mFooterText = String.Empty;
            mState = EngineState.Writing;
        }

        #endregion

        #region "  WriteNext  "

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/WriteNext/*'/>
#if ! GENERICS
        public void WriteNext(object record)
#else
		public void WriteNext(T record)
#endif
        {
            if (mAsyncWriter == null)
                throw new BadUsageException("Before call WriteNext you must call BeginWriteFile or BeginWriteStream.");

            if (record == null)
                throw new BadUsageException("The record to write can´t be null.");

            if (RecordType.IsAssignableFrom(record.GetType()) == false)
                throw new BadUsageException("The record must be of type: " + RecordType.Name);

            WriteRecord(record);
        }

#if ! GENERICS
        private void WriteRecord(object record)
#else
		private void WriteRecord(T record)
#endif
        {
            string currentLine = null;

            try
            {
                mLineNumber++;
                mTotalRecords++;

                bool skip = false;
#if !MINI
                skip = OnBeforeWriteRecord(record);
#endif

                if (skip == false)
                {
                    currentLine = mRecordInfo.RecordToString(record);
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
#if ! GENERICS
        public void WriteNexts(IEnumerable records)
#else
		public void WriteNexts(IEnumerable<T> records)
#endif
        {
            if (mAsyncWriter == null)
                throw new BadUsageException("Before call WriteNext you must call BeginWriteFile or BeginWriteStream.");

            if (records == null)
                throw new ArgumentNullException("The record to write can´t be null.");

            bool first = true;


#if ! GENERICS
            foreach (object rec in records)
#else
			foreach (T rec in records)
#endif
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

                currentLine = mRecordInfo.RecordValuesToString(this.mLastRecordValues);
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

#if ! GENERICS
        /// <summary>Allows to loop record by record in the engine</summary>
        /// <returns>The enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            if (mAsyncReader == null)
                throw new FileHelpersException("You must call BeginRead before use the engine in a for each loop.");
            return new AsyncEnumerator(this);
        }
#else
        /// <summary>Allows to loop record by record in the engine</summary>
        /// <returns>The enumerator</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            if (mAsyncReader == null)
                throw new FileHelpersException("You must call BeginRead before use the engine in a for each loop.");
            return new AsyncEnumerator<T>(this);
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
            return new AsyncEnumerator<T>(this);
        }


#endif

#if ! GENERICS
        private class AsyncEnumerator : IEnumerator
        {
            FileHelperAsyncEngine mEngine;
            public AsyncEnumerator(FileHelperAsyncEngine engine)
#else
		private class AsyncEnumerator<X> : IEnumerator<X>
		{
            X IEnumerator<X>.Current
            {
                get { return mEngine.mLastRecord; }
            }

            void IDisposable.Dispose()
            {
                mEngine.Close();
            }

			FileHelperAsyncEngine<X> mEngine;
			public AsyncEnumerator(FileHelperAsyncEngine<X> engine)
#endif
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

        #region "  Options  "

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        internal RecordOptions mOptions;

        /// <summary>Allows to change some record layout options at runtime</summary>
        public RecordOptions Options
        {
            get { return mOptions; }
            set { mOptions = value; }
        }

        #endregion

        #region "  State  "

        private EngineState mState = EngineState.Closed;

        /// <summary>
        /// Indicates the current state of the engine.
        /// </summary>
        public EngineState State
        {
            get { return mState; }
            set { mState = value; }
        }


        #endregion


        #region "  Event Handling  "

#if ! MINI

#if ! GENERICS

        /// <summary>Called in read operations just before the record string is translated to a record.</summary>
        public event BeforeReadRecordHandler BeforeReadRecord;
        /// <summary>Called in read operations just after the record was created from a record string.</summary>
        public event AfterReadRecordHandler AfterReadRecord;
        /// <summary>Called in write operations just before the record is converted to a string to write it.</summary>
        public event BeforeWriteRecordHandler BeforeWriteRecord;
        /// <summary>Called in write operations just after the record was converted to a string.</summary>
        public event AfterWriteRecordHandler AfterWriteRecord;

        private bool OnBeforeReadRecord(BeforeReadRecordEventArgs e)
        {

            if (BeforeReadRecord != null)
            {
                BeforeReadRecord(this, e);

                return e.SkipThisRecord;
            }

            return false;
        }

        private bool OnAfterReadRecord(string line, object record)
        {
            if (mRecordInfo.mNotifyRead)
                ((INotifyRead)record).AfterRead(this, line);

            if (AfterReadRecord != null)
            {
                AfterReadRecordEventArgs e = null;
                e = new AfterReadRecordEventArgs(line, record, LineNumber);
                AfterReadRecord(this, e);
                return e.SkipThisRecord;
            }

            return false;
        }

        private bool OnBeforeWriteRecord(object record)
        {
            if (mRecordInfo.mNotifyWrite)
                ((INotifyWrite)record).BeforeWrite(this);

            if (BeforeWriteRecord != null)
            {
                BeforeWriteRecordEventArgs e = null;
                e = new BeforeWriteRecordEventArgs(record, LineNumber);
                BeforeWriteRecord(this, e);

                return e.SkipThisRecord;
            }

            return false;
        }

        private string OnAfterWriteRecord(string line, object record)
        {

            if (AfterWriteRecord != null)
            {
                AfterWriteRecordEventArgs e = null;
                e = new AfterWriteRecordEventArgs(record, LineNumber, line);
                AfterWriteRecord(this, e);
                return e.RecordLine;
            }
            return line;
        }


#else
        /// <summary>Called in read operations just before the record string is translated to a record.</summary>
        public event BeforeReadRecordHandler<T> BeforeReadRecord;
        /// <summary>Called in read operations just after the record was created from a record string.</summary>
        public event AfterReadRecordHandler<T> AfterReadRecord;
        /// <summary>Called in write operations just before the record is converted to a string to write it.</summary>
        public event BeforeWriteRecordHandler<T> BeforeWriteRecord;
        /// <summary>Called in write operations just after the record was converted to a string.</summary>
        public event AfterWriteRecordHandler<T> AfterWriteRecord;

		private bool OnBeforeReadRecord(BeforeReadRecordEventArgs<T> e)
		{

			if (BeforeReadRecord != null)
			{
				BeforeReadRecord(this, e);

				return e.SkipThisRecord;
			}

			return false;
		}

		private bool OnAfterReadRecord(string line, T record)
		{
			if (mRecordInfo.mNotifyRead)
				((INotifyRead)record).AfterRead(this, line);

		    if (AfterReadRecord != null)
			{
				AfterReadRecordEventArgs<T> e = null;
				e = new AfterReadRecordEventArgs<T>(line, record, LineNumber);
				AfterReadRecord(this, e);
				return e.SkipThisRecord;
			}
			
			return false;
		}

		private bool OnBeforeWriteRecord(T record)
		{
			if (mRecordInfo.mNotifyWrite)
				((INotifyWrite)record).BeforeWrite(this);

		    if (BeforeWriteRecord != null)
			{
				BeforeWriteRecordEventArgs<T> e = null;
                e = new BeforeWriteRecordEventArgs<T>(record, LineNumber);
				BeforeWriteRecord(this, e);

				return e.SkipThisRecord;
			}

			return false;
		}

		private string OnAfterWriteRecord(string line, T record)
		{

			if (AfterWriteRecord != null)
			{
                AfterWriteRecordEventArgs<T> e = null;
                e = new AfterWriteRecordEventArgs<T>(record, LineNumber, line);
                AfterWriteRecord(this, e);
				return e.RecordLine;
			}
			return line;
		}

#endif

#endif

        #endregion

    }
}


#endif
