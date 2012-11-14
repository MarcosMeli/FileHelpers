// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using ExamplesFx.ColorCode.Styling.StyleSheets;

namespace ExamplesFx.ColorCode
{
    /// <summary>
    /// Provides easy access to ColorCode's built-in style sheets.
    /// </summary>
    internal static class StyleSheets
    {
        /// <summary>
        /// Gets the default style sheet.
        /// </summary>
        /// <remarks>
        /// The default style sheet mimics the default colorization scheme used by Visual Studio 2008 to the extent possible.
        /// </remarks>
        public static IStyleSheet Default { get { return new DefaultStyleSheet(); }}
    }
}
