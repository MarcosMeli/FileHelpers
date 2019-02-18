namespace FileHelpers.Tests.FSharp

open System
open FileHelpers

[<CLIMutable>]
[<FixedLengthRecord>]
type SampleFSharpType = 
    { [<FieldFixedLength(8)>]
      [<FieldConverter(ConverterKind.Date, "ddMMyyyy")>]
      Field1: DateTime

      [<FieldFixedLength(3)>]
      [<FieldAlign(AlignMode.Left, ' ')>]
      [<FieldTrim(TrimMode.Both)>]
      Field2: string

      [<FieldFixedLength(3)>]
      [<FieldAlign(AlignMode.Right, '0')>]
      [<FieldTrim(TrimMode.Both)>]
      [<FieldOptional>]
      Field3: int }
