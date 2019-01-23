using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using FileHelpers.Events;
using FileHelpers.Options;

namespace FileHelpers
{
    /// <summary>Abstract Base class for the engines of the library: 
    /// <see cref="FileHelperEngine"/> and 
    /// <see cref="FileHelperAsyncEngine"/></summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class EngineBase
    {
        // The default is 4k we use 16k
        internal const int DefaultReadBufferSize = 16 * 1024;
        internal const int DefaultWriteBufferSize = 16 * 1024;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal IRecordInfo RecordInfo { get; private set; }

        //private readonly IRecordInfo mRecordInfo;

        #region "  Constructor  "

        /// <summary>
        /// Create an engine on record type, with default encoding
        /// </summary>
        /// <param name="recordType">Class to base engine on</param>
        internal EngineBase(Type recordType)
            : this(recordType, Encoding.GetEncoding(0)) { }

        /// <summary>
        /// Create and engine on type with specified encoding
        /// </summary>
        /// <param name="recordType">Class to base engine on</param>
        /// <param name="encoding">encoding of the file</param>
        internal EngineBase(Type recordType, Encoding encoding)
        {
            if (recordType == null)
                throw new BadUsageException(Messages.Errors.NullRecordClass.Text);

            if (recordType.IsValueType)
            {
                throw new BadUsageException(Messages.Errors.StructRecordClass
                    .RecordType(recordType.Name)
                    .Text);
            }

            mRecordType = recordType;
            RecordInfo = FileHelpers.RecordInfo.Resolve(recordType);
            mEncoding = encoding;

            CreateRecordOptions();
        }

        /// <summary>
        /// Create an engine on the record info provided
        /// </summary>
        /// <param name="ri">Record information</param>
        internal EngineBase(RecordInfo ri)
        {
            mRecordType = ri.RecordType;
            RecordInfo = ri;

            CreateRecordOptions();
        }

        #endregion

        #region "  LineNumber  "

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal int mLineNumber;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal int mTotalRecords;

        /// <include file='FileHelperEngine.docs.xml' path='doc/LineNum/*'/>
        public int LineNumber
        {
            get { return mLineNumber; }
        }

        #endregion

        #region "  TotalRecords  "

        /// <include file='FileHelperEngine.docs.xml' path='doc/TotalRecords/*'/>
        public int TotalRecords
        {
            get { return mTotalRecords; }
        }

        #endregion

        /// <summary>
        /// Builds a line with the name of the fields, for a delimited files it
        /// uses the same delimiter, for a fixed length field it writes the
        /// fields names separated with tabs
        /// </summary>
        /// <returns>field names structured for the heading of the file</returns>
        public string GetFileHeader()
        {
            var delimiter = "\t";

            if (RecordInfo.IsDelimited)
                delimiter = ((DelimitedRecordOptions)Options).Delimiter;

            var res = new StringBuilder();
            for (int i = 0; i < RecordInfo.Fields.Length; i++)
            {
                if (i > 0)
                    res.Append(delimiter);

                var field = RecordInfo.Fields[i];
                res.Append(field.FieldCaption != null
                    ? field.FieldCaption
                    : field.FieldFriendlyName);
            }

            return res.ToString();
        }

        #region "  RecordType  "

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Type mRecordType;

        /// <include file='FileHelperEngine.docs.xml' path='doc/RecordType/*'/>
        public Type RecordType
        {
            get { return mRecordType; }
        }

        #endregion

        #region "  HeaderText  "

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal string mHeaderText = String.Empty;

        /// <summary>The Read Header in the last Read operation. If any.</summary>
        public string HeaderText
        {
            get { return mHeaderText; }
            set { mHeaderText = value; }
        }

        #endregion

        #region "  FooterText"

        /// <summary>The Read Footer in the last Read operation. If any.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected string mFooterText = String.Empty;

        /// <summary>The Read Footer in the last Read operation. If any.</summary>
        public string FooterText
        {
            get { return mFooterText; }
            set { mFooterText = value; }
        }

        #endregion

        #region "  Encoding  "

        /// <summary>
        /// The encoding to Read and Write the streams. 
        /// Default is the system's current ANSI code page.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected Encoding mEncoding = Encoding.GetEncoding(0);

        /// <summary>
        /// The encoding to Read and Write the streams. 
        /// Default is the system's current ANSI code page.
        /// </summary>
        /// <value>Default is the system's current ANSI code page.</value>
        public Encoding Encoding
        {
            get { return mEncoding; }
            set { mEncoding = value; }
        }

        #endregion

        #region "  NewLineForWrite  "
        /// <summary>
        /// Newline string to be used when engine writes to file. 
        /// Default is the system's newline setting (System.Environment.NewLine).
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string mNewLineForWrite = Environment.NewLine;

        /// <summary>
        /// Newline string to be used when engine writes to file. 
        /// Default is the system's newline setting (System.Environment.NewLine).
        /// </summary>
        /// <value>Default is the system's newline setting.</value>
        public string NewLineForWrite
        {
            get { return mNewLineForWrite; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("NewLine string must not be null or empty");
                mNewLineForWrite = value;
            }
        }

        #endregion

        #region "  ErrorManager"

        /// <summary>This is a common class that manage the errors of the library.</summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        protected ErrorManager mErrorManager = new ErrorManager();

        /// <summary>This is a common class that manages the errors of the library.</summary>
        /// <remarks>
        ///   You can find complete information about the errors encountered while processing.
        ///   For example, you can get the errors, their number and save them to a file, etc.
        ///   </remarks>
        ///   <seealso cref="FileHelpers.ErrorManager"/>
        public ErrorManager ErrorManager
        {
            get { return mErrorManager; }
        }

        /// <summary>
        /// Indicates the behavior of the engine when it finds an error.
        /// {Shortcut for <seealso cref="FileHelpers.ErrorManager.ErrorMode"/>)
        /// </summary>
        public ErrorMode ErrorMode
        {
            get { return mErrorManager.ErrorMode; }
            set { mErrorManager.ErrorMode = value; }
        }

        #endregion

        #region "  ResetFields  "

        /// <summary>
        /// Reset back to the beginning
        /// </summary>
        internal void ResetFields()
        {
            mLineNumber = 0;
            mErrorManager.ClearErrors();
            mTotalRecords = 0;
        }

        #endregion

        /// <summary>Event handler called to notify progress.</summary>
        public event EventHandler<ProgressEventArgs> Progress;

        /// <summary>
        /// Determine whether a progress call is needed
        /// </summary>
        protected bool MustNotifyProgress
        {
            get { return Progress != null; }
        }

        /// <summary>
        /// Raises the Progress Event
        /// </summary>
        /// <param name="e">The Event Args</param>
        protected void OnProgress(ProgressEventArgs e)
        {
            if (Progress == null)
                return;

            Progress(this, e);
        }

        private void CreateRecordOptions()
        {
            Options = CreateRecordOptionsCore(RecordInfo);
        }

        internal static RecordOptions CreateRecordOptionsCore(IRecordInfo info)
        {
            RecordOptions options;

            if (info.IsDelimited)
                options = new DelimitedRecordOptions(info);
            else
                options = new FixedRecordOptions(info);

            for (int index = 0; index < options.Fields.Count; index++)
            {
                var field = options.Fields[index];
                field.Parent = options;
                field.ParentIndex = index;
            }

            return options;
        }

        /// <summary>
        /// Allows you to change some record layout options at runtime
        /// </summary>
        public RecordOptions Options { get; private set; }
    }
}