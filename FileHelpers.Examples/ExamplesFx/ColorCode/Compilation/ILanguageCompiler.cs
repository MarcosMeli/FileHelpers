// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;

namespace ExamplesFx.ColorCode.Compilation
{
    internal interface ILanguageCompiler
    {
        CompiledLanguage Compile(ILanguage language);
    }
}