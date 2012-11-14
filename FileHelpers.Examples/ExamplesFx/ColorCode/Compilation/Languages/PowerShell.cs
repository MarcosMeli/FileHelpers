// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using ExamplesFx.ColorCode.Common;

namespace ExamplesFx.ColorCode.Compilation.Languages
{
    internal class PowerShell : ILanguage
    {
        public string Id
        {
            get { return LanguageId.PowerShell; }
        }

        public string Name
        {
            get { return "PowerShell"; }
        }

        public string FirstLinePattern
        {
            get { return null; }
        }

        public IList<LanguageRule> Rules
        {
            get
            {
                return new List<LanguageRule>
                           {
                               new LanguageRule(
                                   @"(?s)(<\#.*?\#>)",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Comment}
                                       }),
                               new LanguageRule(
                                   @"(\#.*?)\r?$",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Comment}
                                       }),
                               new LanguageRule(
                                   @"'[^\n]*?(?<!\\)'",
                                   new Dictionary<int, string>
                                       {
                                           {0, ScopeName.String}
                                       }),
                               new LanguageRule(
                                   @"(?s)@"".*?""@",
                                   new Dictionary<int, string>
                                       {
                                           {0, ScopeName.StringCSharpVerbatim}
                                       }),
                               new LanguageRule(
                                   @"(?s)(""[^\n]*?(?<!`)"")",
                                   new Dictionary<int, string>
                                       {
                                           {0, ScopeName.String}
                                       }),
                               new LanguageRule(
                                   @"\$(?:[\d\w\-]+(?::[\d\w\-]+)?|\$|\?|\^)",
                                   new Dictionary<int, string>
                                       {
                                           {0, ScopeName.PowerShellVariable}
                                       }),
                               new LanguageRule(
                                   @"\${[^}]+}",
                                   new Dictionary<int, string>
                                       {
                                           {0, ScopeName.PowerShellVariable}
                                       }),
                               new LanguageRule(
                                   @"\b(begin|break|catch|continue|data|do|dynamicparam|elseif|else|end|exit|filter|finally|foreach|for|from|function|if|in|param|process|return|switch|throw|trap|try|until|while)\b",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.Keyword}
                                       }),
                               new LanguageRule(
                                   @"-(?:c|i)?(?:eq|ne|gt|ge|lt|le|notlike|like|notmatch|match|notcontains|contains|replace)",
                                   new Dictionary<int, string>
                                       {
                                           {0, ScopeName.PowerShellOperator}
                                       }
                                   ),
                               new LanguageRule(
                                   @"-(?:band|and|as|join|not|bxor|xor|bor|or|isnot|is|split)",
                                   new Dictionary<int, string>
                                       {
                                           {0, ScopeName.PowerShellOperator}
                                       }
                                   ),
                               new LanguageRule(
                                   @"(?:\+=|-=|\*=|/=|%=|=|\+\+|--|\+|-|\*|/|%)",
                                   new Dictionary<int, string>
                                       {
                                           {0, ScopeName.PowerShellOperator}
                                       }
                                   ),
                               new LanguageRule(
                                   @"(?:\>\>|2\>&1|\>|2\>\>|2\>)",
                                   new Dictionary<int, string>
                                       {
                                           {0, ScopeName.PowerShellOperator}
                                       }
                                   ),
                               new LanguageRule(
                                   @"(?s)\[(CmdletBinding)[^\]]+\]",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.PowerShellAttribute}
                                       }),
                               new LanguageRule(
                                   @"(\[)([^\]]+)(\])(::)?",
                                   new Dictionary<int, string>
                                       {
                                           {1, ScopeName.PowerShellOperator},
                                           {2, ScopeName.PowerShellType},
                                           {3, ScopeName.PowerShellOperator},
                                           {4, ScopeName.PowerShellOperator}
                                       })
                           };
            }
        }
    }
}