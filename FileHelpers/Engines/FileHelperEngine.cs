


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
using FileHelpers.Events;
using FileHelpers.Options;
using FileHelpers.Dynamic;

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
        /// <param name="recordType">The Type of the record class</param>
        /// <param name="encoding">The Encoding used by the engine.</param>
        /// 
        public FileHelperEngine(Type recordType, Encoding encoding)
			: base(recordType, encoding)
        {
            
        }

        internal FileHelperEngine(RecordInfo ri)
            : base(ri)
        {
        }


        #endregion

    }

    /// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngine/*'/>
	/// <include file='Examples.xml' path='doc/examples/FileHelperEngine/*'/>
    /// <typeparam name="T">The record type.</typeparam>
    [DebuggerDisplay("FileHelperEngine for type: {RecordType.Name}. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
	public class FileHelperEngine<T>
        : EventEngineBase<T>,
          IFileHelperEngine<T>
        where T : class
    {
        

                    private bool mObjectEngine;

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
		    mObjectEngine = typeof (T) == typeof (object);
		}

        protected FileHelperEngine(Type type, Encoding encoding)
            : base(type, encoding)
        {
            mObjectEngine = typeof(T) == typeof(object);
        }
		

		internal FileHelperEngine(RecordInfo ri)
			: base(ri)
		{
            mObjectEngine = typeof(T) == typeof(object);
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
			using (var fs = new StreamReader(fileName, mEncoding, true, DefaultReadBufferSize))
			{
				T[] tempRes;

                tempRes = ReadStream(fs, maxRecords);
				fs.Close();

				return tempRes;
			}
		}

        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadFile/*'/>
        public List<T> ReadFileAsList(string fileName)
        {
            return ReadFileAsList(fileName, int.MaxValue);
        }

        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadFile/*'/>
        /// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
        public List<T> ReadFileAsList(string fileName, int maxRecords)
        {
            using (var fs = new StreamReader(fileName, mEncoding, true, DefaultReadBufferSize))
            {
                var res = ReadStreamAsList(fs, maxRecords);
                fs.Close();
                return res;
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
            var result = ReadStreamAsList(reader, maxRecords, null);

            if (mObjectEngine)
            {
                return (T[])((ArrayList)result).ToArray(mRecordInfo.RecordType);
            }
            else
                return ((List<T>)result).ToArray();
		}


        private T[] ReadStream(TextReader reader, int maxRecords, DataTable dt)
        {
            var result = ReadStreamAsList(reader, maxRecords, dt);

            if (mObjectEngine)
            {
                return (T[]) ((ArrayList) result).ToArray(mRecordInfo.RecordType);
            }
            else
                return ((List<T>)result).ToArray();
        }

        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadStream/*'/>
		/// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public List<T> ReadStreamAsList(TextReader reader, int maxRecords)
		{
            var result = ReadStreamAsList(reader, maxRecords, null);
            
            if (mObjectEngine)
            {
                var res = new List<T>(result.Count);
                for (int i = 0; i < result.Count; i++)
                {
                    res.Add((T) result[i]);
                }
                return res;
            }
            else
                return (List<T>)result;
		}

#if !MINI
		private IList ReadStreamAsList(TextReader reader, int maxRecords, DataTable dt)
        {
#endif
            if (reader == null)
                throw new ArgumentNullException("reader", "The reader of the Stream can´t be null");
            NewLineDelimitedRecordReader recordReader = new NewLineDelimitedRecordReader(reader);

            ResetFields();
            mHeaderText = String.Empty;
            mFooterText = String.Empty;

		    IList result;

            if (mObjectEngine)
                result = new ArrayList();
            else
                result = new List<T>();

            int currentRecord = 0;
            
		    var streamInfo = new StreamInfoProvider(reader);
            using (var freader = new ForwardReader(recordReader, mRecordInfo.IgnoreLast))
            {
                freader.DiscardForward = true;

                string currentLine, completeLine;

                mLineNumber = 1;

                completeLine = freader.ReadNextLine();
                currentLine = completeLine;

#if !MINI
                if (MustNotifyProgress) // Avoid object creation
                    OnProgress(new ProgressEventArgs(0, -1, streamInfo.Position, streamInfo.TotalBytes));
#endif

                if (mRecordInfo.IgnoreFirst > 0)
                {
                    for (int i = 0; i < mRecordInfo.IgnoreFirst && currentLine != null; i++)
                    {
                        mHeaderText += currentLine + StringHelper.NewLine;
                        currentLine = freader.ReadNextLine();
                        mLineNumber++;
                    }
                }

                bool byPass = false;

                if (maxRecords < 0)
                    maxRecords = int.MaxValue;

                LineInfo line = new LineInfo(currentLine) {mReader = freader};

                object[] values = new object[mRecordInfo.FieldCount];
                while (currentLine != null && currentRecord < maxRecords)
                {
                    try
                    {
                        mTotalRecords++;
                        currentRecord++;

                        line.ReLoad(currentLine);

                        var skip = false;

                        T record = (T)mRecordInfo.Operations.CreateRecordHandler();
#if !MINI
                        if (MustNotifyProgress) // Avoid object creation
                            OnProgress(new ProgressEventArgs(currentRecord, -1, streamInfo.Position, streamInfo.TotalBytes));

                        BeforeReadEventArgs<T> e = null;
                        if (MustNotifyRead)
                        {
                            e = new BeforeReadEventArgs<T>(this, record, currentLine, LineNumber);
                            skip = OnBeforeReadRecord(e);
                            if (e.RecordLineChanged)
                                line.ReLoad(e.RecordLine);
                        }
#endif

                        if (skip == false)
                        {
                            if (mRecordInfo.Operations.StringToRecord(record, line, values))
                            {
#if !MINI
                                if (MustNotifyRead) // Avoid object creation
                                    skip = OnAfterReadRecord(currentLine, record, e.RecordLineChanged, LineNumber);
#endif
                                if (skip == false)
                                {
#if MINI
								resArray.Add(record);
#else
                                    if (dt == null)
                                        result.Add(record);
                                    else
                                        dt.Rows.Add(mRecordInfo.Operations.RecordToValues(record));
#endif
                                }
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
                                var err = new ErrorInfo
                                                    {
                                                        mLineNumber = freader.LineNumber,
                                                        mExceptionInfo = ex,
                                                        mRecordString = completeLine,
                            //							mColumnNumber = mColumnNum
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
                            mLineNumber++;
                        }
                    }
                }

                if (mRecordInfo.IgnoreLast > 0)
                {
                    mFooterText = freader.RemainingText;
                }
            }

		    return result;
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

            using (var reader = new InternalStringReader(source))
			{
			    var res = ReadStream(reader, maxRecords);
				reader.Close();
				return res;
			}
		}


        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadString/*'/>
        public List<T> ReadStringAsList(string source)
        {
            return ReadStringAsList(source, int.MaxValue);
        }

        /// <include file='FileHelperEngine.docs.xml' path='doc/ReadString/*'/>
        /// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
        public List<T> ReadStringAsList(string source, int maxRecords)
        {
            if (source == null)
                source = string.Empty;

            using (var reader = new InternalStringReader(source))
            {
                var res = ReadStreamAsList(reader, maxRecords);
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
            using (var fs = new StreamWriter(fileName, false, mEncoding, EngineBase.DefaultWriteBufferSize))
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

			if (!string.IsNullOrEmpty(mHeaderText))
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
                if (MustNotifyProgress) // Avoid object creation
                    OnProgress(new ProgressEventArgs(0, max));
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
						throw new BadUsageException(string.Format("The record at index {0} is null.", recIndex));

					if (first)
					{
						first = false;
						if (mRecordInfo.RecordType.IsInstanceOfType(rec) == false)
							throw new BadUsageException("This engine works with record of type " + mRecordInfo.RecordType.Name + " and you use records of type " + rec.GetType().Name );
					}


					bool skip = false;
					#if !MINI
                    if (MustNotifyProgress) // Avoid object creation
                        OnProgress(new ProgressEventArgs(recIndex + 1, max));
                    
                    if (MustNotifyWrite)
						skip = OnBeforeWriteRecord(rec, LineNumber);
					#endif

					if (skip == false)
					{
                        currentLine = mRecordInfo.Operations.RecordToString(rec);
						#if !MINI
                        if (MustNotifyWrite)
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

            using (TextWriter writer = StreamHelper.CreateFileAppender(fileName, mEncoding, true, false, EngineBase.DefaultWriteBufferSize))
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
			using (var fs = new StreamReader(fileName, mEncoding, true, DefaultReadBufferSize))
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

            using (var reader = new InternalStringReader(source))
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
            DataTable dt = mRecordInfo.Operations.CreateEmptyDataTable();
			dt.BeginLoadData();
			ReadStream(reader, maxRecords, dt);
			dt.EndLoadData();

			return dt;
		}

		#endif

		#endregion


	}
}


