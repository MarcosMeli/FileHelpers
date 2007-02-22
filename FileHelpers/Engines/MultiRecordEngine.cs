#undef GENERICS
//#define GENERICS
//#if NET_2_0

#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text;
using FileHelpers.MasterDetail;

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

	/// <summary>This engine allows you to parse and write files that contain
	/// records of different types and that are in a linear relationship
	/// (for Master-Detail check the <see cref="MasterDetailEngine"/>
	/// </summary>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="class_diagram.html">Class Diagram</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	/// <seealso href="example_datalink.html">Example of the DataLink</seealso>
	/// <seealso href="attributes.html">Attributes List</seealso>
#if ! GENERICS
	public sealed class MultiRecordEngine : 
		EngineBase, IEnumerable, IDisposable
#else
	public sealed class MultiRecordEngine<M,D> : 
		EngineBase, IEnumerable, IDisposable
#endif
	{
		private RecordInfo[] mMultiRecordInfo;
		private Hashtable mRecordInfoHash;
		private RecordTypeSelector mRecordSelector;

		/// <summary>
		/// The Selector used by the engine in Read operations to determine the Type to use.
		/// </summary>
		public RecordTypeSelector RecordSelector
		{
			get { return mRecordSelector; }
			set { mRecordSelector = value; }
		}

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
			mMultiRecordInfo = new RecordInfo[mTypes.Length];
			mRecordInfoHash = new Hashtable(mTypes.Length);
			for(int i=0; i < mTypes.Length; i++)
			{
				if (mTypes[i] == null)
					throw new BadUsageException("The type at index "+ i.ToString() + " is null.");

				if (mRecordInfoHash.Contains(mTypes[i]))
					throw new BadUsageException("The type '"+ mTypes[i].Name + " is already in the engine. You can't pass the same type twice to the constructor.");

				mMultiRecordInfo[i] = new RecordInfo(mTypes[i]); 
				mRecordInfoHash.Add(mTypes[i], mMultiRecordInfo[i]);
			}
			mRecordSelector = recordSelector;
		}

		#endregion

		#region "  Events  "

