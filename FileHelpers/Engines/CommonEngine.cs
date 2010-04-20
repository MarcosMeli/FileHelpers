

using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Collections.Generic;
using FileHelpers.Options;

namespace FileHelpers
{
	/// <summary>This class only have <b>static methods</b> to work with files and strings (the most common of them)</summary>
    public sealed class CommonEngine
    {
        // No instanciate
        private CommonEngine()
        { }

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
            var engine = new FileHelperEngine(recordClass);
            return engine.ReadFileAsDT(fileName, maxRecords);
        }



        /// <summary>
        /// Used to read a file without instanciate the engine.<br />
        /// <b>This is feature limited method try to use the non static methods.</b>
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <returns>The read records.</returns>
        public static T[] ReadFile<T>(string fileName) where T : class
        {
            return ReadFile<T>(fileName, int.MaxValue);
        }

        /// <summary>
        /// Used to read a file without instanciate the engine.<br />
        /// <b>This is feature limited method try to use the non static methods.</b>
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="maxRecords">The max number of records to read. Int32.MaxValue or -1 to read all records.</param>
        /// <returns>The read records.</returns>
        public static T[] ReadFile<T>(string fileName, int maxRecords) where T: class
        {
            FileHelperEngine<T> engine = new FileHelperEngine<T>();
            return engine.ReadFile(fileName, maxRecords);
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

        /// <summary>
        /// Used to read a string without instanciate the engine.<br />
        /// <b>This is feature limited method try to use the non static methods.</b>
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>The read records.</returns>
        public static T[] ReadString<T>(string input) where T : class
        {
            FileHelperEngine<T> engine = new FileHelperEngine<T>();
            return engine.ReadString(input);
        }

        /// <summary>
        /// Used to write a file without instanciate the engine.<br />
        /// <b>This is feature limited method try to use the non static methods.</b>
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="records">The records to write (Can be an array, List, etc)</param>
        public static void WriteFile<T>(string fileName, IEnumerable<T> records) where T : class
        {
            FileHelperEngine<T> engine = new FileHelperEngine<T>();
            engine.WriteFile(fileName, records);
        }


        [Obsolete("Your must use WriteString<T>(IEnumerable<T>) without the type parameter", true)]
                public static string WriteString(Type type, IEnumerable records)
                {
                    return null;
                }

	    /// <summary>
        /// Used to write a string without instanciate the engine.<br />
        /// <b>This is feature limited method try to use the non static methods.</b>
        /// </summary>
        /// <param name="records">The records to write (Can be an array, List, etc)</param>
        /// <returns>The string with the writen records.</returns>
        public static string WriteString<T>(IEnumerable<T> records) where T : class
        {
            FileHelperEngine<T> engine = new FileHelperEngine<T>();
            return engine.WriteString(records);
        }

        #endregion

        /// <summary><b>Faster way</b> to Transform the records of type sourceType in the sourceFile in records of type destType and write them to the destFile.</summary>
        /// <param name="sourceType">The Type of the records in the source File.</param>
        /// <param name="destType">The Type of the records in the dest File.</param>
        /// <param name="sourceFile">The file with records to be transformed</param>
        /// <param name="destFile">The destination file with the transformed records</param>
        /// <returns>The number of transformed records</returns>
        public static int TransformFileAsync<TSource, TDest>(string sourceFile, string destFile) 
            where TSource : class, ITransformable<TDest>
            where TDest : class 
        {
            var engine = new FileTransformEngine<TSource, TDest>();
            return engine.TransformFileAsync(sourceFile, destFile);
        }

        /// <summary>Transform the records of type sourceType in the sourceFile in records of type destType and write them to the destFile. (but returns the transformed records) WARNING: this is a slower method that the TransformFileAssync.</summary>
        /// <param name="sourceType">The Type of the records in the source File.</param>
        /// <param name="destType">The Type of the records in the dest File.</param>
        /// <param name="sourceFile">The file with records to be transformed</param>
        /// <param name="destFile">The destination file with the transformed records</param>
        /// <returns>The transformed records.</returns>
        public static object[] TransformFile<TSource, TDest>(string sourceFile, string destFile) 
            where TSource : class, ITransformable<TDest>
            where TDest : class 
        {
            var engine = new FileTransformEngine<TSource, TDest>();
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

#if MINI
				IComparable xv = mFieldInfo.GetValue(x) as IComparable;
				return xv.CompareTo(mFieldInfo.GetValue(y)) * mAscending;
#else
                if (mGetFieldValueHandler == null)
                    mGetFieldValueHandler = ReflectionHelper.CreateGetFieldMethod(mFieldInfo);

                var xv = mGetFieldValueHandler(x) as IComparable;
                return xv.CompareTo(mGetFieldValueHandler(y)) * mAscending;
#endif

            }

#if ! (MINI)
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

            IRecordInfo ri = null;
            foreach (object obj in records)
            {
                if (obj != null)
                {
                    ri = RecordInfo.Resolve(obj.GetType());
                    break;
                }
            }

            if (ri == null)
                return new DataTable();

            return ri.Operations.RecordsToDataTable(records, maxRecords);
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
            IRecordInfo ri = RecordInfo.Resolve(recordType);
            return ri.Operations.RecordsToDataTable(records, maxRecords);
        }

#endif

