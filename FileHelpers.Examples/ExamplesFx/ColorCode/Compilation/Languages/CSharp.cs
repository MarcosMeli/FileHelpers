// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using ExamplesFx.ColorCode.Common;

namespace ExamplesFx.ColorCode.Compilation.Languages
{
    internal class CSharp : ILanguage
    {
        public string Id
        {
            get { return LanguageId.CSharp; }
        }

        public string Name
        {
            get { return "C#"; }
        }

        public string FirstLinePattern
        {
            get { return null; }
        }

        public IList<LanguageRule> Rules
        {
            get
            {
                return new List<LanguageRule> {
                    new LanguageRule(
                        @"/\*([^*]|[\r\n]|(\*+([^*/]|[\r\n])))*\*+/",
                        new Dictionary<int, string> {
                            {0, ScopeName.Comment},
                        }),
                    new LanguageRule(
                        @"(///)(?:\s*?(<[/a-zA-Z0-9\s""=]+>))*([^\r\n]*)",
                        new Dictionary<int, string> {
                            {1, ScopeName.XmlDocTag},
                            {2, ScopeName.XmlDocTag},
                            {3, ScopeName.XmlDocComment},
                        }),
                    new LanguageRule(
                        @"(//.*?)\r?$",
                        new Dictionary<int, string> {
                            {1, ScopeName.Comment}
                        }),
                    new LanguageRule(
                        @"'[^\n]*?(?<!\\)'",
                        new Dictionary<int, string> {
                            {0, ScopeName.String}
                        }),
                    new LanguageRule(
                        @"(?s)@""(?:""""|.)*?""(?!"")",
                        new Dictionary<int, string> {
                            {0, ScopeName.StringCSharpVerbatim}
                        }),
                    new LanguageRule(
                        @"(?s)(""[^\n]*?(?<!\\)"")",
                        new Dictionary<int, string> {
                            {0, ScopeName.String}
                        }),
                    new LanguageRule(
                        @"\[(assembly|module|type|return|param|method|field|property|event):[^\]""]*(""[^\n]*?(?<!\\)"")?[^\]]*\]",
                        new Dictionary<int, string> {
                            {1, ScopeName.Keyword},
                            {2, ScopeName.String}
                        }),
                    new LanguageRule(
                        @"^\s*(\#define|\#elif|\#else|\#endif|\#endregion|\#error|\#if|\#line|\#pragma|\#region|\#undef|\#warning).*?$",
                        new Dictionary<int, string> {
                            {1, ScopeName.PreprocessorKeyword}
                        }),
                    new LanguageRule(
                        @"\b(abstract|as|ascending|base|bool|break|by|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|descending|do|double|dynamic|else|enum|equals|event|explicit|extern|false|finally|fixed|float|for|foreach|from|get|goto|group|if|implicit|in|int|into|interface|internal|is|join|let|lock|long|namespace|new|null|object|on|operator|orderby|out|override|params|partial|private|protected|public|readonly|ref|return|sbyte|sealed|select|set|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|var|virtual|void|volatile|where|while|yield)\b",
                        new Dictionary<int, string> {
                            {1, ScopeName.Keyword}
                        }),
                };
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}