#if NET_1_1
		
        /// <summary>Called in read operations just before the record string is translated to a record.</summary>
        public event BeforeReadRecordHandler BeforeReadRecord;
        /// <summary>Called in read operations just after the record was created from a record string.</summary>
        public event AfterReadRecordHandler AfterReadRecord;
        /// <summary>Called in write operations just before the record is converted to a string to write it.</summary>
        public event BeforeWriteRecordHandler BeforeWriteRecord;
        /// <summary>Called in write operations just after the record was converted to a string.</summary>
        public event AfterWriteRecordHandler AfterWriteRecord;

		private bool OnBeforeReadRecord(string line)
		{
			if (BeforeReadRecord != null)
			{
				BeforeReadRecordEventArgs e = null;
				e = new BeforeReadRecordEventArgs(line, LineNumber);
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
		public event EventHandler<BeforeReadRecordEventArgs<object>> BeforeReadRecord;
		/// <summary>Called in read operations just after the record was created from a record string.</summary>
		public event EventHandler<AfterReadRecordEventArgs<object>> AfterReadRecord;
		/// <summary>Called in write operations just before the record is converted to a string to write it.</summary>
		public event EventHandler<BeforeWriteRecordEventArgs<object>> BeforeWriteRecord;
		/// <summary>Called in write operations just after the record was converted to a string.</summary>
		public event EventHandler<AfterWriteRecordEventArgs<object>> AfterWriteRecord;


        private bool OnBeforeReadRecord(string line)
        {
            if (BeforeReadRecord != null)
            {
                BeforeReadRecordEventArgs<object> e = null;
                e = new BeforeReadRecordEventArgs<object>(line, LineNumber);
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
                AfterReadRecordEventArgs<object> e = null;
                e = new AfterReadRecordEventArgs<object>(line, record, LineNumber);
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
                BeforeWriteRecordEventArgs<object> e = null;
                e = new BeforeWriteRecordEventArgs<object>(record, LineNumber);
                BeforeWriteRecord(this, e);

                return e.SkipThisRecord;
            }

            return false;
        }

        private string OnAfterWriteRecord(string line, object record)
        {
            if (AfterWriteRecord != null)
            {
                AfterWriteRecordEventArgs<object> e = null;
                e = new AfterWriteRecordEventArgs<object>(record, LineNumber, line);
                AfterWriteRecord(this, e);
                return e.RecordLine;
            }
            return line;
        }

#endif

		#endregion

		#region "  ReadFile  "

		/// <summary>
		/// Read a File and returns the records.
		/// </summary>
		/// <param name="fileName">The file with the records.</param>
		/// <returns>The read records of the differents types all mixed.</returns>
		public object[] ReadFile(string fileName)
		{
			using (StreamReader fs = new StreamReader(fileName, mEncoding, true))
			{
				object[] tempRes;
				tempRes = ReadStream(fs);
				fs.Close();

				return tempRes;
			}

		}

		#endregion

		#region "  ReadStream  "

		/// <include file='MultiRecordEngine.docs.xml' path='doc/ReadStream/*'/>
		public object[] ReadStream(TextReader reader)
		{
			if (reader == null)
				throw new ArgumentNullException("reader", "The reader of the Stream can´t be null");

			if (mRecordSelector == null)
				throw new BadUsageException("The Recordselector can´t be null, please pass a not null Selector in the constructor.");

			ResetFields();
			mHeaderText = String.Empty;
			mFooterText = String.Empty;

			ArrayList resArray = new ArrayList();

			ForwardReader freader = new ForwardReader(reader, mMultiRecordInfo[0].mIgnoreLast);
			freader.DiscardForward = true;

			string currentLine, completeLine;

			mLineNumber = 1;

			completeLine = freader.ReadNextLine();
			currentLine = completeLine;

			#if !MINI
				ProgressHelper.Notify(mNotifyHandler, mProgressMode, 0, -1);
			#endif
			int currentRecord = 0;

			if (mMultiRecordInfo[0].mIgnoreFirst > 0)
			{
				for (int i = 0; i < mMultiRecordInfo[0].mIgnoreFirst && currentLine != null; i++)
				{
					mHeaderText += currentLine + StringHelper.NewLine;
					currentLine = freader.ReadNextLine();
					mLineNumber++;
				}
			}


			bool byPass = false;

//			MasterDetails record = null;
			ArrayList tmpDetails = new ArrayList();

			LineInfo line = new LineInfo(currentLine);
			line.mReader = freader;
			
			while (currentLine != null)
			{
				try
				{

					mTotalRecords++;
					currentRecord++;
					
					line.ReLoad(currentLine);

                    bool skip = false;
					#if !MINI
						ProgressHelper.Notify(mNotifyHandler, mProgressMode, currentRecord, -1);
                        skip = OnBeforeReadRecord(currentLine);
					#endif

					Type currType = mRecordSelector(this, currentLine);

					if (currType != null)
					{
						RecordInfo info = (RecordInfo) mRecordInfoHash[currType];

                        if (skip == false)
                        {
                            object record = info.StringToRecord(line);

#if !MINI
							skip = OnAfterReadRecord(currentLine, record);
#endif
                        	
							if (skip == false && record != null)
								resArray.Add(record);

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

			if (mMultiRecordInfo[0].mIgnoreLast > 0)
			{
				mFooterText = freader.RemainingText;
			}

			return resArray.ToArray();
		}

		#endregion

		#region "  ReadString  "

		/// <include file='MultiRecordEngine.docs.xml' path='doc/ReadString/*'/>
		public object[] ReadString(string source)
		{
			StringReader reader = new StringReader(source);
			object[] res = ReadStream(reader);
			reader.Close();
			return res;
		}

		#endregion

		#region "  WriteFile  "

		/// <include file='MultiRecordEngine.docs.xml' path='doc/WriteFile/*'/>
		public void WriteFile(string fileName, object[] records)
		{
			WriteFile(fileName, records, -1);
		}

		/// <include file='MultiRecordEngine.docs.xml' path='doc/WriteFile2/*'/>
		public void WriteFile(string fileName, object[] records, int maxRecords)
		{
			using (StreamWriter fs = new StreamWriter(fileName, false, mEncoding))
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
		public void WriteStream(TextWriter writer, object[] records)
		{
			WriteStream(writer, records, -1);
		}

		/// <include file='MultiRecordEngine.docs.xml' path='doc/WriteStream2/*'/>
		public void WriteStream(TextWriter writer, object[] records, int maxRecords)
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
			int max = records.Length;

			if (maxRecords >= 0)
				max = Math.Min(records.Length, maxRecords);


			#if !MINI
				ProgressHelper.Notify(mNotifyHandler, mProgressMode, 0, max);
                
			#endif

			for (int i = 0; i < max; i++)
			{
				try
				{
					if (records[i] == null)
						throw new BadUsageException("The record at index " + i.ToString() + " is null.");

                    bool skip = false;
					#if !MINI
						ProgressHelper.Notify(mNotifyHandler, mProgressMode, i+1, max);
                        skip = OnBeforeWriteRecord(records[i]);
					#endif

					RecordInfo info = (RecordInfo) mRecordInfoHash[records[i].GetType()];

					if (info == null)
						throw new BadUsageException("The record at index " + i.ToString() + " is of type '"+records[i].GetType().Name+"' and you don't add it in the constructor.");

                    if (skip == false)
                    {
                        currentLine = info.RecordToString(records[i]);
#if !MINI
                        currentLine = OnAfterWriteRecord(currentLine, records[i]);
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

			}

			mTotalRecords = records.Length;

			if (mFooterText != null && mFooterText != string.Empty)
				if (mFooterText.EndsWith(StringHelper.NewLine))
					writer.Write(mFooterText);
				else
					writer.WriteLine(mFooterText);

		}

		#endregion

		#region "  WriteString  "

		/// <include file='MultiRecordEngine.docs.xml' path='doc/WriteString/*'/>
		public string WriteString(object[] records)
		{
			return WriteString(records, -1);
		}

		/// <include file='MultiRecordEngine.docs.xml' path='doc/WriteString2/*'/>
		public string WriteString(object[] records, int maxRecords)
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
		public void AppendToFile(string fileName, object[] records)
		{
			using(TextWriter writer = StreamHelper.CreateFileAppender(fileName, mEncoding, true, false))
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
		

		//      ASYNC METHODS !!!!!!!
		
		ForwardReader mAsyncReader;

		#region "  LastRecord  "

		private object mLastRecord;

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/LastRecord/*'/>
		public object LastRecord
		{
			get { return mLastRecord; }
		}

		#endregion

		#region "  BeginReadStream"


		/// <summary>
		/// Method used to use this engine in Async mode. Work together with <see cref="ReadNext"/>. (Remember to call Close after read the data)
		/// </summary>
		/// <param name="reader">The source Reader.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void BeginReadStream(TextReader reader)
		{
			if (reader == null)
				throw new ArgumentNullException("The TextReader can´t be null.");

			ResetFields();
			mHeaderText = String.Empty;
			mFooterText = String.Empty;

			if (mRecordInfo.mIgnoreFirst > 0)
			{
				for (int i = 0; i < mRecordInfo.mIgnoreFirst; i++)
				{
					string temp = reader.ReadLine();
					mLineNumber++;
					if (temp != null)
						mHeaderText += temp + StringHelper.NewLine;
					else
						break;
				}
			}

			mAsyncReader = new ForwardReader(reader, mRecordInfo.mIgnoreLast, mLineNumber);
			mAsyncReader.DiscardForward = true;
		}

		#endregion

		#region "  BeginReadFile  "

		/// <summary>
		/// Method used to use this engine in Async mode. Work together with <see cref="ReadNext"/>. (Remember to call Close after read the data)
		/// </summary>
		/// <param name="fileName">The source file.</param>
		public void BeginReadFile(string fileName)
		{
			BeginReadStream(new StreamReader(fileName, mEncoding, true));
		}

		/// <summary>
		/// Method used to use this engine in Async mode. Work together with <see cref="ReadNext"/>. (Remember to call Close after read the data)
		/// </summary>
		/// <param name="sourceData">The source String</param>
		public void BeginReadString(string sourceData)
		{
			if (sourceData == null)
				sourceData = String.Empty;

			BeginReadStream(new StringReader(sourceData));
		}

		#endregion
		
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
			{
			}
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
							mLastRecord = info.StringToRecord(line);

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


					if (mRecordInfo.mIgnoreLast > 0)
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
			return (object[])  arr.ToArray(RecordType);
		}

		#endregion
		
		#region "  IEnumerable implementation  "
 		
		IEnumerator IEnumerable.GetEnumerator()
		{
			if (mAsyncReader == null)
				throw new FileHelperException("You must call BeginRead before use the engine in a foreach loop.");

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
		
		void IDisposable.Dispose()
		{
			Close();
			GC.SuppressFinalize(this);
		}
 		
		/// <summary>Destructor</summary>
#if ! GENERICS
		~MultiRecordEngine()
#else
		~FileHelperAsyncEngine<T>
#endif
		{
			Close();
		}

		#endregion

	}
}

//#endif