#undef GENERICS
//#define GENERICS
//#if NET_2_0

#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

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
    public sealed class FileHelperAsyncEngine<T> : 
		EngineBase, IEnumerable, IDisposable
#endif
    {
		#region "  Constructor  "

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngineCtr/*'/>
#if ! GENERICS
		public FileHelperAsyncEngine(Type recordType) 
			: base(recordType)
#else
		public FileHelperAsyncEngine() 
			: base(typeof(T))
#endif
		{
		}

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngineCtr/*'/>
		/// <param name="encoding">The encoding used by the Engine.</param>
#if ! GENERICS
		public FileHelperAsyncEngine(Type recordType, Encoding encoding)
			: base(recordType, encoding)
#else
		public FileHelperAsyncEngine(Encoding encoding)
			: base(typeof(T), encoding)
#endif
		{
		}

		#endregion

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        ForwardReader mAsyncReader;
#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        TextWriter mAsyncWriter;

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

        #endregion

        #region "  BeginReadStream"

        /// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginReadStream/*'/>
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
			
			while (true)
			{
				if (currentLine != null)
				{
					try
					{
						mTotalRecords++;
						line.ReLoad(currentLine);

#if ! GENERICS
						mLastRecord = mRecordInfo.StringToRecord(line);
#else
						mLastRecord = (T) mRecordInfo.StringToRecord(line);
#endif

						if (mLastRecord != null)
						{
							byPass = true;
							return;
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
		/// Useful to long opened async engines that wants to save pending values or for engines used for logging.
		/// </summary>
		public void Flush()
		{
			if (mAsyncWriter != null)
				mAsyncWriter.Flush();
		}
		
		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/Close/*'/>
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

		#region "  BeginWriteStream"

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginWriteStream/*'/>
		public void BeginWriteStream(TextWriter writer)
		{
			if (writer == null)
				throw new ArgumentException("writer", "The TextWriter can´t be null.");

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


		#region "  BeginappendToFile  "

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginAppendToFile/*'/>
		public void BeginAppendToFile(string fileName)
		{
			mAsyncWriter = StreamHelper.CreateFileAppender(fileName, mEncoding, false);
			mHeaderText = String.Empty;
			mFooterText = String.Empty;
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

				currentLine = mRecordInfo.RecordToString(record);
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


		#region "  IEnumerable implementation  "

        /// <summary>Allows to loop record by record in the engine</summary>
        /// <returns>The enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
 		{
 			if (mAsyncReader == null)
 				throw new FileHelpersException("You must call BeginRead before use the engine in a for each loop.");
 			
#if ! GENERICS
			return new AsyncEnumerator(this);
#else
				return new AsyncEnumerator<T>(this);
#endif
 		}
 		
#if ! GENERICS
		private class AsyncEnumerator : IEnumerator
		{
			FileHelperAsyncEngine mEngine;
			public AsyncEnumerator(FileHelperAsyncEngine engine)
#else
		private class AsyncEnumerator<X> : IEnumerator
		{
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

	}
}

//#endif