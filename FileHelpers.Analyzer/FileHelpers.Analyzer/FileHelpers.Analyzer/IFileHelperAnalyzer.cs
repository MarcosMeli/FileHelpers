using System;
using System.Collections;
using System.Collections.Generic;

namespace FileHelpersAnalyzer
{
    public interface IFileHelperAnalyzer
    {
        string Id { get; }
        string FixTitle { get; }
    }
}