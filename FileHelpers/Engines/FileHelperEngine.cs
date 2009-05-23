#region "  © Copyright 2005-07 to Marcos Meli - http://www.devoo.net"

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion


using System;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections.Generic;

#if ! MINI
using System.Data;
using FileHelpers.RunTime;

#endif


namespace FileHelpers
{


    public class FileHelperEngine
        : FileHelperEngine<object>
    {
        #region "  Constructor  "


        /// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngineCtr/*'/>
		public FileHelperEngine(Type recordType)
			: this(recordType, Encoding.Default)
        { }

        /// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngineCtr/*'/>
        /// <param name="encoding">The Encoding used by the engine.</param>
		public FileHelperEngine(Type recordType, Encoding encoding)
			: base(recordType, encoding)
        {
            
        }

        internal FileHelperEngine(RecordInfo ri)
            : base(ri)
        {
        }


        #endregion




        public string WriteString(IEnumerable records)
        {
            if (records == null)
                return base.WriteString(null);

            List<object> arr = new List<object>();
            foreach (var record in records)
            {
                arr.Add(record);
            }
            return base.WriteString(arr);
        }

        /// <summary>Called in read operations just before the record string is translated to a record.</summary>
        public new event BeforeReadRecordHandler BeforeReadRecord;
        /// <summary>Called in read operations just after the record was created from a record string.</summary>
        public new event AfterReadRecordHandler AfterReadRecord;
        /// <summary>Called in write operations just before the record is converted to a string to write it.</summary>
        public new event BeforeWriteRecordHandler BeforeWriteRecord;
        /// <summary>Called in write operations just after the record was converted to a string.</summary>
        public new event AfterWriteRecordHandler AfterWriteRecord;



        protected override bool OnBeforeReadRecord(BeforeReadRecordEventArgs<object> e)
        {
            BeforeReadRecordEventArgs enew = new BeforeReadRecordEventArgs(e.RecordLine, e.LineNumber);

            if (BeforeReadRecord != null)
            {
                BeforeReadRecord(this, enew);
                e.RecordLine = enew.RecordLine;
                return enew.SkipThisRecord;
            }

            return base.OnBeforeReadRecord(e);
        }

        protected override bool OnAfterReadRecord(string line, object record)
        {

            if (AfterReadRecord != null)
            {
                if (mRecordInfo.mNotifyRead)
                    ((INotifyRead)record).AfterRead(this, line);

                AfterReadRecordEventArgs e = null;
                e = new AfterReadRecordEventArgs(line, record, LineNumber);
                AfterReadRecord(this, e);
                return e.SkipThisRecord;
            }

            return base.OnAfterReadRecord(line, record);
        }

        protected override bool OnBeforeWriteRecord(object record)
        {

            if (BeforeWriteRecord != null)
            {
                if (mRecordInfo.mNotifyWrite)
                    ((INotifyWrite)record).BeforeWrite(this);

                BeforeWriteRecordEventArgs e = null;
                e = new BeforeWriteRecordEventArgs(record, LineNumber);
                BeforeWriteRecord(this, e);

                return e.SkipThisRecord;
            }

            return base.OnBeforeWriteRecord(record);
        }

        protected override string OnAfterWriteRecord(string line, object record)
        {

            if (AfterWriteRecord != null)
            {
                AfterWriteRecordEventArgs e = null;
                e = new AfterWriteRecordEventArgs(record, LineNumber, line);
                AfterWriteRecord(this, e);
                return e.RecordLine;
            }
            
            return base.OnAfterWriteRecord(line, record);
        }

    }

