using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using FileHelpers.Events;
#if ! MINI

#endif


namespace FileHelpers
{

    /// <summary>
    /// Basic engine to read record by record
    /// </summary>
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

        ///// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngineCtr/*'/>
        ///// <param name="ri">Record information on new type</param>
        //internal FileHelperEngine(RecordInfo ri)
        //    : base(ri)
        //{
        //}


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
        
       private readonly bool mObjectEngine;

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

        /// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngineCtr/*'/>
        /// <param name="encoding">The Encoding used by the engine.</param>
        /// <param name="recordType">Type of record we are reading</param>
        protected FileHelperEngine(Type recordType, Encoding encoding)
            : base(recordType, encoding)
        {
            mObjectEngine = typeof(T) == typeof(object);
        }


        ///// <include file='FileHelperEngine.docs.xml' path='doc/FileHelperEngineCtr/*'/>
        ///// <param name="ri">Record information</param>
        //internal FileHelperEngine(RecordInfo ri)
        //    : base(ri)
        //{
        //    mObjectEngine = typeof(T) == typeof(object);
        //}


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
            using (var fs = new InternalStreamReader(fileName, mEncoding, true, DefaultReadBufferSize))
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
            using (var fs = new InternalStreamReader(fileName, mEncoding, true, DefaultReadBufferSize))
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
                return (T[])((ArrayList)result).ToArray(RecordInfo.RecordType);
            }
            else
                return ((List<T>)result).ToArray();
		}


        private void ReadStream(TextReader reader, int maxRecords, DataTable dt)
        {
            ReadStreamAsList(reader, maxRecords, dt);
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
            var recordReader = new NewLineDelimitedRecordReader(reader);

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
            using (var freader = new ForwardReader(recordReader, RecordInfo.IgnoreLast))
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

                if (RecordInfo.IgnoreFirst > 0)
                {
                    for (int i = 0; i < RecordInfo.IgnoreFirst && currentLine != null; i++)
                    {
                        mHeaderText += currentLine + StringHelper.NewLine;
                        currentLine = freader.ReadNextLine();
                        mLineNumber++;
                    }
                }

                bool byPass = false;

                if (maxRecords < 0)
                    maxRecords = int.MaxValue;

                var line = new LineInfo(currentLine) {mReader = freader};

                var values = new object[RecordInfo.FieldCount];

                while (currentLine != null && currentRecord < maxRecords)
                {
                    //Alireza Kousha 4/14/2011 : Fix Reported on: http://www.filehelpers.com/forums/viewtopic.php?f=4&t=1109 adding the following line:
                    completeLine = currentLine;  // <------------------------------

                    try
                    {
                        mTotalRecords++;
                        currentRecord++;

                        line.ReLoad(currentLine);

                        var skip = false;

                        var record = (T)RecordInfo.Operations.CreateRecordHandler();
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
                            if (RecordInfo.Operations.StringToRecord(record, line, values))
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
                                        dt.Rows.Add(RecordInfo.Operations.RecordToValues(record));
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
                            mLineNumber++;
                        }
                    }
                }

                if (RecordInfo.IgnoreLast > 0)
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

        ///// <include file='FileHelperEngine.docs.xml' path='doc/WriteFile/*'/>
        //public void WriteFileWithHeader(string fileName, IEnumerable<T> records)
        //{
        //    WriteFile(fileName, records, -1, true);
        //}

        ///// <include file='FileHelperEngine.docs.xml' path='doc/WriteFile2/*'/>
        //public void WriteFileWithHeader(string fileName, IEnumerable<T> records, int maxRecords)
        //{
        //    WriteFile(fileName, records, maxRecords, true)
        //}


		/// <include file='FileHelperEngine.docs.xml' path='doc/WriteFile/*'/>
		public void WriteFile(string fileName, IEnumerable<T> records)
		{
			WriteFile(fileName, records, -1);
		}

        ///// <include file='FileHelperEngine.docs.xml' path='doc/WriteFile2/*'/>
        //public void WriteFile(string fileName, IEnumerable<T> records, int maxRecords)
        //{
        //    WriteFile(fileName, records, maxRecords, false);
        //}

		/// <include file='FileHelperEngine.docs.xml' path='doc/WriteFile2/*'/>
		public void WriteFile(string fileName, IEnumerable<T> records, int maxRecords)
		{
            using (var fs = new StreamWriter(fileName, false, mEncoding, DefaultWriteBufferSize))
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

        ///// <include file='FileHelperEngine.docs.xml' path='doc/WriteStream/*'/>
        //[EditorBrowsable(EditorBrowsableState.Advanced)]
        //public void WriteStream(TextWriter writer, IEnumerable<T> records, int maxRecords)
        //{
        //    WriteStream(writer, records, maxRecords);
        //}


        ///// <include file='FileHelperEngine.docs.xml' path='doc/WriteStream/*'/>
        //[EditorBrowsable(EditorBrowsableState.Advanced)]
        //public void WriteStreamWithHeader(TextWriter writer, IEnumerable<T> records)
        //{
        //    WriteStream(writer, records, -1, true);
        //}

        ///// <include file='FileHelperEngine.docs.xml' path='doc/WriteStream/*'/>
        //[EditorBrowsable(EditorBrowsableState.Advanced)]
        //public void WriteStreamWithHeader(TextWriter writer, IEnumerable<T> records, int maxRecords)
        //{
        //    WriteStream(writer, records, maxRecords, true);
        //}

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
            foreach (var rec in records)
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
						if (RecordInfo.RecordType.IsInstanceOfType(rec) == false)
							throw new BadUsageException("This engine works with record of type " + RecordInfo.RecordType.Name + " and you use records of type " + rec.GetType().Name );
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
                        currentLine = RecordInfo.Operations.RecordToString(rec);
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

				recIndex++;
			}

			mTotalRecords = recIndex;

			if (!string.IsNullOrEmpty(mFooterText))
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
			var sb = new StringBuilder();
            using (var writer = new StringWriter(sb))
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

            using (TextWriter writer = StreamHelper.CreateFileAppender(fileName, mEncoding, true, false, DefaultWriteBufferSize))
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
            using (var fs = new InternalStreamReader(fileName, mEncoding, true, DefaultReadBufferSize))
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
            DataTable dt = RecordInfo.Operations.CreateEmptyDataTable();
			dt.BeginLoadData();
			ReadStream(reader, maxRecords, dt);
			dt.EndLoadData();

			return dt;
		}

		#endif

		#endregion

    }
}
