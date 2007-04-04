#region "  © Copyright 2005-07 to Marcos Meli - http://www.marcosmeli.com.ar" 

// Errors, suggestions, contributions, send a mail to: marcos@filehelpers.com.

#endregion

using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Threading;
using Excel;

namespace FileHelpers.DataLink
{
	/// <summary><para>This class implements the <see cref="DataStorage"/> for Microsoft Excel Files.</para>
	/// <para><b>WARNING you need to have installed Microsoft Excel 2000 or newer to use this feature.</b></para>
	/// <para><b>To use this class you need to reference the FileHelpers.ExcelStorage.dll file.</b></para>
	/// </summary>
	/// <remmarks><b>This class is contained in the FileHelpers.ExcelStorage.dll and need the Interop.Office.dll and Interop.Excel.dll to work correctly.</b></remmarks>
	public sealed class ExcelStorage : DataStorage
	{
		#region "  Constructors  "

		/// <summary>Create a new ExcelStorage to work with the specified type</summary>
		/// <param name="recordType">The type of records.</param>
		public ExcelStorage(Type recordType):base(recordType)
		{
			// Temporary

//			if (RecordHasDateFields())
//				throw new NotImplementedException("For now the ExcelStorage don´t work with DateTime fields, sorry for the problems.");
		}

		/// <summary>Create a new ExcelStorage to work with the specified type</summary>
		/// <param name="recordType">The type of records.</param>
		/// <param name="startRow">The row of the first data cell. Begining in 1.</param>
		/// <param name="startCol">The column of the first data cell. Begining in 1.</param>
		public ExcelStorage(Type recordType, int startRow, int startCol) : this(recordType)
		{
			mStartColumn = startCol;
			mStartRow = startRow;
		}

		/// <summary>Create a new ExcelStorage to work with the specified type</summary>
		/// <param name="recordType">The type of records.</param>
		/// <param name="startRow">The row of the first data cell. Begining in 1.</param>
		/// <param name="startCol">The column of the first data cell. Begining in 1.</param>
		/// <param name="fileName">The file path to work with.</param>
		public ExcelStorage(Type recordType, string fileName, int startRow, int startCol) : this(recordType, startRow, startCol)
		{
			mFileName = fileName;
		}

		#endregion

		#region "  Private Fields  "

		private string mSheetName = String.Empty;
		private string mFileName = String.Empty;

		private int mStartRow = 1;
		private int mStartColumn = 1;

		private int mHeaderRows = 0;

		private ApplicationClass mApp;
		private Workbook mBook;
		private Worksheet mSheet;
		//private RecordInfo mRecordInfo;

		private string mTemplateFile = string.Empty;

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

