// Copyright (c) Microsoft Corporation.  All rights reserved.

using ExamplesFx.ColorCode.Styling;

namespace ExamplesFx.ColorCode
{
    /// <summary>
    /// Defines the contract for a style sheet.
    /// </summary>
    internal interface IStyleSheet
    {
        /// <summary>
        /// Gets the dictionary of styles for the style sheet.
        /// </summary>
        StyleDictionary Styles { get; }
    }
}