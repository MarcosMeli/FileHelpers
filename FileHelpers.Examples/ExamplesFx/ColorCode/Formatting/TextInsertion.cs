// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using ExamplesFx.ColorCode.Parsing;

namespace ExamplesFx.ColorCode.Formatting
{
    internal class TextInsertion
    {
        public virtual int Index { get; set; }
        public virtual string Text { get; set; }
        public virtual Scope Scope { get; set; }
    }
}