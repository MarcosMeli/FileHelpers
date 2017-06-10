using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using FileHelpers.Events;
using Microsoft.Office.Interop.Excel;

namespace FileHelpers.DataLink
{
    /// <summary><para>This class implements <see cref="DataStorage"/> for Microsoft Excel Files.</para>
    /// <para><b>WARNING you need to have installed Microsoft Excel 2000 or newer to use this feature.</b></para>
    /// <para><b>To use this class you need to reference the FileHelpers.ExcelStorage.dll file.</b></para>
    /// </summary>
    /// <remarks><b>This class is contained in the FileHelpers.ExcelStorage.dll and need the Interop.Office.dll and Interop.Excel.dll to work correctly.</b></remarks>
    public sealed class ExcelStorage : ExcelStorageBase
    {

        #region "  Constructors  "

        /// <summary>Create a new ExcelStorage to work with the specified type</summary>
        /// <param name="recordType">The type of records.</param>
        public ExcelStorage(Type recordType) : base(recordType)
        {
        }

        /// <summary>Create a new ExcelStorage to work with the specified type</summary>
        /// <param name="recordType">The type of records.</param>
        /// <param name="startRow">The row of the first data cell. Begining in 1.</param>
        /// <param name="startCol">The column of the first data cell. Begining in 1.</param>
        public ExcelStorage(Type recordType, int startRow, int startCol) : base(recordType, startRow, startCol)
        {
        }

        /// <summary>Create a new ExcelStorage to work with the specified type</summary>
        /// <param name="recordType">The type of records.</param>
        /// <param name="startRow">The row of the first data cell. Begining in 1.</param>
        /// <param name="startCol">The column of the first data cell. Begining in 1.</param>
        /// <param name="fileName">The file path to work with.</param>
        public ExcelStorage(Type recordType, string fileName, int startRow, int startCol)
            : base(recordType, fileName, startRow, startCol)
        {
        }

        #endregion

        #region "  Private Fields  "

        private ApplicationClass mApp;
        private Workbook mBook;
        private Worksheet mSheet;
        private List<string> mSheets;

        #endregion

        #region "  Public Properties  "

        /// <summary>Returns the names of the worksheets.</summary>
        public List<string> Sheets
        {
            get
            {
                if (mSheets == null)
                {
                    try
                    {
                        ReadAndStoreSheetNames();
                    }
                    finally
                    {
                        CloseAndCleanUp();
                    }
                }
                return mSheets;
            }
        }

        #endregion

        #region "  InitExcel  "

        private void InitExcel()
        {
            try
            {
                mApp = new ApplicationClass();
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                if (ex.Message.IndexOf("00024500-0000-0000-C000-000000000046") >= 0)
                    throw new ExcelBadUsageException("Excel 2000 or newer is not installed in this system.");
                else
                    throw;
            }

            mBook = null;
            mSheet = null;
            mApp.Visible = false;
            mApp.AlertBeforeOverwriting = false;
            mApp.ScreenUpdating = false;
            mApp.DisplayAlerts = false;
            mApp.EnableAnimations = false;
            mApp.Interactive = false;

        }

        #endregion

        #region "  CloseAndCleanUp  "

        private void CloseAndCleanUp()
        {
            if (mSheet != null)
            {
                DisposeCOMObject(mSheet);
                mSheet = null;
            }

            if (mBook != null)
            {
                mBook.Close(false, mv, mv);
                DisposeCOMObject(mBook);
                mBook = null;
            }

            if (mApp != null)
            {
                //this.mApp.Interactive = true;
                mApp.Quit();
                DisposeCOMObject(mApp);
                mApp = null;
            }
        }

        private static void DisposeCOMObject(object comObject)
        {
            while (System.Runtime.InteropServices.Marshal.ReleaseComObject(comObject) > 1)
            { }
        }

        #endregion

        private readonly Missing mv = Missing.Value;

        #region "  OpenWorkbook  "

