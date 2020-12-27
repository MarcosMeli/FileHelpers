using System;
using FileHelpers.Converters;

namespace FileHelpers.Tests
{
    [FixedLengthRecord]
    public class SampleType
    {
        [FieldFixedLength(8)]
        [DateTimeConverter("ddMMyyyy")]
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

    [FixedLengthRecord]
    [IgnoreFirst]
    public class SampleTypeIgnoreFirst
    {
        [FieldFixedLength(8)]
        [DateTimeConverter("ddMMyyyy")]
        public DateTime Field1;

        [FieldFixedLength(3)]
        [FieldAlign(AlignMode.Left, ' ')]
        [FieldTrim(TrimMode.Both)]
        public string Field2;

        [FieldFixedLength(3)]
        [FieldAlign(AlignMode.Right, '0')]
        [FieldTrim(TrimMode.Both)]
        [Int32Converter]
        public int Field3;
    }

    [FixedLengthRecord]
    [IgnoreFirst(2)]
    [IgnoreLast(2)]
    public class SampleTypeIgnoreFirstLast
    {
        [FieldFixedLength(8)]
        [DateTimeConverter("ddMMyyyy")]
        public DateTime Field1;

        [FieldFixedLength(3)]
        [FieldAlign(AlignMode.Left, ' ')]
        [FieldTrim(TrimMode.Both)]
        public string Field2;

        [FieldFixedLength(3)]
        [FieldAlign(AlignMode.Right, '0')]
        [FieldTrim(TrimMode.Both)]
        [Int32Converter]
        public int Field3;
    }


    [FixedLengthRecord]
    public class SampleTypeInt
    {
        [FieldFixedLength(8)]
        public int Field1;

        [FieldFixedLength(3)]
        [FieldAlign(AlignMode.Left, ' ')]
        [FieldTrim(TrimMode.Both)]
        public int Field2;
    }

    [DelimitedRecord(",")]
    public class SampleTypeNullableGuid
    {
        public Guid? Field1;
    }
}