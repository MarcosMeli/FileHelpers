#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace FileHelpers.DataLink
{
	/// <summary><para>This class implements the <see cref="DataStorage"/> for Microsoft Excel Files using a OleDbConnection</para></summary>
	public sealed class ExcelStorageOleDb : DataStorage
	{
        static string CreateConnectionString(string file, bool hasHeaders)
        {
            string mConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source={0};Extended Properties=\"Excel 8.0;{1}\"";
            string extProps = string.Empty;

            if (hasHeaders)
                extProps += " HDR=YES;";
            else
                extProps += " HDR=NO;";


            return String.Format(mConnectionString, file, extProps);
        }



		#region "  Constructors  "

		/// <summary>Create a new ExcelStorage to work with the specified type</summary>
		/// <param name="recordType">The type of records.</param>
		public ExcelStorageOleDb(Type recordType):base(recordType)
		{}

		/// <summary>Create a new ExcelStorage to work with the specified type</summary>
		/// <param name="recordType">The type of records.</param>
		/// <param name="startRow">The row of the first data cell. Begining in 1.</param>
		/// <param name="startCol">The column of the first data cell. Begining in 1.</param>
		public ExcelStorageOleDb(Type recordType, int startRow, int startCol) : this(recordType)
		{
			mStartColumn = startCol;
			mStartRow = startRow;
		}

		/// <summary>Create a new ExcelStorage to work with the specified type</summary>
		/// <param name="recordType">The type of records.</param>
		/// <param name="startRow">The row of the first data cell. Begining in 1.</param>
		/// <param name="startCol">The column of the first data cell. Begining in 1.</param>
		/// <param name="fileName">The file path to work with.</param>
        public ExcelStorageOleDb(Type recordType, string fileName, int startRow, int startCol)
            : this(recordType, startRow, startCol)
		{
			mFileName = fileName;
		}

		#endregion

		#region "  Private Fields  "

		private string mSheetName = String.Empty;
		private string mFileName = String.Empty;

//        private static string mConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source={0};Extended Properties=\"Excel 8.0;Hdr=YES;\"";

		private int mStartRow = 1;
		private int mStartColumn = 1;

        private bool mHasHeaderRow = false;

		private DataTable mDtExcel;
        private OleDbConnection mCnExcel;
        private OleDbDataAdapter mDaExcel;
        private DataSet mDsExcel;

		#endregion

		#region "  Public Properties  "

		/// <summary>The Start Row where is the data. Starting at 1.</summary>
		public int StartRow
		{
			get { return mStartRow; }
			set { mStartRow = value; }
		}

		/// <summary>The Start Column where is the data. Starting at 1.</summary>
		public int StartColumn
		{
			get { return mStartColumn; }
			set { mStartColumn = value; }
		}

        /// <summary>
        /// Indicates if in the StartColumn and StartRow has headers or not.
        /// </summary>
		public bool HasHeaderRow
		{
            get { return mHasHeaderRow; }
            set { mHasHeaderRow= value; }
		}

		/// <summary>The Excel File Name.</summary>
		public string FileName
		{
			get { return mFileName; }
			set { mFileName = value; }
		}

		/// <summary>The Excel Sheet Name, if empty means the current worksheet in the file.</summary>
		public string SheetName
		{
			get { return mSheetName; }
			set { mSheetName = value; }
		}

		private bool mOverrideFile = true;

		/// <summary>Indicates what the Storage does if the file exist.</summary>
		public bool OverrideFile
		{
			get { return mOverrideFile; }
			set { mOverrideFile = value; }
		}
        ///// <summary>Indicates the Connection String to Open the Excel File.</summary>
        //public string ConnectionString
        //{
        //    get { return mConnectionString; }
        //    set { mConnectionString = value; }
        //}


		#endregion

		#region "  OpenConnection  "

		private void OpenConnection()
		{
			try
			{
                if (mCnExcel != null && mCnExcel.State == ConnectionState.Open)
                {
                    mCnExcel.Close();
#if NET_2_0
                mCnExcel.Dispose();
#endif
                }

                mCnExcel= new OleDbConnection(CreateConnectionString(mFileName, HasHeaderRow));
                mCnExcel.Open();
                mDtExcel = new DataTable();

			}
			catch
			{
                throw;
			}

		}

		#endregion

		#region "  CloseAndCleanUp  "

		private void CloseAndCleanUp()
		{
            if (this.mCnExcel != null && this.mCnExcel.State == ConnectionState.Open)
            {
                this.mCnExcel.Close();
                //mCnExcel.Dispose();
            }
		}

		#endregion

		#region "  OpenWorkbook  "

		private void OpenWorkbook()
		{

            FileInfo info = new FileInfo(mFileName);
            if (info.Exists == false)
                throw new FileNotFoundException("Excel File '" + mFileName + "' not found.", mFileName);


            this.OpenConnection();


			if (this.mSheetName == null || this.mSheetName == string.Empty)
            {
                string sheet;   
            	sheet = GetFirstSheet(mCnExcel);
            	this.mSheetName = sheet;
			}

			try
			{
                string sheet = this.SheetName + (this.mSheetName.EndsWith("$") ? "" : "$") ;
                string command = String.Format("SELECT * FROM [{0}]", sheet);
                OleDbCommand cm = new OleDbCommand(command, mCnExcel);
                mDaExcel = new OleDbDataAdapter(cm);
                mDsExcel = new DataSet();
                mDaExcel.Fill(mDsExcel);
                mDtExcel = mDsExcel.Tables[0];
			}
			catch
			{
				throw new BadUsageException("The sheet '" + mSheetName + "' was not found in the workbook.");
			}

		}

		private static string GetFirstSheet(OleDbConnection conn)
		{
#if NET_2_0
			DataTable dt = conn.GetSchema("Tables");
#else
			DataTable dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
#endif
	
			if (dt != null && dt.Rows.Count > 0)
				return dt.Rows[0]["TABLE_NAME"].ToString();
			else
				throw new BadUsageException("A Valid sheet was not found in the workbook.");
		}

		#endregion

		#region "  CreateWorkbook methods  "

		private void OpenOrCreateWorkbook(string filename)
		{
            if (!File.Exists(filename))
                CreateWorkbook();

            OpenWorkbook();

		}

		private void CreateWorkbook()
		{
            if (mCnExcel != null && mCnExcel.State == ConnectionState.Open)
            {
                mCnExcel.Close();
#if NET_2_0
                mCnExcel.Dispose();
#endif
            }

            //Delete the file if Exists
            if (File.Exists(this.mFileName) && this.mOverrideFile)
            {
                File.Delete(this.mFileName);
            }

            this.OpenConnection();

            string cmCreate = "";
			if (this.mSheetName == null || mSheetName == string.Empty)
            {
                this.mSheetName = "Sheet1$";
            }

      
            OleDbCommand cm = new OleDbCommand();
            cm.Connection = mCnExcel;
            cmCreate = string.Format("CREATE TABLE [{0}] (", this.mSheetName.Replace("$", ""));
            foreach( FieldInfo field in RecordType.GetFields() )
            {
                cmCreate += field.Name + " char(255)," ;
            }
            if( cmCreate.EndsWith(",") )
            {
                cmCreate = cmCreate.Substring(0,cmCreate.Length-1);
            }
            cmCreate += ")";

            cm.CommandText = cmCreate;
            cm.ExecuteNonQuery();

            mCnExcel.Close();
      
		}

		#endregion

		#region "  SaveWorkbook  "

        /// <summary>
        /// Update Rows in Workbook and Close the File
        /// </summary>
		private void SaveWorkbook()
        {
            #region Build and Add the Insert Command to DataAdapter

            string cmdIns = string.Format("INSERT INTO [{0}] (", this.mSheetName + (this.mSheetName.EndsWith("$") ? "" : "$") );
            string values = "";
            foreach( DataColumn col in mDtExcel.Columns)
            {
                cmdIns += col.ColumnName + ",";
                values += "?,";
            }
            cmdIns = cmdIns.Substring(0,cmdIns.Length-1); //Clean the ","
            values = values.Substring(0,values.Length-1); //Clean the ","
            cmdIns += ") VALUES( " + values + ")";

            OleDbCommand cmd = new OleDbCommand( cmdIns, mCnExcel);
            mDaExcel.InsertCommand = cmd;
            foreach (DataColumn col in mDtExcel.Columns)
            {
#if NET_2_0
                cmd.Parameters.AddWithValue("@" + col.ColumnName, col.DataType).SourceColumn = col.ColumnName;
#else
                cmd.Parameters.Add("@" + col.ColumnName, col.DataType).SourceColumn = col.ColumnName;
#endif
            }
            #endregion


            mDaExcel.Update(mDtExcel);

            CloseAndCleanUp();
		}

		#endregion


		#region "  RowValues  "

		private object[] RowValues(int row, int startCol, int numberOfCols)
		{
			if (this.mDtExcel == null)
			{
				return null;
			}
			object[] res;

            if (numberOfCols == 1)
            {
                if (startCol <= mDtExcel.Columns.Count)
                {
                    res = new object[] { mDtExcel.Rows[row][startCol-1] };
                }
                else
                {
                    throw new BadUsageException("The Start Column is Greater Than the Columns Count in " + this.mSheetName);
                }
            }
            else
            {
            
                res = new object[numberOfCols];
                int counter = 0;
                int colPos = startCol - 1; 

                //If the numbers of selected columns is Greater Than columns in DataTable
                while (counter < res.Length && colPos < this.mDtExcel.Columns.Count)
                {
                    res[counter] = mDtExcel.Rows[row][colPos];
                    counter++;
                    colPos++;
                }
            }

			return res;
		}

		private void WriteRowValues(object[] values, int row, int startCol)
		{
			if (this.mDtExcel == null)
				return;

            DataRow dtRow;
            if (row > mDtExcel.Rows.Count - 1)
            {
                dtRow = mDtExcel.NewRow();
                mDtExcel.Rows.Add(dtRow);
            }
            else
            {
                dtRow = mDtExcel.Rows[row];
            }

            int colPos = startCol-1;
            int i=0;
            while (colPos < mDtExcel.Columns.Count && i < values.Length)
            {
                dtRow[colPos] = values[i];
                colPos++;
                i++;
            }
            
		}

		#endregion

		#region "  InsertRecords  "

		/// <summary>Insert all the records in the specified Excel File.</summary>
		/// <param name="records">The records to insert.</param>
		public override void InsertRecords(object[] records)
		{
			if (records == null || records.Length == 0)
				return;

			try
			{
				int recordNumber = 0;
				Notify(mNotifyHandler, mProgressMode, 0, records.Length);

				this.OpenOrCreateWorkbook(this.mFileName);

                mDtExcel = mDsExcel.Tables[0].Clone();

                //Verify Properties Limits.
                ValidatePropertiesForInsert(records);

				for (int row = mStartRow-1; row < records.Length; row++)
				{
					recordNumber++;
					Notify(mNotifyHandler, mProgressMode, recordNumber, records.Length);

                    WriteRowValues(RecordToValues(records[row]), row, mStartColumn);
				}

				SaveWorkbook();
			}
			catch
			{
				throw;
			}
			finally
			{
				CloseAndCleanUp();
			}

		}

		#endregion

		#region "  ExtractRecords  "

		/// <summary>Returns the records extracted from Excel file.</summary>
		/// <returns>The extracted records.</returns>
		public override object[] ExtractRecords()
		{

			ArrayList res = new ArrayList();

			try
			{

				Notify(mNotifyHandler, mProgressMode, 0, -1);

				object[] colValues = new object[mRecordInfo.mFieldCount];

				this.OpenWorkbook();

                //Verify Properties Limits.
                ValidatePropertiesForExtract();

                //mStartRow-1 because rows start at 0, and the user
                //can be assign StartRow =2 
                for (int recordNumber = mStartRow-1; recordNumber < mDtExcel.Rows.Count; recordNumber++)
                {
                        try
                        {
                            Notify(mNotifyHandler, mProgressMode, recordNumber, -1);

                            colValues = RowValues(recordNumber, mStartColumn, mRecordInfo.mFieldCount);

                            object record = ValuesToRecord(colValues);
                            res.Add(record);

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
                                    AddError(recordNumber+1, ex, string.Empty);
                                    break;
                            }
                        }

                }

			}
			catch
			{
				throw;
			}
			finally
			{
				CloseAndCleanUp();
			}

			return (object[]) res.ToArray(this.RecordType);
		}

		#endregion


        #region Validation
        private void ValidatePropertiesForExtract()
        {
            if (this.mFileName == String.Empty)
                throw new BadUsageException("You need to specify the WorkBookFile of the ExcelDataLink.");

            if (this.mStartRow <= 0 )
                throw new BadUsageException("The StartRow Property is Invalid. Must be Greater or Equal Than 1.");

            if (this.mStartRow > mDtExcel.Rows.Count)
                throw new BadUsageException("The StartRow Property is Invalid. Must be Less or Equal to Worksheet row's count.");

            if (this.mStartColumn <= 0)
                throw new BadUsageException("The StartColumn Property is Invalid. Must be Greater or Equal Than 1.");

            if (this.mStartColumn > mDtExcel.Columns.Count)
                throw new BadUsageException("The StartColumn Property is Invalid. Must be Less or Equal To Worksheet Column's count.");

        }

        private void ValidatePropertiesForInsert(object[] records)
        {
            if (this.mFileName == String.Empty)
                throw new BadUsageException("You need to specify the WorkBookFile of the ExcelDataLink.");

            if (this.mStartRow <= 0)
                throw new BadUsageException("The StartRow Property is Invalid. Must be Greater or Equal Than 1.");

            if (this.mStartRow > records.Length)
                throw new BadUsageException("The StartRow Property is Invalid. Must be Less or Equal to Worksheet row's count.");

            if (this.mStartColumn <= 0)
                throw new BadUsageException("The StartColumn Property is Invalid. Must be Greater or Equal Than 1.");

            if (this.mStartColumn > mRecordInfo.mFieldCount)
                throw new BadUsageException("The StartColumn Property is Invalid. Must be Less or Equal To Record Type Propertie's count.");

        }

        #endregion


        /// <summary>
        /// An useful method to direct extract a DataTable from an Excel File without need to instanciate anything.
        /// </summary>
        /// <param name="file">The Excel file to read.</param>
        /// <param name="row">The initial row (the first is 1)</param>
        /// <param name="col">The initial column (the first is 1)</param>
        /// <param name="hasHeader">Indicates is there ir a header row.</param>
        /// <returns>The DataTable generated reading the excel file at the specified position.</returns>
		public static DataTable ExtractDataTable(string file, int row, int col, bool hasHeader)
		{

			OleDbConnection connExcel;
			//private OleDbDataAdapter mDaExcel;

            connExcel = new OleDbConnection(CreateConnectionString(file, hasHeader));
			connExcel.Open();
			DataTable res = new DataTable();

			string sheetName = GetFirstSheet(connExcel);

			string sheet = sheetName + (sheetName.EndsWith("$") ? "" : "$") ;
			string command = String.Format("SELECT * FROM [{0}]", sheet);

			OleDbCommand cm = new OleDbCommand(command, connExcel);
			OleDbDataAdapter da = new OleDbDataAdapter(cm);
			da.Fill(res);

			connExcel.Close();
			return res;

		}

    }
}