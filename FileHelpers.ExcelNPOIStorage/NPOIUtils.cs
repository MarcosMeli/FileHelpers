using System;
using System.Collections.Generic;
using System.Text;
using NPOI.SS.UserModel;
using System.Collections;

namespace FileHelpers.ExcelNPOIStorage {

	public static class NPOIUtils {

		public static object GetCellValue( ICell cell ) {
			switch( cell.CellType ) {
			case CellType.Unknown:
			default:
				return "[NULL]";
            case CellType.BLANK:
			    return null;
			case CellType.BOOLEAN:
				return cell.BooleanCellValue;
			case CellType.STRING:
				return cell.StringCellValue;
			case CellType.NUMERIC:
                    if(DateUtil.IsCellDateFormatted(cell)) {
                        return cell.DateCellValue;
                    }
                    else { return cell.NumericCellValue; }
			case CellType.ERROR:
				return cell.ErrorCellValue;
			case CellType.FORMULA:
				return string.Concat( "=", cell.CellFormula );
			}
		}

		public static void SetCellValue( ICell cell, object value ) {

			if( value == null )
				cell.SetCellValue( null as string );

			if( value is string || value is String )
				cell.SetCellValue( value as string );

			if( value is bool || value is Boolean )
				cell.SetCellValue( (bool)value );

			if( value is DateTime )
				cell.SetCellValue( (DateTime)value );

			cell.SetCellValue( Convert.ToDouble( value ) );
		}
	}
}
