
'-> Never forget:  Imports FileHelpers
Imports FileHelpers

<DelimitedRecord(",")> _
Public Class Customer

    Public CustId As Integer
    Public Name As String
    Public Balance As Decimal
    <FieldConverter(ConverterKind.Date, "ddMMyyyy")> _
    Public AddedDate As DateTime



    '-> este método es para cambiar el formato no
    '-> no para el uso básico

    <TransformToRecord(GetType(Customer2))> _
      Public Function CrearSimilar() As Customer2
        Dim res As Customer2 = New Customer2

        '-> Simplemente copiamos los valores que nos interesan
        res.CustId = Me.CustId
        res.Name = Me.Name
        res.Balance = Me.Balance
        res.AddedDate = Me.AddedDate

        Return res
    End Function


End Class


