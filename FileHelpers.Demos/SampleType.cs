using System;
using FileHelpers;

namespace FileHelpersSamples
{
    /// <summary>
    /// Sample Fixed length layout.
    /// Shows the use of different converts and padding options
    /// </summary>
    [FixedLengthRecord]
    public class SampleType
    {
        [FieldFixedLength(8)]
        [FieldConverter(ConverterKind.Date, "ddMMyyyy")]
        public DateTime Field1;

        [FieldFixedLength(3)]
        [FieldAlign(AlignMode.Right, '0')]
        public string Field2;

        [FieldFixedLength(3)]
        [FieldAlign(AlignMode.Right, '0')]
        [FieldTrim(TrimMode.Both)]
        [FieldConverter(ConverterKind.Int32)]
        public int Field3;
    }
}