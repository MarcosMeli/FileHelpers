using System;
using NPOI.SS.UserModel;

namespace FileHelpers.ExcelNPOIStorage
{
    /// <summary>
    /// Helper class for NPOI work
    /// </summary>
    public static class NPOIUtils
    {
        /// <summary>
        /// Get a cell value
        /// </summary>
        /// <param name="cell">The cell object</param>
        /// <returns>The value of the cell</returns>
        public static object GetCellValue(ICell cell)
        {
            switch (cell.CellType) {
                default:
                    return null;
                case CellType.Blank:
                    return null;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    if (DateUtil.IsCellDateFormatted(cell))
                        return cell.DateCellValue;
                    else
                        return cell.NumericCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Formula:
                    return string.Concat("=", cell.CellFormula);
            }
        }
        /// <summary>
        /// Sets a cell value
        /// </summary>
        /// <param name="cell">The cell</param>
        /// <param name="value">The value to be assigned to the cell</param>
        public static void SetCellValue(ICell cell, object value)
        {
            if (value == null)
                cell.SetCellValue(null as string);

            else if (value is string ||
                     value is String)
                cell.SetCellValue(value as string);

            else if (value is bool ||
                     value is Boolean)
                cell.SetCellValue((bool) value);

            else if (value is DateTime) {
                //It works
                var wb = cell.Sheet.Workbook;
                var cellStyle = wb.CreateCellStyle();
                cellStyle.DataFormat = cell.Sheet.Workbook.GetCreationHelper().CreateDataFormat().GetFormat("dd/mm/yyyy" );
                cell.CellStyle = cellStyle;                
                cell.SetCellValue((DateTime) value);
            }

            else
                cell.SetCellValue(Convert.ToDouble(value));
        }
    }
}
