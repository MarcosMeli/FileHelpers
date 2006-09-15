Imports System.IO

'-> Never forget:  Imports FileHelpers
Imports FileHelpers

Module MainModule

    Sub Main()

        Dim engine As New FileHelperEngine(GetType(Customer))
        Dim custs() As Customer

        ' To read use:
        custs = DirectCast(engine.ReadFile("yourfile.txt"), Customer())

        ' To write use:
        engine.WriteFile("yourfile.txt", custs)


        'If you are using .NET 2.0 or greater is 
        'better if you use the Generics version:

        'Dim engine As New FileHelperEngine(Of Customer)()
        'Dim custs() As Customer

        ' To read use (no casts)
        'custs = engine.ReadFile("yourfile.txt")

        ' To write use 
        'engine.WriteFile("yourfile.txt", custs)

    End Sub


    '------------------------
    '   RECORD CLASS (Example, change at your will)
    '   TIP: Remember to use the wizard to generate this class

    <DelimitedRecord(",")> _
    Public Class Customer

        Public CustId As Integer
        Public Name As String
        Public Balance As Decimal
        <FieldConverter(ConverterKind.Date, "ddMMyyyy")> _
        Public AddedDate As DateTime
    End Class

End Module
