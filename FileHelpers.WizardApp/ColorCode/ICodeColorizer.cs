// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.IO;

namespace ExamplesFx.ColorCode
{
    /// <summary>
    /// Defines the contract for a code colorizer.
    /// </summary>
    /// <seealso cref="CodeColorizer"/>
    internal interface ICodeColorizer
    {
        /// <summary>
        /// Colorizes source code using the specified language, the default formatter, and the default style sheet.
        /// </summary>
        /// <param name="sourceCode">The source code to colorize.</param>
        /// <param name="language">The language to use to colorize the source code.</param>
        /// <returns>The colorized source code.</returns>
        string Colorize(string sourceCode,
            ILanguage language);

        /// <summary>
        /// Colorizes source code using the specified language, the default formatter, and the default style sheet.
        /// </summary>
        /// <param name="sourceCode">The source code to colorize.</param>
        /// <param name="language">The language to use to colorize the source code.</param>
        /// <param name="textWriter">The text writer to which the colorized source code will be written.</param>
        void Colorize(string sourceCode,
            ILanguage language,
            TextWriter textWriter);

        /// <summary>
        /// Colorizes source code using the specified language, formatter, and style sheet.
        /// </summary>
        /// <param name="sourceCode">The source code to colorize.</param>
        /// <param name="language">The language to use to colorize the source code.</param>
        /// <param name="textWriter">The text writer to which the colorized source code will be written.</param>
        /// <param name="formatter">The formatter to use to colorize the source code.</param>
        /// <param name="styleSheet">The style sheet to use to colorize the source code.</param>
        /// <param name="textWriter">The text writer to which the colorized source code will be written.</param>
        void Colorize(string sourceCode,
            ILanguage language,
            IFormatter formatter,
            IStyleSheet styleSheet,
            TextWriter textWriter);
    }
}