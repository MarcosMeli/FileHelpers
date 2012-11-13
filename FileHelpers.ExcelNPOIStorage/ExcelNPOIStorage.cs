﻿using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers.DataLink;
using NPOI.HSSF.UserModel;
using System.Reflection;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using NPOI.SS.Util.CellWalk;
using System.Threading;
using FileHelpers.Events;
using System.Globalization;
using System.Collections;

namespace FileHelpers.ExcelNPOIStorage {

	/// <summary><para>This class implements the <see cref="DataStorage"/> for Microsoft Excel Files using the NPOI library.</para>
	/// <para><b>WARNING you need to reference NPOI.dll in your project to use this feature.</b></para>
	/// <para><b>To use this class you need to reference the FileHelpers.ExcelNPOIStorage.dll file.</b></para>
	/// </summary>
	/// <remmarks><b>This class is contained in the FileHelpers.ExcelNPOIStorage.dll and need the NPOI.dll to work correctly.</b></remmarks>
    public sealed class ExcelNPOIStorage : ExcelStorageBase {

		//private readonly Missing mv = Missing.Value;

		#region "  Constructors  "
		/// <summary>Create a new ExcelStorage to work with the specified type</summary>
		/// <param name="recordType">The type of records.</param>
		public ExcelNPOIStorage( Type recordType )
			: base( recordType ) { }

		/// <summary>Create a new ExcelStorage to work with the specified type</summary>
		/// <param name="recordType">The type of records.</param>
		/// <param name="startRow">The row of the first data cell. Begining in 1.</param>
		/// <param name="startCol">The column of the first data cell. Begining in 1.</param>
		public ExcelNPOIStorage( Type recordType, int startRow, int startCol )
			: base( recordType, startRow, startCol ) { }

		/// <summary>Create a new ExcelStorage to work with the specified type</summary>
		/// <param name="recordType">The type of records.</param>
		/// <param name="startRow">The row of the first data cell. Begining in 1.</param>
		/// <param name="startCol">The column of the first data cell. Begining in 1.</param>
		/// <param name="fileName">The file path to work with.</param>
		public ExcelNPOIStorage( Type recordType, string fileName, int startRow, int startCol )
			: base( recordType, fileName, startRow, startCol ) { }
		#endregion

		#region "  Private Fields  "
		
		private HSSFWorkbook mWorkbook;
		private HSSFSheet mSheet;
		//private RecordInfo mRecordInfo;

        #endregion

		#region "  Public Properties  "
		
		#endregion

		#region "  InitExcel  "
		private void InitExcel() {
			mWorkbook = null;
			mSheet = null;
		}
		#endregion

		#region "  CloseAndCleanUp  "
		private void CloseAndCleanUp() {

			if( mSheet != null )
				mSheet = null;

			if( mWorkbook != null )
				mWorkbook = null;

			GC.Collect();
			GC.WaitForPendingFinalizers();
		}
		#endregion

		#region "  OpenWorkbook  "
		private void OpenWorkbook( string filename ) {

			FileInfo info = new FileInfo( filename );
			if( info.Exists == false )
				throw new FileNotFoundException( string.Concat( "Excel File '", filename, "' not found." ), filename );

			using( FileStream file = new FileStream( filename, FileMode.Open, FileAccess.Read ) ) {

				mWorkbook = new HSSFWorkbook( file );

				if( String.IsNullOrEmpty( SheetName ) )
					mSheet = (HSSFSheet)mWorkbook.GetSheetAt( 0 );
				else {
					try {
						mSheet = (HSSFSheet)mWorkbook.GetSheet( SheetName );
						if( mSheet == null )
							throw new ExcelBadUsageException( string.Concat( "The sheet '", SheetName, "' was not found in the workbook." ) );
					} catch {
						throw new ExcelBadUsageException( string.Concat( "The sheet '", SheetName, "' was not found in the workbook." ) );
					}
				}
			}
		}
		#endregion

		#region "  CreateWorkbook methods  "
		private void OpenOrCreateWorkbook( string filename ) {
			if( File.Exists( filename ) )
				OpenWorkbook( filename );
			else
				CreateWorkbook();
		}

		private void CreateWorkbook() {
			mWorkbook = new HSSFWorkbook();
			mSheet = (HSSFSheet)mWorkbook.CreateSheet( SheetName );
		}
		#endregion

		#region "  SaveWorkbook  "
		private void SaveWorkbook() {
			if( mWorkbook == null )
				return;

			using( var fileData = new FileStream( FileName, FileMode.Create ) ) {
				mWorkbook.Write( fileData );
			}
		}

		private void SaveWorkbook( string filename ) {
			FileName = filename;
			SaveWorkbook();
		}
		#endregion

		#region "  CellAsString  "

        protected override string CellAsString(object row, object col)
        {
            return this.CellAsString(mSheet.GetRow((int)row), (int)col);
        }

		private string CellAsString( IRow row, int col ) {
			if( mSheet == null )
				return null;

			ICell cell = CellUtil.GetCell( row, col );
			return cell.StringCellValue;
		}

		#endregion

		#region "  ColLeter  "
		//static string _ColLetter( int col /* 0 origin */) {
		//	// col = [0...25] 
		//	if( col >= 0 && col <= 25 )
		//		return ((char)('A' + col)).ToString();
		//	return "";
		//}
		//static string ColLetter( int col /* 1 Origin */) {
		//	if( col < 1 || col > 256 )
		//		throw new ExcelBadUsageException( "Column out of range; must be between 1 and 256" ); // Excel limits 
		//	col--; // make 0 origin 
		//	// good up to col ZZ 
		//	int col2 = (col / 26) - 1;
		//	int col1 = (col % 26);
		//	return _ColLetter( col2 ) + _ColLetter( col1 );
		//}
		#endregion

