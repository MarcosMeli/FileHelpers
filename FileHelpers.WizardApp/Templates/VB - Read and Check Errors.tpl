
Dim engine As New FileHelperEngine(${ClassName})

engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue

Dim res As ${ClassName}() = DirectCast(engine.ReadFile("YourFile.txt"), ${ClassName}())

If engine.ErrorManager.ErrorCount > 0 Then
   engine.ErrorManager.SaveErrors("Errors.txt")
End If

