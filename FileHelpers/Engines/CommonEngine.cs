#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcosdotnet[at]yahoo.com.ar.

#endregion

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Text;


namespace FileHelpers
{
	/// <summary>This class only have <b>static methods</b> to work with files and strings (the most common of them)</summary>
	public sealed class CommonEngine
	{
		// No instanciate
		private CommonEngine()
		{}

		#region "  FileHelperEngine  "

		/// <summary>
		/// Used to read a file without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="fileName">The file name</param>
		/// <returns>The read records.</returns>
		public static object[] ReadFile(Type recordClass, string fileName)
		{
			FileHelperEngine engine = new FileHelperEngine(recordClass);
			return engine.ReadFile(fileName);
		}

		/// <summary>
		/// Used to read a string without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="input">The input string.</param>
		/// <returns>The read records.</returns>
		public static object[] ReadString(Type recordClass, string input)
		{
			FileHelperEngine engine = new FileHelperEngine(recordClass);
			return engine.ReadString(input);
		}

		/// <summary>
		/// Used to write a file without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="fileName">The file name</param>
		/// <param name="records">The records to write</param>
		public static void WriteFile(Type recordClass, string fileName, object[] records)
		{
			FileHelperEngine engine = new FileHelperEngine(recordClass);
			engine.WriteFile(fileName, records);
		}

		/// <summary>
		/// Used to write a string without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="records">The records to write</param>
		/// <returns>The string with the writen records.</returns>
		public static string WriteString(Type recordClass, object[] records)
		{
			FileHelperEngine engine = new FileHelperEngine(recordClass);
			return engine.WriteString(records);
		}

		#endregion

		/// <summary><b>Faster way</b> to Transform the records of type sourceType in the sourceFile in records of type destType and write them to the destFile.</summary>
		/// <param name="sourceType">The Type of the records in the source File.</param>
		/// <param name="destType">The Type of the records in the dest File.</param>
		/// <param name="sourceFile">The file with records to be transformed</param>
		/// <param name="destFile">The destination file with the transformed records</param>
		/// <returns>The number of transformed records</returns>
		public static int TransformFileAsync(string sourceFile, Type sourceType, string destFile, Type destType)
		{
			FileTransformEngine engine = new FileTransformEngine(sourceType, destType);
			return engine.TransformFileAsync(sourceFile, destFile);
		}

		/// <summary>Transform the records of type sourceType in the sourceFile in records of type destType and write them to the destFile. (but returns the transformed records) WARNING: this is a slower method that the TransformFileAssync.</summary>
		/// <param name="sourceType">The Type of the records in the source File.</param>
		/// <param name="destType">The Type of the records in the dest File.</param>
		/// <param name="sourceFile">The file with records to be transformed</param>
		/// <param name="destFile">The destination file with the transformed records</param>
		/// <returns>The transformed records.</returns>
		public static object[] TransformFile(string sourceFile, Type sourceType, string destFile, Type destType)
		{
			FileTransformEngine engine = new FileTransformEngine(sourceType, destType);
			return engine.TransformFile(sourceFile, destFile);
		}


		/// <summary>
		/// Read the contents of a file and sort the records.
		/// </summary>
		/// <param name="recordClass">Record Class (remember that need to implement the IComparer interface, or you can use SortFileByfield)</param>
		/// <param name="fileName">The file to read.</param>
		public static object[] ReadSortedFile(Type recordClass, string fileName)
		{
			if (typeof(IComparable).IsAssignableFrom(recordClass) == false)
				throw new BadUsageException("The record class must implement the interface IComparable to use the Sort feature.");

			FileHelperEngine engine = new FileHelperEngine(recordClass);
			object[] res = engine.ReadFile(fileName);

			if (res.Length == 0)
				return res;

			Array.Sort(res);
			return res;
		}

		/// <summary>
		/// Sort the contents of the source file and write them to the destination file. 
		/// </summary>
		/// <param name="recordClass">Record Class (remember that need to implement the IComparer interface, or you can use SortFileByfield)</param>
		/// <param name="sourceFile"></param>
		/// <param name="sortedFile"></param>
		public static void SortFile(Type recordClass, string sourceFile, string sortedFile)
		{
			if (typeof(IComparable).IsAssignableFrom(recordClass) == false)
				throw new BadUsageException("The record class must implement the interface IComparable to use the Sort feature.");

			FileHelperEngine engine = new FileHelperEngine(recordClass);
			object[] res = engine.ReadFile(sourceFile);

			if (res.Length == 0)
				engine.WriteFile(sortedFile, res);

			Array.Sort(res);
			engine.WriteFile(sortedFile, res);
		}

		public static void SortFileByField(Type recordClass, string fieldName, bool asc, string sourceFile, string sortedFile)
		{

			FileHelperEngine engine = new FileHelperEngine(recordClass);
			FieldInfo fi = engine.mRecordInfo.GetFieldInfo(fieldName);

			if (fi == null)
				throw new BadUsageException("The record class not contains the field " + fieldName);

			object[] res = engine.ReadFile(sourceFile);

			IComparer comparer = new FieldComparer(fi, asc);
			Array.Sort(res, comparer);

			engine.WriteFile(sortedFile, res);
		}

		public static void SortRecordsByField(object[] records, string fieldName)
		{
			SortRecordsByField(records, fieldName, true);
		}

		public static void SortRecordsByField(object[] records, string fieldName, bool asc)
		{
			if (records.Length > 0 && records[0] != null)
			{
				FileHelperEngine engine = new FileHelperEngine(records[0].GetType());
				FieldInfo fi = engine.mRecordInfo.GetFieldInfo(fieldName);

				if (fi == null)
					throw new BadUsageException("The record class not contains the field " + fieldName);

				IComparer comparer = new FieldComparer(fi, asc);
				Array.Sort(records, comparer);
			}
		}

		public static void SortRecords(object[] records)
		{
			if (records.Length > 0 && records[0] != null)
			{
				Type recordClass = records[0].GetType();

				if (typeof(IComparable).IsAssignableFrom(recordClass) == false)
					throw new BadUsageException("The record class must implement the interface IComparable to use the Sort feature.");

				Array.Sort(records);
			}
		}

		internal class FieldComparer : IComparer
		{
			FieldInfo mFieldInfo;
			int mAscending;
			
			public FieldComparer(FieldInfo fi, bool asc)
			{
				mFieldInfo = fi;
				mAscending = asc ? 1 : -1;
				if (typeof(IComparable).IsAssignableFrom(mFieldInfo.FieldType) == false)
					throw new BadUsageException("The field " + mFieldInfo.Name + " need to implement the interface IComparable");

			}

			public int Compare(object x, object y)
			{
				IComparable xv = mFieldInfo.GetValue(x) as IComparable;
				return xv.CompareTo(mFieldInfo.GetValue(y)) * mAscending;
			}
		}


	}
}