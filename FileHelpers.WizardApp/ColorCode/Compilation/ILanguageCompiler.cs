// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace ColorCode.Compilation
{
    internal interface ILanguageCompiler
    {
        CompiledLanguage Compile(ILanguage language);
    }
}