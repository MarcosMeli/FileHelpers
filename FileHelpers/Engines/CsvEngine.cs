using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using FileHelpers.Dynamic;
using FileHelpers.Options;


namespace FileHelpers
{
    /// <summary>A class to read generic CSV files delimited for any char.</summary>
    [DebuggerDisplay("CsvEngine. ErrorMode: {ErrorManager.ErrorMode.ToString()}. Encoding: {Encoding.EncodingName}")]
    public sealed class CsvEngine : FileHelperEngine
    {
        #region "  Static Methods  "

        /// <summary>Reads a CSV File and return their contents as DataTable (The file must have the field names in the first row)</summary>
        /// <param name="delimiter">The delimiter for each field</param>
        /// <param name="filename">The file to read.</param>
        /// <returns>The contents of the file as a DataTable</returns>
        public static DataTable CsvToDataTable(string filename, char delimiter)
        {
            return CsvToDataTable(filename, "RecorMappingClass", delimiter, true);
        }

        /// <summary>
        /// Reads a CSV File and return their contents as DataTable
        /// (The file must have the field names in the first
        /// row)
        /// </summary>
        /// <param name="classname">The name of the record class</param>
        /// <param name="delimiter">The delimiter for each field</param>
        /// <param name="filename">The file to read.</param>
        /// <returns>The contents of the file as a DataTable</returns>
        public static DataTable CsvToDataTable(string filename, string classname, char delimiter)
        {
            return CsvToDataTable(filename, classname, delimiter, true);
        }


        /// <summary>
        /// Reads a CSV File and return their contents as DataTable
        /// </summary>
        /// <param name="classname">The name of the record class</param>
        /// <param name="delimiter">The delimiter for each field</param>
        /// <param name="filename">The file to read.</param>
        /// <param name="hasHeader">Indicates if the file contains a header with the field names.</param>
        /// <returns>The contents of the file as a DataTable</returns>
        public static DataTable CsvToDataTable(string filename, string classname, char delimiter, bool hasHeader)
        {
            var options = new CsvOptions(classname, delimiter, filename);
            if (hasHeader == false)
                options.HeaderLines = 0;
            return CsvToDataTable(filename, options);
        }

        /// <summary>
        /// Reads a CSV File and return their contents as DataTable
        /// </summary>
        /// <param name="classname">The name of the record class</param>
        /// <param name="delimiter">The delimiter for each field</param>
        /// <param name="filename">The file to read.</param>
        /// <param name="hasHeader">Indicates if the file contains a header with the field names.</param>
        /// <param name="ignoreEmptyLines">Indicates if blank lines in the file should not be included in the returned DataTable</param>
        /// <returns>The contents of the file as a DataTable</returns>
        public static DataTable CsvToDataTable(string filename,
            string classname,
            char delimiter,
            bool hasHeader,
            bool ignoreEmptyLines)
        {
            var options = new CsvOptions(classname, delimiter, filename);
            if (hasHeader == false)
                options.HeaderLines = 0;
            options.IgnoreEmptyLines = ignoreEmptyLines;
            return CsvToDataTable(filename, options);
        }

        /// <summary>
        /// Reads a CSV File and return their contents as
        /// DataTable
        /// </summary>
        /// <param name="filename">The file to read.</param>
        /// <param name="options">The options used to create the record mapping class.</param>
        /// <returns>The contents of the file as a DataTable</returns>
        public static DataTable CsvToDataTable(string filename, CsvOptions options)
        {
            var engine = new CsvEngine(options);
            return engine.ReadFileAsDT(filename);
        }


        /// <summary>
        /// Simply dumps the DataTable contents to a delimited file
        /// using a ',' as delimiter.
        /// </summary>
        /// <param name="dt">The source Data Table</param>
        /// <param name="filename">The destination file.</param>
        public static void DataTableToCsv(DataTable dt, string filename)
        {
            DataTableToCsv(dt, filename, new CsvOptions("Tempo1", ',', dt.Columns.Count));
        }


        /// <summary>
        /// Simply dumps the DataTable contents to a delimited file using a ','
        /// as delimiter.
        /// </summary>
        /// <param name="dt">The source Data Table</param>
        /// <param name="filename">The destination file.</param>
        /// <param name="delimiter">The delimiter to be used on the file</param>
        public static void DataTableToCsv(DataTable dt, string filename, char delimiter)
        {
            DataTableToCsv(dt, filename, new CsvOptions("Tempo1", delimiter, dt.Columns.Count));
        }


        /// <summary>
        /// Simply dumps the DataTable contents to a delimited file. Only
        /// allows to set the delimiter.
        /// If options.IncludeHeaderNames = true, add to the very first row of the CSV, the name of the columns
        /// taken from DataTable
        /// </summary>
        /// <param name="dt">The source Data Table</param>
        /// <param name="filename">The destination file.</param>
        /// <param name="options">The options used to write the file</param>
        public static void DataTableToCsv(DataTable dt, string filename, CsvOptions options)
        {
            using (var fs = new StreamWriter(filename, false, options.Encoding, DefaultWriteBufferSize)) {
		        // output header
		        if( options.IncludeHeaderNames ){
			        for (int i = 0; i < dt.Columns.Count; i++)
			        {
			        	if (i > 0)
		                {
		                    fs.Write(options.Delimiter);
		                }
			        	fs.Write(options.ValueToString(dt.Columns[i].ColumnName));
			        }
			        fs.Write(StringHelper.NewLine);
		        }
        		        		
        		//output rows
                foreach (DataRow dr in dt.Rows) {
                    object[] fields = dr.ItemArray;

                    for (int i = 0; i < fields.Length; i++) {
                        if (i > 0)
                            fs.Write(options.Delimiter);

                        fs.Write(options.ValueToString(fields[i]));
                    }
                    fs.Write(StringHelper.NewLine);
                }
                fs.Close();
            }
        }
        
        
//		private static string ObjectString(CsvOptions options, object o)
//		{
//		}

        #endregion

        #region "  Constructor  "

        /// <summary>
        /// <para>Create a CsvEngine using the specified sample file with their headers.</para>
        /// <para>With this constructor will ignore the first line of the file. Use CsvOptions overload.</para>
        /// </summary>
        /// <param name="className">The name of the record class</param>
        /// <param name="delimiter">The delimiter for each field</param>
        /// <param name="sampleFile">A sample file with a header that contains the names of the fields.</param>
        public CsvEngine(string className, char delimiter, string sampleFile)
            : this(new CsvOptions(className, delimiter, sampleFile)) {}

        /// <summary>
        /// <para>Create a CsvEngine using the specified number of fields.</para>
        /// <para>With this constructor will ignore the first line of the file. Use CsvOptions overload.</para>
        /// </summary>
        /// <param name="className">The name of the record class</param>
        /// <param name="delimiter">The delimiter for each field</param>
        /// <param name="numberOfFields">The number of fields of each record</param>
        public CsvEngine(string className, char delimiter, int numberOfFields)
            : this(new CsvOptions(className, delimiter, numberOfFields)) {}

        /// <summary>
        /// Create a CsvEngine using the specified sample file with
        /// their headers.
        /// </summary>
        /// <param name="options">The options used to create the record mapping class.</param>
        public CsvEngine(CsvOptions options)
            : base(GetMappingClass(options)) {}

        #endregion

        private static Type GetMappingClass(CsvOptions options)
        {
            var cb = new CsvClassBuilder(options);
            return cb.CreateRecordClass();
        }
    }
}