using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace FileHelpers.Examples.Sorting.SortBigFilesString1
{
    //-> Name:Sort Big File without Record Class 1
    //-> Runnable:false
    //-> Description:Shows how to sort a big file without a record class
    [Ignore("Does not have input")]
    public class BigFileSortString1
        : ExampleBase
    {
        //-> Implements <a href="http://en.wikipedia.org/wiki/External_sorting">External Sorting (wikipedia)</a>

        //-> You don't need to declare a record class to sort a file, you can sort with a compare method

        protected override void Run()
        {
            //-> File:SortingWithoutRecord.cs

            // Sort comparing the raw lines
            var sorter = new BigFileSorter((x, y) =>
                    string.Compare(x, y, StringComparison.Ordinal));


            sorter.Sort("unsorted.txt", "sorted.txt");
            //-> /File
        }

    }
}