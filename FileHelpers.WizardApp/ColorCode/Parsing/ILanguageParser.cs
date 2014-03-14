// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;

namespace ExamplesFx.ColorCode.Parsing
{
    internal interface ILanguageParser
    {
        void Parse(string sourceCode,
            ILanguage language,
            Action<string, IList<Scope>> parseHandler);
    }
}