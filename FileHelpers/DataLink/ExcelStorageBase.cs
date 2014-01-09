using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.DataLink
{
    /// <summary><para>This class is a common base class for both FileHelpers.DataLink.ExcelStorage (FileHelpers.ExcelStorage.dll) and FileHelpers.DataLink.ExcelNPOIStorage (FileHelpers.ExcelNPOIStorage.dll).</para>
    /// </summary>
    public abstract class ExcelStorageBase : DataStorage
    {
        private ExcelUpdateLinksMode mUpdateLinks = ExcelUpdateLinksMode.NeverUpdate;

        /// <summary>
        /// Specifies the way links in the file are updated. By default the library never update the links
        /// </summary>
        public ExcelUpdateLinksMode UpdateLinks
        {
            get { return mUpdateLinks; }
            set { mUpdateLinks = value; }
        }

        #region "  Constructors  "

        /// <summary>Create a new ExcelStorage to work with the specified type</summary>
        /// <param name="recordType">The type of records.</param>
        public ExcelStorageBase(Type recordType)
            : base(recordType)
        {
            // Temporary

            //			if (RecordHasDateFields())
            //				throw new NotImplementedException("For now the ExcelStorage don�t work with DateTime fields, sorry for the problems.");
        }

        /// <summary>Create a new ExcelStorage to work with the specified type</summary>
        /// <param name="recordType">The type of records.</param>
        /// <param name="startRow">The row of the first data cell. Begining in 1.</param>
        /// <param name="startCol">The column of the first data cell. Begining in 1.</param>
        public ExcelStorageBase(Type recordType, int startRow, int startCol)
            : this(recordType)
        {
            mStartColumn = startCol;
            mStartRow = startRow;
        }

        /// <summary>Create a new ExcelStorage to work with the specified type</summary>
        /// <param name="recordType">The type of records.</param>
        /// <param name="startRow">The row of the first data cell. Begining in 1.</param>
        /// <param name="startCol">The column of the first data cell. Begining in 1.</param>
        /// <param name="fileName">The file path to work with.</param>
        public ExcelStorageBase(Type recordType, string fileName, int startRow, int startCol)
            : this(recordType, startRow, startCol)
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

        private string mTemplateFile = string.Empty;
        private ExcelReadStopBehavior mExcelReadStopBehavior = ExcelReadStopBehavior.StopOnEmptyRow;
        private int mExcelReadStopAfterEmptyRows = 1;

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

        /// <summary>
        /// Indicates the behavior to use when determining when to stop reading records from the source xls file.
        /// </summary>
        public ExcelReadStopBehavior ExcelReadStopBehavior
        {
            get { return mExcelReadStopBehavior; }
            set { mExcelReadStopBehavior = value; }
        }

        /// <summary>
        /// Defines how many empty rows indicate when to stop reading records from the source xls file.
        /// Only applies when ExcelReadStopBehavior == ExcelReadStopBehavior.StopOnEmptyRow.
        /// Default behavior is stop after entountering at least 1 empty row.
        /// </summary>
        public int ExcelReadStopAfterEmptyRows
        {
            get { return mExcelReadStopAfterEmptyRows; }
            set { mExcelReadStopAfterEmptyRows = value; }
        }

        #endregion

        #region "  CellIsEmpty  "

        /// <summary>
        /// Indicates whether the given cell should be considered empty.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        protected bool CellIsEmpty(object row, object col)
        {
            var cellAsString = CellAsString(row, col);
            return string.IsNullOrEmpty(cellAsString);
            //return cellAsString == String.Empty;
        }

        #endregion

        #region "  CellAsString  "

        /// <summary>
        /// Determine if a given cell is empty.
        /// </summary>
        /// <param name="row">Row index (1-based)</param>
        /// <param name="col">Column index (1-based)</param>
        protected abstract string CellAsString(object row, object col);

        #endregion

        #region "  RowIsEmpty  "

        /// <summary>
        /// Indicates whether the given row should be considered empty.
        /// </summary>
        /// <param name="cRow"></param>
        /// <returns></returns>
        protected bool RowIsEmpty(int cRow)
        {
            for (int column = StartColumn; column < StartColumn + RecordFieldCount; column++) {
                if (CellIsEmpty(cRow, column) == false)
                    return false;
            }
            return true;
        }

        #endregion

        #region "  ShouldStopOnRow  "

        /// <summary>
        /// Indicates whether the storage reader should stop reading the worksheet at the given row index.
        /// </summary>
        /// <param name="cRow">The current row index (1-based)</param>
        protected bool ShouldStopOnRow(int cRow)
        {
            switch (this.ExcelReadStopBehavior) {
                case ExcelReadStopBehavior.StopOnEmptyFirstCell:
                    return CellIsEmpty(cRow, StartColumn);

                case ExcelReadStopBehavior.StopOnEmptyRow:
                {
                    // work backwards from most far-away row (counting ahead by value of property ExcelReadStopAfterEmptyRows)
                    for (
                        int fwdRowIndex = cRow + (ExcelReadStopAfterEmptyRows - 1);
                        fwdRowIndex >= cRow;
                        fwdRowIndex--) {
                        // as soon as we encounter a non-empty row then we can bail-out of loop and return ShouldStopOnRow=false
                        if (!RowIsEmpty(fwdRowIndex))
                            return false;
                    }
                    // we never found a non-empty row so return ShouldStopOnRow=true
                    return true;
                }

                default:
                    throw new ArgumentOutOfRangeException("Need to support new ExcelReadStopBehavior: " +
                                                          this.ExcelReadStopBehavior);
            }
        }

        /// <summary>
        /// Indicates whether the storage reader should data from the given row index.
        /// </summary>
        /// <param name="cRow">The current row index (1-based)</param>
        protected bool ShouldReadRowData(int cRow)
        {
            switch (this.ExcelReadStopBehavior) {
                case ExcelReadStopBehavior.StopOnEmptyFirstCell:
                {
                    // we already checked in ShouldStopOnRow()
                    return true;
                }

                case ExcelReadStopBehavior.StopOnEmptyRow:
                {
                    // don't attempt to read empty rows
                    return !RowIsEmpty(cRow);
                }

                default:
                    throw new ArgumentOutOfRangeException("Need to support new ExcelReadStopBehavior: " +
                                                          this.ExcelReadStopBehavior);
            }
        }

        #endregion
    }

    /// <summary>
    /// Specifies the way links in the file are updated.
    /// </summary>
    public enum ExcelUpdateLinksMode
    {
        /// <summary>Never update links for this workbook on opening</summary>
        NeverUpdate = 0,

        /// <summary>User specifies how links will be updated</summary>
        UserPrompted = 1,

        /// <summary>Always update links for this workbook on opening</summary>
        AlwaysUpdate = 2
    }

    /// <summary>
    /// Specifies how to determine when to stop reading rows from Excel
    /// </summary>
    public enum ExcelReadStopBehavior
    {
        /// <summary>First cell of the row being empty means we should stop reading</summary>
        StopOnEmptyFirstCell = 1,

        /// <summary>All cells in the row being empty means we should stop reading</summary>
        StopOnEmptyRow = 0,
    }
}