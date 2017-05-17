using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace FileHelpers
{
    /// <summary>
    /// A class to loop through the field values
    /// </summary>
    [DelimitedRecord(",")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public sealed class RecordIndexer
        : IEnumerable<string>
    {
        /// <summary>
        /// Get the record indexer,  engine will load the lines into an array
        /// </summary>
        internal RecordIndexer() {}

        [FieldQuoted(QuoteMode.OptionalForRead, MultilineMode.AllowForRead)]
        private readonly string[] values = null;

        /// <summary>
        /// The number of fields in the record
        /// </summary>
        public int FieldCount
        {
            get { return values.Length; }
        }

        /// <summary>
        /// Get the field value at the specified index.
        /// </summary>
        /// <param name="index">The index of the field (zero based)</param>
        /// <returns>The field value</returns>
        public string this[int index]
        {
            get { return values[index]; }
        }

        /// <summary>
        /// Get the enumerator of the list
        /// </summary>
        /// <returns></returns>
        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return new ArrayEnumerator(values);
        }

        /// <summary>
        /// Get enumerator of the list
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<string>) this).GetEnumerator();
        }

        ///// <summary>
        ///// The Header of the Csv File
        ///// </summary>
        //public string Header
        //{
        //    get { return mHeader; }
        //}

        //[FieldHidden]
        //private string mHeader;

        #region "  ArrayEnumerator  "

        /// <summary>
        /// Create an enumerator off an array of strings
        /// </summary>
        private sealed class ArrayEnumerator : IEnumerator<string>
        {
            /// <summary>
            /// Array to return one at a time
            /// </summary>
            private readonly string[] mValues;

            /// <summary>
            /// Position in enumerator,  -1 to start
            /// </summary>
            private int i;

            /// <summary>
            /// Create an enumerator off a string array
            /// </summary>
            /// <param name="values">values to return one at a time</param>
            public ArrayEnumerator(string[] values)
            {
                mValues = values;
                i = -1;
            }

            /// <summary>
            /// Get the current item we are working on
            /// </summary>
            string IEnumerator<string>.Current
            {
                get { return mValues[i]; }
            }

            /// <summary>
            /// Clean up not needed
            /// </summary>
            public void Dispose() {}

            /// <summary>
            /// move the pointer along
            /// </summary>
            /// <returns></returns>
            public bool MoveNext()
            {
                i++;

                if (i >= mValues.Length)
                    return false;

                return true;
            }

            /// <summary>
            /// Go back to the start
            /// </summary>
            public void Reset()
            {
                i = -1;
            }

            /// <summary>
            /// Get the current element
            /// </summary>
            public object Current
            {
                get { return mValues[i]; }
            }
        }

        #endregion

        #region INotifyRead Members

        //void INotifyRead.AfterRead(EngineBase engine, string line)
        //{
        //    mHeader = engine.HeaderText;
        //}

        #endregion
    }
}