using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace FileHelpers
{
    /// <summary>
    /// Support record class for string sorting
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DelimitedRecord("\r\n")]
    public sealed class SorterRecord
        : IComparable<SorterRecord>
    {
        /// <summary>
        /// Value of the string
        /// </summary>
        internal string Value;

        /// <summary>
        /// Compare to the sorter record
        /// </summary>
        /// <param name="other">Record to compare</param>
        /// <returns>Comparison</returns>
        public int CompareTo(SorterRecord other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}