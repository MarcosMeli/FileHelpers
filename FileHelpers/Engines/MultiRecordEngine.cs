#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
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
	/// <returns>the action used for the current record (Master, Detail, Skip)</returns>
	public delegate Type RecordTypeSelector(string recordString);

	#endregion

	public sealed class MultiRecordEngine : EngineBase
	{
		private RecordInfo[] mMultiRecordInfo;
		private Hashtable mRecordInfoHash;
		private RecordTypeSelector mRecordSelector;

		private Type[] mTypes;
		#region "  Constructor  "

		public MultiRecordEngine(Type[] recordTypes, RecordTypeSelector recordSelector) : base(recordTypes[0])
		{
			mTypes = recordTypes;
			mMultiRecordInfo = new RecordInfo[mTypes.Length];
			mRecordInfoHash = new Hashtable(mTypes.Length);
			for(int i=0; i < mTypes.Length; i++)
			{
				mMultiRecordInfo[i] = new RecordInfo(mTypes[i]); 
				mRecordInfoHash.Add(mTypes[i], mMultiRecordInfo[i]);
			}
			mRecordSelector = recordSelector;
		}

		#endregion

		#region "  ReadFile  "

		/// <include file='MasterDetailEngine.docs.xml' path='doc/ReadFile/*'/>
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

		/// <include file='MasterDetailEngine.docs.xml' path='doc/ReadStream/*'/>
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

			while (currentLine != null)
			{
				try
				{

					mTotalRecords++;
					currentRecord++; 

					#if !MINI
						ProgressHelper.Notify(mNotifyHandler, mProgressMode, currentRecord, -1);
					#endif

					Type currType = mRecordSelector(currentLine);

					if (currType != null)
					{
						RecordInfo info = (RecordInfo) mRecordInfoHash[currType];
						resArray.Add(info.StringToRecord(currentLine, freader));
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

		/// <include file='MasterDetailEngine.docs.xml' path='doc/ReadString/*'/>
		public object[] ReadString(string source)
		{
			StringReader reader = new StringReader(source);
			object[] res = ReadStream(reader);
			reader.Close();
			return res;
		}

		#endregion

		#region "  WriteFile  "

		/// <include file='MasterDetailEngine.docs.xml' path='doc/WriteFile/*'/>
		public void WriteFile(string fileName, MasterDetails[] records)
		{
			WriteFile(fileName, records, -1);
		}

		/// <include file='MasterDetailEngine.docs.xml' path='doc/WriteFile2/*'/>
		public void WriteFile(string fileName, MasterDetails[] records, int maxRecords)
		{
			using (StreamWriter fs = new StreamWriter(fileName, false, mEncoding))
			{
				WriteStream(fs, records, maxRecords);
				fs.Close();
			}

		}

		#endregion

		#region "  WriteStream  "

		public void WriteStream(TextWriter writer, object[] records)
		{
			WriteStream(writer, records, -1);
		}

		/// <include file='MasterDetailEngine.docs.xml' path='doc/WriteStream2/*'/>
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
					
					#if !MINI
						ProgressHelper.Notify(mNotifyHandler, mProgressMode, i+1, max);
					#endif

					RecordInfo info = (RecordInfo) mRecordInfoHash[records[i].GetType()];

					currentLine = info.RecordToString(records[i]);
					writer.WriteLine(currentLine);

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

		/// <include file='MasterDetailEngine.docs.xml' path='doc/WriteString/*'/>
		public string WriteString(object[] records)
		{
			return WriteString(records, -1);
		}

		/// <include file='MasterDetailEngine.docs.xml' path='doc/WriteString2/*'/>
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

		/// <include file='MasterDetailEngine.docs.xml' path='doc/AppendToFile1/*'/>
		public void AppendToFile(string fileName, object record)
		{
			AppendToFile(fileName, new object[] {record});
		}

		/// <include file='MasterDetailEngine.docs.xml' path='doc/AppendToFile2/*'/>
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
	}
}