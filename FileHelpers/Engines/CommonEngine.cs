#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

#if NET_2_0
using System.Collections.Generic;
#endif

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
			return ReadFile(recordClass, fileName, int.MaxValue);
		}

		/// <summary>
		/// Used to read a file without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="fileName">The file name</param>
		/// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
		/// <returns>The read records.</returns>
		public static object[] ReadFile(Type recordClass, string fileName, int maxRecords)
		{
			FileHelperEngine engine = new FileHelperEngine(recordClass);
			return engine.ReadFile(fileName, maxRecords);
		}






		/// <summary>
		/// Used to read a file as a DataTable without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="fileName">The file name</param>
		/// <returns>The datatable representing all the read records.</returns>
		public static DataTable ReadFileAsDT(Type recordClass, string fileName)
		{
			return ReadFileAsDT(recordClass, fileName, -1);
		}

		/// <summary>
		/// Used to read a file as a DataTable without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="fileName">The file name</param>
		/// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
		/// <returns>The datatable representing all the read records.</returns>
		public static DataTable ReadFileAsDT(Type recordClass, string fileName, int maxRecords)
		{
			FileHelperEngine engine = new FileHelperEngine(recordClass);
			return engine.ReadFileAsDT(fileName, maxRecords);
		}


#if NET_2_0
		/// <summary>
		/// Used to read a file without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="fileName">The file name</param>
		/// <returns>The read records.</returns>
		public static T[] ReadFile<T>(string fileName)
		{
			return ReadFile<T>(fileName, int.MaxValue);
		}

		/// <summary>
		/// Used to read a file without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="fileName">The file name</param>
		/// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
		/// <returns>The read records.</returns>
		public static T[] ReadFile<T>(string fileName, int maxRecords)
		{
			FileHelperEngine<T> engine = new FileHelperEngine<T>();
			return engine.ReadFile(fileName, maxRecords);
		}

#endif
		
		/// <summary>
		/// Used to read a string without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="input">The input string.</param>
		/// <returns>The read records.</returns>
		public static object[] ReadString(Type recordClass, string input)
		{
			return ReadString(recordClass, input, -1);
		}

		/// <summary>
		/// Used to read a string without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="input">The input string.</param>
		/// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
		/// <returns>The read records.</returns>
		public static object[] ReadString(Type recordClass, string input, int maxRecords)
		{
			FileHelperEngine engine = new FileHelperEngine(recordClass);
			return engine.ReadString(input, maxRecords);
		}

#if NET_2_0
		/// <summary>
		/// Used to read a string without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="input">The input string.</param>
		/// <returns>The read records.</returns>
		public static T[] ReadString<T>(string input)
		{
			FileHelperEngine<T> engine = new FileHelperEngine<T>();
			return engine.ReadString(input);
		}
#endif
		
		/// <summary>
		/// Used to write a file without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="fileName">The file name</param>
		/// <param name="records">The records to write (Can be an array, ArrayList, etc)</param>
		public static void WriteFile(Type recordClass, string fileName, IEnumerable records)
		{
			FileHelperEngine engine = new FileHelperEngine(recordClass);
			engine.WriteFile(fileName, records);
		}

#if NET_2_0
		/// <summary>
		/// Used to write a file without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="fileName">The file name</param>
        /// <param name="records">The records to write (Can be an array, List&lt;T&gt;, etc)</param>
 		public static void WriteFile<T>(string fileName, IEnumerable<T> records)
		{
			FileHelperEngine<T> engine = new FileHelperEngine<T>();
			engine.WriteFile(fileName, records);
		}
