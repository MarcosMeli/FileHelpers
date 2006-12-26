#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
using System.Reflection;
using System.Text;

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
	public sealed class FileTransformEngine
	{
		#region "  Constructor  "

		/// <summary>Create a new instance of the class.</summary>
		/// <param name="sourceType">The source record Type.</param>
		/// <param name="destType">The destination record Type.</param>
		public FileTransformEngine(Type sourceType, Type destType)
		{
			//throw new NotImplementedException("This feature is not ready yet. In the next release maybe work =)");
			ErrorHelper.CheckNullParam(sourceType, "sourceType");
			ErrorHelper.CheckNullParam(destType, "destType");
			ErrorHelper.CheckDifferentsParams(sourceType, "sourceType", destType, "destType");

			mSourceType = sourceType;
			mDestinationType = destType;

			ValidateRecordTypes();
		}

		#endregion

		private Type mSourceType;
		private Type mDestinationType;

		private Encoding mSourceEncoding = Encoding.Default;
		private Encoding mDestinationEncoding = Encoding.Default;

		/// <summary>Transform the contents of the sourceFile and write them to the destFile.(use only if you need the array of the transformed records, TransformFileAsync is faster)</summary>
		/// <param name="sourceFile">The source file.</param>
		/// <param name="destFile">The destination file.</param>
		/// <returns>The transformed records in the destFile.</returns>
		public object[] TransformFile(string sourceFile, string destFile)
		{
			ErrorHelper.CheckNullParam(sourceFile, "sourceFile");
			ErrorHelper.CheckNullParam(destFile, "destFile");
			ErrorHelper.CheckDifferentsParams(sourceFile, "sourceFile", destFile, "destFile");

			if (mConvert1to2 == null)
			   throw new BadUsageException("You must define a method in the class " + SourceType.Name + " with the attribute [TransfortToRecord(typeof(" + DestinationType.Name + "))] that return an object of type " + DestinationType.Name);

			return Transform(sourceFile, destFile, mSourceType, mDestinationType, mConvert1to2);
		}


		/// <summary>Transform the contents of the sourceFile and write them to the destFile. (faster and use less memory, best choice for big files)</summary>
		/// <param name="sourceFile">The source file.</param>
		/// <param name="destFile">The destination file.</param>
		/// <returns>The number of transformed records.</returns>
		public int TransformFileAsync(string sourceFile, string destFile)
		{
			ErrorHelper.CheckNullParam(sourceFile, "sourceFile");
			ErrorHelper.CheckNullParam(destFile, "destFile");
			ErrorHelper.CheckDifferentsParams(sourceFile, "sourceFile", destFile, "destFile");

			if (mConvert1to2 == null)
				throw new BadUsageException("You must define a method in the class " + SourceType.Name + " with the attribute [TransfortToRecord(typeof(" + DestinationType.Name + "))] that return an object of type " + DestinationType.Name);

			return TransformAsync(sourceFile, destFile, mSourceType, mDestinationType, mConvert1to2);
		}

		private static object[] mEmptyArray = new object[]{};

		private object[] Transform(string sourceFile, string destFile, Type sourceType, Type destType, MethodInfo method)
		{
			FileHelperEngine sourceEngine = new FileHelperEngine(sourceType);
			FileHelperEngine destEngine = new FileHelperEngine(destType);

			sourceEngine.Encoding = mSourceEncoding;
			destEngine.Encoding = mDestinationEncoding;
			
			object[] res = sourceEngine.ReadFile(sourceFile);

			ArrayList arr = new ArrayList(res.Length);
			for (int i = 0; i < res.Length; i++)
			{
				arr.Add(method.Invoke(res[i], mEmptyArray));
			}

			destEngine.WriteFile(destFile, arr.ToArray());


			return res;
			
		}

		private int TransformAsync(string sourceFile, string destFile, Type sourceType, Type destType, MethodInfo method)
		{
			FileHelperAsyncEngine sourceEngine = new FileHelperAsyncEngine(sourceType);
			FileHelperAsyncEngine destEngine = new FileHelperAsyncEngine(destType);

			sourceEngine.Encoding = mSourceEncoding;
			destEngine.Encoding = mDestinationEncoding;

			sourceEngine.BeginReadFile(sourceFile);
			destEngine.BeginWriteFile(destFile);

			while (sourceEngine.ReadNext() != null)
			{
				destEngine.WriteNext(method.Invoke(sourceEngine.LastRecord, mEmptyArray));
			}

			sourceEngine.Close();
			destEngine.Close();

			return sourceEngine.TotalRecords;
		}

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

	}
}