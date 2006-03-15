
Dim engine As New FileHelperEngine(Of ${ClassName})()

engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue

Dim res As ${ClassName}() = engine.ReadFile("YourFile.txt")

If engine.ErrorManager.ErrorCount > 0 Then
   engine.ErrorManager.SaveErrors("Errors.txt")
End If

