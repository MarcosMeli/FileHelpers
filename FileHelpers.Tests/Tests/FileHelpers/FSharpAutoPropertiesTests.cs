#if NETCOREAPP
using FileHelpers.Tests.Helpers;
using NFluent;
using NUnit.Framework;

namespace FileHelpers.Tests.FileHelpers
{
    public class FSharpAutoPropertiesTests
    {
        [Test]
        public void AutoPropertiesFromFSharpAreHandled()
        {
            const string source = @"
namespace FileHelpers

open System
open FileHelpers

[<CLIMutable>]
[<FixedLengthRecord>]
type SampleFSharpType = 
    { [<FieldFixedLength(8)>]
      [<FieldConverter(ConverterKind.Date, ""ddMMyyyy"")>]
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
";

            var assembly = FSharpHelper.Compile(source);
            var type = assembly.GetType("FileHelpers.SampleFSharpType");

            var fileHelpersAssembly = typeof(EngineBase).Assembly;
            var dateConverterType = fileHelpersAssembly.GetType("FileHelpers.Converters.DateTimeConverter");

            var engine = new FileHelperEngine(type);

            Check.That(engine.Options.FieldCount).IsEqualTo(3);

            Check.That(engine.Options.Fields[0].Converter.GetType()).IsEqualTo(dateConverterType);
            Check.That(engine.Options.Fields[2].IsOptional).IsEqualTo(true);
        }
    }
}
#endif
