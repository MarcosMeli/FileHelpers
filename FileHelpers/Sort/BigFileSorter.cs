using System;
using System.Text;

namespace FileHelpers
{
    /// <summary>
    /// This class help to sort really big files using the External Sorting algorithm
    /// http://en.wikipedia.org/wiki/External_sorting
    /// </summary>
    public sealed class BigFileSorter
        : BigFileSorter<SorterRecord>
    {
        /// <summary>
        /// Create a big file sorter with the minimum buffer size
        /// </summary>
        public BigFileSorter()
            :this(0) // 20Mb default size
        {
        }

        /// <summary>
        /// Create a big file sorter using the block size
        /// </summary>
        /// <param name="blockFileSizeInBytes">Block size to work on</param>
        public BigFileSorter(int blockFileSizeInBytes)
            :this(null, null, blockFileSizeInBytes)
        {
        }

        /// <summary>
        /// Create a big file sorter using and encoding
        /// </summary>
        /// <param name="encoding">Encoding of the file</param>
        public BigFileSorter(Encoding encoding)
            : this(null, encoding, 0)
        {
        }

        /// <summary>
        /// Create a big file sorter using comparison operator
        /// </summary>
        /// <param name="sorter">Comparison operator</param>
        public BigFileSorter(Comparison<string> sorter)
            : this(sorter, 0)
        {
        }

        /// <summary>
        /// Create a big file sorter with a comparison operator and block size
        /// </summary>
        /// <param name="sorter">Comparison operator</param>
        /// <param name="blockFileSizeInBytes">Block size to work on file</param>
        public BigFileSorter(Comparison<string> sorter, int blockFileSizeInBytes)
            : this(sorter, null, blockFileSizeInBytes)
        {
        }

        /// <summary>
        /// Create a bug file sorter
        /// </summary>
        /// <param name="sorter">Comparison operator</param>
        /// <param name="encoding">encoding of the file</param>
        /// <param name="blockFileSizeInBytes">Block size to work on</param>
        public BigFileSorter(Comparison<string> sorter, Encoding encoding, int blockFileSizeInBytes)
            :base(CreateSorter(sorter), encoding, blockFileSizeInBytes)
        {
        }

        /// <summary>
        /// Create a record sorter based on string value
        /// </summary>
        /// <param name="sorter">string sorter to convert</param>
        /// <returns>new record comparison sorter</returns>
        private static Comparison<SorterRecord> CreateSorter(Comparison<string> sorter)
        {
            if (sorter == null)
                return null;

            return (x, y) => sorter(x.Value, y.Value);
        }

        ///// <summary>
        ///// A fast way to sort a big file. For more options you need to
        ///// instantiate the BigFileSorter class instead of using static methods
        ///// </summary>
        //public static void SimpleSort(string source, string destination)
        //{
        //    var sorter = new BigFileSorter();
        //    sorter.Sort(source, destination);
        //}

        ////  TODO:   OVERRIDE HERE !!!!

        ///// <summary>
        ///// A fast way to sort a big file. For more options you need to
        ///// instantiate the BigFileSorter class instead of using static methods
        ///// </summary>
        //public static void SimpleSort(Encoding encoding, string source, string destination)
        //{
        //    var sorter = new BigFileSorter(encoding);
        //    sorter.Sort(source, destination);
        //}
    }
}
