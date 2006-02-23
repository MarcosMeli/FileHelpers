#region "  © Copyright 2005 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Reflection;

namespace FileHelpers.DataLink
{
	/// <summary>
	/// This class has the responsability to enable the two directional
	/// transformation.
	/// <list type="bullet">
	/// <item> File &lt;-> File  (with different record class)</item>
	/// </list>
	/// </summary>
	/// <seealso href="quick_start.html">Quick Start Guide</seealso>
	/// <seealso href="class_diagram.html">Class Diagram</seealso>
	/// <seealso href="examples.html">Examples of Use</seealso>
	/// <seealso href="example_datalink.html">Example of the DataLink</seealso>
	/// <seealso href="attributes.html">Attributes List</seealso>
	public sealed class FileTransformDataLink
	{
		#region "  Constructor  "

		/// <summary>Create a new instance of the class.</summary>
		/// <param name="sourceType">The source record Type.</param>
		/// <param name="destType">The destination record Type.</param>
		public FileTransformDataLink(Type sourceType, Type destType)
		{
			throw new NotImplementedException("This feature is not ready yet. In the next release maybe work =)");
            //ErrorHelper.CheckNullParam(sourceType, "sourceType");
            //ErrorHelper.CheckNullParam(destType, "destType");
            //ErrorHelper.CheckDifferentsParams(sourceType, "sourceType", destType, "destType");

            //mRecordType1 = sourceType;
            //mRecordType2 = destType;

            //ValidateRecordTypes();
		}

		#endregion

		private Type mRecordType1;
		private Type mRecordType2;


		/// <summary>Transform the contents of the sourceFile and write them to the destFile.</summary>
		/// <param name="sourceFile">The source file.</param>
		/// <param name="destFile">The destination file.</param>
		/// <returns>The transformed records in the destFile.</returns>
		public object[] TransformFile1To2(string sourceFile, string destFile)
		{
			ErrorHelper.CheckNullParam(sourceFile, "sourceFile");
			ErrorHelper.CheckNullParam(destFile, "destFile");
			ErrorHelper.CheckDifferentsParams(sourceFile, "sourceFile", destFile, "destFile");

			FileHelperEngine sourceEngine = new FileHelperEngine(mRecordType1);
			object[] res = sourceEngine.ReadFile(sourceFile);

			//TODO: Falta transformar esto 

			FileHelperEngine destEngine = new FileHelperEngine(mRecordType2);
			destEngine.WriteFile(destFile, res);

			return res;
		}


		MethodInfo mConvert1to2 = null;
		MethodInfo mConvert2to1 = null;

		/// <summary>The source record Type.</summary>
		public Type RecordType1
		{
			get { return mRecordType1; }
		}

		/// <summary>The destination record Type.</summary>
		public Type RecordType2
		{
			get { return mRecordType2; }
		}

		private void ValidateRecordTypes()
		{
			mConvert1to2 = GetTransformMethod(RecordType1, RecordType2);
			if (mConvert1to2 == null)
				throw new BadUsageException("You must to define a method in the class " + RecordType1.Name + " with the attribute [TransfortToRecord(typeof(" + RecordType1.Name + "))]");

			mConvert2to1 = GetTransformMethod(RecordType2, RecordType1);
			if (mConvert2to1 == null)
				throw new BadUsageException("You must to define a method in the class " + RecordType2.Name + " with the attribute [TransfortToRecord(typeof(" + RecordType2.Name + "))]");
		}

		private MethodInfo GetTransformMethod(Type sourceType, Type destType)
		{
			MethodInfo[] methods = sourceType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic);
			foreach (MethodInfo m in methods)
			{
				if (m.IsDefined(typeof (TransformToRecordAttribute), false))
				{
					TransformToRecordAttribute ta = (TransformToRecordAttribute) m.GetCustomAttributes(typeof (TransformToRecordAttribute), false)[0];
					if (ta.TargetType == destType)
					{
						return m;
					}
				}
			}

			return null;
		}

	}
}