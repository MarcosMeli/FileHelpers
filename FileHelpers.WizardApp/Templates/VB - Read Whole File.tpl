
Dim engine As New FileHelperEngine(GetType(${ClassName}))

Dim res = DirectCast(engine.ReadFile("YourFile.txt"), ${ClassName}())

For Each record In res

	'-> Use the current record here

Next