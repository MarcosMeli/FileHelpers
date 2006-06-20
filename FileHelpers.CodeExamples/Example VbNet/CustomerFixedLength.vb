
'-> Never forget:  Imports FileHelpers
Imports FileHelpers


<FixedLengthRecord()> _
Public Class Customer2

    <FieldFixedLength(5)> _
    Public CustId As Integer

    <FieldFixedLength(20), _
     FieldTrimAttribute(TrimMode.Right)> _
    Public Name As String

    <FieldFixedLength(8), _
     FieldConverter(GetType(TwoDecimalConverter))> _
    Public Balance As Decimal

    <FieldFixedLength(8), _
    FieldConverter(ConverterKind.Date, "ddMMyyyy")> _
    Public AddedDate As DateTime

End Class

Friend Class TwoDecimalConverter
    Inherits ConverterBase

    Public Overrides Function StringToField(ByVal from As String) As Object
        Dim res As Decimal = Convert.ToDecimal(from)
        Return res / 100
    End Function

    Public Overrides Function FieldToString(ByVal from As Object) As String
        Dim d As Decimal = CType(from, Decimal)
        Return Math.Round(d * 100).ToString()
    End Function

End Class