        /// <summary>
        /// Reads the file1 and file2 using the recordType and write it to destinationFile
        /// </summary>
        public static void MergeFiles(Type recordType, string file1, string file2, string destinationFile)
        {
            FileHelperAsyncEngine engineRead = new FileHelperAsyncEngine(recordType);
            FileHelperAsyncEngine engineWrite = new FileHelperAsyncEngine(recordType);

            engineWrite.BeginWriteFile(destinationFile);

            object[] readRecords;

            // Read FILE 1
            engineRead.BeginReadFile(file1);

            readRecords = engineRead.ReadNexts(50);
            while (readRecords.Length > 0)
            {
                engineWrite.WriteNexts(readRecords);
                readRecords = engineRead.ReadNexts(50);
            }
            engineRead.Close();

            // Read FILE 2
            engineRead.BeginReadFile(file2);

            readRecords = engineRead.ReadNexts(50);
            while (readRecords.Length > 0)
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

            var list = engine.ReadFileAsList(file1);
            list.AddRange(engine.ReadFileAsList(file2));

            var res = list.ToArray();
            list = null; // <- better performance (memory)

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
        public static object[] MergeAndSortFile(Type recordType, string file1, string file2, string destFile)
        {
            FileHelperEngine engine = new FileHelperEngine(recordType);

            var list = engine.ReadFileAsList(file1);
            list.AddRange(engine.ReadFileAsList(file2));

            var res = list.ToArray();
            list = null; // <- better performance (memory)

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
        public static T[] RemoveDuplicateRecords<T>(T[] arr) where T : IComparableRecord<T>
        {
            if (arr == null || arr.Length <= 1)
                return arr;

            List<T> nodup = new List<T>();

            for (int i = 0; i < arr.Length; i++)
            {
                bool isUnique = true;

                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i].IsEqualRecord(arr[j]))
                    {
                        isUnique = false;
                        break;
                    }
                }

                if (isUnique) nodup.Add(arr[i]);
            }

