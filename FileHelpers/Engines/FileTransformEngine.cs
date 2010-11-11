using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Diagnostics;

namespace FileHelpers
{

    /// <summary>
	/// This class allow you to convert the records of a file to a different record format.
	/// </summary>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="class_diagram.html">Class Diagram</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	/// <seealso href="attributes.html">Attributes List</seealso>
    /// <typeparam name="TSource">The source record type.</typeparam>
    /// <typeparam name="TDestination">The destination record type.</typeparam>
    [DebuggerDisplay("FileTransformanEngine for types: {SourceType.Name} --> {DestinationType.Name}. Source Encoding: {SourceEncoding.EncodingName}. Destination Encoding: {DestinationEncoding.EncodingName}")]
    public sealed class FileTransformEngine<TSource, TDestination>
        where TSource: class, ITransformable<TDestination> 
        where TDestination: class
	{

        #region "  Constructor  "

        /// <summary>Create a new FileTransformEngine.</summary>
		public FileTransformEngine()
		{
		}

		#endregion

		#region "  Private Fields  "

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private static object[] mEmptyArray = new object[] { };

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Encoding mSourceEncoding = Encoding.Default;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private Encoding mDestinationEncoding = Encoding.Default;

        private ErrorMode mErrorMode;

        /// <summary>Indicates the behavior of the engine when an error is found.</summary>
        public ErrorMode ErrorMode
        {
            get { return mErrorMode; }
            set
            {
                mErrorMode = value;
                mSourceErrorManager = new ErrorManager(value);
                mDestinationErrorManager = new ErrorManager(value);
            }
        }

        private ErrorManager mSourceErrorManager = new ErrorManager();

        /// <summary>
        /// Allow access the <see cref="ErrorManager"/> of the engine used to
        /// read the source file, is null before any file is transformed
        /// </summary>
        public ErrorManager SourceErrorManager
        {
            get { return mSourceErrorManager; }
        }

        private ErrorManager mDestinationErrorManager = new ErrorManager();

        /// <summary>
        /// Allow access the <see cref="ErrorManager"/> of the engine used to
        /// write the destination file, is null before any file is transformed
        /// </summary>
        public ErrorManager DestinationErrorManager
        {
            get { return mDestinationErrorManager; }
        }


		#endregion

		#region "  TransformFile  " 

		/// <summary>
        /// Transform the contents of the sourceFile and write them to the
        /// destFile.(use only if you need the array of the transformed
        /// records, TransformFileFast is faster)
        /// </summary>
		/// <param name="sourceFile">The source file.</param>
		/// <param name="destFile">The destination file.</param>
		/// <returns>The transformed records.</returns>
		public TDestination[] TransformFile(string sourceFile, string destFile)
		{
			ExHelper.CheckNullParam(sourceFile, "sourceFile");
			ExHelper.CheckNullParam(destFile, "destFile");
			ExHelper.CheckDifferentsParams(sourceFile, "sourceFile", destFile, "destFile");

			return CoreTransformFile(sourceFile, destFile);
		}


		/// <summary>
        /// Transform the contents of the sourceFile and write them to the
        /// destFile. (faster and uses less memory, best choice for big
        /// files)
        /// </summary>
		/// <param name="sourceFile">The source file.</param>
		/// <param name="destFile">The destination file.</param>
		/// <returns>The number of transformed records.</returns>
		public int TransformFileFast(string sourceFile, string destFile)
		{
			ExHelper.CheckNullParam(sourceFile, "sourceFile");
			ExHelper.CheckNullParam(destFile, "destFile");
			ExHelper.CheckDifferentsParams(sourceFile, "sourceFile", destFile, "destFile");

            return CoreTransformAsync(new InternalStreamReader(sourceFile, SourceEncoding, true, EngineBase.DefaultReadBufferSize * 5), new StreamWriter(destFile, false, DestinationEncoding, EngineBase.DefaultWriteBufferSize * 5));
		}

        /// <summary>
        /// Transform the contents of the sourceFile and write them to the
        /// destFile. (faster and uses less memory, best choice for big
        /// files)
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="destFile">The destination file.</param>
        /// <returns>The number of transformed records.</returns>
        public int TransformFileFast(TextReader sourceStream, string destFile)
        {
            ExHelper.CheckNullParam(sourceStream, "sourceStream");
            ExHelper.CheckNullParam(destFile, "destFile");

            return CoreTransformAsync(sourceStream, new StreamWriter(destFile, false, DestinationEncoding, EngineBase.DefaultWriteBufferSize * 5));
        }

        /// <summary>
        /// Transform the contents of the sourceFile and write them to the
        /// destFile. (faster and uses less memory, best choice for big
        /// files)
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <param name="destStream">The destination stream.</param>
        /// <returns>The number of transformed records.</returns>
        public int TransformFileFast(TextReader sourceStream, StreamWriter destStream)
        {
            ExHelper.CheckNullParam(sourceStream, "sourceStream");
            ExHelper.CheckNullParam(destStream, "destStream");

            return CoreTransformAsync(sourceStream, destStream);
        }

        /// <summary>
        /// Transform the contents of the sourceFile and write them to the
        /// destFile. (faster and uses less memory, best choice for big
        /// files)
        /// </summary>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="destStream">The destination stream.</param>
        /// <returns>The number of transformed records.</returns>
        public int TransformFileFast(string sourceFile, StreamWriter destStream)
        {
            ExHelper.CheckNullParam(sourceFile, "sourceFile");
            ExHelper.CheckNullParam(destStream, "destStream");

            return CoreTransformAsync(new InternalStreamReader(sourceFile, SourceEncoding, true, EngineBase.DefaultReadBufferSize * 5), destStream);
        }
		#endregion

//		public string TransformString(string sourceData)
//		{
//			if (mConvert1to2 == null)
//				throw new BadUsageException("You must define a method in the class " + SourceType.Name + " with the attribute [TransfortToRecord(typeof(" + DestinationType.Name + "))] that return an object of type " + DestinationType.Name);
//
//			return CoreTransformAsync(sourceFile, destFile, mSourceType, mDestinationType, mConvert1to2);
//		}


		/// <summary>
        /// Transforms an array of records from the source type to the destination type
        /// </summary>
		/// <param name="sourceRecords">An array of the source records.</param>
		/// <returns>The transformed records.</returns>
		public TDestination[] TransformRecords(TSource[] sourceRecords)
		{
			return CoreTransformRecords(sourceRecords);
			//return CoreTransformAsync(sourceFile, destFile, mSourceType, mDestinationType, mConvert1to2);
		}

		/// <summary>
        /// Transform a file that contains source records to an array of the destination type
        /// </summary>
		/// <param name="sourceFile">A file containing the source records.</param>
		/// <returns>The transformed records.</returns>

		public TDestination[] ReadAndTransformRecords(string sourceFile)
		{
			var engine = new FileHelperAsyncEngine<TSource>(mSourceEncoding);
            engine.ErrorMode = this.ErrorMode;
            mSourceErrorManager = engine.ErrorManager;
            mDestinationErrorManager = new ErrorManager(ErrorMode);

			var res = new List<TDestination>();

			engine.BeginReadFile(sourceFile);
			foreach (TSource record in engine)
			{
				res.Add(record.TransformTo());
			}
			engine.Close();

			return res.ToArray();
		}

		#region "  Transform Internal Methods  "

        			
        private TDestination[] CoreTransform(InternalStreamReader sourceFile, StreamWriter destFile)
        {

                FileHelperEngine<TSource> sourceEngine = new FileHelperEngine<TSource>(mSourceEncoding);
                FileHelperEngine<TDestination> destEngine = new FileHelperEngine<TDestination>(mDestinationEncoding);

                sourceEngine.ErrorMode = this.ErrorMode;
                destEngine.ErrorManager.ErrorMode = this.ErrorMode;

                mSourceErrorManager = sourceEngine.ErrorManager;
                mDestinationErrorManager = destEngine.ErrorManager;

                TSource[] source = sourceEngine.ReadStream(sourceFile);
                TDestination[] transformed = CoreTransformRecords(source);

                destEngine.WriteStream(destFile, transformed);

                return transformed;
            
		}

		private TDestination[] CoreTransformRecords(TSource[] sourceRecords)
		{
			var res = new List<TDestination>(sourceRecords.Length);
			
			for (int i = 0; i < sourceRecords.Length; i++)
			{
				res.Add(sourceRecords[i].TransformTo());
			}
			return res.ToArray();
		}

		
		private TDestination[] CoreTransformFile(string sourceFile, string destFile)
		{
			TDestination[] tempRes;

			using (var fs = new InternalStreamReader(sourceFile, mSourceEncoding, true, EngineBase.DefaultReadBufferSize * 10))
			{
                using (var ds = new StreamWriter(destFile, false, mDestinationEncoding, EngineBase.DefaultWriteBufferSize * 10))
				{
					tempRes = CoreTransform(fs, ds);
					ds.Close();
				}
				
				fs.Close();
			}


			return tempRes;
	}

        private int CoreTransformAsync(TextReader sourceFile, StreamWriter destFile)
		{
            var sourceEngine = new FileHelperAsyncEngine<TSource>();
			var destEngine = new FileHelperAsyncEngine<TDestination>();

            sourceEngine.ErrorMode = this.ErrorMode;
            destEngine.ErrorMode = this.ErrorMode;

            mSourceErrorManager = sourceEngine.ErrorManager;
            mDestinationErrorManager = destEngine.ErrorManager;

			sourceEngine.Encoding = mSourceEncoding;
			destEngine.Encoding = mDestinationEncoding;

			sourceEngine.BeginReadStream(sourceFile);
			destEngine.BeginWriteStream(destFile);

			foreach (TSource record in sourceEngine)
			{
				destEngine.WriteNext(record.TransformTo());
			}
			
			sourceEngine.Close();
			destEngine.Close();

			return sourceEngine.TotalRecords;
		}

		#endregion

		#region "  Properties  "

		/// <summary>The source record Type.</summary>
		public Type SourceType
		{
			get { return typeof(TSource); }
		}

		/// <summary>The destination record Type.</summary>
		public Type DestinationType
		{
			get { return typeof(TDestination); }
		}

		/// <summary>The Encoding of the Source File.</summary>
		public Encoding SourceEncoding
		{
			get { return mSourceEncoding; }
			set { mSourceEncoding = value; }
		}

		/// <summary>The Encoding of the Destination File.</summary>
		public Encoding DestinationEncoding
		{
			get { return mDestinationEncoding; }
			set { mDestinationEncoding = value; }
		}

		#endregion

	}
}