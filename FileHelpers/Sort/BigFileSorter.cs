using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FileHelpers
{

    // Based on this code: http://splinter.com.au/blog/?p=142
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DelimitedRecord("\r\n")]
    public sealed class SorterRecord 
        : IComparable<SorterRecord>
    {
        internal string Value;

        public int CompareTo(SorterRecord other)
        {
            return Value.CompareTo(other.Value);
        }
    }

    /// <summary>
    /// This class help to sort really big files using the External Sorting algorithm
    /// http://en.wikipedia.org/wiki/External_sorting
    /// </summary>
    public sealed class BigFileSorter
        : BigFileSorter<SorterRecord>
    {
        public BigFileSorter()
            :this(0) // 20Mb default size
        {
        }

        public BigFileSorter(int blockFileSizeInBytes)
            :this(null, null, blockFileSizeInBytes)
        {
        }

        public BigFileSorter(Encoding encoding)
            : this(null, encoding, 0)
        {
        }

        public BigFileSorter(Comparison<string> sorter)
            : this(sorter, 0)
        {
        }

        public BigFileSorter(Comparison<string> sorter, int blockFileSizeInBytes)
            : this(sorter, null, blockFileSizeInBytes)
        {
        }

        public BigFileSorter(Comparison<string> sorter, Encoding encoding, int blockFileSizeInBytes)
            :base(CreateSorter(sorter), encoding, blockFileSizeInBytes)
        {
        }

        private static Comparison<SorterRecord> CreateSorter(Comparison<string> sorter)
        {
            if (sorter == null)
                return null;

            return (x, y) => sorter(x.Value, y.Value);
        }
    }

}
