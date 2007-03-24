[FixedLengthRecord]
public class SampleType
{

	[FieldFixedLength(8)]
	[FieldConverter(ConverterKind.Date, "ddMMyyyy")]
	public DateTime Field1;

	[FieldFixedLength(3)]
	[FieldAlign(AlignMode.Left, ' ')]
	[FieldTrim(TrimMode.Both)]
	public string Field2;

	[FieldFixedLength(3)]
	[FieldAlign(AlignMode.Right, '0')]
	[FieldTrim(TrimMode.Both)]
	public int Field3;

}