		/// <summary>The numbers of header rows.</summary>
		public int HeaderRows
		{
			get { return mHeaderRows; }
			set { mHeaderRows = value; }
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

		/// <summary>
		/// Indicates the source xls file to be used as template when write data.
		/// </summary>
		public string TemplateFile
		{
			get { return mTemplateFile; }
			set { mTemplateFile = value; }
		}

		#endregion



		#region "  InitExcel  "

		private void InitExcel()
		{
			try
			{
				this.mApp = new ApplicationClass();
			}
			catch (System.Runtime.InteropServices.COMException ex)
			{
				if (ex.Message.IndexOf("00024500-0000-0000-C000-000000000046") >= 0)
					throw new ExcelBadUsageException("Excel 2000 or newer is not installed in this system.");
				else
					throw;
			}

			this.mBook = null;
			this.mSheet = null;
			this.mApp.Visible = false;
			this.mApp.AlertBeforeOverwriting = false;
			this.mApp.ScreenUpdating = false;
			this.mApp.DisplayAlerts = false;
			this.mApp.EnableAnimations = false;
			this.mApp.Interactive = false;
		}

		#endregion

		#region "  CloseAndCleanUp  "

		private void CloseAndCleanUp()
		{
			if (this.mSheet != null)
			{
				DisposeCOMObject(mSheet);
				mSheet = null;
			}

			if (this.mBook != null)
			{
				this.mBook.Close(false, mv, mv);
				DisposeCOMObject(mBook);
				this.mBook = null;
			}
			
			//Thread.Sleep(100);
			
			if (this.mApp != null)
			{
				//this.mApp.Interactive = true;
				this.mApp.Quit();
				DisposeCOMObject(mApp);
				this.mApp = null;
			}
			
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}
		private void DisposeCOMObject(object comObject)
		{
			while (System.Runtime.InteropServices.Marshal.ReleaseComObject(comObject) > 1)
			{}
		}

		#endregion

		private readonly Missing mv = Missing.Value;

		#region "  OpenWorkbook  "

		private void OpenWorkbook(string filename)
		{
			FileInfo info = new FileInfo(filename);
			if (info.Exists == false)
				throw new FileNotFoundException("Excel File '" + filename + "' not found.", filename);

			this.mBook = this.mApp.Workbooks.Open(info.FullName, mv, mv, mv, mv, mv, mv, mv, mv, mv, mv, mv, mv);

			if (this.mSheetName == null || mSheetName == string.Empty)
				this.mSheet = (Worksheet) this.mBook.ActiveSheet;
			else
			{
				try
				{
					this.mSheet = (Worksheet) this.mBook.Sheets[mSheetName];
				}
				catch
				{
					throw new ExcelBadUsageException("The sheet '" + mSheetName + "' was not found in the workbook.");
				}
			}

		}

		#endregion

		#region "  CreateWorkbook methods  "

		private void OpenOrCreateWorkbook(string filename)
		{
			if (File.Exists(filename))
				OpenWorkbook(filename);
			else
				CreateWorkbook();
		}

		private void CreateWorkbook()
		{
			this.mBook = this.mApp.Workbooks.Add(mv);
			this.mSheet = (Worksheet) this.mBook.ActiveSheet;
		}

		#endregion

		#region "  SaveWorkbook  "

		private void SaveWorkbook()
		{
			if (this.mBook != null)
				this.mBook.Save();
		}

		private void SaveWorkbook(string filename)
		{
			if (this.mBook != null)
				this.mBook.SaveAs(filename, mv, mv, mv, mv, mv, XlSaveAsAccessMode.xlNoChange, mv, mv, mv, mv);
		}

		#endregion

		#region "  CellAsString  "

		private string CellAsString(object row, object col)
		{
			if (this.mSheet == null)
			{
				return null;
			}
			Range r;
			r = (Range) this.mSheet.Cells[row, col];
			object res = r.Value;
			DisposeCOMObject(r);
			return Convert.ToString(res);
		}

		#endregion

		#region "  ColLeter  "

		string _ColLetter(int col /* 0 origin */) 
		{ 
			// col = [0...25] 
			if (col >= 0 && col <= 25) 
				return ((char)('A' + col)).ToString(); 
			return ""; 
		} 
		string ColLetter(int col /* 1 Origin */) 
		{ 
			if (col < 1 || col > 256) 
				throw new ExcelBadUsageException("Column out of range; must be between 1 and 256"); // Excel limits 
			col--; // make 0 origin 
			// good up to col ZZ 
			int col2 = (col / 26)-1; 
			int col1 = (col % 26); 
			return _ColLetter(col2) + _ColLetter(col1); 
		} 
		
		#endregion

		#region "  RowValues  "

		private object[] RowValues(int row, int startCol, int numberOfCols)
		{
			if (this.mSheet == null)
			{
				return null;
			}
			object[] res;

			Range r;
			if (numberOfCols == 1)
			{
				r = mSheet.get_Range(ColLetter(startCol) + row.ToString(), ColLetter(startCol + numberOfCols - 1) + row.ToString());
				res = new object[] {r.Value};
				//DisposeCOMObject(r);
			}
			else
			{
				r = mSheet.get_Range(ColLetter(startCol) + row.ToString(), ColLetter(startCol + numberOfCols - 1) + row.ToString());
				object[,] resTemp = (object[,]) r.Value2;
				//DisposeCOMObject(r);

				res = new object[numberOfCols];

				for (int i = 1; i <= numberOfCols; i++)
				{
					res[i - 1] = resTemp[1, i];
				}

			}

			return res;
		}

		private void WriteRowValues(object[] values, int row, int startCol)
		{
			if (this.mSheet == null)
				return;

			Range r = this.mSheet.get_Range(ColLetter(startCol) + row.ToString(), ColLetter(startCol + values.Length - 1) + row.ToString());

			r.Value2 = values;
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

				this.InitExcel();

				if (mOverrideFile && File.Exists(mFileName))
					File.Delete(mFileName);

				if (mTemplateFile != string.Empty)
				{
					if (File.Exists(mTemplateFile) == false)
						throw new ExcelBadUsageException("Template file not found: '" + mTemplateFile + "'");

					if (mTemplateFile != mFileName)
						File.Copy(mTemplateFile, mFileName, true);
				}
				
				this.OpenOrCreateWorkbook(this.mFileName);

				for (int i = 0; i < records.Length; i++)
				{
					recordNumber++;
					Notify(mNotifyHandler, mProgressMode, recordNumber, records.Length);

					WriteRowValues(RecordToValues(records[i]), mStartRow + i, mStartColumn);
				}

				SaveWorkbook(this.mFileName);
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
			if (this.mFileName == String.Empty)
				throw new ExcelBadUsageException("You need to specify the WorkBookFile of the ExcelDataLink.");


			ArrayList res = new ArrayList();

			try
			{
				int cRow = mStartRow;

				int recordNumber = 0;
				Notify(mNotifyHandler, mProgressMode, 0, -1);

				object[] colValues = new object[RecordFieldCount];

				this.InitExcel();
				this.OpenWorkbook(this.mFileName);

				while (CellAsString(cRow, mStartColumn) != String.Empty)
				{
					try
					{
						recordNumber++;
						Notify(mNotifyHandler, mProgressMode, recordNumber, -1);

						colValues = RowValues(cRow, mStartColumn, RecordFieldCount);

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
								AddError(cRow, ex, ColumnsToValues(colValues));
								break;
						}
					}
					finally
					{
						cRow++;
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

		private string ColumnsToValues(object[] values)
		{
			if (values == null || values.Length == 0)
				return string.Empty;

			string res = string.Empty;
			if (values[0] != null)
				res = values[0].ToString();

			for(int i = 1; i < values.Length; i++)
			{
				if (values[i] == null)
					res += ",";
				else
					res += "," + values[i].ToString();
			}
			
			return res;
		}
	}
}