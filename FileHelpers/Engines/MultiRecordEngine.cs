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
	public sealed class MultiRecordEngine : EngineBase
	{
		private RecordInfo[] mMultiRecordInfo;
		private Hashtable mRecordInfoHash;
		private RecordTypeSelector mRecordSelector;
		private Type[] mTypes;
		
		#region "  Constructor  "

		/// <summary>Create a new instance of the MultiRecordEngine</summary>
		/// <param name="recordTypes">The Types of the records that this engine can handle.</param>
		/// <param name="recordSelector">The selector that indicates to the engine what Type to use in each line.</param>
		public MultiRecordEngine(Type[] recordTypes, RecordTypeSelector recordSelector) : base(GetFirstType(recordTypes))
		{
			ErrorHelper.CheckNullParam(recordSelector, "recordSelector");

			mTypes = recordTypes;
			mMultiRecordInfo = new RecordInfo[mTypes.Length];
			mRecordInfoHash = new Hashtable(mTypes.Length);
			for(int i=0; i < mTypes.Length; i++)
			{
				if (mTypes[i] == null)
					throw new BadUsageException("The type at index "+i.ToString() + " is null.");

				mMultiRecordInfo[i] = new RecordInfo(mTypes[i]); 
				mRecordInfoHash.Add(mTypes[i], mMultiRecordInfo[i]);
			}
			mRecordSelector = recordSelector;
		}

		#endregion

        #if ! MINI

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

        private void OnAfterReadRecord(string line, object record)
        {
			if (mRecordInfo.mNotifyRead)
				((INotifyRead)record).AfterRead(this, line);

            if (AfterReadRecord != null)
            {
                AfterReadRecordEventArgs e = null;
                e = new AfterReadRecordEventArgs(line, record, LineNumber);
                AfterReadRecord(this, e);
            }
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

        #endif

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
                            object record = info.StringToRecord(currentLine, freader);


                            if (record != null)
                                resArray.Add(record);
#if !MINI
                        	OnAfterReadRecord(currentLine, record);
#endif
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
		public void WriteFile(string fileName, object[] records)
		{
			WriteFile(fileName, records, -1);
		}

		/// <include file='MasterDetailEngine.docs.xml' path='doc/WriteFile2/*'/>
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

		private static Type GetFirstType(Type[] types)
		{
			if (types == null)
				throw new ArgumentNullException("A null Type[] is not valid for the MultiRecordEngine.");
			else if (types.Length == 0)
				throw new ArgumentException("An empty Type[] is not valid for the MultiRecordEngine.");
			else
				return types[0];
		}
	}
}