#endif

		/// <summary>
		/// Used to write a string without instanciate the engine.<br />
		/// <b>This is feature limited method try to use the non static methods.</b>
		/// </summary>
		/// <param name="recordClass">The record class.</param>
		/// <param name="records">The records to write (Can be an array, ArrayList, etc)</param>
		/// <returns>The string with the writen records.</returns>
		public static string WriteString(Type recordClass, IEnumerable records)
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
		/// <param name="recordClass">Record Class (remember that need to implement the IComparable interface or use the SortFileByfield instead)</param>
		/// <param name="sourceFile">The source file.</param>
		/// <param name="sortedFile">The destination File.</param>
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

		/// <summary>
		/// Sort the content of a File using the field name provided
		/// </summary>
		/// <param name="recordClass">The class for each record of the file.</param>
		/// <param name="fieldName">The name of the field used to sort the file.</param>
		/// <param name="asc">The sort direction.</param>
		/// <param name="sourceFile">The source file.</param>
		/// <param name="sortedFile">The destination File.</param>
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

		/// <summary>
		/// Sort the Record Array based in the field name provided. (for advanced sorting use SortRecords)
		/// </summary>
		/// <param name="fieldName">The field name.</param>
		/// <param name="records">The records Array.</param>
		public static void SortRecordsByField(object[] records, string fieldName)
		{
			SortRecordsByField(records, fieldName, true);
		}

		/// <summary>
		/// Sort the Record Array based in the field name provided. (for advanced sorting use SortRecords)
		/// </summary>
		/// <param name="fieldName">The field name.</param>
		/// <param name="records">The records Array.</param>
		/// <param name="ascending">The direction of the sort. True means Ascending.</param>
		public static void SortRecordsByField(object[] records, string fieldName, bool ascending)
		{
			if (records.Length > 0 && records[0] != null)
			{
				FileHelperEngine engine = new FileHelperEngine(records[0].GetType());
				FieldInfo fi = engine.mRecordInfo.GetFieldInfo(fieldName);

				if (fi == null)
					throw new BadUsageException("The record class not contains the field " + fieldName);

				IComparer comparer = new FieldComparer(fi, ascending);
				
				Array.Sort(records, comparer);
			}
		}

		/// <summary>
		/// Sort the Record Array. The records must be of a Type that implements the IComparable interface.
		/// </summary>
		/// <param name="records">The records Array.</param>
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

		#region "  FieldComparer  "

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

#if NET_1_1 || MINI
				IComparable xv = mFieldInfo.GetValue(x) as IComparable;
				return xv.CompareTo(mFieldInfo.GetValue(y)) * mAscending;
#else
				if (mGetFieldValueHandler == null)
					mGetFieldValueHandler = RecordInfo.CreateGetFieldMethod(mFieldInfo);

				IComparable xv = mGetFieldValueHandler(x) as IComparable;
				return xv.CompareTo(mGetFieldValueHandler(y)) * mAscending;
#endif

			}

#if ! (NET_1_1 || MINI)
			private GetFieldValueCallback mGetFieldValueHandler;
#endif

		}

		#endregion

		
#if ! MINI

		/// <summary>Converts any collection of records to a DataTebla using reflection. WARNING: this methods returns null if the number of records is 0, pass the Type of the records to get an empty DataTable.</summary>
		/// <param name="records">The records to be converted to a DataTable</param>
		/// <returns>The datatable containing the records as DataRows</returns>
		public static DataTable RecordsToDataTable(ICollection records)
		{
			return RecordsToDataTable(records, -1);
		}

		/// <summary>Converts any collection of records to a DataTebla using reflection. WARNING: this methods returns null if the number of records is 0, pass the Type of the records to get an empty DataTable.</summary>
		/// <param name="records">The records to be converted to a DataTable</param>
		/// <param name="maxRecords">The max number of records to add to the datatable. -1 for all.</param>
		/// <returns>The datatable containing the records as DataRows</returns>
		public static DataTable RecordsToDataTable(ICollection records, int maxRecords)
		{

			RecordInfo ri = null;
			foreach (object obj in records)
			{
				if (obj != null)
				{
					ri = new RecordInfo(obj.GetType());
					break;
				}
			}

			if (ri == null)
				return new DataTable();

			return ri.RecordsToDataTable(records, maxRecords);
		}

		/// <summary>Converts any collection of records to a DataTebla using reflection. If the number of records is 0 this methods returns an empty DataTable with the columns based on the fields of the Type.</summary>
		/// <param name="records">The records to be converted to a DataTable</param>
		/// <returns>The datatable containing the records as DataRows</returns>
		/// <param name="recordType">The type of the inner records.</param>
		public static DataTable RecordsToDataTable(ICollection records, Type recordType)
		{
			return RecordsToDataTable(records, recordType, -1);
		}

		/// <summary>Converts any collection of records to a DataTebla using reflection. If the number of records is 0 this methods returns an empty DataTable with the columns based on the fields of the Type.</summary>
		/// <param name="records">The records to be converted to a DataTable</param>
		/// <returns>The datatable containing the records as DataRows</returns>
		/// <param name="maxRecords">The max number of records to add to the datatable. -1 for all.</param>
		/// <param name="recordType">The type of the inner records.</param>
		public static DataTable RecordsToDataTable(ICollection records, Type recordType, int maxRecords)
		{
			RecordInfo ri = new RecordInfo(recordType);
			return ri.RecordsToDataTable(records, maxRecords);
		}

