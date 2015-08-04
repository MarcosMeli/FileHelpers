using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.CodeFixes;

namespace FileHelpersAnalyzer
{
    public abstract class FileHelpersCodeFixProvider<T> : CodeFixProvider
        where T:IFileHelperAnalyzer, new()
    {
        protected T Analyzer { get; } = new T();

        public sealed override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create(Analyzer.Id);

        public sealed override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }
        
    }
}