            return nodup.ToArray();
        }

        #endregion

        /// <summary>
        /// Shortcut method to read the first n lines of a text file.
        /// </summary>
        /// <param name="file">The file name</param>
        /// <param name="lines">The number of lines to read.</param>
        /// <returns>The first n lines of the file.</returns>
        public static string RawReadFirstLines(string file, int lines)
        {
            var sb = new StringBuilder(Math.Min(lines * 50, 10000));

            var reader = new StreamReader(file);

            for (int i = 0; i < lines; i++)
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;
                else
                    sb.Append(line + StringHelper.NewLine);
            }
            reader.Close();

            return sb.ToString();
        }

                /// <summary>
                /// Shortcut method to read the first n lines of a text file as array.
                /// </summary>
                /// <param name="file">The file name</param>
                /// <param name="lines">The number of lines to read.</param>
                /// <returns>The first n lines of the file.</returns>
                public static string[] RawReadFirstLinesArray(string file, int lines)
                {
                    return RawReadFirstLinesArray(file, lines, Encoding.Default);
                }

	    /// <summary>
        /// Shortcut method to read the first n lines of a text file as array.
        /// </summary>
        /// <param name="file">The file name</param>
        /// <param name="lines">The number of lines to read.</param>
        /// <param name="encoding">The Encoding used to read the file</param>
        /// <returns>The first n lines of the file.</returns>
        public static string[] RawReadFirstLinesArray(string file, int lines, Encoding encoding)
        {

            var res = new List<string>(lines);
            using(var reader = new StreamReader(file, encoding))
            {
                for (int i = 0; i < lines; i++)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;
                    else
                        res.Add(line);
                }
            }

	        return res.ToArray();
        }


        #region "  ReadCSV  "



        /// <summary>
        /// A fast way to read record by record a CSV file delimited by ','. The fields can be quoted.
        /// </summary>
        /// <param name="filename">The csv file to read</param>
        /// <returns>An enumeration of <see cref="RecordIndexer"/></returns>
        public static IEnumerable<RecordIndexer> ReadCsv(string filename)
        {
            return ReadCsv(filename, ',');
        }

        /// <summary>
        /// A fast way to read record by record a CSV file with a custom delimiter. The fields can be quoted.
        /// </summary>
        /// <param name="filename">The csv file to read</param>
        /// <param name="delimiter">The field delimiter.</param>
        /// <returns>An enumeration of <see cref="RecordIndexer"/></returns>
        public static IEnumerable<RecordIndexer> ReadCsv(string filename, char delimiter)
        {
            return ReadCsv(filename, delimiter, 0);
        }

        /// <summary>
        /// A fast way to read record by record a CSV file with a custom delimiter. The fields can be quoted.
        /// </summary>
        /// <param name="filename">The csv file to read</param>
        /// <param name="delimiter">The field delimiter.</param>
        /// <param name="headerLines">The number of header lines in the CSV file</param>
        /// <returns>An enumeration of <see cref="RecordIndexer"/></returns>
        public static IEnumerable<RecordIndexer> ReadCsv(string filename, char delimiter, int headerLines)
        {
            return ReadCsv(filename, delimiter, headerLines, Encoding.Default);
        }

        /// <summary>
        /// A fast way to read record by record a CSV file with a custom delimiter. The fields can be quoted.
        /// </summary>
        /// <param name="filename">The csv file to read</param>
        /// <param name="delimiter">The field delimiter.</param>
        /// <param name="encoding">The file <see cref="Encoding"/></param>
        /// <returns>An enumeration of <see cref="RecordIndexer"/></returns>
        public static IEnumerable<RecordIndexer> ReadCsv(string filename, char delimiter, Encoding encoding)
        {
            return ReadCsv(filename, delimiter, 0, encoding);
        }

        /// <summary>
        /// A fast way to read record by record a CSV file with a custom delimiter. The fields can be quoted.
        /// </summary>
        /// <param name="filename">The csv file to read</param>
        /// <param name="delimiter">The field delimiter.</param>
        /// <param name="encoding">The file <see cref="Encoding"/></param>
        /// <param name="headerLines">The number of header lines in the CSV file</param>
        /// <returns>An enumeration of <see cref="RecordIndexer"/></returns>
        public static IEnumerable<RecordIndexer> ReadCsv(string filename, char delimiter, int headerLines, Encoding encoding)
        {
            FileHelperAsyncEngine<RecordIndexer> engine = new FileHelperAsyncEngine<RecordIndexer>(encoding);
            ((DelimitedRecordOptions)engine.Options).Delimiter = delimiter.ToString();
            engine.Options.IgnoreFirstLines = headerLines;
            engine.BeginReadFile(filename);

            return engine;
        }

		#endregion
    }

#if ! (MINI)
	internal delegate object GetFieldValueCallback(object record);
#endif

}