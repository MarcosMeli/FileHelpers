using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;

namespace Demos
{
    //-> {Example.Name:Sort Big File without Record Class 2}
    //-> {Example.Runnable:false}
    //-> {Example.Description:Shows how to sort a big file without a record class}

    public class BigFileSortString2
        : DemoParent
    {

        //-> {Example.File:SortingWithoutRecord.cs}
        public override void Run()
        {
            // Implements http://en.wikipedia.org/wiki/External_sorting

            var sorter = new BigFileSorter(
                (x, y) => 
                    {
                        // You can add here any custom function
                        return x.Length.CompareTo(y.Length);
                    });
            sorter.DeleteTempFiles = true;
            sorter.Sort("unsorted.txt", "sorted.txt");
        }

        //-> {/Example.File}

    }
}
