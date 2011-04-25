// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using System.IO;
using ColorCode.Parsing;

namespace ColorCode
{
    /// <summary>
    /// Defines the contract for a source code formatter.
    /// </summary>
    internal interface IFormatter
    {
        /// <summary>
        /// Writes the parsed source code to the ouput using the specified style sheet.
        /// </summary>
        /// <param name="parsedSourceCode">The parsed source code to format and write to the output.</param>
        /// <param name="scopes">The captured scopes for the parsed source code.</param>
        /// <param name="styleSheet">The style sheet according to which the source code will be formatted.</param>
        /// <param name="textWriter">The text writer to which the formatted source code will be written.</param>
        void Write(string parsedSourceCode,
                   IList<Scope> scopes,
                   IStyleSheet styleSheet,
                   TextWriter textWriter);

        /// <summary>
        /// Generates and writes the footer to the output.
        /// </summary>
        /// <param name="styleSheet">The style sheet according to which the footer will be generated.</param>
        /// <param name="textWriter">The text writer to which footer will be written.</param>
        void WriteFooter(IStyleSheet styleSheet,
                         TextWriter textWriter);

        /// <summary>
        /// Generates and writes the header to the output.
        /// </summary>
        /// <param name="styleSheet">The style sheet according to which the header will be generated.</param>
        /// <param name="textWriter">The text writer to which header will be written.</param>
        void WriteHeader(IStyleSheet styleSheet,
                         TextWriter textWriter);
    }
}