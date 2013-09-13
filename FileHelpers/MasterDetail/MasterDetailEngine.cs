//#undef GENERICS
#define GENERICS
#if NET_2_0

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using FileHelpers.Events;

namespace FileHelpers.MasterDetail
{

    /// <summary>
    /// Read a master detail file, eg Orders followed by detail records
    /// </summary>
    public sealed class MasterDetailEngine
        : MasterDetailEngine<object, object>
    {

        #region "  Constructor  "

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr1/*'/>
        public MasterDetailEngine(Type masterType, Type detailType)
            : this(masterType, detailType, null)
        {
        }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr1/*'/>
        public MasterDetailEngine(Type masterType, Type detailType, MasterDetailSelector recordSelector)
            : base(masterType, detailType, recordSelector)
        {
        }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr2/*'/>
        public MasterDetailEngine(Type masterType, Type detailType, CommonSelector action, string selector)
            : base(masterType, detailType, action, selector)
        {
        }

        #endregion       
    }

    /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngine/*'/>
    /// <include file='Examples.xml' path='doc/examples/MasterDetailEngine/*'/>
    /// <typeparam name="TMaster">The Master Record Type</typeparam>
    /// <typeparam name="TDetail">The Detail Record Type</typeparam>
    public class MasterDetailEngine<TMaster, TDetail> 
        : EngineBase
        where TMaster : class
        where TDetail : class
    {

        #region "  Constructor  "

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr1/*'/>
        public MasterDetailEngine()
            : this(null)
        {
        }


        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr1/*'/>
        public MasterDetailEngine(MasterDetailSelector recordSelector)
            : this(typeof(TMaster), typeof(TDetail), recordSelector)
        {
        }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr1/*'/>
        internal MasterDetailEngine(Type masterType, Type detailType, MasterDetailSelector recordSelector)
            : base(detailType)
        {
            mMasterType = masterType;
            mMasterInfo = FileHelpers.RecordInfo.Resolve(mMasterType); // Container.Resolve<IRecordInfo>(mMasterType);
            mRecordSelector = recordSelector;
        }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr2/*'/>
        public MasterDetailEngine(CommonSelector action, string selector)
            : this(typeof(TMaster), typeof(TDetail), action, selector)
        {
        }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr2/*'/>
        internal MasterDetailEngine(Type masterType, Type detailType, CommonSelector action, string selector)
            : base(detailType)
        {
            mMasterType = masterType;
            mMasterInfo = FileHelpers.RecordInfo.Resolve(mMasterType);

            var sel = new MasterDetailEngine<object, object>.CommonSelectorInternal(action, selector, mMasterInfo.IgnoreEmptyLines || RecordInfo.IgnoreEmptyLines);
            mRecordSelector = new MasterDetailSelector(sel.CommonSelectorMethod);

        }

        #endregion

        #region CommonSelectorInternal

        internal class CommonSelectorInternal
		{
            readonly CommonSelector mAction;
            readonly string mSelector;
            readonly bool mIgnoreEmpty = false;


			internal CommonSelectorInternal(CommonSelector action, string selector, bool ignoreEmpty)
			{
				mAction = action;
				mSelector = selector;
				mIgnoreEmpty = ignoreEmpty;
			}

			internal RecordAction CommonSelectorMethod(string recordString)
			{
				if (mIgnoreEmpty && recordString == string.Empty)
					return RecordAction.Skip;

				switch(mAction)
				{
					case CommonSelector.DetailIfContains:
                        if (recordString.IndexOf (mSelector, StringComparison.Ordinal) >= 0)
							return RecordAction.Detail;
						else
							return RecordAction.Master;

					case CommonSelector.MasterIfContains:
                        if (recordString.IndexOf (mSelector, StringComparison.Ordinal) >= 0)
							return RecordAction.Master;
						else
							return RecordAction.Detail;

					case CommonSelector.DetailIfBegins:
                        if (recordString.StartsWith (mSelector, StringComparison.Ordinal))
							return RecordAction.Detail;
						else
							return RecordAction.Master;
					
					case CommonSelector.MasterIfBegins:
                        if (recordString.StartsWith (mSelector, StringComparison.Ordinal))
							return RecordAction.Master;
						else
							return RecordAction.Detail;

					case CommonSelector.DetailIfEnds:
                        if (recordString.EndsWith (mSelector, StringComparison.Ordinal))
							return RecordAction.Detail;
						else
							return RecordAction.Master;

					case CommonSelector.MasterIfEnds:
                        if (recordString.EndsWith (mSelector, StringComparison.Ordinal))
							return RecordAction.Master;
						else
							return RecordAction.Detail;

					case CommonSelector.DetailIfEnclosed:
                        if (recordString.StartsWith (mSelector, StringComparison.Ordinal) && recordString.EndsWith (mSelector, StringComparison.Ordinal))
							return RecordAction.Detail;
						else
							return RecordAction.Master;

					case CommonSelector.MasterIfEnclosed:
                        if (recordString.StartsWith (mSelector, StringComparison.Ordinal) && recordString.EndsWith (mSelector, StringComparison.Ordinal))
							return RecordAction.Master;
						else
							return RecordAction.Detail;

				}

				return RecordAction.Skip;
			}
		}

        #endregion

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private readonly IRecordInfo mMasterInfo;

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private MasterDetailSelector mRecordSelector;


#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private readonly Type mMasterType;

        /// <summary>
        /// the type of the master records handled by this engine.
        /// </summary>
        public Type MasterType
        {
            get { return mMasterType; }
        }

        /// <summary>
        /// The <see cref="MasterDetailSelector" /> to get the <see cref="RecordAction" /> (only for read operations)
        /// </summary>
	    public MasterDetailSelector RecordSelector
	    {
	        get { return mRecordSelector; }
	        set { mRecordSelector = value; }
	    }

	    #region "  ReadFile  "

        /// <include file='MasterDetailEngine.docs.xml' path='doc/ReadFile/*'/>
#if ! GENERICS
		public MasterDetails[] ReadFile(string fileName)
		{
			using (StreamReader fs = new StreamReader(fileName, mEncoding, true, EngineBase.DefaultReadBufferSize))
			{
				MasterDetails[] tempRes;
				tempRes = ReadStream(fs);
				fs.Close();

				return tempRes;
			}

		}
#else
        public MasterDetails<TMaster, TDetail>[] ReadFile(string fileName)
        {
            using (var fs = new StreamReader(fileName, mEncoding, true, DefaultReadBufferSize))
            {
                MasterDetails<TMaster, TDetail>[] tempRes;
                tempRes = ReadStream(fs);
                fs.Close();

                return tempRes;
            }

        }
#endif

        #endregion

        #region "  ReadStream  "

        /// <include file='MasterDetailEngine.docs.xml' path='doc/ReadStream/*'/>
#if ! GENERICS
		public MasterDetails[] ReadStream(TextReader reader)
#else
        public MasterDetails<TMaster, TDetail>[] ReadStream(TextReader reader)
#endif
        {
            if (reader == null)
                throw new ArgumentNullException("reader", "The reader of the Stream can't be null");

            if (RecordSelector == null)
                throw new BadUsageException("The RecordSelector can't be null on read operations.");

            var recordReader = new NewLineDelimitedRecordReader(reader);

            ResetFields();
            mHeaderText = String.Empty;
            mFooterText = String.Empty;

            var resArray = new ArrayList();

            using (var freader = new ForwardReader(recordReader, mMasterInfo.IgnoreLast))
            {
                freader.DiscardForward = true;

                mLineNumber = 1;

                var completeLine = freader.ReadNextLine();
                var currentLine = completeLine;

#if !MINI
                if (MustNotifyProgress) // Avoid object creation
                    OnProgress(new ProgressEventArgs(0, -1));
                
#endif
                int currentRecord = 0;

                if (mMasterInfo.IgnoreFirst > 0)
                {
                    for (int i = 0; i < mMasterInfo.IgnoreFirst && currentLine != null; i++)
                    {
                        mHeaderText += currentLine + StringHelper.NewLine;
                        currentLine = freader.ReadNextLine();
                        mLineNumber++;
                    }
                }


                bool byPass = false;

#if ! GENERICS
                MasterDetails record = null;
#else
            MasterDetails<TMaster, TDetail> record = null;
#endif
                var tmpDetails = new ArrayList();

                var line = new LineInfo(currentLine)
                    {
                        mReader = freader
                    };


                var valuesMaster = new object[mMasterInfo.FieldCount];
                var valuesDetail = new object[RecordInfo.FieldCount];

                while (currentLine != null)
                {
                    try
                    {
                        currentRecord++;

                        line.ReLoad(currentLine);

#if !MINI
                        if (MustNotifyProgress) // Avoid object creation
                            OnProgress(new ProgressEventArgs(currentRecord, -1));
#endif
                        var action = RecordAction.Skip;
                        try
                        {
                            action = RecordSelector(currentLine);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Supplied Record selector failed to process record",ex);
                        }

                        switch (action)
                        {
                            case RecordAction.Master:
                                if (record != null)
                                {
#if ! GENERICS
                                    record.mDetails = tmpDetails.ToArray();
#else
                                record.Details = (TDetail[])tmpDetails.ToArray(typeof(TDetail));
#endif
                                    resArray.Add(record);
                                }

                                mTotalRecords++;
#if ! GENERICS
                                record = new MasterDetails();
#else
                            record = new MasterDetails<TMaster, TDetail>();
#endif
                                tmpDetails.Clear();
#if ! GENERICS
                                object lastMaster = mMasterInfo.StringToRecord(line, valuesMaster);
#else
                                var lastMaster = (TMaster)mMasterInfo.Operations.StringToRecord(line, valuesMaster);
#endif

                                if (lastMaster != null)
                                    record.Master = lastMaster;

                                break;

                            case RecordAction.Detail:
#if ! GENERICS
                                object lastChild = mRecordInfo.StringToRecord(line, valuesDetail);
#else
                                var lastChild = (TDetail)RecordInfo.Operations.StringToRecord(line, valuesDetail);
#endif

                                if (lastChild != null)
                                    tmpDetails.Add(lastChild);
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
                                var err = new ErrorInfo
                                    {
                                        mLineNumber = mLineNumber,
                                        mExceptionInfo = ex,
                                        mRecordString = completeLine
                                    };
                                //							err.mColumnNumber = mColumnNum;

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

                if (record != null)
                {
#if ! GENERICS
                    record.mDetails = tmpDetails.ToArray();
#else
                record.Details = (TDetail[])tmpDetails.ToArray(typeof(TDetail));
#endif
                    resArray.Add(record);
                }

                if (mMasterInfo.IgnoreLast > 0)
                {
                    mFooterText = freader.RemainingText;
                }
            }
#if ! GENERICS
			return (MasterDetails[]) resArray.ToArray(typeof (MasterDetails));
#else
            return (MasterDetails<TMaster, TDetail>[])resArray.ToArray(typeof(MasterDetails<TMaster, TDetail>));
#endif
        }

        #endregion

        #region "  ReadString  "

        /// <include file='MasterDetailEngine.docs.xml' path='doc/ReadString/*'/>
#if ! GENERICS
		public MasterDetails[] ReadString(string source)
		{
			var reader = new InternalStringReader(source);
			MasterDetails[] res = ReadStream(reader);
			reader.Close();
			return res;
		}
#else
        public MasterDetails<TMaster, TDetail>[] ReadString(string source)
        {
            var reader = new StringReader(source);
            MasterDetails<TMaster, TDetail>[] res = ReadStream(reader);
            reader.Close();
            return res;
        }
#endif


        #endregion

        #region "  WriteFile  "

#if ! GENERICS
				/// <include file='MasterDetailEngine.docs.xml' path='doc/WriteFile/*'/>
				public void WriteFile(string fileName, MasterDetails[] records)
				{
					WriteFile(fileName, records, -1);
				}

				/// <include file='MasterDetailEngine.docs.xml' path='doc/WriteFile2/*'/>
				public void WriteFile(string fileName, MasterDetails[] records, int maxRecords)
				{
					using (StreamWriter fs = new StreamWriter(fileName, false, mEncoding, EngineBase.DefaultWriteBufferSize))
					{
						WriteStream(fs, records, maxRecords);
						fs.Close();
					}

				}
#else
        /// <include file='MasterDetailEngine.docs.xml' path='doc/WriteFile/*'/>
        public void WriteFile(string fileName, IEnumerable<MasterDetails<TMaster, TDetail>> records)
        {
            WriteFile(fileName, records, -1);
        }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/WriteFile2/*'/>
        public void WriteFile(string fileName, IEnumerable<MasterDetails<TMaster, TDetail>> records, int maxRecords)
        {
            using (var fs = new StreamWriter(fileName, false, mEncoding, DefaultWriteBufferSize))
            {
                WriteStream(fs, records, maxRecords);
                fs.Close();
            }

        }
#endif


        #endregion

        #region "  WriteStream  "

        /// <include file='MasterDetailEngine.docs.xml' path='doc/WriteStream/*'/>
#if ! GENERICS
		public void WriteStream(TextWriter writer, MasterDetails[] records)
#else
        public void WriteStream(TextWriter writer, IEnumerable<MasterDetails<TMaster, TDetail>> records)
#endif
        {
            WriteStream(writer, records, -1);
        }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/WriteStream2/*'/>
#if ! GENERICS
		public void WriteStream(TextWriter writer, MasterDetails[] records, int maxRecords)
#else
        public void WriteStream(TextWriter writer, IEnumerable<MasterDetails<TMaster, TDetail>> records, int maxRecords)
#endif
        {
            if (writer == null)
                throw new ArgumentNullException("writer", "The writer of the Stream can be null");

            if (records == null)
                throw new ArgumentNullException("records", "The records can be null. Try with an empty array.");

            ResetFields();

            if (!string.IsNullOrEmpty(mHeaderText))
                if (mHeaderText.EndsWith (StringHelper.NewLine, StringComparison.Ordinal))
                    writer.Write(mHeaderText);
                else
                    writer.WriteLine(mHeaderText);


            string currentLine = null;

            int max = maxRecords;
            if (records is IList)
                max = Math.Min(max < 0 ? int.MaxValue : max, ((IList)records).Count);

#if !MINI
            if (MustNotifyProgress) // Avoid object creation
                OnProgress(new ProgressEventArgs(0, max));
#endif

            int recIndex = 0;

#if ! GENERICS
			foreach(MasterDetails rec in records)
#else
            foreach (var rec in records)
#endif
            {
                if (recIndex == maxRecords)
                    break;

                try
                {
                    if (rec == null)
                        throw new BadUsageException("The record at index " + recIndex.ToString() + " is null.");

#if !MINI
                    if (MustNotifyProgress) // Avoid object creation
                        OnProgress(new ProgressEventArgs(recIndex + 1, max));
#endif

                    currentLine = mMasterInfo.Operations.RecordToString(rec.Master);
                    writer.WriteLine(currentLine);

                    if (rec.Details != null)
                        for (int d = 0; d < rec.Details.Length; d++)
                        {
                            currentLine = RecordInfo.Operations.RecordToString(rec.Details[d]);
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
                            var err = new ErrorInfo
                                {
                                    mLineNumber = mLineNumber,
                                    mExceptionInfo = ex,
                                    mRecordString = currentLine
                                };
                            //							err.mColumnNumber = mColumnNum;
                            mErrorManager.AddError(err);
                            break;
                    }
                }

            }

            mTotalRecords = recIndex;

            if (!string.IsNullOrEmpty(mFooterText))
                if (mFooterText.EndsWith (StringHelper.NewLine, StringComparison.Ordinal))
                    writer.Write(mFooterText);
                else
                    writer.WriteLine(mFooterText);

        }

        #endregion

        #region "  WriteString  "

        /// <include file='MasterDetailEngine.docs.xml' path='doc/WriteString/*'/>
#if ! GENERICS
		public string WriteString(MasterDetails[] records)
#else
        public string WriteString(IEnumerable<MasterDetails<TMaster, TDetail>> records)
#endif
        {
            return WriteString(records, -1);
        }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/WriteString2/*'/>
#if ! GENERICS
		public string WriteString(MasterDetails[] records, int maxRecords)
#else
        public string WriteString(IEnumerable<MasterDetails<TMaster, TDetail>> records, int maxRecords)
#endif
        {
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            WriteStream(writer, records, maxRecords);
            string res = writer.ToString();
            writer.Close();
            return res;
        }

        #endregion

        #region "  AppendToFile  "

        /// <include file='MasterDetailEngine.docs.xml' path='doc/AppendToFile1/*'/>
#if ! GENERICS
		public void AppendToFile(string fileName, MasterDetails record)
		{
			AppendToFile(fileName, new MasterDetails[] {record});
		}
#else
        public void AppendToFile(string fileName, MasterDetails<TMaster, TDetail> record)
        {
            AppendToFile(fileName, new MasterDetails<TMaster, TDetail>[] { record });
        }
#endif

        /// <include file='MasterDetailEngine.docs.xml' path='doc/AppendToFile2/*'/>
#if ! GENERICS
		public void AppendToFile(string fileName, MasterDetails[] records)
#else
        public void AppendToFile(string fileName, IEnumerable<MasterDetails<TMaster, TDetail>> records)
#endif
        {
            using (TextWriter writer = StreamHelper.CreateFileAppender(fileName, mEncoding, true, false, DefaultWriteBufferSize))
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

#endif