		#region "  RowValues  "
		private object[] RowValues( int rowNum, int startCol, int numberOfCols ) {
			if( mSheet == null )
				return null;

			if( numberOfCols == 1 ) {
				IRow row = HSSFCellUtil.GetRow( rowNum, (HSSFSheet)mSheet );

				ICell cell = HSSFCellUtil.GetCell( row, startCol );
				return new object[] { NPOIUtils.GetCellValue( cell ) };
			} else {

				CellRangeAddress range = new CellRangeAddress( rowNum, rowNum, startCol, startCol + numberOfCols - 1 );

				CellWalk cw = new CellWalk( mSheet, range );
				cw.SetTraverseEmptyCells( true );
				CellExtractor ce = new CellExtractor();
				cw.Traverse( ce );

				return ce.CellValues;
			}
		}

		private void WriteRowValues( object[] values, int rowNum, int startCol ) {
			if( mSheet == null )
				return;

			CellRangeAddress range = new CellRangeAddress( rowNum, rowNum, startCol, startCol + values.Length - 1 );

			CellWalk cw = new CellWalk( mSheet, range );
			cw.SetTraverseEmptyCells( true );

			CellInserter ci = new CellInserter( new List<object>( values ) );
			cw.Traverse( ci );
		}
		#endregion

		#region "  InsertRecords  "
		/// <summary>Insert all the records in the specified Excel File.</summary>
		/// <param name="records">The records to insert.</param>
		public override void InsertRecords( object[] records ) {
			if( records == null || records.Length == 0 )
				return;

			CultureInfo oldCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo( "en-US" );
			try {
				int recordNumber = 0;
				OnProgress( new ProgressEventArgs( 0, records.Length ) );

				InitExcel();

				if( OverrideFile && File.Exists( FileName ) )
					File.Delete( FileName );

				if( !String.IsNullOrEmpty( TemplateFile ) ) {
					if( File.Exists( TemplateFile ) == false )
						throw new ExcelBadUsageException( string.Concat( "Template file not found: '", TemplateFile, "'" ) );

					if( String.Compare( TemplateFile, FileName, true ) != 0 )
						File.Copy( TemplateFile, FileName, true );
				}

				OpenOrCreateWorkbook( FileName );

				for( int i = 0; i < records.Length; i++ ) {
					recordNumber++;
					OnProgress( new ProgressEventArgs( recordNumber, records.Length ) );

					WriteRowValues( RecordToValues( records[i] ), StartRow + i, StartColumn );
				}

				SaveWorkbook( FileName );
			} catch {
				throw;
			} finally {
				CloseAndCleanUp();
				Thread.CurrentThread.CurrentCulture = oldCulture;
			}
		}

		#endregion

		#region "  ExtractRecords  "

		/// <summary>Returns the records extracted from Excel file.</summary>
		/// <returns>The extracted records.</returns>
		public override object[] ExtractRecords() {
			if( String.IsNullOrEmpty( FileName ) )
				throw new ExcelBadUsageException( "You need to specify the WorkBookFile of the ExcelDataLink." );

			ArrayList res = new ArrayList();

			CultureInfo oldCulture = Thread.CurrentThread.CurrentCulture;
			Thread.CurrentThread.CurrentCulture = new CultureInfo( "en-US" );
			try {
				int cRow = StartRow;

				int recordNumber = 0;
				OnProgress( new ProgressEventArgs( recordNumber, -1 ) );

				object[] colValues = new object[RecordFieldCount];

				InitExcel();
				OpenWorkbook( FileName );

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
			} catch {
				throw;
			} finally {
				CloseAndCleanUp();
				Thread.CurrentThread.CurrentCulture = oldCulture;
			}

			return (object[])res.ToArray( RecordType );
		}
		#endregion

		private static string ColumnsToValues( object[] values ) {
			if( values == null || values.Length == 0 )
				return string.Empty;

			string res = string.Empty;
			if( values[0] != null )
				res = values[0].ToString();

			for( int i = 1; i < values.Length; i++ )
				res += "," + values[i] == null ? String.Empty : values[i].ToString();

			return res;
		}

		private class CellExtractor : ICellHandler {
			private List<object> _cells;

			/// <summary>
			/// Initializes a new instance of the CellExtractor class.
			/// </summary>
			public CellExtractor() {
				_cells = new List<object>();
			}

			public object[] CellValues { get { return _cells.ToArray(); } }

			#region ICellHandler Members
			public void OnCell( ICell cell, ICellWalkContext ctx ) {
				_cells.Add( NPOIUtils.GetCellValue( cell ) );
			}
			#endregion
		}

		private class CellInserter : ICellHandler {
			private List<object> _cells = null;
			private List<object>.Enumerator _valuesEnumerator;

			/// <summary>
			/// Initializes a new instance of the CellInserter class.
			/// </summary>
			public CellInserter( List<object> cellValues ) {
				_cells = cellValues;
				_valuesEnumerator = _cells.GetEnumerator();
			}

			#region ICellHandler Members
			public void OnCell( ICell cell, ICellWalkContext ctx ) {

				if( _valuesEnumerator.MoveNext() )
					NPOIUtils.SetCellValue( cell, _valuesEnumerator.Current );
				else
					NPOIUtils.SetCellValue( cell, null );
			}
			#endregion
		}
	}

	/// <summary>
	/// Specifies the way links in the file are updated.
	/// </summary>
	public enum ExcelUpdateLinksMode {
		/// <summary>User specifies how links will be updated</summary>
		UserPrompted = 1,
		/// <summary>Never update links for this workbook on opening</summary>
		NeverUpdate = 2,
		/// <summary>Always update links for this workbook on opening</summary>
		AlwaysUpdate = 3
	}
}