        private void OpenWorkbook(string filename)
        {
            FileInfo info = new FileInfo(filename);
            if (info.Exists == false)
                throw new FileNotFoundException("Excel File '" + filename + "' not found.", filename);

            mBook = mApp.Workbooks.Open(info.FullName, (int)UpdateLinks,
                mv, mv, mv, mv, mv, mv, mv, mv, mv, mv, mv);

            if (string.IsNullOrEmpty(SheetName))
                mSheet = (Worksheet)mBook.ActiveSheet;
            else
            {
                try
                {
                    mSheet = (Worksheet)mBook.Sheets[SheetName];
                }
                catch
                {
                    throw new ExcelBadUsageException("The sheet '" + SheetName + "' was not found in the workbook.");
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
            mBook = mApp.Workbooks.Add(mv);
            mSheet = (Worksheet)mBook.ActiveSheet;
        }

        #endregion

        #region "  SaveWorkbook  "

        private void SaveWorkbook(string filename)
        {
            if (mBook == null)
                return;

            if (File.Exists(filename))
            {
                mBook.Save();
            }
            else
            {
                mBook.SaveAs(filename, mv, mv, mv, mv, mv, XlSaveAsAccessMode.xlNoChange, mv, mv, mv, mv);
            }
        }

        #endregion

        #region "  CellAsString  "

        /// <summary>
        /// Determine if a given cell is empty.
        /// </summary>
        /// <param name="row">Row index (1-based)</param>
        /// <param name="col">Column index (1-based)</param>
        protected override string CellAsString(object row, object col)
        {
            if (mSheet == null)
            {
                return null;
            }
            var r = (Range)mSheet.Cells[row, col];
            object res = r.Value;
            DisposeCOMObject(r);
            return Convert.ToString(res);
        }

        #endregion

        #region "  ColLeter  "

        private static string _ColLetter(int col /* 0 origin */)
        {
            // col = [0...25] 
            if (col >= 0 && col <= 25)
                return ((char)('A' + col)).ToString();
            return "";
        }

        private static string ColLetter(int col /* 1 Origin */)
        {
            if (col < 1 || col > 256)
                throw new ExcelBadUsageException("Column out of range; must be between 1 and 256"); // Excel limits 
            col--; // make 0 origin 
                   // good up to col ZZ 
            int col2 = (col / 26) - 1;
            int col1 = (col % 26);
            return _ColLetter(col2) + _ColLetter(col1);
        }

        #endregion

        #region "  RowValues  "

        private object[] RowValues(int row, int startCol, int numberOfCols)
        {
            if (mSheet == null)
            {
                return null;
            }
            object[] res;

            Range r;
            if (numberOfCols == 1)
            {
                r = mSheet.get_Range(ColLetter(startCol) + row.ToString(), ColLetter(startCol + numberOfCols - 1) + row.ToString());
                res = new object[] { r.Value };
            }
            else
            {
                r = mSheet.get_Range(ColLetter(startCol) + row.ToString(), ColLetter(startCol + numberOfCols - 1) + row.ToString());

                //TODO Eandis team check if correct
                object[,] resTemp = (object[,])r.Value;

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
            if (mSheet == null)
                return;

            Range r = mSheet.get_Range(ColLetter(startCol) + row.ToString(), ColLetter(startCol + values.Length - 1) + row.ToString());

            r.Value = values;
        }

        #endregion

        #region "  InsertRecords  "

        /// <summary>Insert all the records in the specified Excel File.</summary>
        /// <param name="records">The records to insert.</param>
        public override void InsertRecords(object[] records)
        {
            if (records == null || records.Length == 0)
                return;

            System.Globalization.CultureInfo oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            try
            {
                int recordNumber = 0;
                OnProgress(new ProgressEventArgs(0, records.Length));

                InitExcel();

                if (OverrideFile && File.Exists(FileName))
                    File.Delete(FileName);

                if (TemplateFile != string.Empty)
                {
                    if (File.Exists(TemplateFile) == false)
                        throw new ExcelBadUsageException("Template file not found: '" + TemplateFile + "'");

                    if (TemplateFile != FileName)
                        File.Copy(TemplateFile, FileName, true);
                }

                OpenOrCreateWorkbook(FileName);

                for (int i = 0; i < records.Length; i++)
                {
                    recordNumber++;
                    OnProgress(new ProgressEventArgs(recordNumber, records.Length));

                    WriteRowValues(RecordToValues(records[i]), StartRow + i, StartColumn);
                }

                SaveWorkbook(FileName);
            }
            finally
            {
                CloseAndCleanUp();
                Thread.CurrentThread.CurrentCulture = oldCulture;
            }
        }

        #endregion

        #region "  ExtractRecords  "

        /// <summary>Returns the records extracted from Excel file.</summary>
        /// <returns>The extracted records.</returns>
        public override object[] ExtractRecords()
        {
            if (FileName == String.Empty)
                throw new ExcelBadUsageException("You need to specify the WorkBookFile of the ExcelDataLink.");

            ArrayList res = new ArrayList();

            System.Globalization.CultureInfo oldCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            try
            {
                int cRow = StartRow;

                int recordNumber = 0;
                OnProgress(new ProgressEventArgs(0, -1));

                object[] colValues = new object[RecordFieldCount];

                InitExcel();
                OpenWorkbook(FileName);

                while (ShouldStopOnRow(cRow) == false)
                {
                    try
                    {
                        if (ShouldReadRowData(cRow))
                        {
                            recordNumber++;
                            OnProgress(new ProgressEventArgs(recordNumber, -1));

                            colValues = RowValues(cRow, StartColumn, RecordFieldCount);

                            object record = ValuesToRecord(colValues);
                            res.Add(record);
                        }
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
            finally
            {
                CloseAndCleanUp();
                Thread.CurrentThread.CurrentCulture = oldCulture;
            }

            return (object[])res.ToArray(RecordType);
        }

        #endregion

        private void ReadAndStoreSheetNames()
        {
            mSheets = new List<string>();

            InitExcel();
            OpenWorkbook(FileName);

            foreach (Worksheet sheet in mBook.Worksheets)
                mSheets.Add(sheet.Name);
        }

        private static string ColumnsToValues(object[] values)
        {
            if (values == null || values.Length == 0)
                return string.Empty;

            string res = string.Empty;
            if (values[0] != null)
                res = values[0].ToString();

            for (int i = 1; i < values.Length; i++)
            {
                if (values[i] == null)
                    res += ",";
                else
                    res += "," + values[i];
            }

            return res;
        }
    }
}