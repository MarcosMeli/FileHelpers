

#undef GENERICS
//#define GENERICS
//#if NET_2_0

using System;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text;
using FileHelpers.Events;
using FileHelpers.MasterDetail;
//using Container=FileHelpers.Container;

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

	/// <summary><para>This engine allows you to parse and write files that contain
    /// records of different types and that are in a linear relationship</para>
    /// <para>(for Master-Detail check the <see cref="MasterDetailEngine"/>)</para>
	/// </summary>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="class_diagram.html">Class Diagram</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	/// <seealso href="attributes.html">Attributes List</seealso>
    [DebuggerDisplay("MultiRecordEngine for types: {ListTypes()}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
#if ! GENERICS
	public sealed class MultiRecordEngine : 
		EventEngineBase<object>, IEnumerable, IDisposable
#else
	public sealed class MultiRecordEngine<M,D> : 
		EngineBase, IEnumerable, IDisposable
#endif
	{

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IRecordInfo[] mMultiRecordInfo;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Hashtable mRecordInfoHash;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private RecordTypeSelector mRecordSelector;

        private string ListTypes()
        {
            string res = string.Empty;
            bool first = true;
            foreach (Type t in mRecordInfoHash.Keys)
	        {
                if (first)
                    first = false;
                else
                    res += ", ";

                res += t.Name;
	        }

            return res;
        }


		/// <summary>
		/// The Selector used by the engine in Read operations to determine the Type to use.
		/// </summary>
		public RecordTypeSelector RecordSelector
		{
			get { return mRecordSelector; }
			set { mRecordSelector = value; }
		}


        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Type[] mTypes;
		
		#region "  Constructor  "

		/// <summary>Create a new instance of the MultiRecordEngine</summary>
		/// <param name="recordTypes">The Types of the records that this engine can handle.</param>
		public MultiRecordEngine(params Type[] recordTypes) : this(null, recordTypes)
		{
		}

		/// <summary>Create a new instance of the MultiRecordEngine</summary>
		/// <param name="recordTypes">The Types of the records that this engine can handle.</param>
		/// <param name="recordSelector">Used only in read operations. The selector indicates to the engine what Type to use in each read line.</param>
		public MultiRecordEngine(RecordTypeSelector recordSelector, params Type[] recordTypes) : base(GetFirstType(recordTypes))
		{
			mTypes = recordTypes;
			mMultiRecordInfo = new IRecordInfo[mTypes.Length];
			mRecordInfoHash = new Hashtable(mTypes.Length);
			for(int i=0; i < mTypes.Length; i++)
			{
				if (mTypes[i] == null)
					throw new BadUsageException("The type at index "+ i.ToString() + " is null.");

				if (mRecordInfoHash.Contains(mTypes[i]))
					throw new BadUsageException("The type '"+ mTypes[i].Name + " is already in the engine. You can't pass the same type twice to the constructor.");

			    mMultiRecordInfo[i] = RecordInfo.Resolve(mTypes[i]);
			    mRecordInfoHash.Add(mTypes[i], mMultiRecordInfo[i]);
			}
			mRecordSelector = recordSelector;
		}

		#endregion

		#region "  Events  "

        ///// <summary>Called in read operations just before the record string is translated to a record.</summary>
        //public event EventHandler<BeforeReadRecordEventArgs> BeforeReadRecord;
        ///// <summary>Called in read operations just after the record was created from a record string.</summary>
        //public event EventHandler<AfterReadRecordEventArgs> AfterReadRecord;
        ///// <summary>Called in write operations just before the record is converted to a string to write it.</summary>
        //public event EventHandler<BeforeWriteRecordEventArgs> BeforeWriteRecord;
        ///// <summary>Called in write operations just after the record was converted to a string.</summary>
        //public event EventHandler<AfterWriteRecordEventArgs> AfterWriteRecord;


        //private bool OnBeforeReadRecord(BeforeReadRecordEventArgs e)
        //{
        //    if (BeforeReadRecord != null)
        //    {
        //        BeforeReadRecord(this, e);

        //        return e.SkipThisRecord;
        //    }

        //    return false;
        //}

        //private bool OnAfterReadRecord(string line, object record)
        //{
        //    if (mRecordInfo.NotifyRead)
        //        ((INotifyRead)record).AfterRead(this, line);

        //    if (AfterReadRecord != null)
        //    {
        //        AfterReadRecordEventArgs e = new AfterReadRecordEventArgs(line, record, LineNumber);
        //        AfterReadRecord(this, e);
            	
        //        return e.SkipThisRecord;
        //    }
        //    return false;
        //}


        //private bool OnBeforeWriteRecord(object record)
        //{
        //    if (mRecordInfo.NotifyWrite)
        //        ((INotifyWrite)record).BeforeWrite(this);
			
        //    if (BeforeWriteRecord != null)
        //    {
        //        BeforeWriteRecordEventArgs e = new BeforeWriteRecordEventArgs(record, LineNumber);
        //        BeforeWriteRecord(this, e);

        //        return e.SkipThisRecord;
        //    }

        //    return false;
        //}

        //private string OnAfterWriteRecord(string line, object record)
        //{
        //    if (AfterWriteRecord != null)
        //    {
        //        AfterWriteRecordEventArgs e = new AfterWriteRecordEventArgs(record, LineNumber, line);
        //        AfterWriteRecord(this, e);
        //        return e.RecordLine;
        //    }
        //    return line;
        //}

		#endregion

		#region "  ReadFile  "

		/// <summary>
		/// Read a File and returns the records.
		/// </summary>
		/// <param name="fileName">The file with the records.</param>
		/// <returns>The read records of the differents types all mixed.</returns>
		public object[] ReadFile(string fileName)
		{
            using (var fs = new StreamReader(fileName, mEncoding, true, DefaultReadBufferSize))
			{
			    return ReadStream(fs);
			}
		}

		#endregion

		#region "  ReadStream  "
        public object[] ReadStream(TextReader reader)
        {
            return ReadStream(new NewLineDelimitedRecordReader(reader));
        }

        /// <include file='MultiRecordEngine.docs.xml' path='doc/ReadStream/*'/>
		public object[] ReadStream(IRecordReader reader)
		{
			if (reader == null)
				throw new ArgumentNullException("reader", "The reader of the Stream can´t be null");

			if (mRecordSelector == null)
				throw new BadUsageException("The Recordselector can´t be null, please pass a not null Selector in the constructor.");

			ResetFields();
			mHeaderText = String.Empty;
			mFooterText = String.Empty;

			ArrayList resArray = new ArrayList();

            using (ForwardReader freader = new ForwardReader(reader, mMultiRecordInfo[0].IgnoreLast))
            {
                freader.DiscardForward = true;

                string currentLine, completeLine;

                mLineNumber = 1;

                completeLine = freader.ReadNextLine();
                currentLine = completeLine;

#if !MINI
                if (MustNotifyProgress) // Avoid object creation
                    OnProgress(new ProgressEventArgs(0, -1));
#endif
                int currentRecord = 0;

                if (mMultiRecordInfo[0].IgnoreFirst > 0)
                {
                    for (int i = 0; i < mMultiRecordInfo[0].IgnoreFirst && currentLine != null; i++)
                    {
                        mHeaderText += currentLine + StringHelper.NewLine;
                        currentLine = freader.ReadNextLine();
                        mLineNumber++;
                    }
                }


                bool byPass = false;

                LineInfo line = new LineInfo(currentLine);
                line.mReader = freader;

                while (currentLine != null)
                {
                    try
                    {
                        mTotalRecords++;
                        currentRecord++;

                        line.ReLoad(currentLine);

                        var skip = false;

                        Type currType = mRecordSelector(this, currentLine);

                        if (currType != null)
                        {
                            RecordInfo info = (RecordInfo) mRecordInfoHash[currType];
                            if (info == null)
                                throw new BadUsageException("A record is of type '" + currType.Name +
                                                            "' which this engine is not configured to handle. Try adding this type to the constructor.");

                            var record = info.Operations.CreateRecordHandler();

#if !MINI
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

#endif

                            if (skip == false)
                            {
                                var values = new object[info.FieldCount];
                                if (info.Operations.StringToRecord(record, line, values))
                                {
#if !MINI
                                    if (MustNotifyRead) // Avoid object creation
                                        skip = OnAfterReadRecord(currentLine, record, e.RecordLineChanged, LineNumber);
#endif

                                    if (skip == false)
                                        resArray.Add(record);
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
                                var err = new ErrorInfo();
                                err.mLineNumber = freader.LineNumber;
                                err.mExceptionInfo = ex;
                                //							err.mColumnNumber = mColumnNum;
                                err.mRecordString = completeLine;

                                mErrorManager.AddError(err);
                                break;
                        }
                    }
                    finally
                    {
                        if (byPass == false)
                        {
                            currentLine = freader.ReadNextLine();
                            completeLine = currentLine;
                            mLineNumber = freader.LineNumber;
                        }
                    }
                }

                if (mMultiRecordInfo[0].IgnoreLast > 0)
                {
                    mFooterText = freader.RemainingText;
                }
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
            using (var fs = new StreamWriter(fileName, false, mEncoding, EngineBase.DefaultWriteBufferSize))
			{
				WriteStream(fs, records, maxRecords);
				fs.Close();
			}

		}

		#endregion

		#region "  WriteStream  "

		/// <summary>
		/// Write the records to a file
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="records"></param>
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

			if (mHeaderText != null && mHeaderText.Length != 0)
				if (mHeaderText.EndsWith(StringHelper.NewLine))
					writer.Write(mHeaderText);
				else
					writer.WriteLine(mHeaderText);


			string currentLine = null;

			//ConstructorInfo constr = mType.GetConstructor(new Type[] {});
			int max = maxRecords;

			if (records is IList)
                max = Math.Min(max < 0 ? int.MaxValue : max, ((IList)records).Count);


#if !MINI
            if (MustNotifyProgress) // Avoid object creation
                OnProgress(new ProgressEventArgs(0, max));
               
#endif
			int recIndex = 0;

			foreach (object rec in records)
			{
				if (recIndex == maxRecords)
					break;
				try
				{
					if (rec == null)
						throw new BadUsageException("The record at index " + recIndex.ToString() + " is null.");

					bool skip = false;
#if !MINI
                    if (MustNotifyProgress) // Avoid object creation
                        OnProgress(new ProgressEventArgs(recIndex + 1, max));

                    if (MustNotifyWrite)
                        skip = OnBeforeWriteRecord(rec, LineNumber);
#endif

					IRecordInfo info = (IRecordInfo) mRecordInfoHash[rec.GetType()];

					if (info == null)
						throw new BadUsageException("The record at index " + recIndex.ToString() + " is of type '" + rec.GetType().Name+"' and the engine dont handle this type. You can add it to the constructor.");

					if (skip == false)
					{
                        currentLine = info.Operations.RecordToString(rec);
#if !MINI
                        if (MustNotifyWrite)
                            currentLine = OnAfterWriteRecord(currentLine, rec);
#endif
						writer.WriteLine(currentLine);
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
				recIndex++;
			}

			mTotalRecords = recIndex;

			if (mFooterText != null && mFooterText != string.Empty)
				if (mFooterText.EndsWith(StringHelper.NewLine))
					writer.Write(mFooterText);
				else
					writer.WriteLine(mFooterText);

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
			StringBuilder sb = new StringBuilder();
			StringWriter writer = new StringWriter(sb);
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
			AppendToFile(fileName, new object[] {record});
		}

		/// <include file='MultiRecordEngine.docs.xml' path='doc/AppendToFile2/*'/>
		public void AppendToFile(string fileName, IEnumerable records)
		{
			using(TextWriter writer = StreamHelper.CreateFileAppender(fileName, mEncoding, true, false, EngineBase.DefaultWriteBufferSize))
			{
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
			else if (types.Length == 0)
				throw new BadUsageException("An empty Type[] is not valid for the MultiRecordEngine.");
			else if (types.Length == 1)
				throw new BadUsageException("You only provide one type to the engine constructor. You need 2 or more types, for one type you can use the FileHelperEngine.");
			else
				return types[0];
		}
		

		// ASYNC METHODS --------------

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        ForwardReader mAsyncReader;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        TextWriter mAsyncWriter;

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

        public void BeginReadStream(TextReader reader)
        {
            BeginReadStream(new NewLineDelimitedRecordReader(reader));
        }

		/// <summary>
		/// Method used to use this engine in Async mode. Work together with <see cref="ReadNext"/>. (Remember to call Close after read the data)
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

			if (mRecordInfo.IgnoreFirst > 0)
			{
				for (int i = 0; i < mRecordInfo.IgnoreFirst; i++)
				{
				    string temp = reader.ReadRecord();
					mLineNumber++;
					if (temp != null)
						mHeaderText += temp + StringHelper.NewLine;
					else
						break;
				}
			}

			mAsyncReader = new ForwardReader(reader, mRecordInfo.IgnoreLast, mLineNumber) {DiscardForward = true};
		}

		#endregion

		#region "  BeginReadFile  "

		/// <summary>
		/// Method used to use this engine in Async mode. Work together with <see cref="ReadNext"/>. (Remember to call Close after read the data)
		/// </summary>
		/// <param name="fileName">The source file.</param>
		public void BeginReadFile(string fileName)
		{
            BeginReadStream(new StreamReader(fileName, mEncoding, true, DefaultReadBufferSize));
		}

		/// <summary>
		/// Method used to use this engine in Async mode. Work together with <see cref="ReadNext"/>. (Remember to call Close after read the data)
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
		/// Useful to long opened async engines that wants to save pending values or for engines used for logging.
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
			try
			{
				if (mAsyncReader != null)
					mAsyncReader.Close();

				mAsyncReader = null;
			}
			catch
			{}

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
			{}

		}

		#endregion
		
		
		// DIFFERENT FROM THE ASYNC ENGINE
		
		#region "  ReadNext  "

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/ReadNext/*'/>
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

			LineInfo line = new LineInfo(currentLine);
			line.mReader = mAsyncReader;
			
		
			while (true)
			{
				if (currentLine != null)
				{
					try
					{
						mTotalRecords++;

						Type currType = mRecordSelector(this, currentLine);
						
						line.ReLoad(currentLine);

						if (currType != null)
						{
							RecordInfo info = (RecordInfo) mRecordInfoHash[currType];
                            if (info == null)
                                throw new BadUsageException("A record is of type '" + currType.Name +
                                                            "' which this engine is not configured to handle. Try adding this type to the constructor.");
							object[] values = new object[info.FieldCount];
                            mLastRecord = info.Operations.StringToRecord(line, values);

							if (mLastRecord != null)
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
					mLastRecord = null;


					if (mRecordInfo.IgnoreLast > 0)
						mFooterText = mAsyncReader.RemainingText;

					try
					{
						mAsyncReader.Close();
					}
					catch
					{
					}

					return;
				}
			}
		}


		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/ReadNexts/*'/>
		public object[] ReadNexts(int numberOfRecords)
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
			MultiRecordEngine mEngine;

			public AsyncEnumerator(MultiRecordEngine engine)
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
		/// <summary>Write the next record to a file or stream opened with <see cref="BeginWriteFile" />, <see cref="BeginWriteStream" /> or <see cref="BeginAppendToFile" /> method.</summary>
		/// <param name="record">The record to write.</param>
		public void WriteNext(object record)
		{
			if (mAsyncWriter == null)
				throw new BadUsageException("Before call WriteNext you must call BeginWriteFile or BeginWriteStream.");

			if (record == null)
				throw new BadUsageException("The record to write can´t be null.");

			WriteRecord(record);
		}
		
		/// <summary>Write the nexts records to a file or stream opened with <see cref="BeginWriteFile" />, <see cref="BeginWriteStream" /> or <see cref="BeginAppendToFile" /> method.</summary>
		/// <param name="records">The records to write (Can be an array, ArrayList, etc)</param>
		public void WriteNexts(IEnumerable records)
		{
			if (mAsyncWriter == null)
				throw new BadUsageException("Before call WriteNext you must call BeginWriteFile or BeginWriteStream.");

			if (records == null)
				throw new ArgumentNullException("The record to write can´t be null.");

			int nro = 0;
			foreach (object rec in records)
			{
				nro++;

				if (rec == null)
					throw new BadUsageException("The record at index " + nro.ToString() + " is null.");

				WriteRecord(rec);
			}
		}







		private void WriteRecord(object record)
		{
			string currentLine = null;

			try
			{
				bool skip = false;
//#if !MINI
//				ProgressHelper.Notify(mNotifyHandler, mProgressMode, i+1, max);
//				skip = OnBeforeWriteRecord(records[i]);
//#endif

				mLineNumber++;
				mTotalRecords++;

				IRecordInfo info = (IRecordInfo) mRecordInfoHash[record.GetType()];

				if (info == null)
					throw new BadUsageException("A record is of type '" + record.GetType().Name+ "' and the engine dont handle this type. You can add it to the constructor.");

				if (skip == false)
				{
                    currentLine = info.Operations.RecordToString(record);
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

		#endregion




		#region "  BeginWriteStream"

		///	<summary>Set the stream to be used in the <see cref="WriteNext" /> operation.</summary>
		///	<remarks><para>When you finish to write to the file you must call <b><see cref="Close" /></b> method.</para></remarks>
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
			if (mHeaderText != null && mHeaderText != string.Empty)
				if (mHeaderText.EndsWith(StringHelper.NewLine))
					mAsyncWriter.Write(mHeaderText);
				else
					mAsyncWriter.WriteLine(mHeaderText);
		}

		#endregion

		#region "  BeginWriteFile  "

		///	<summary>Open a file for write operations. If exist the engine override it. You can use <see cref="WriteNext"/> or <see cref="WriteNexts"/> to write records.</summary>
		///	<remarks><para>When you finish to write to the file you must call <b><see cref="Close" /></b> method.</para></remarks>
		/// <param name="fileName">The file path to be opened for write.</param>
		public void BeginWriteFile(string fileName)
		{
            BeginWriteStream(new StreamWriter(fileName, false, mEncoding, EngineBase.DefaultWriteBufferSize));
		}

		#endregion


		#region "  BeginappendToFile  "

		///	<summary>Open a file to be appended at the end.</summary>
		///	<remarks><para>This method open and seek to the end the file.</para>
		///	<para>When you finish to append to the file you must call <b><see cref="Close" /></b> method.</para></remarks>
		///	<param name="fileName">The file path to be opened to write at the end.</param>
		public void BeginAppendToFile(string fileName)
		{
            mAsyncWriter = StreamHelper.CreateFileAppender(fileName, mEncoding, false, true, EngineBase.DefaultWriteBufferSize);
			mHeaderText = String.Empty;
			mFooterText = String.Empty;
		}

		#endregion

	}
}

//#endif
