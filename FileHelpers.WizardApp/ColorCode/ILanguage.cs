// Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Collections.Generic;
using ExamplesFx.ColorCode.Compilation;

namespace ExamplesFx.ColorCode
{
    /// <summary>
    /// Defines how ColorCode will parse the source code of a given language.
    /// </summary>
    internal interface ILanguage
    {
        /// <summary>
        /// Gets the identifier of the language (e.g., csharp).
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the first line pattern (regex) to use when determining if the language matches a source text.
        /// </summary>
        string FirstLinePattern { get; }

        /// <summary>
        /// Gets the "friendly" name of the language (e.g., C#).
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the collection of language rules in the language.
        /// </summary>
        IList<LanguageRule> Rules { get; }
    }
}