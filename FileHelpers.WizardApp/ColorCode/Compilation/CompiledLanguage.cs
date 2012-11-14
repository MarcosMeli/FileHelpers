// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using System.Text.RegularExpressions;
using ExamplesFx.ColorCode.Common;

namespace ExamplesFx.ColorCode.Compilation
{
    internal class CompiledLanguage
    {
        public CompiledLanguage(string id,
                                string name,
                                Regex regex,
                                IList<string> captures)
        {
            Guard.ArgNotNullAndNotEmpty(id, "id");
            Guard.ArgNotNullAndNotEmpty(name, "name");
            Guard.ArgNotNull(regex, "regex");
            Guard.ArgNotNullAndNotEmpty(captures, "captures");
            
            Id = id;
            Name = name;
            Regex = regex;
            Captures = captures;
        }

        public IList<string> Captures { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public Regex Regex { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}