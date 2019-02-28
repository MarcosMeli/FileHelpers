using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FSharp.Compiler;
using FSharp.Compiler.SourceCodeServices;
using Microsoft.FSharp.Control;
using Microsoft.FSharp.Core;

namespace FileHelpers.FSharp.Tests.Helpers
{
    public static class CompileHelper
    {
        public static Assembly Compile(string source)
        {
            var fileHelpersAssembly = typeof(EngineBase).Assembly;

            var checker = FSharpChecker.Create(FSharpOption<int>.None,
                FSharpOption<bool>.None,
                FSharpOption<bool>.None,
                FSharpOption<ReferenceResolver.Resolver>.None,
                FSharpOption<FSharpFunc<Tuple<string, DateTime>, FSharpOption<Tuple<object, IntPtr, int>>>>.None);

            var file = Path.GetTempFileName();

            File.WriteAllText(file + ".fs", source);

            var action = checker.CompileToDynamicAssembly(new[] {"-o", file + ".dll", "-a", file + ".fs", "--reference:" + fileHelpersAssembly.Location},
                FSharpOption<Tuple<TextWriter, TextWriter>>.None, 
                FSharpOption<string>.None);

            var (compilationErrors, exitCode, assembly) = FSharpAsync
                .StartAsTask(action, FSharpOption<TaskCreationOptions>.None, FSharpOption<CancellationToken>.None)
                .Result;

            if (compilationErrors.Any() && exitCode != 0)
            {
                var errors = new StringBuilder();

                foreach (var error in compilationErrors)
                {
                    errors.AppendLine(error.ToString());
                }

                throw new InvalidOperationException($"Cannot compile fsharp: {errors}");
            }

            return assembly.Value;
        }
    }
}