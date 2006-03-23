#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
using System.IO;
using System.Text;


namespace FileHelpers
{
	/// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngine/*'/>
	/// <include file='Examples.xml' path='doc/examples/FileHelperEngine/*'/>
	public sealed class FileHelperEngine : EngineBase
	{

		#region "  Constructor  "

		/// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngineCtr/*'/>
		public FileHelperEngine(Type recordType) : base(recordType)
		{
		}

		#endregion

		#region "  ReadFile  "

		/// <include file='FileHelperEngine.docs.xml' path='doc/ReadFile/*'/>
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

		/// <include file='FileHelperEngine.docs.xml' path='doc/ReadStream/*'/>
		public object[] ReadStream(TextReader reader)
		{
			if (reader == null)
				throw new ArgumentNullException("reader", "The reader of the Stream can´t be null");

			ResetFields();
			mHeaderText = String.Empty;
			mFooterText = String.Empty;

			ArrayList resArray = new ArrayList();
			int currentRecord = 0;

			ForwardReader freader = new ForwardReader(reader, mRecordInfo.mIgnoreLast);
			freader.DiscardForward = true;

			string currentLine, completeLine;

			mLineNumber = 1;

			completeLine = freader.ReadNextLine();
			currentLine = completeLine;

			#if !MINI
				ProgressHelper.Notify(mNotifyHandler, mProgressMode, 0, -1);
			#endif


			if (mRecordInfo.mIgnoreFirst > 0)
			{
				for (int i = 0; i < mRecordInfo.mIgnoreFirst && currentLine != null; i++)
				{
					mHeaderText += currentLine + "\r\n";
					currentLine = freader.ReadNextLine();
					mLineNumber++;
				
				}
			}



			bool byPass = false;

			while (currentLine != null)
			{
				try
				{
					mTotalRecords++;
					currentRecord++; 
				
					#if !MINI
						ProgressHelper.Notify(mNotifyHandler, mProgressMode, currentRecord, -1);
					#endif

					resArray.Add(mRecordInfo.StringToRecord(currentLine));
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
							err.mLineNumber = mLineNumber;
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
						mLineNumber++;
					}
				}

			}

			if (mRecordInfo.mIgnoreLast > 0)
			{
				mFooterText = freader.RemainingText;
			}

			return (object[]) resArray.ToArray(RecordType);
		}

		#endregion

		#region "  ReadString  "

		/// <include file='FileHelperEngine.docs.xml' path='doc/ReadString/*'/>
		public object[] ReadString(string source)
		{
			StringReader reader = new StringReader(source);
			object[] res = ReadStream(reader);
			reader.Close();
			return res;
		}

		#endregion

		#region "  WriteFile  "

		/// <include file='FileHelperEngine.docs.xml' path='doc/WriteFile/*'/>
		public void WriteFile(string fileName, object[] records)
		{
			WriteFile(fileName, records, -1);
		}

		/// <include file='FileHelperEngine.docs.xml' path='doc/WriteFile2/*'/>
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

		/// <include file='FileHelperEngine.docs.xml' path='doc/WriteStream/*'/>
		public void WriteStream(TextWriter writer, object[] records)
		{
			WriteStream(writer, records, -1);
		}

		/// <include file='FileHelperEngine.docs.xml' path='doc/WriteStream2/*'/>
		public void WriteStream(TextWriter writer, object[] records, int maxRecords)
		{
			if (writer == null)
				throw new ArgumentNullException("writer", "The writer of the Stream can be null");

			if (records == null)
				throw new ArgumentNullException("records", "The records can be null. Try with an empty array.");

			if (records.Length > 0 && records[0] != null && mRecordInfo.mRecordType.IsInstanceOfType(records[0]) == false)
				throw new BadUsageException("This engine works with record of type " + mRecordInfo.mRecordType.Name + " and you use records of type " + records[0].GetType().Name );

			ResetFields();

			if (mHeaderText != null && mHeaderText.Length != 0)
				if (mHeaderText.EndsWith("\r\n"))
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

					currentLine = mRecordInfo.RecordToString(records[i]);
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
				if (mFooterText.EndsWith("\r\n"))
					writer.Write(mFooterText);
				else
					writer.WriteLine(mFooterText);

    	}

		#endregion

		#region "  WriteString  "

		/// <include file='FileHelperEngine.docs.xml' path='doc/WriteString/*'/>
		public string WriteString(object[] records)
		{
			return WriteString(records, -1);
		}

		/// <include file='FileHelperEngine.docs.xml' path='doc/WriteString2/*'/>
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

		/// <include file='FileHelperEngine.docs.xml' path='doc/AppendToFile1/*'/>
		public void AppendToFile(string fileName, object record)
		{
			AppendToFile(fileName, new object[] {record});
		}

		/// <include file='FileHelperEngine.docs.xml' path='doc/AppendToFile2/*'/>
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


		//		public static int DataTableToCVS(DataTable dt, string fileName)
		//		{
		//			FileHelperEngine engine = new FileHelperEngine();
		//			engine.mRecordInfo = new RecordInfo();
		//			//engine.mRecordInfo
		////			mRecordInfo.AddFields(new FieldBase[] {});
		//		}

	}
}