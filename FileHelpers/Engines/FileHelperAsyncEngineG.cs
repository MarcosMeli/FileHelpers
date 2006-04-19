#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar"

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
using System.IO;

#if NET_2_0

namespace FileHelpers
{
	/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngine/*'/>
	/// <include file='Examples.xml' path='doc/examples/FileHelperAsyncEngine/*'/>
	public sealed class FileHelperAsyncEngine<T> : EngineBase
	{
		#region "  Constructor  "

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/FileHelperAsyncEngineCtr/*'/>
		public FileHelperAsyncEngine() : base(typeof(T))
		{
		}

		#endregion

		ForwardReader mAsyncReader;
		TextWriter mAsyncWriter;

		#region "  LastRecord  "

		private T mLastRecord;

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/LastRecord/*'/>
		public T LastRecord
		{
			get { return mLastRecord; }
		}

		#endregion

		#region "  BeginReadStream"

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginReadStream/*'/>
		public void BeginReadStream(TextReader reader)
		{
			if (reader == null)
				throw new BadUsageException("The TextReader can´t be null.");

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

		private void ReadNextRecord()
		{
			string currentLine = mAsyncReader.ReadNextLine();
			mLineNumber++;

			bool byPass = false;

			mLastRecord = default(T);

			while (true)
			{
				if (currentLine != null)
				{
					try
					{
						mTotalRecords++;
						mLastRecord = (T) mRecordInfo.StringToRecord(currentLine, mAsyncReader);
						byPass = true;
						return;
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
					mLastRecord = default(T);

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
		public T[] ReadNexts(int numberOfRecords)
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

			return (T[]) arr.ToArray(RecordType);
		}

		#endregion

		#region "  EndsRead  "

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/EndsRead/*'/>
		public void EndsRead()
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

		#region "  BeginWriteStream"

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/BeginWriteStream/*'/>
		public void BeginWriteStream(TextWriter writer)
		{
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
		public void WriteNexts(T[] records)
		{
			if (mAsyncWriter == null)
				throw new BadUsageException("Before call WriteNext you must call BeginWriteFile or BeginWriteStream.");

			if (records == null)
				throw new BadUsageException("The record to write can´t be null.");

			if (records.Length == 0)
				return;

			if (RecordType.IsAssignableFrom(records[0].GetType()) == false)
				throw new BadUsageException("The record must be of type: " + RecordType.Name);

			foreach (T rec in records)
			{
				WriteRecord(rec);
			}

		}

		#endregion

		#region "  EndsWrite  "

		/// <include file='FileHelperAsyncEngine.docs.xml' path='doc/EndsWrite/*'/>
		public void EndsWrite()
		{
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
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}

		}

		#endregion
	}
}

#endif