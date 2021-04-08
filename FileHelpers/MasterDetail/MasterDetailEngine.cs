using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using FileHelpers.Engines;
using FileHelpers.Events;
using FileHelpers.Helpers;
using FileHelpers.Options;

namespace FileHelpers.MasterDetail
{
    /// <summary>
    /// Read a master detail file, eg Orders followed by detail records
    /// </summary>
    public sealed class MasterDetailEngine
        : MasterDetailEngine<object, object>
    {
        #region "  Constructor  "

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr/*'/>
        public MasterDetailEngine(Type masterType, Type detailType)
            : this(masterType, detailType, null) { }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr/*'/>
        /// <param name="masterType">The master record class.</param>
		/// <param name="detailType">The detail record class.</param>
		/// <param name="recordSelector">The <see cref="MasterDetailSelector" /> to get the <see cref="RecordAction" /> (only for read operations)</param>
        public MasterDetailEngine(Type masterType, Type detailType, MasterDetailSelector recordSelector)
            : base(masterType, detailType, recordSelector) { }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr/*'/>
        /// <param name="masterType">The master record class.</param>
		/// <param name="detailType">The detail record class.</param>
		/// <param name="action">The <see cref="CommonSelector" /> used by the engine (only for read operations)</param>
		/// <param name="selector">The string passed as the selector.</param>
        public MasterDetailEngine(Type masterType, Type detailType, CommonSelector action, string selector)
            : base(masterType, detailType, action, selector) { }

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
            : this(null) { }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr1/*'/>
        public MasterDetailEngine(MasterDetailSelector recordSelector)
            : this(typeof(TMaster), typeof(TDetail), recordSelector) { }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr1/*'/>
        internal MasterDetailEngine(Type masterType, Type detailType, MasterDetailSelector recordSelector)
            : base(detailType)
        {
            mMasterType = masterType;
            mMasterInfo = FileHelpers.RecordInfo.Resolve(mMasterType);
            MasterOptions = CreateRecordOptionsCore(mMasterInfo);
            mRecordSelector = recordSelector;
        }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr2/*'/>
        public MasterDetailEngine(CommonSelector action, string selector)
            : this(typeof(TMaster), typeof(TDetail), action, selector) { }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/MasterDetailEngineCtr2/*'/>
        internal MasterDetailEngine(Type masterType, Type detailType, CommonSelector action, string selector)
            : base(detailType)
        {
            mMasterType = masterType;
            mMasterInfo = FileHelpers.RecordInfo.Resolve(mMasterType);
            MasterOptions = CreateRecordOptionsCore(mMasterInfo);

            var sel = new MasterDetailEngine<object, object>.CommonSelectorInternal(action,
                selector,
                mMasterInfo.IgnoreEmptyLines || RecordInfo.IgnoreEmptyLines);
            mRecordSelector = new MasterDetailSelector(sel.CommonSelectorMethod);
        }

        #endregion

        /// <summary>
        /// Allows you to change some record layout options at runtime
        /// </summary>
        public RecordOptions MasterOptions { get; private set; }

        #region CommonSelectorInternal

        internal class CommonSelectorInternal
        {
            private readonly CommonSelector mAction;
            private readonly string mSelector;
            private readonly bool mIgnoreEmpty = false;

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

                switch (mAction)
                {
                    case CommonSelector.DetailIfContains:
                        if (recordString.IndexOf(mSelector) >= 0)
                            return RecordAction.Detail;
                        else
                            return RecordAction.Master;

                    case CommonSelector.MasterIfContains:
                        if (recordString.IndexOf(mSelector) >= 0)
                            return RecordAction.Master;
                        else
                            return RecordAction.Detail;

                    case CommonSelector.DetailIfBegins:
                        if (recordString.StartsWith(mSelector))
                            return RecordAction.Detail;
                        else
                            return RecordAction.Master;

                    case CommonSelector.MasterIfBegins:
                        if (recordString.StartsWith(mSelector))
                            return RecordAction.Master;
                        else
                            return RecordAction.Detail;

                    case CommonSelector.DetailIfEnds:
                        if (recordString.EndsWith(mSelector))
                            return RecordAction.Detail;
                        else
                            return RecordAction.Master;

                    case CommonSelector.MasterIfEnds:
                        if (recordString.EndsWith(mSelector))
                            return RecordAction.Master;
                        else
                            return RecordAction.Detail;

                    case CommonSelector.DetailIfEnclosed:
                        if (recordString.StartsWith(mSelector) &&
                            recordString.EndsWith(mSelector))
                            return RecordAction.Detail;
                        else
                            return RecordAction.Master;

                    case CommonSelector.MasterIfEnclosed:
                        if (recordString.StartsWith(mSelector) &&
                            recordString.EndsWith(mSelector))
                            return RecordAction.Master;
                        else
                            return RecordAction.Detail;
                }

                return RecordAction.Skip;
            }
        }

        #endregion

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly IRecordInfo mMasterInfo;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private MasterDetailSelector mRecordSelector;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
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

        #endregion

        #region "  ReadStream  "

        /// <include file='MasterDetailEngine.docs.xml' path='doc/ReadStream/*'/>
        public MasterDetails<TMaster, TDetail>[] ReadStream(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader), "The reader of the Stream can't be null");

            if (RecordSelector == null)
                throw new BadUsageException("The RecordSelector can't be null on read operations.");

            var recordReader = new NewLineDelimitedRecordReader(reader);

            ResetFields();
            HeaderText = string.Empty;
            mFooterText = string.Empty;

            var resArray = new ArrayList();

            using (var freader = new ForwardReader(recordReader, mMasterInfo.IgnoreLast))
            {
                freader.DiscardForward = true;

                mLineNumber = 1;

                var completeLine = freader.ReadNextLine();
                var currentLine = completeLine;

                if (MustNotifyProgress) // Avoid object creation
                    OnProgress(new ProgressEventArgs(0, -1));

                int currentRecord = 0;

                if (mMasterInfo.IgnoreFirst > 0)
                {
                    for (int i = 0; i < mMasterInfo.IgnoreFirst && currentLine != null; i++)
                    {
                        HeaderText += currentLine + Environment.NewLine;
                        currentLine = freader.ReadNextLine();
                        mLineNumber++;
                    }
                }

                bool byPass = false;

                MasterDetails<TMaster, TDetail> record = null;
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

                        if (MustNotifyProgress) // Avoid object creation
                            OnProgress(new ProgressEventArgs(currentRecord, -1));
                        var action = RecordAction.Skip;
                        try
                        {
                            action = RecordSelector(currentLine);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Supplied Record selector failed to process record", ex);
                        }

                        switch (action)
                        {
                            case RecordAction.Master:
                                if (record != null)
                                {
                                    record.Details = (TDetail[])tmpDetails.ToArray(typeof(TDetail));
                                    resArray.Add(record);
                                }

                                mTotalRecords++;
                                record = new MasterDetails<TMaster, TDetail>();
                                tmpDetails.Clear();
                                var lastMaster = (TMaster)mMasterInfo.Operations.StringToRecord(line, valuesMaster);

                                if (lastMaster != null)
                                    record.Master = lastMaster;

                                break;

                            case RecordAction.Detail:
                                var lastChild = (TDetail)RecordInfo.Operations.StringToRecord(line, valuesDetail);

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
                                    mRecordString = completeLine,
                                    mRecordTypeName = RecordInfo.RecordType.Name
                                };
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
                    record.Details = (TDetail[])tmpDetails.ToArray(typeof(TDetail));
                    resArray.Add(record);
                }

                if (mMasterInfo.IgnoreLast > 0)
                    mFooterText = freader.RemainingText;
            }
            return (MasterDetails<TMaster, TDetail>[])resArray.ToArray(typeof(MasterDetails<TMaster, TDetail>));
        }

        #endregion

        #region "  ReadString  "

        /// <include file='MasterDetailEngine.docs.xml' path='doc/ReadString/*'/>
        public MasterDetails<TMaster, TDetail>[] ReadString(string source)
        {
            var reader = new StringReader(source);
            MasterDetails<TMaster, TDetail>[] res = ReadStream(reader);
            reader.Close();
            return res;
        }

        #endregion

        #region "  WriteFile  "

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

        #endregion

        #region "  WriteStream  "

        /// <include file='MasterDetailEngine.docs.xml' path='doc/WriteStream/*'/>
        public void WriteStream(TextWriter writer, IEnumerable<MasterDetails<TMaster, TDetail>> records)
        {
            WriteStream(writer, records, -1);
        }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/WriteStream2/*'/>
        public void WriteStream(TextWriter writer, IEnumerable<MasterDetails<TMaster, TDetail>> records, int maxRecords)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer), "The writer of the Stream can be null");

            if (records == null)
                throw new ArgumentNullException(nameof(records), "The records can be null. Try with an empty array.");

            ResetFields();

            writer.NewLine = NewLineForWrite;

            WriteHeader(writer);

            string currentLine = null;

            int max = maxRecords;
            if (records is IList)
            {
                max = Math.Min(max < 0
                    ? int.MaxValue
                    : max,
                    ((IList)records).Count);
            }

            if (MustNotifyProgress) // Avoid object creation
                OnProgress(new ProgressEventArgs(0, max));

            int recIndex = 0;

            foreach (var rec in records)
            {
                if (recIndex == maxRecords)
                    break;

                try
                {
                    if (rec == null)
                        throw new BadUsageException("The record at index " + recIndex.ToString() + " is null.");

                    if (MustNotifyProgress) // Avoid object creation
                        OnProgress(new ProgressEventArgs(recIndex + 1, max));

                    currentLine = mMasterInfo.Operations.RecordToString(rec.Master);
                    writer.WriteLine(currentLine);

                    if (rec.Details != null)
                    {
                        for (int d = 0; d < rec.Details.Length; d++)
                        {
                            currentLine = RecordInfo.Operations.RecordToString(rec.Details[d]);
                            writer.WriteLine(currentLine);
                        }
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
                                mRecordString = currentLine,
                                mRecordTypeName = RecordInfo.RecordType.Name
                            };
                            mErrorManager.AddError(err);
                            break;
                    }
                }
            }

            mTotalRecords = recIndex;

            WriteFooter(writer);
        }

        #endregion

        #region "  WriteString  "

        /// <include file='MasterDetailEngine.docs.xml' path='doc/WriteString/*'/>
        public string WriteString(IEnumerable<MasterDetails<TMaster, TDetail>> records)
        {
            return WriteString(records, -1);
        }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/WriteString2/*'/>
        public string WriteString(IEnumerable<MasterDetails<TMaster, TDetail>> records, int maxRecords)
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
        public void AppendToFile(string fileName, MasterDetails<TMaster, TDetail> record)
        {
            AppendToFile(fileName, new MasterDetails<TMaster, TDetail>[] { record });
        }

        /// <include file='MasterDetailEngine.docs.xml' path='doc/AppendToFile2/*'/>
        public void AppendToFile(string fileName, IEnumerable<MasterDetails<TMaster, TDetail>> records)
        {
            using (
                TextWriter writer = StreamHelper.CreateFileAppender(fileName,
                    mEncoding,
                    true,
                    false,
                    DefaultWriteBufferSize))
            {
                HeaderText = string.Empty;
                mFooterText = string.Empty;

                WriteStream(writer, records);
                writer.Close();
            }
        }

        #endregion
    }
}
