// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace ColorCode.Compilation
{
    internal static class RuleFormats
    {
        public static string JavaScript;
        public static string ServerScript;

        static RuleFormats()
        {
            const string script = @"(?xs)(<)(script)
                                        {0}[\s\n]+({1})[\s\n]*(=)[\s\n]*(""{2}""){0}[\s\n]*(>)
                                        (.*?)
                                        (</)(script)(>)";

            const string attributes = @"(?:[\s\n]+([a-z0-9-_]+)[\s\n]*(=)[\s\n]*([^\s\n""']+?)
                                           |[\s\n]+([a-z0-9-_]+)[\s\n]*(=)[\s\n]*(""[^\n]+?"")
                                           |[\s\n]+([a-z0-9-_]+)[\s\n]*(=)[\s\n]*('[^\n]+?')
                                           |[\s\n]+([a-z0-9-_]+) )*";

            JavaScript = string.Format(script, attributes, "type|language", "[^\n]*javascript");
            ServerScript = string.Format(script, attributes, "runat", "server");
        }
    }
}