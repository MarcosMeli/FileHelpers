
Dim record As ${ClassName}()

Dim engine As New FileHelperAsyncEngine(GetType(${ClassName}))

engine.BeginReadFile("YourFile.txt")

While Not engine.ReadNext() is Nothing

	record = DirectCast(engine.LastRecord, ${ClassName})

	'-> Use the current record here

End While

engine.Close()
