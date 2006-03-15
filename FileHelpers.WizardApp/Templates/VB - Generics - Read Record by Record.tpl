
Dim record As ${ClassName}()

Dim engine As New FileHelperAsyncEngine(Of ${ClassName})()

engine.BeginReadFile("YourFile.txt")

While Not engine.ReadNext() is Nothing

	record = engine.LastRecord

	'-> Use the current record here
	
End While

engine.EndsRead()
