using System;
using System.Collections;
using System.Collections.Generic;

namespace ExamplesFx.TreeView
{
    public interface ISearchableNode
    {
        string GetName();
        string GetDescription();
        string GetDescriptionExtra();
    }
}