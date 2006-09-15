Imports System.IO

'-> Never forget:  Imports FileHelpers
Imports FileHelpers

Module MainModule

    Sub Main()

        Delimited()

        FixedLength()

        ErrorHandling()

        FormatTransformation()

    End Sub

#Region "  Delimited  "

    Sub Delimited()

        Console.WriteLine()
        Console.WriteLine("Reading a Delimited file")
        Console.WriteLine()


        '-> Estas dos lineas son el uso de la librería
        Dim engine As New FileHelperEngine(GetType(Customer))
        Dim clientes As Customer()
        clientes = CType(engine.ReadFile("..\Data\CustomersDelimited.txt"), Customer())



        '-> From the version 1.4.0 you can write
        '-> only one line to do this:

        '-> clientes = CType(CommonEngine.ReadFile(GetType(Cliente), "..\Data\CustomersDelimited.txt"), Cliente())

        '-> Aqui es donde ustedes agregan su código
        For Each cli As Customer In clientes
            Console.WriteLine()
            Console.WriteLine("Customer: " + cli.CustId.ToString() + " - " + cli.Name)
            Console.WriteLine("Added Date: " + cli.AddedDate.ToString("d-M-yyyy"))
            Console.WriteLine("Balance: " + cli.Balance.ToString())
            Console.WriteLine()
            Console.WriteLine("-----------------------------")
        Next

        Console.ReadLine()
        Console.WriteLine("Writing data to a Delimited file...")

        '-> Esta línea es para escribir
        engine.WriteFile("temp.txt", clientes)

        Console.WriteLine()
        Console.WriteLine("Data Successful written !!!")
        Console.ReadLine()

        If File.Exists("temp.txt") Then File.Delete("temp.txt")

    End Sub

#End Region

#Region "  FixedLength  "

    Sub FixedLength()

        Console.WriteLine()
        Console.WriteLine("Reading a Fixed Length File")
        Console.WriteLine()


        '-> Estas dos lineas son el uso de la librería
        Dim engine As New FileHelperEngine(GetType(Customer2))
        Dim clientes As Customer2()

        clientes = CType(engine.ReadFile("..\Data\CustomersFixedLength.txt"), Customer2())



        '-> A partir de la versión 1.4.0 se puede
        '-> inclusive escribir en una sola línea:

        '-> clientes = CType(CommonEngine.ReadFile(GetType(Cliente2), "..\Data\CustomersDelimited.txt"), Cliente2())

        '-> Aqui es donde ustedes agregan su código
        For Each cli As Customer2 In clientes
            Console.WriteLine()
            Console.WriteLine("Customer: " + cli.CustId.ToString() + " - " + cli.Name)
            Console.WriteLine("Added Date: " + cli.AddedDate.ToString("d-M-yyyy"))
            Console.WriteLine("Balance: " + cli.Balance.ToString())
            Console.WriteLine()
            Console.WriteLine("-----------------------------")
        Next

        Console.ReadLine()
        Console.WriteLine("Writing data to a Fixed Length file...")

        '-> Esta línea es para escribir
        engine.WriteFile("temp.txt", clientes)

        Console.WriteLine()
        Console.WriteLine("Data Successful written !!!")
        Console.ReadLine()

        If File.Exists("temp.txt") Then File.Delete("temp.txt")

    End Sub

#End Region

#Region "  FormatTransformation  "

    Sub FormatTransformation()

        Console.WriteLine("Testing Format Transformation")
        Console.WriteLine()


        Dim engine As FileTransformEngine = New FileTransformEngine(GetType(Customer), GetType(Customer2))
        engine.TransformFile("..\Data\CustomersDelimited.txt", "temp.txt")


        Console.WriteLine("Format Transformation Successful !!!")

        If File.Exists("temp.txt") Then File.Delete("temp.txt")
        Console.ReadLine()

    End Sub

#End Region

#Region "  ErrorHandling  "


    Sub ErrorHandling()

        Console.WriteLine("Testing Error Handling...")
        Console.WriteLine()

        '-> Estas dos lineas son el uso de la librería
        Dim engine As New FileHelperEngine(GetType(Customer))

        engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue

        Dim clientes As Customer() = CType(engine.ReadFile("..\Data\CustomersWithErrors.txt"), Customer())

        If engine.ErrorManager.ErrorCount > 0 Then

            Console.Write("Records: ")
            Console.WriteLine(engine.TotalRecords)

            Console.Write("Successful: ")
            Console.WriteLine(clientes.Length)

            Console.Write("With Error: ")
            Console.WriteLine(engine.ErrorManager.ErrorCount)

            Console.Write("Error: ")
            Console.WriteLine(engine.ErrorManager.Errors(0).ExceptionInfo.Message)

        End If

        engine.ErrorManager.SaveErrors("errores.txt")

        Console.ReadLine()

        If File.Exists("errores.txt") Then File.Delete("errores.txt")

    End Sub

#End Region

End Module
