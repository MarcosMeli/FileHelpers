
Dim engine As New FileHelperEngine(Of ${ClassName})()

Dim res As ${ClassName}() = engine.ReadFile("YourFile.txt")

For Each record As ${ClassName} In res

	'-> Use the current record here

Next