using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace Demos
{
    //-> {Example.Name:Sort Big File without Record Class 1}
    //-> {Example.Runnable:false}
    //-> {Example.Description:Shows how to sort a big file without a record class}

    public class BigFileSortString1
        : DemoParent
    {

        //-> {Example.File:SortingWithoutRecord.cs}
        public override void Run()
        {
            // Implements http://en.wikipedia.org/wiki/External_sorting
            // Uses the comparison in the construct

            var sorter = new BigFileSorter((x, y) => x.CompareTo(y)); 
            sorter.DeleteTempFiles = true;
            sorter.Sort("unsorted.txt", "sorted.txt");
        }

        //-> {/Example.File}

    }
}
