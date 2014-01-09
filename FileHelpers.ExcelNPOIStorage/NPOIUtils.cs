using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;
using System.Collections;
using System.Diagnostics;

namespace FileHelpers.ExcelNPOIStorage
{
    public static class NPOIUtils
    {
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
                //// Fuck this shit
                //var wb = cell.Sheet.Workbook;
                //var cellStyle = wb.CreateCellStyle();
                //// Can only be created once for each custom format, the code below is wrong
                //cellStyle.DataFormat = cell.Sheet.Workbook.GetCreationHelper().CreateDataFormat().GetFormat("dd/mm/yyyy" );
                //cell.CellStyle = cellStyle;
                cell.SetCellValue((DateTime) value);
            }

            else
                cell.SetCellValue(Convert.ToDouble(value));
        }
    }
}