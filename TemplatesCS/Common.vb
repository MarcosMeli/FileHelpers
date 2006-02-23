Option Strict On
Option Explicit On 
Option Compare Text

Imports CodeSmith.Engine
Imports Microsoft.VisualBasic
Imports SchemaExplorer
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Diagnostics
Imports System.IO.Directory

Public Class FileHelperTemplate
    Inherits CodeTemplate

     Public Function GetVBVariableType(ByVal column As ColumnSchema) As String

      If column.Name.EndsWith("TypeCode") Then Return column.Name

      Select Case column.DataType

         Case DbType.AnsiString
            Return "String"

         Case DbType.AnsiStringFixedLength
            Return "String"

         Case DbType.Binary
            Return "Byte()"

         Case DbType.Boolean
            Return "Boolean"

         Case DbType.Byte
            Return "Byte"

         Case DbType.Currency
            Return "Decimal"

         Case DbType.Date
            Return "Date"

         Case DbType.DateTime
            Return "DateTime"

         Case DbType.Decimal
            Return "Decimal"

         Case DbType.Double
            Return "Double"

         Case DbType.Guid
            Return "Guid"

         Case DbType.Int16
            Return "Short"

         Case DbType.Int32
            Return "Integer"

         Case DbType.Int64
            Return "Long"

         Case DbType.Object
            Return "Object"

         Case DbType.SByte
            Return "SByte"

         Case DbType.Single
            Return "Float"

         Case DbType.String
            Return "String"

         Case DbType.StringFixedLength
            Return "String"

         Case DbType.Time
            Return "TimeSpan"

         Case DbType.UInt16
            Return "UShort"

         Case DbType.UInt32
            Return "UInt"

         Case DbType.UInt64
            Return "ULong"

         Case DbType.VarNumeric
            Return "Decimal"

         Case Else
            Return "__UNKNOWN__" + column.NativeType
      End Select

   End Function

    Public Function GetCSVariableType(ByVal column As ColumnSchema) As String

      If column.Name.EndsWith("TypeCode") Then Return column.Name

      Select Case column.DataType
      
			case DbType.AnsiString
				return "string"
			case DbType.AnsiStringFixedLength
				return "string"
			case DbType.String
				return "string"
			case DbType.StringFixedLength
				return "string"
			case DbType.Binary 
				return "byte[]"
			case DbType.Boolean
				return "bool"
			case DbType.Byte
				return "byte"
			case DbType.Currency
				return "decimal"
			case DbType.Date 
				return "DateTime"
			case DbType.DateTime
				return "DateTime"
			case DbType.VarNumeric: 
				return "decimal"
			case DbType.Decimal
				return "decimal"
			case DbType.Double
				return "double"
			case DbType.Guid
				return "Guid"
			case DbType.Int16
				return "short"
			case DbType.Int32
				return "int"
			case DbType.Int64
				return "long"
			case DbType.Object
				return "object"
			case DbType.SByte
				return "sbyte"
			case DbType.Single
				return "float"
			case DbType.Time
				return "TimeSpan"
			case DbType.UInt16
				return "ushort"
			case DbType.UInt32
				return "uint"
			case DbType.UInt64
				return "ulong"
			case else
				return "__UNKNOWN__" + column.NativeType

			
	End Select
   End Function


     Public Function GetViewVBVariableType(ByVal column As ViewColumnSchema) As String

      If column.Name.EndsWith("TypeCode") Then Return column.Name

      Select Case column.DataType

         Case DbType.AnsiString
            Return "String"

         Case DbType.AnsiStringFixedLength
            Return "String"

         Case DbType.Binary
            Return "Byte()"

         Case DbType.Boolean
            Return "Boolean"

         Case DbType.Byte
            Return "Byte"

         Case DbType.Currency
            Return "Decimal"

         Case DbType.Date
            Return "Date"

         Case DbType.DateTime
            Return "DateTime"

         Case DbType.Decimal
            Return "Decimal"

         Case DbType.Double
            Return "Double"

         Case DbType.Guid
            Return "Guid"

         Case DbType.Int16
            Return "Short"

         Case DbType.Int32
            Return "Integer"

         Case DbType.Int64
            Return "Long"

         Case DbType.Object
            Return "Object"

         Case DbType.SByte
            Return "SByte"

         Case DbType.Single
            Return "Float"

         Case DbType.String
            Return "String"

         Case DbType.StringFixedLength
            Return "String"

         Case DbType.Time
            Return "TimeSpan"

         Case DbType.UInt16
            Return "UShort"

         Case DbType.UInt32
            Return "UInt"

         Case DbType.UInt64
            Return "ULong"

         Case DbType.VarNumeric
            Return "Decimal"

         Case Else
            Return "__UNKNOWN__" + column.NativeType
      End Select

   End Function

    Public Function GetviewCSVariableType(ByVal column As ViewColumnSchema) As String

      If column.Name.EndsWith("TypeCode") Then Return column.Name

      Select Case column.DataType
      
			case DbType.AnsiString
				return "string"
			case DbType.AnsiStringFixedLength
				return "string"
			case DbType.String
				return "string"
			case DbType.StringFixedLength
				return "string"
			case DbType.Binary 
				return "byte[]"
			case DbType.Boolean
				return "bool"
			case DbType.Byte
				return "byte"
			case DbType.Currency
				return "decimal"
			case DbType.Date 
				return "DateTime"
			case DbType.DateTime
				return "DateTime"
			case DbType.VarNumeric: 
				return "decimal"
			case DbType.Decimal
				return "decimal"
			case DbType.Double
				return "double"
			case DbType.Guid
				return "Guid"
			case DbType.Int16
				return "short"
			case DbType.Int32
				return "int"
			case DbType.Int64
				return "long"
			case DbType.Object
				return "object"
			case DbType.SByte
				return "sbyte"
			case DbType.Single
				return "float"
			case DbType.Time
				return "TimeSpan"
			case DbType.UInt16
				return "ushort"
			case DbType.UInt32
				return "uint"
			case DbType.UInt64
				return "ulong"
			case else
				return "__UNKNOWN__" + column.NativeType

			
	End Select
   End Function


End Class