#endif

		/// <summary>
		/// Reads the file1 and file2 using the recordType and write it to destinationFile
		/// </summary>
		public static void MergeFiles(Type recordType, string file1, string file2, string destinationFile)
		{
			FileHelperAsyncEngine engineRead= new FileHelperAsyncEngine(recordType);
			FileHelperAsyncEngine engineWrite = new FileHelperAsyncEngine(recordType);
			
			engineWrite.BeginWriteFile(destinationFile);

			object[] readRecords;

			// Read FILE 1
			engineRead.BeginReadFile(file1);
			
			readRecords = engineRead.ReadNexts(50);
			while(readRecords.Length > 0)
			{
				engineWrite.WriteNexts(readRecords);
				readRecords = engineRead.ReadNexts(50);
			}
			engineRead.Close();

			// Read FILE 2
			engineRead.BeginReadFile(file2);

			readRecords = engineRead.ReadNexts(50);
			while(readRecords.Length > 0)
			{
				engineWrite.WriteNexts(readRecords);
				readRecords = engineRead.ReadNexts(50);
			}
			engineRead.Close();
			
			engineWrite.Close();
		} 

		/// <summary>
		/// Merge the contents of 2 files and write them sorted to a destination file.
		/// </summary>
		/// <param name="recordType">The record Type.</param>
		/// <param name="file1">File with contents to be merged.</param>
		/// <param name="file2">File with contents to be merged.</param>
		/// <param name="field">The name of the field used to sort the records.</param>
		/// <param name="destFile">The destination file.</param>
		/// <returns>The merged and sorted records.</returns>
		public static object[] MergeAndSortFile(Type recordType, string file1, string file2, string destFile, string field)
		{
			return MergeAndSortFile(recordType, file1, file2, destFile, field, true);
		}

		/// <summary>
		/// Merge the contents of 2 files and write them sorted to a destination file.
		/// </summary>
		/// <param name="recordType">The record Type.</param>
		/// <param name="file1">File with contents to be merged.</param>
		/// <param name="file2">File with contents to be merged.</param>
		/// <param name="field">The name of the field used to sort the records.</param>
		/// <param name="ascending">Indicate the order of sort.</param>
		/// <param name="destFile">The destination file.</param>
		/// <returns>The merged and sorted records.</returns>
		public static object[] MergeAndSortFile(Type recordType, string file1, string file2, string destFile, string field, bool ascending)
		{
			FileHelperEngine engine = new FileHelperEngine(recordType);

			ArrayList arr = new ArrayList();

			arr.AddRange(engine.ReadFile(file1));
			arr.AddRange(engine.ReadFile(file2));
            
			object[] res = (object[]) arr.ToArray(recordType);
			arr = null; // <- better performance (memory)

			CommonEngine.SortRecordsByField(res, field, ascending);

			engine.WriteFile(destFile, res);

			return res;
		}	

		/// <summary>
		/// Merge the contents of 2 files and write them sorted to a destination file.
		/// </summary>
		/// <param name="recordType">The record Type.</param>
		/// <param name="file1">File with contents to be merged.</param>
		/// <param name="file2">File with contents to be merged.</param>
		/// <param name="destFile">The destination file.</param>
		/// <returns>The merged and sorted records.</returns>
		public static object[] MergeAndSortFile(Type recordType,string file1, string file2, string destFile)
		{
			FileHelperEngine engine = new FileHelperEngine(recordType);

			ArrayList arr = new ArrayList();

			arr.AddRange(engine.ReadFile(file1));
			arr.AddRange(engine.ReadFile(file2));
            
			object[] res = (object[]) arr.ToArray(recordType);
			arr = null; // <- better performance (allow the GC to collect it)
			
			CommonEngine.SortRecords(res);

			engine.WriteFile(destFile, res);
			return res;
		}	


						
		/// <summary>Simply dumps the DataTable contents to a delimited file using a ',' as delimiter.</summary>
		/// <param name="dt">The source Data Table</param>
		/// <param name="filename">The destination file.</param>
		public static void DataTableToCsv(DataTable dt, string filename)
		{
			CsvEngine.DataTableToCsv(dt, filename);
		}


		/// <summary>Simply dumps the DataTable contents to a delimited file. Only allows to set the delimiter.</summary>
		/// <param name="dt">The source Data Table</param>
		/// <param name="filename">The destination file.</param>
		/// <param name="delimiter">The delimiter used to write the file</param>
		public static void DataTableToCsv(DataTable dt, string filename, char delimiter)
		{
			CsvEngine.DataTableToCsv(dt, filename, new CsvOptions("Tempo", delimiter, dt.Columns.Count));
		}
	

		/// <summary>Simply dumps the DataTable contents to a delimited file. Only allows to set the delimiter.</summary>
		/// <param name="dt">The source Data Table</param>
		/// <param name="filename">The destination file.</param>
		/// <param name="options">The options used to write the file</param>
		public static void DataTableToCsv(DataTable dt, string filename, CsvOptions options)
		{
			CsvEngine.DataTableToCsv(dt, filename, options);
		}

		/// <summary>Reads a Csv File and return their contents as DataTable (The file must have the field names in the first row)</summary>
		/// <param name="delimiter">The delimiter for each field</param>
		/// <param name="filename">The file to read.</param>
		/// <returns>The contents of the file as a DataTable</returns>
		public static DataTable CsvToDataTable(string filename, char delimiter)
		{
			return CsvEngine.CsvToDataTable(filename, delimiter);
		}

		/// <summary>Reads a Csv File and return their contents as DataTable (The file must have the field names in the first row)</summary>
		/// <param name="classname">The name of the record class</param>
		/// <param name="delimiter">The delimiter for each field</param>
		/// <param name="filename">The file to read.</param>
		/// <returns>The contents of the file as a DataTable</returns>
		public static DataTable CsvToDataTable(string filename, string classname, char delimiter)
		{
			return CsvEngine.CsvToDataTable(filename, classname, delimiter);
		}


		/// <summary>Reads a Csv File and return their contents as DataTable</summary>
		/// <param name="classname">The name of the record class</param>
		/// <param name="delimiter">The delimiter for each field</param>
		/// <param name="filename">The file to read.</param>
		/// <param name="hasHeader">Indicates if the file contains a header with the field names.</param>
		/// <returns>The contents of the file as a DataTable</returns>
		public static DataTable CsvToDataTable(string filename, string classname, char delimiter, bool hasHeader)
		{
			return CsvEngine.CsvToDataTable(filename, classname, delimiter, hasHeader);
		}

		/// <summary>Reads a Csv File and return their contents as DataTable</summary>
		/// <param name="filename">The file to read.</param>
		/// <param name="options">The options used to create the record mapping class.</param>
		/// <returns>The contents of the file as a DataTable</returns>
		public static DataTable CsvToDataTable(string filename, CsvOptions options)
		{
			return CsvEngine.CsvToDataTable(filename, options);
		}

		#region "  RemoveDuplicateRecords  "

		/// <summary>
		/// This method allow to remove the duplicated records from an array.
		/// </summary>
		/// <param name="arr">The array with the records to be checked.</param>
		/// <returns>An array with the result of remove the duplicate records from the source array.</returns>
		public static IComparableRecord[] RemoveDuplicateRecords(IComparableRecord[] arr)
		{
			if (arr == null || arr.Length == 0)
				return arr;

			ArrayList nodup = new ArrayList();

			for(int i = 0; i < arr.Length; i++)
			{
				bool isUnique = true; 
				
				for(int j = i+1; j < arr.Length; j++)
				{
					if (arr[i].IsEqualRecord(arr[j]))
					{
						isUnique = false;
						break;
					}
				}

				if (isUnique) nodup.Add(arr[i]); 
			}

			return (IComparableRecord[]) nodup.ToArray(arr[1].GetType());
    
		}

		#endregion

#if NET_1_1

		/// <summary>
		/// Shortcut method to read all the text in a file.
		/// </summary>
		/// <param name="file">The file name</param>
		/// <returns>The contents of the files</returns>
		public static string RawReadAllFile(string file)
		{
			StreamReader reader = new StreamReader(file);
			string res = reader.ReadToEnd();
			reader.Close();
			return res;
		}

#endif

		/// <summary>
		/// Shortcut method to read the first n lines of a text file.
		/// </summary>
		/// <param name="file">The file name</param>
		/// <param name="lines">The number of lines to read.</param>
		/// <returns>The first n lines of the file.</returns>
		public static string RawReadFirstLines(string file, int lines)
		{
			StringBuilder sb = new StringBuilder(Math.Min(lines * 50, 10000));

			StreamReader reader = new StreamReader(file);

			string line;
			for(int i = 0; i < lines; i++)
			{
				line = reader.ReadLine();
				if (line != null)
					sb.Append(line + StringHelper.NewLine);
			}
			reader.Close();

			return sb.ToString();
		}


	}

#if ! (NET_1_1 || MINI)
	internal delegate object GetFieldValueCallback(object record);
#endif

}