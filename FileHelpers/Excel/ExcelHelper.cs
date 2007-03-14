#region "  © Copyright 2005-06 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Data;
using System.Data.OleDb;
using System.Text;

namespace FileHelpers.DataLink
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public abstract class ExcelHelper
	{

		protected string CreateConnectionString(string file)
		{
			string extProps = string.Empty;

			extProps += ExtraProps();

			if (this.HasHeaders)
				extProps += " HDR=YES;";
			else
				extProps += " HDR=NO;";


			return String.Format(mConnectionString, file, extProps);
		}

		protected virtual string ExtraProps()
		{
			return string.Empty;
		}

		#region "  Constructors  "

		protected ExcelHelper()
		{}

		protected ExcelHelper(int startRow, int startCol)
		{
			mStartColumn = startCol;
			mStartRow = startRow;
		}

		#endregion

		#region "  Private Fields  "

		private string mSheetName = String.Empty;

		private static string mConnectionString = "Provider=Microsoft.Jet.OleDb.4.0;Data Source={0};Extended Properties=\"Excel 8.0;{1}\"";

		private int mStartRow = 1;
		private int mStartColumn = 1;

		private bool mHasHeaders = true;

//		private DataTable mDtExcel;
//        private OleDbConnection mCnExcel;
//        private OleDbDataAdapter mDaExcel;
//        private DataSet mDsExcel;

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

		/// <summary>Indicates if the sheet has a header row.</summary>
		public bool HasHeaders
		{
			get { return mHasHeaders; }
			set { mHasHeaders= value; }
		}

		/// <summary>The Excel Sheet Name, if empty means the current worksheet in the file.</summary>
		public string SheetName
		{
			get { return mSheetName; }
			set { mSheetName = value; }
		}

		//private bool mOverrideFile = true;

//		/// <summary>Indicates what the Storage does if the file exist.</summary>
//		public bool OverrideFile
//		{
//			get { return mOverrideFile; }
//			set { mOverrideFile = value; }
//		}

//        /// <summary>Indicates the Connection String to Open the Excel File.</summary>
//        public string ConnectionString
//        {
//            get { return mConnectionString; }
//            set { mConnectionString = value; }
//        }


		#endregion

//		#region "  OpenConnection  "
//
//		private void OpenConnection()
//		{
//			try
//			{
//                if (mCnExcel != null && mCnExcel.State == ConnectionState.Open)
//                {
//                    mCnExcel.Close();
//#if NET_2_0
//				    mCnExcel.Dispose();
//#endif
//                }
//
//                mCnExcel= new OleDbConnection(String.Format(mConnectionString, mFileName));
//                mCnExcel.Open();
//                mDtExcel = new DataTable();
//
//			}
//			catch
//			{
//                throw;
//			}
//
//		}
//
//		#endregion
//
//		#region "  CloseAndCleanUp  "
//
//		private void CloseAndCleanUp()
//		{
//            if (this.mCnExcel != null && this.mCnExcel.State == ConnectionState.Open)
//            {
//                this.mCnExcel.Close();
//                //mCnExcel.Dispose();
//            }
//		}
//
//		#endregion
//
//		#region "  OpenWorkbook  "
//
//		private void OpenWorkbook()
//		{
//
//            FileInfo info = new FileInfo(mFileName);
//            if (info.Exists == false)
//                throw new FileNotFoundException("Excel File '" + mFileName + "' not found.", mFileName);
//
//
//            this.OpenConnection();
//
//
//			if (this.mSheetName == null || this.mSheetName == string.Empty)
//            {
//
//string sheet;
//            	sheet = GetFirstSheet(mCnExcel);
//            	this.mSheetName = sheet;
//			}
//
//			try
//			{
//                string sheet = this.SheetName + (this.mSheetName.EndsWith("$") ? "" : "$") ;
//                string command = String.Format("SELECT * FROM [{0}]", sheet);
//                OleDbCommand cm = new OleDbCommand(command, mCnExcel);
//                mDaExcel = new OleDbDataAdapter(cm);
//                mDsExcel = new DataSet();
//                mDaExcel.Fill(mDsExcel);
//                mDtExcel = mDsExcel.Tables[0];
//			}
//			catch
//			{
//				throw new BadUsageException("The sheet '" + mSheetName + "' was not found in the workbook.");
//			}
//
//		}
//
		internal static string GetFirstSheet(OleDbConnection conn)
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

//		#endregion
//
//		#region "  CreateWorkbook methods  "
//
//		private void OpenOrCreateWorkbook(string filename)
//		{
//            if (!File.Exists(filename))
//                CreateWorkbook();
//
//            OpenWorkbook();
//
//		}
//
//		private void CreateWorkbook()
//		{
//            if (mCnExcel != null && mCnExcel.State == ConnectionState.Open)
//            {
//                mCnExcel.Close();
//#if NET_2_0
//                mCnExcel.Dispose();
//#endif
//            }
//
//            //Delete the file if Exists
//            if (File.Exists(this.mFileName) && this.mOverrideFile)
//            {
//                File.Delete(this.mFileName);
//            }
//
//            this.OpenConnection();
//
//            string cmCreate = "";
//			if (this.mSheetName == null || mSheetName == string.Empty)
//            {
//                this.mSheetName = "Sheet1$";
//            }
//
//      
//            OleDbCommand cm = new OleDbCommand();
//            cm.Connection = mCnExcel;
//            cmCreate = string.Format("CREATE TABLE [{0}] (", this.mSheetName.Replace("$", ""));
//            foreach( FieldInfo field in RecordType.GetFields() )
//            {
//                cmCreate += field.Name + " char(255)," ;
//            }
//            if( cmCreate.EndsWith(",") )
//            {
//                cmCreate = cmCreate.Substring(0,cmCreate.Length-1);
//            }
//            cmCreate += ")";
//
//            cm.CommandText = cmCreate;
//            cm.ExecuteNonQuery();
//
//            mCnExcel.Close();
//      
//		}
//
//		#endregion

//		#region "  SaveWorkbook  "
//
//        /// <summary>
//        /// Update Rows in Workbook and Close the File
//        /// </summary>
//		private void SaveWorkbook()
//        {
//            #region Build and Add the Insert Command to DataAdapter
//
//            string cmdIns = string.Format("INSERT INTO [{0}] (", this.mSheetName + (this.mSheetName.EndsWith("$") ? "" : "$") );
//            string values = "";
//            foreach( DataColumn col in mDtExcel.Columns)
//            {
//                cmdIns += col.ColumnName + ",";
//                values += "?,";
//            }
//            cmdIns = cmdIns.Substring(0,cmdIns.Length-1); //Clean the ","
//            values = values.Substring(0,values.Length-1); //Clean the ","
//            cmdIns += ") VALUES( " + values + ")";
//
//            OleDbCommand cmd = new OleDbCommand( cmdIns, mCnExcel);
//            mDaExcel.InsertCommand = cmd;
//            foreach (DataColumn col in mDtExcel.Columns)
//            {
//                cmd.Parameters.Add("@" + col.ColumnName, col.DataType).SourceColumn = col.ColumnName;
//            }
//            #endregion
//
//
//            mDaExcel.Update(mDtExcel);
//
//            CloseAndCleanUp();
//		}
//
//		#endregion
//
//
//		#region "  RowValues  "
//
//		private object[] RowValues(int row, int startCol, int numberOfCols)
//		{
//			if (this.mDtExcel == null)
//			{
//				return null;
//			}
//			object[] res;
//
//            if (numberOfCols == 1)
//            {
//                if (startCol < mDtExcel.Columns.Count)
//                {
//                    res = new object[] { mDtExcel.Rows[row][startCol] };
//                }
//                else
//                {
//                    throw new BadUsageException("The Start Column is Greater Than the Columns Count in " + this.mSheetName);
//                }
//            }
//            else
//            {
//            
//                res = new object[numberOfCols];
//                int counter = 0;
//                int colPos = startCol - 1; 
//
//                //If the numbers of selected columns is Greater Than columns in DataTable
//                while (counter < res.Length && colPos < this.mDtExcel.Columns.Count)
//                {
//                    res[counter] = mDtExcel.Rows[row][colPos];
//                    counter++;
//                    colPos++;
//                }
//            }
//
//			return res;
//		}
//
//		private void WriteRowValues(object[] values, int row, int startCol)
//		{
//			if (this.mDtExcel == null)
//				return;
//
//            DataRow dtRow;
//            if (row > mDtExcel.Rows.Count - 1)
//            {
//                dtRow = mDtExcel.NewRow();
//                mDtExcel.Rows.Add(dtRow);
//            }
//            else
//            {
//                dtRow = mDtExcel.Rows[row];
//            }
//
//            int colPos = startCol-1;
//            int i=0;
//            while (colPos < mDtExcel.Columns.Count && i < values.Length)
//            {
//                dtRow[colPos] = values[i];
//                colPos++;
//                i++;
//            }
//            
//		}
//
//		#endregion

//		#region "  InsertRecords  "
//
//		/// <summary>Insert all the records in the specified Excel File.</summary>
//		/// <param name="records">The records to insert.</param>
//		public void InsertRecords(object[] records)
//		{
//			if (records == null || records.Length == 0)
//				return;
//
//			try
//			{
//				int recordNumber = 0;
//				Notify(mNotifyHandler, mProgressMode, 0, records.Length);
//
//				this.OpenOrCreateWorkbook(this.mFileName);
//
//                mDtExcel = mDsExcel.Tables[0].Clone();
//
//                //Verify Properties Limits.
//                ValidatePropertiesForInsert(records);
//
//				for (int row = mStartRow-1; row < records.Length; row++)
//				{
//					recordNumber++;
//					Notify(mNotifyHandler, mProgressMode, recordNumber, records.Length);
//
//                    WriteRowValues(RecordToValues(records[row]), row, mStartColumn);
//				}
//
//				SaveWorkbook();
//			}
//			catch
//			{
//				throw;
//			}
//			finally
//			{
//				CloseAndCleanUp();
//			}
//
//		}
//
//		#endregion
//
//
//        #region Validation
//        private void ValidatePropertiesForExtract()
//        {
//            if (this.mFileName == String.Empty)
//                throw new BadUsageException("You need to specify the WorkBookFile of the ExcelDataLink.");
//
//            if (this.mStartRow <= 0 )
//                throw new BadUsageException("The StartRow Property is Invalid. Must be Greater or Equal Than 1.");
//
//            if (this.mStartRow > mDtExcel.Rows.Count)
//                throw new BadUsageException("The StartRow Property is Invalid. Must be Less or Equal to Worksheet row's count.");
//
//            if (this.mStartColumn <= 0)
//                throw new BadUsageException("The StartColumn Property is Invalid. Must be Greater or Equal Than 1.");
//
//            if (this.mStartColumn > mDtExcel.Columns.Count)
//                throw new BadUsageException("The StartColumn Property is Invalid. Must be Less or Equal To Worksheet Column's count.");
//
//        }
//
//        private void ValidatePropertiesForInsert(object[] records)
//        {
//            if (this.mFileName == String.Empty)
//                throw new BadUsageException("You need to specify the WorkBookFile of the ExcelDataLink.");
//
//            if (this.mStartRow <= 0)
//                throw new BadUsageException("The StartRow Property is Invalid. Must be Greater or Equal Than 1.");
//
//            if (this.mStartRow > records.Length)
//                throw new BadUsageException("The StartRow Property is Invalid. Must be Less or Equal to Worksheet row's count.");
//
//            if (this.mStartColumn <= 0)
//                throw new BadUsageException("The StartColumn Property is Invalid. Must be Greater or Equal Than 1.");
//
//            if (this.mStartColumn > mRecordInfo.mFieldCount)
//                throw new BadUsageException("The StartColumn Property is Invalid. Must be Less or Equal To Record Type Propertie's count.");
//
//        }
//
//        #endregion
//
//
//		public DataTable ExtractDataTable(string file, int row, int col)
//		{
//
//			OleDbConnection connExcel;
//			//private OleDbDataAdapter mDaExcel;
//				
//			connExcel= new OleDbConnection(String.Format(mConnectionString, file));
//			connExcel.Open();
//			DataTable res = new DataTable();
//
//			string sheetName = GetFirstSheet(connExcel);
//
//			string sheet = sheetName + (sheetName.EndsWith("$") ? "" : "$") ;
//			string command = String.Format("SELECT * FROM [{0}]", sheet);
//
//			OleDbCommand cm = new OleDbCommand(command, connExcel);
//			OleDbDataAdapter da = new OleDbDataAdapter(cm);
//			da.Fill(res);
//
//			connExcel.Close();
//			return res;
//
//		}
//
	}
}