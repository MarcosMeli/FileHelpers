#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
using System.IO;
using System.Text;

namespace FileHelpers.MasterDetail
{

	#region "  Delegate  "

	/// <summary>
	/// Delegate thats determines the Type of the current record (Master, Detail, Skip)
	/// </summary>
	/// <param name="recordString">The string of the current record.</param>
	/// <returns>the action used for the current record (Master, Detail, Skip)</returns>
	public delegate RecordAction MasterDetailSelector(string recordString);

	#endregion

	#region "  Common Actions and Selector  "

	/// <summary>The Action taken when the selector string is found.</summary>
	public enum CommonActions
	{
		/// <summary>Parse the current record as <b>Master</b> if the selector string is found.</summary>
		MasterIfContains,
		/// <summary>Parse the current record as <b>Detail</b> if the selector string is found.</summary>
		DetailIfContains
	}

	internal class CommonSelector
	{
		CommonActions mAction;
		string mSelector;

		internal CommonSelector(CommonActions action, string selector)
		{
			mAction = action;
			mSelector = selector;
		}

		internal RecordAction CommonSelectorMethod(string recordString)
		{
			if (mAction == CommonActions.DetailIfContains)
				if (recordString.IndexOf(mSelector) >= 0)
					return RecordAction.Detail;
				else
					return RecordAction.Master;
			else if (recordString.IndexOf(mSelector) >= 0)
				return RecordAction.Master;
			else
				return RecordAction.Detail;

		}
	}

	#endregion

	/// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngine/*'/>
	/// <include file='Examples.xml' path='doc/examples/MasterDetailEngine/*'/>
	public sealed class MasterDetailEngine : EngineBase
	{
		private RecordInfo mMasterInfo;
		private MasterDetailSelector mRecordSelector;

		private Type mMasterType;

		/// <summary>Returns the type of the master records handled by this engine.</summary>
		public Type MasterType
		{
			get { return mMasterType; }
		}

		#region "  Constructor  "

		/// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr1/*'/>
		public MasterDetailEngine(Type masterType, Type detailType, MasterDetailSelector recordSelector) : base(detailType)
		{
			mMasterType = masterType;
			mMasterInfo = new RecordInfo(masterType);
			mRecordSelector = recordSelector;
		}

		/// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr2/*'/>
		public MasterDetailEngine(Type masterType, Type detailType, CommonActions action, string selector)
			: base(detailType)
		{
			mMasterInfo = new RecordInfo(masterType);

			CommonSelector sel = new CommonSelector(action, selector);
			mRecordSelector = new MasterDetailSelector(sel.CommonSelectorMethod);
		}

		#endregion

		#region "  ReadFile  "

		/// <include file='MasterDetailEngine.docs.xml' path='doc/ReadFile/*'/>
		public MasterDetails[] ReadFile(string fileName)
		{
			using (StreamReader fs = new StreamReader(fileName, mEncoding, true))
			{
				MasterDetails[] tempRes;
				tempRes = ReadStream(fs);
				fs.Close();

				return tempRes;
			}

		}

		#endregion

		#region "  ReadStream  "

		/// <include file='MasterDetailEngine.docs.xml' path='doc/ReadStream/*'/>
		public MasterDetails[] ReadStream(TextReader reader)
		{
			if (reader == null)
				throw new ArgumentNullException("reader", "The reader of the Stream can´t be null");

			if (mRecordSelector == null)
				throw new BadUsageException("The Recordselector can´t be null, please pass a not null Selector in the constructor.");

			ResetFields();
			mHeaderText = String.Empty;
			mFooterText = String.Empty;

			ArrayList resArray = new ArrayList();

			ForwardReader freader = new ForwardReader(reader, mMasterInfo.mIgnoreLast);
			freader.DiscardForward = true;

			string currentLine, completeLine;

			mLineNumber = 1;

			completeLine = freader.ReadNextLine();
			currentLine = completeLine;

			if (mMasterInfo.mIgnoreFirst > 0)
			{
				for (int i = 0; i < mMasterInfo.mIgnoreFirst && currentLine != null; i++)
				{
					mHeaderText += currentLine + "\r\n";
					currentLine = freader.ReadNextLine();
					mLineNumber++;
				}
			}


			bool byPass = false;

			MasterDetails record = null;
			ArrayList tmpDetails = new ArrayList();

			while (currentLine != null)
			{
				try
				{
					RecordAction action = mRecordSelector(currentLine);

					switch (action)
					{
						case RecordAction.Master:
							if (record != null)
							{
								record.mDetails = tmpDetails.ToArray();
								resArray.Add(record);
							}

							mTotalRecords++;
							record = new MasterDetails();
							tmpDetails.Clear();
							record.mMaster = mMasterInfo.StringToRecord(currentLine);

							break;

						case RecordAction.Detail:
							tmpDetails.Add(mRecordInfo.StringToRecord(currentLine));
							break;
						default:
							break;
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

			if (record != null)
			{
				record.mDetails = tmpDetails.ToArray();
				resArray.Add(record);
			}

			if (mMasterInfo.mIgnoreLast > 0)
			{
				mFooterText = freader.RemainingText;
			}

			return (MasterDetails[]) resArray.ToArray(typeof (MasterDetails));
		}

		#endregion

		#region "  ReadString  "

		/// <include file='MasterDetailEngine.docs.xml' path='doc/ReadString/*'/>
		public MasterDetails[] ReadString(string source)
		{
			StringReader reader = new StringReader(source);
			MasterDetails[] res = ReadStream(reader);
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

		/// <include file='MasterDetailEngine.docs.xml' path='doc/WriteStream/*'/>
		public void WriteStream(TextWriter writer, MasterDetails[] records)
		{
			WriteStream(writer, records, -1);
		}

		/// <include file='MasterDetailEngine.docs.xml' path='doc/WriteStream2/*'/>
		public void WriteStream(TextWriter writer, MasterDetails[] records, int maxRecords)
		{
			if (writer == null)
				throw new ArgumentNullException("writer", "The writer of the Stream can be null");

			if (records == null)
				throw new ArgumentNullException("records", "The records can be null. Try with an empty array.");

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


			for (int i = 0; i < max; i++)
			{
				try
				{
					currentLine = mMasterInfo.RecordToString(records[i].mMaster);
					writer.WriteLine(currentLine);

					if (records[i].mDetails != null)
						for (int d = 0; d < records[i].mDetails.Length; d++)
						{
							currentLine = mRecordInfo.RecordToString(records[i].mDetails[d]);
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
				if (mFooterText.EndsWith("\r\n"))
					writer.Write(mFooterText);
				else
					writer.WriteLine(mFooterText);

		}

		#endregion

		#region "  WriteString  "

		/// <include file='MasterDetailEngine.docs.xml' path='doc/WriteString/*'/>
		public string WriteString(MasterDetails[] records)
		{
			return WriteString(records, -1);
		}

		/// <include file='MasterDetailEngine.docs.xml' path='doc/WriteString2/*'/>
		public string WriteString(MasterDetails[] records, int maxRecords)
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
		public void AppendToFile(string fileName, MasterDetails record)
		{
			AppendToFile(fileName, new MasterDetails[] {record});
		}

		/// <include file='MasterDetailEngine.docs.xml' path='doc/AppendToFile2/*'/>
		public void AppendToFile(string fileName, MasterDetails[] records)
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