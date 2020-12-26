<FixedLengthRecord> _
Public Class SampleType

	<FieldFixedLength(8),
	 DateTimeConverter("ddMMyyyy")>
	Public Field1 As DateTime

	<FieldFixedLength(3), _
	 FieldAlign(AlignMode.Left, " "c), _
	 FieldTrim(TrimMode.Both)> _
	public Field2 As String

	<FieldFixedLength(3), _
	 FieldAlign(AlignMode.Right, "0"c), _
	 FieldTrim(TrimMode.Both) > _
	public Field3 As Integer

End Class