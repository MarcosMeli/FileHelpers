#undef GENERICS
//#define GENERICS
//#if NET_2_0

#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Collections;
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
	/// <seealso href="example_datalink.html">Example of the DataLink</seealso>
	/// <seealso href="attributes.html">Attributes List</seealso>
#if NET_2_0
    [DebuggerDisplay("FileTransformanEngine for types: {SourceType.Name} --> {DestinationType.Name}. Source Encoding: {SourceEncoding.EncodingName}. Destination Encoding: {DestinationEncoding.EncodingName}")]
#endif
#if ! GENERICS
	public sealed class FileTransformEngine
	{
#else
    /// <typeparam name="Source">The source record type.</typeparam>
    /// <typeparam name="Destination">The destination record type.</typeparam>
    public sealed class FileTransformEngine<Source, Destination>
	{
#endif

        #region "  Constructor  "

        /// <summary>Create a new instance of the class.</summary>
		/// <param name="sourceType">The source record Type.</param>
		/// <param name="destType">The destination record Type.</param>

#if ! GENERICS
		public FileTransformEngine(Type sourceType, Type destType)
		{
#else
		public FileTransformEngine()
		{
			Type sourceType = typeof(Source);
			Type destType = typeof(Destination);

#endif
			//throw new NotImplementedException("This feature is not ready yet. In the next release maybe work =)");
			ExHelper.CheckNullParam(sourceType, "sourceType");
			ExHelper.CheckNullParam(destType, "destType");
			ExHelper.CheckDifferentsParams(sourceType, "sourceType", destType, "destType");

			mSourceType = sourceType;
			mDestinationType = destType;

			ValidateRecordTypes();
		}

		#endregion

		#region "  Private Fields  "

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private static object[] mEmptyArray = new object[] { };

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private Type mSourceType;
#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private Type mDestinationType;

#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private Encoding mSourceEncoding = Encoding.Default;
#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        private Encoding mDestinationEncoding = Encoding.Default;


		#endregion

		#region "  TransformFile  " 

		/// <summary>Transform the contents of the sourceFile and write them to the destFile.(use only if you need the array of the transformed records, TransformFileAsync is faster)</summary>
		/// <param name="sourceFile">The source file.</param>
		/// <param name="destFile">The destination file.</param>
		/// <returns>The transformed records.</returns>
#if ! GENERICS
		public object[] TransformFile(string sourceFile, string destFile)
#else
		public Destination[] TransformFile(string sourceFile, string destFile)
#endif
		{
			ExHelper.CheckNullParam(sourceFile, "sourceFile");
			ExHelper.CheckNullParam(destFile, "destFile");
			ExHelper.CheckDifferentsParams(sourceFile, "sourceFile", destFile, "destFile");

			if (mConvert1to2 == null)
				throw new BadUsageException("You must define a method in the class " + SourceType.Name + " with the attribute [TransfortToRecord(typeof(" + DestinationType.Name + "))] that return an object of type " + DestinationType.Name);

			return CoreTransformFile(sourceFile, destFile, mSourceType, mDestinationType, mConvert1to2);
		}


		/// <summary>Transform the contents of the sourceFile and write them to the destFile. (faster and use less memory, best choice for big files)</summary>
		/// <param name="sourceFile">The source file.</param>
		/// <param name="destFile">The destination file.</param>
		/// <returns>The number of transformed records.</returns>
		public int TransformFileAsync(string sourceFile, string destFile)
		{
			ExHelper.CheckNullParam(sourceFile, "sourceFile");
			ExHelper.CheckNullParam(destFile, "destFile");
			ExHelper.CheckDifferentsParams(sourceFile, "sourceFile", destFile, "destFile");

			if (mConvert1to2 == null)
				throw new BadUsageException("You must define a method in the class " + SourceType.Name + " with the attribute [TransfortToRecord(typeof(" + DestinationType.Name + "))] that return an object of type " + DestinationType.Name);

			return CoreTransformAsync(sourceFile, destFile, mSourceType, mDestinationType, mConvert1to2);
		}


		#endregion

//		public string TransformString(string sourceData)
//		{
//			if (mConvert1to2 == null)
//				throw new BadUsageException("You must define a method in the class " + SourceType.Name + " with the attribute [TransfortToRecord(typeof(" + DestinationType.Name + "))] that return an object of type " + DestinationType.Name);
//
//			return CoreTransformAsync(sourceFile, destFile, mSourceType, mDestinationType, mConvert1to2);
//		}


		/// <summary>Transform an array of records from the source type to the destination type</summary>
		/// <param name="sourceRecords">An array of the source records.</param>
		/// <returns>The transformed records.</returns>
#if ! GENERICS
		public object[] TransformRecords(object[] sourceRecords)
#else
		public Destination[] TransformRecords(Source[] sourceRecords)
#endif
		{
			if (mConvert1to2 == null)
				throw new BadUsageException("You must define a method in the class " + SourceType.Name + " with the attribute [TransfortToRecord(typeof(" + DestinationType.Name + "))] that return an object of type " + DestinationType.Name);

			return CoreTransformRecords(sourceRecords, mConvert1to2);
			//return CoreTransformAsync(sourceFile, destFile, mSourceType, mDestinationType, mConvert1to2);
		}

		/// <summary>Transform a file that contains source records to an array of the destination type</summary>
		/// <param name="sourceFile">A file containing the source records.</param>
		/// <returns>The transformed records.</returns>

#if ! GENERICS
		public object[] ReadAndTransformRecords(string sourceFile)
#else
		public Destination[] ReadAndTransformRecords(string sourceFile)
#endif
		{
			if (mConvert1to2 == null)
				throw new BadUsageException("You must define a method in the class " + SourceType.Name + " with the attribute [TransfortToRecord(typeof(" + DestinationType.Name + "))] that return an object of type " + DestinationType.Name);

			FileHelperAsyncEngine engine = new FileHelperAsyncEngine(mSourceType, mSourceEncoding);

			ArrayList res = new ArrayList();

			engine.BeginReadFile(sourceFile);
#if ! GENERICS
			foreach (object record in engine)
#else
			foreach (Source record in engine)
#endif
			{
				res.Add(CoreTransformOneRecord(record, mConvert1to2));
			}
			engine.Close();

#if ! GENERICS
			return (object[]) res.ToArray(mDestinationType);
#else
			return (Destination[]) res.ToArray(mDestinationType);
#endif
		}

		#region "  Transform Internal Methods  "


			
#if ! GENERICS
        private object[] CoreTransform(StreamReader sourceFile, StreamWriter destFile, Type sourceType, Type destType, MethodInfo method)
		{

            FileHelperEngine sourceEngine = new FileHelperEngine(sourceType, mSourceEncoding);
			FileHelperEngine destEngine = new FileHelperEngine(destType, mDestinationEncoding);

            object[] source = sourceEngine.ReadStream(sourceFile);
			object[] transformed = CoreTransformRecords(source, method);
#else
        private Destination[] CoreTransform(StreamReader sourceFile, StreamWriter destFile, Type sourceType, Type destType, MethodInfo method)
        {

            FileHelperEngine<Source> sourceEngine = new FileHelperEngine<Source>(mSourceEncoding);
            FileHelperEngine<Destination> destEngine = new FileHelperEngine<Destination>(mDestinationEncoding);

            Source[] source = sourceEngine.ReadStream(sourceFile);
			Destination[] transformed = CoreTransformRecords(source, method);
#endif 

			destEngine.WriteStream(destFile, transformed);

			return transformed;
		}

#if ! GENERICS
		private object[] CoreTransformRecords(object[] sourceRecords, MethodInfo method)
#else
		private Destination[] CoreTransformRecords(Source[] sourceRecords, MethodInfo method)
#endif 
		{
			ArrayList res = new ArrayList(sourceRecords.Length);
			
			for (int i = 0; i < sourceRecords.Length; i++)
			{
				res.Add(CoreTransformOneRecord(sourceRecords[i], method));
			}
#if ! GENERICS
			return (object[]) res.ToArray(mDestinationType);
#else
			return (Destination[]) res.ToArray(mDestinationType);
#endif 
		}

		
#if ! GENERICS
		private object[] CoreTransformFile(string sourceFile, string destFile, Type sourceType, Type destType, MethodInfo method)
		{
			object[] tempRes;
#else
		private Destination[] CoreTransformFile(string sourceFile, string destFile, Type sourceType, Type destType, MethodInfo method)
		{
			Destination[] tempRes;
#endif 

			using (StreamReader fs = new StreamReader(sourceFile, mSourceEncoding, true))
			{
				using (StreamWriter ds = new StreamWriter(destFile, false, mDestinationEncoding))
				{
					tempRes = CoreTransform(fs, ds, sourceType, destType, method);
					ds.Close();
				}
				
				fs.Close();
			}


			return tempRes;
	}

		private int CoreTransformAsync(string sourceFile, string destFile, Type sourceType, Type destType, MethodInfo method)
		{
			FileHelperAsyncEngine sourceEngine = new FileHelperAsyncEngine(sourceType);
			FileHelperAsyncEngine destEngine = new FileHelperAsyncEngine(destType);

			sourceEngine.Encoding = mSourceEncoding;
			destEngine.Encoding = mDestinationEncoding;

			sourceEngine.BeginReadFile(sourceFile);
			destEngine.BeginWriteFile(destFile);

#if ! GENERICS
			foreach (object record in sourceEngine)
			{
				destEngine.WriteNext(CoreTransformOneRecord(record, method));
			}
#else
			foreach (Source record in sourceEngine)
			{
				destEngine.WriteNext(CoreTransformOneRecord(record, method));
			}
#endif
			
			sourceEngine.Close();
			destEngine.Close();

			return sourceEngine.TotalRecords;
		}

#if ! GENERICS
		private static object CoreTransformOneRecord(object record, MethodInfo method)
		{
			return method.Invoke(record, mEmptyArray);
		}
#else
		private static Destination CoreTransformOneRecord(Source record, MethodInfo method)
		{
			return (Destination) method.Invoke(record, mEmptyArray);
		}
#endif

		#endregion

		#region "  Properties  "


#if NET_2_0
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
#endif
        MethodInfo mConvert1to2 = null;
		//MethodInfo mConvert2to1 = null;

		/// <summary>The source record Type.</summary>
		public Type SourceType
		{
			get { return mSourceType; }
		}

		/// <summary>The destination record Type.</summary>
		public Type DestinationType
		{
			get { return mDestinationType; }
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

		#region "  Helper Methods  "

		private void ValidateRecordTypes()
		{
			mConvert1to2 = GetTransformMethod(SourceType, DestinationType);
			//			mConvert2to1 = GetTransformMethod(DestinationType, SourceType);

			//			if (mConvert2to1 == null)
			//				throw new BadUsageException("You must define a method in the class " + RecordType2.Name + " with the attribute [TransfortToRecord(typeof(" + RecordType2.Name + "))]");
		}

		private MethodInfo GetTransformMethod(Type sourceType, Type destType)
		{
			MethodInfo res = null;
			
			MethodInfo[] methods = sourceType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
			foreach (MethodInfo m in methods)
			{
				if (m.IsDefined(typeof (TransformToRecordAttribute), false))
				{
					TransformToRecordAttribute ta = (TransformToRecordAttribute) m.GetCustomAttributes(typeof (TransformToRecordAttribute), false)[0];
					if (ta.TargetType == destType)
					{
						if (m.ReturnType != destType)
							throw new BadUsageException("The method " + m.Name + " must return an object of type " + destType.Name + " (not " + m.ReturnType.Name + ")");
						else if (m.GetParameters().Length > 0)
							throw new BadUsageException("The method " + m.Name + " must have not parameters");
						else
							res = m;

						break;
					}
				}
			}

			return res;
		}

		#endregion

	}
}

//#endif
