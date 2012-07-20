using System;
using System.Collections;
using System.Collections.Generic;

namespace ExamplesFramework
{
    public interface ISearchableNode
    {
        string GetName();
        string GetDescription();
        string GetDescriptionExtra();
    }
}