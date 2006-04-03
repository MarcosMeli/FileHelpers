
Dim engine As New FileHelperEngine(GetType(${ClassName}))

Dim res As ${ClassName}() = DirectCast(engine.ReadFile("YourFile.txt"), ${ClassName}())

For Each record As ${ClassName} In res

	'-> Use the current record here

Next