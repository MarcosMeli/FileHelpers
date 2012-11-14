// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace ExamplesFx.ColorCode.Compilation
{
    internal interface ILanguageCompiler
    {
        CompiledLanguage Compile(ILanguage language);
    }
}