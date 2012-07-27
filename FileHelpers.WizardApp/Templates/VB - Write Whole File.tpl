Dim arr As List(Of ${ClassName})()

Dim record As ${ClassName}

' record = New ${ClassName}();
'-> Assign Record Values ...

'-> Add the current record to the array
' arr.Add(record)

Dim engine As New FileHelperEngine(GetType(${ClassName}))

engine.WriteFile("YourFile.txt", arr)
