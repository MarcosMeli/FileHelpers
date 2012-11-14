// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.IO;
using System.Text;
using ExamplesFx.ColorCode.Formatting;
using ExamplesFx.ColorCode.Common;
using ExamplesFx.ColorCode.Compilation;
using ExamplesFx.ColorCode.Parsing;

namespace ExamplesFx.ColorCode
{
    /// <summary>
    /// Colorizes source code.
    /// </summary>
    internal class CodeColorizer : ICodeColorizer
    {
        private readonly ILanguageParser languageParser;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeColorizer"/> class.
        /// </summary>
        public CodeColorizer()
        {
            languageParser = new LanguageParser(new LanguageCompiler(Languages.CompiledLanguages), Languages.LanguageRepository);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeColorizer"/> class.
        /// </summary>
        /// <param name="languageParser">The language parser that the <see cref="CodeColorizer"/> instance will use for its lifetime.</param>
        public CodeColorizer(ILanguageParser languageParser)
        {
            Guard.ArgNotNull(languageParser, "languageParser");
            
            this.languageParser = languageParser;
        }

        /// <summary>
        /// Colorizes source code using the specified language, the default formatter, and the default style sheet.
        /// </summary>
        /// <param name="sourceCode">The source code to colorize.</param>
        /// <param name="language">The language to use to colorize the source code.</param>
        /// <returns>The colorized source code.</returns>
        public string Colorize(string sourceCode, ILanguage language)
        {
            var buffer = new StringBuilder(sourceCode.Length * 2);

            using (TextWriter writer = new StringWriter(buffer))
            {
                Colorize(sourceCode, language, writer);

                writer.Flush();
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Colorizes source code using the specified language, the default formatter, and the default style sheet.
        /// </summary>
        /// <param name="sourceCode">The source code to colorize.</param>
        /// <param name="language">The language to use to colorize the source code.</param>
        /// <param name="textWriter">The text writer to which the colorized source code will be written.</param>
        public void Colorize(string sourceCode, ILanguage language, TextWriter textWriter)
        {
            Colorize(sourceCode, language, Formatters.Default, StyleSheets.Default, textWriter);
        }

        /// <summary>
        /// Colorizes source code using the specified language, formatter, and style sheet.
        /// </summary>
        /// <param name="sourceCode">The source code to colorize.</param>
        /// <param name="language">The language to use to colorize the source code.</param>
        /// <param name="formatter">The formatter to use to colorize the source code.</param>
        /// <param name="styleSheet">The style sheet to use to colorize the source code.</param>
        /// <param name="textWriter">The text writer to which the colorized source code will be written.</param>
        public void Colorize(string sourceCode,
                             ILanguage language,
                             IFormatter formatter,
                             IStyleSheet styleSheet,
                             TextWriter textWriter)
        {
            Guard.ArgNotNull(language, "language");
            Guard.ArgNotNull(formatter, "formatter");
            Guard.ArgNotNull(styleSheet, "styleSheet");
            Guard.ArgNotNull(textWriter, "textWriter");

            formatter.WriteHeader(styleSheet, textWriter);

            languageParser.Parse(sourceCode, language, (parsedSourceCode, captures) => formatter.Write(parsedSourceCode, captures, styleSheet, textWriter));

            formatter.WriteFooter(styleSheet, textWriter);
        }
    }
}