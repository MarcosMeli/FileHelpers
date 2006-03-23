using System;
using System.ComponentModel;
using System.Text;

namespace FileHelpers
{
	/// <summary>Base class for the two engines of the library: <see cref="FileHelperEngine"/> and <see cref="FileHelperAsyncEngine"/></summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class EngineBase
	{
		internal RecordInfo mRecordInfo;

		#region "  Constructor  "

		/// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngineCtr/*'/>
		internal EngineBase(Type recordType)
		{
			if (recordType == null)
				throw new BadUsageException("The record type can't be null");
			if (recordType.IsValueType)
				throw new BadUsageException("The record type must be a class not a struct.");

			mRecordType = recordType;
			mRecordInfo = new RecordInfo(recordType);
			//InitFields();
		}

		#endregion

		#region "  LineNumber  "

		internal int mLineNumber;
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

		#region "  RecordType  "

		private Type mRecordType;

		/// <include file='FileHelperEngine.docs.xml' path='doc/RecordType/*'/>
		public Type RecordType
		{
			get { return mRecordType; }
		}

		#endregion

		#region "  HeaderText  "

		internal string mHeaderText = String.Empty;

		/// <summary>The read header in the last read operation. If any.</summary>
		public string HeaderText
		{
			get { return mHeaderText; }
			set { mHeaderText = value; }
		}

		#endregion

		#region "  FooterText"

		internal string mFooterText = String.Empty;

		/// <summary>The read footer in the last read operation. If any.</summary>
		public string FooterText
		{
			get { return mFooterText; }
			set { mFooterText = value; }
		}

		#endregion

		#region "  Encoding  "

		internal Encoding mEncoding = Encoding.Default;

		/// <summary>The encoding to Read and Write the streams.</summary>
		/// <remarks>Default is the system's current ANSI code page.</remarks>
		/// <value>Default is the system's current ANSI code page.</value>
		public Encoding Encoding
		{
			get { return mEncoding; }
			set { mEncoding = value; }
		}

		#endregion

		#region "  ErrorManager"

		/// <summary>This is a common class that manage the errors of the library.</summary>
		protected ErrorManager mErrorManager = new ErrorManager();

		/// <summary>This is a common class that manage the errors of the library.</summary>
		/// <remarks>You can, for example, get the errors, their number, Save them to a file, etc.</remarks>
		public ErrorManager ErrorManager
		{
			get { return mErrorManager; }
		}

		#endregion

		#region "  ResetFields  "

		internal void ResetFields()
		{
			mLineNumber = 0;
			mErrorManager.ClearErrors();
			mTotalRecords = 0;
		}

		#endregion

		#if ! MINI
		
		/// <summary></summary>
		protected ProgressMode mProgressMode = ProgressMode.DontNotify;

		/// <summary></summary>
		protected ProgressChangeHandler mNotifyHandler = null;

		/// <summary>Set the handler to the engine used to notify progress into the operations.</summary>
		/// <param name="handler">The <see cref="ProgressChangeHandler"/></param>
		public void SetProgressHandler(ProgressChangeHandler handler)
		{
			SetProgressHandler(handler, ProgressMode.NotifyRecords);
		}

		/// <summary>Set the handler to the engine used to notify progress into the operations.</summary>
		/// <param name="handler">Your <see cref="ProgressChangeHandler"/> method.</param>
		/// <param name="mode">The <see cref="ProgressMode"/> to use.</param>
		public void SetProgressHandler(ProgressChangeHandler handler, ProgressMode mode)
		{
			mNotifyHandler = handler;

			if (mode == ProgressMode.NotifyBytes)
				throw new NotImplementedException("Not implemented yet. Planed for version 1.4.0");

			mProgressMode = mode;
		}
		#endif

	}
}