    /// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngine/*'/>
	/// <include file='Examples.xml' path='doc/examples/FileHelperEngine/*'/>
    [DebuggerDisplay("FileHelperEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
    /// <typeparam name="T">The record type.</typeparam>
	public class FileHelperEngine<T>: EngineBase
    {
        

		#region "  Constructor  "


		/// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngineCtr/*'/>
		public FileHelperEngine() 
			: this(Encoding.Default)

		{}

		/// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngineCtr/*'/>
		/// <param name="encoding">The Encoding used by the engine.</param>
		public FileHelperEngine(Encoding encoding) 
			: base(typeof(T), encoding)
		{
			CreateRecordOptions();
		}

        protected FileHelperEngine(Type type, Encoding encoding)
            : base(type, encoding)
        {
            CreateRecordOptions();
        }


		private void CreateRecordOptions()
		{
			if (mRecordInfo.IsDelimited)
				mOptions = new DelimitedRecordOptions(mRecordInfo);
			else
				mOptions = new FixedRecordOptions(mRecordInfo);
		}

		internal FileHelperEngine(RecordInfo ri)
			: base(ri)
		{
			CreateRecordOptions();
		}


		#endregion

		#region "  ReadFile  "

		/// <include file='FileHelperEngine.docs.xml' path='doc/ReadFile/*'/>
		public T[] ReadFile(string fileName)
		{
			return ReadFile(fileName, int.MaxValue);
		}

		/// <include file='FileHelperEngine.docs.xml' path='doc/ReadFile/*'/>
		/// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
		public T[] ReadFile(string fileName, int maxRecords)
		{
			using (StreamReader fs = new StreamReader(fileName, mEncoding, true))
			{
				T[] tempRes;

                tempRes = ReadStream(fs, maxRecords);
				fs.Close();

				return tempRes;
			}
		}

		#endregion


		#region "  ReadStream  "

		/// <include file='FileHelperEngine.docs.xml' path='doc/ReadStream/*'/>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public T[] ReadStream(TextReader reader)
		{
			return ReadStream(reader, int.MaxValue);
		}

		/// <include file='FileHelperEngine.docs.xml' path='doc/ReadStream/*'/>
		/// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public T[] ReadStream(TextReader reader, int maxRecords)
		{

#if ! MINI

			return ReadStream(reader, maxRecords, null);
		}


		private T[] ReadStream(TextReader reader, int maxRecords, DataTable dt)
        {
#endif
            if (reader == null)
                throw new ArgumentNullException("reader", "The reader of the Stream can´t be null");
            NewLineDelimitedRecordReader recordReader = new NewLineDelimitedRecordReader(reader);

            ResetFields();
            mHeaderText = String.Empty;
            mFooterText = String.Empty;

            ArrayList resArray = new ArrayList();
            int currentRecord = 0;

            using (ForwardReader freader = new ForwardReader(recordReader, mRecordInfo.mIgnoreLast))
            {
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
                        mHeaderText += currentLine + StringHelper.NewLine;
                        currentLine = freader.ReadNextLine();
                        mLineNumber++;
                    }
                }

                bool byPass = false;

                if (maxRecords < 0)
                    maxRecords = int.MaxValue;

                LineInfo line = new LineInfo(currentLine);
                line.mReader = freader;

                object[] values = new object[mRecordInfo.mFieldCount];
                while (currentLine != null && currentRecord < maxRecords)
                {
                    try
                    {
                        mTotalRecords++;
                        currentRecord++;

                        line.ReLoad(currentLine);

                        bool skip = false;
#if !MINI
                        ProgressHelper.Notify(mNotifyHandler, mProgressMode, currentRecord, -1);
                    BeforeReadRecordEventArgs<T> e = new BeforeReadRecordEventArgs<T>(currentLine, LineNumber);
                        skip = OnBeforeReadRecord(e);
                        if (e.RecordLineChanged)
                            line.ReLoad(e.RecordLine);
#endif

                        if (skip == false)
                        {
                            object record = mRecordInfo.StringToRecord(line, values);

#if !MINI
						skip = OnAfterReadRecord(currentLine, (T) record);
#endif

                            if (skip == false && record != null)
                            {
#if MINI
								resArray.Add(record);
#else
                                if (dt == null)
                                    resArray.Add(record);
                                else
                                    dt.Rows.Add(mRecordInfo.RecordToValues(record));
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
                            mLineNumber++;
                        }
                    }
                }

                if (mRecordInfo.mIgnoreLast > 0)
                {
                    mFooterText = freader.RemainingText;
                }
            }

			return (T[])
                   resArray.ToArray(RecordType);
        }

        #endregion

		
		
		#region "  ReadString  "

		/// <include file='FileHelperEngine.docs.xml' path='doc/ReadString/*'/>
		public T[] ReadString(string source)
		{
			return ReadString(source, int.MaxValue);
		}

		/// <include file='FileHelperEngine.docs.xml' path='doc/ReadString/*'/>
		/// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
		public T[] ReadString(string source, int maxRecords)
		{
			if (source == null)
				source = string.Empty;

			using (StringReader reader = new StringReader(source))
			{
				T[] res;
				res= ReadStream(reader, maxRecords);
				reader.Close();
				return res;
			}
		}

		#endregion

		#region "  WriteFile  "

		/// <include file='FileHelperEngine.docs.xml' path='doc/WriteFile/*'/>
		public void WriteFile(string fileName, IEnumerable<T> records)
		{
			WriteFile(fileName, records, -1);
		}

		/// <include file='FileHelperEngine.docs.xml' path='doc/WriteFile2/*'/>
		public void WriteFile(string fileName, IEnumerable<T> records, int maxRecords)
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
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void WriteStream(TextWriter writer, IEnumerable<T> records)
		{
			WriteStream(writer, records, -1);
		}

		/// <include file='FileHelperEngine.docs.xml' path='doc/WriteStream2/*'/>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void WriteStream(TextWriter writer, IEnumerable<T> records, int maxRecords)
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
			int max = maxRecords;
			if (records is IList)
				max = Math.Min(max < 0 ? int.MaxValue : max, ((IList)records).Count);

			#if !MINI
				ProgressHelper.Notify(mNotifyHandler, mProgressMode, 0, max);
			#endif

			int recIndex = 0;

			bool first = true;
            foreach (T rec in records)
			{
				if (recIndex == maxRecords)
					break;
				
				mLineNumber++;
				try
				{
					if (rec == null)
						throw new BadUsageException("The record at index " + recIndex.ToString() + " is null.");

					if (first)
					{
						first = false;
						if (mRecordInfo.mRecordType.IsInstanceOfType(rec) == false)
							throw new BadUsageException("This engine works with record of type " + mRecordInfo.mRecordType.Name + " and you use records of type " + rec.GetType().Name );
					}


					bool skip = false;
					#if !MINI
						ProgressHelper.Notify(mNotifyHandler, mProgressMode, recIndex+1, max);
						skip = OnBeforeWriteRecord(rec);
					#endif

					if (skip == false)
					{
						currentLine = mRecordInfo.RecordToString(rec);
						#if !MINI
						currentLine = OnAfterWriteRecord(currentLine, rec);
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

				recIndex++;
			}

			mTotalRecords = recIndex;

			if (mFooterText != null && mFooterText != string.Empty)
				if (mFooterText.EndsWith(StringHelper.NewLine))
					writer.Write(mFooterText);
				else
					writer.WriteLine(mFooterText);

    	}

		#endregion

		#region "  WriteString  "

		/// <include file='FileHelperEngine.docs.xml' path='doc/WriteString/*'/>
		public string WriteString(IEnumerable<T> records)
		{
			return WriteString(records, -1);
		}

		/// <include file='FileHelperEngine.docs.xml' path='doc/WriteString2/*'/>
		public string WriteString(IEnumerable<T> records, int maxRecords)
		{
			StringBuilder sb = new StringBuilder();
            using (StringWriter writer = new StringWriter(sb))
            {
                WriteStream(writer, records, maxRecords);
                string res = writer.ToString();
                return res;
            }
		}

		#endregion

		#region "  AppendToFile  "

		/// <include file='FileHelperEngine.docs.xml' path='doc/AppendToFile1/*'/>
		public void AppendToFile(string fileName, T record)
		{
			AppendToFile(fileName, new T[] {record});
		}

		/// <include file='FileHelperEngine.docs.xml' path='doc/AppendToFile2/*'/>
		public void AppendToFile(string fileName, IEnumerable<T> records)
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

		#region "  DataTable Ops  "

		#if ! MINI
	
		/// <summary>
		/// Read the records of the file and fill a DataTable with them
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <returns>The DataTable with the read records.</returns>
		public DataTable ReadFileAsDT(string fileName)
		{
			return ReadFileAsDT(fileName, -1);
		}

		/// <summary>
		/// Read the records of the file and fill a DataTable with them
		/// </summary>
		/// <param name="fileName">The file name.</param>
		/// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
		/// <returns>The DataTable with the read records.</returns>
		public DataTable ReadFileAsDT(string fileName, int maxRecords)
		{
			using (StreamReader fs = new StreamReader(fileName, mEncoding, true))
			{
				DataTable res;
				res = ReadStreamAsDT(fs, maxRecords);
				fs.Close();

				return res;
			}
		}

		
		
		/// <summary>
		/// Read the records of a string and fill a DataTable with them.
		/// </summary>
		/// <param name="source">The source string with the records.</param>
		/// <returns>The DataTable with the read records.</returns>
		public DataTable ReadStringAsDT(string source)
		{
			return ReadStringAsDT(source, -1);
		}

		/// <summary>
		/// Read the records of a string and fill a DataTable with them.
		/// </summary>
		/// <param name="source">The source string with the records.</param>
		/// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
		/// <returns>The DataTable with the read records.</returns>
		public DataTable ReadStringAsDT(string source, int maxRecords)
		{
			if (source == null)
				source = string.Empty;

			using (StringReader reader = new StringReader(source))
			{
				DataTable res;
				res = ReadStreamAsDT(reader, maxRecords);
				reader.Close();
				return res;
			}
		}

		/// <summary>
		/// Read the records of the stream and fill a DataTable with them
		/// </summary>
		/// <param name="reader">The stream with the source records.</param>
		/// <returns>The DataTable with the read records.</returns>
		public DataTable ReadStreamAsDT(TextReader reader)
		{
			return ReadStreamAsDT(reader, -1);
		}

		/// <summary>
		/// Read the records of the stream and fill a DataTable with them
		/// </summary>
		/// <param name="reader">The stream with the source records.</param>
		/// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
		/// <returns>The DataTable with the read records.</returns>
		public DataTable ReadStreamAsDT(TextReader reader, int maxRecords)
		{
			DataTable dt = mRecordInfo.CreateEmptyDataTable();
			dt.BeginLoadData();
			ReadStream(reader, maxRecords, dt);
			dt.EndLoadData();

			return dt;
		}

		#endif

		#endregion


		#region "  Event Handling  "

#if ! MINI

        /// <summary>Called in read operations just before the record string is translated to a record.</summary>
        public event BeforeReadRecordHandler<T> BeforeReadRecord;
        /// <summary>Called in read operations just after the record was created from a record string.</summary>
        public event AfterReadRecordHandler<T> AfterReadRecord;
        /// <summary>Called in write operations just before the record is converted to a string to write it.</summary>
        public event BeforeWriteRecordHandler<T> BeforeWriteRecord;
        /// <summary>Called in write operations just after the record was converted to a string.</summary>
        public event AfterWriteRecordHandler<T> AfterWriteRecord;

        protected virtual bool OnBeforeReadRecord(BeforeReadRecordEventArgs<T> e)
		{

			if (BeforeReadRecord != null)
			{
				BeforeReadRecord(this, e);

				return e.SkipThisRecord;
			}

			return false;
		}

        protected virtual bool OnAfterReadRecord(string line, T record)
		{
			if (mRecordInfo.mNotifyRead)
				((INotifyRead)record).AfterRead(this, line);

		    if (AfterReadRecord != null)
			{
				AfterReadRecordEventArgs<T> e = null;
				e = new AfterReadRecordEventArgs<T>(line, record, LineNumber);
				AfterReadRecord(this, e);
				return e.SkipThisRecord;
			}
			
			return false;
		}

        protected virtual bool OnBeforeWriteRecord(T record)
		{
			if (mRecordInfo.mNotifyWrite)
				((INotifyWrite)record).BeforeWrite(this);

		    if (BeforeWriteRecord != null)
			{
				BeforeWriteRecordEventArgs<T> e = null;
                e = new BeforeWriteRecordEventArgs<T>(record, LineNumber);
				BeforeWriteRecord(this, e);

				return e.SkipThisRecord;
			}

			return false;
		}

		protected virtual string OnAfterWriteRecord(string line, T record)
		{

			if (AfterWriteRecord != null)
			{
                AfterWriteRecordEventArgs<T> e = null;
                e = new AfterWriteRecordEventArgs<T>(record, LineNumber, line);
                AfterWriteRecord(this, e);
				return e.RecordLine;
			}
			return line;
		}

#endif



        #endregion

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        internal RecordOptions mOptions;

		/// <summary>
		/// Allows to change some record layout options at runtime
		/// </summary>
		public RecordOptions Options
		{
			get { return mOptions; }
			set { mOptions = value; }
		}

	}

}


