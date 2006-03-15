Dim arr As New ArrayList()

Dim record As ${ClassName}

' record = New ${ClassName}();
'-> Assign Record Values ...

'-> Add the current record to the array
' arr.Add(record)

Dim engine As New FileHelperEngine(${ClassName})

engine.WriteFile("YourFile.txt", arr.ToArray(GetType(${ClassName})
