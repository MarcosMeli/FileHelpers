using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FileHelpers
{
    
    /// <summary>
    /// A class to loop through the field values
    /// </summary>
    [DelimitedRecord(",")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public sealed class RecordIndexer
        :IEnumerable<string> 
        //, INotifyRead
    {

        internal RecordIndexer()
        {}

        [FieldQuoted(QuoteMode.OptionalForRead, MultilineMode.AllowForRead)]
        private string[] values;

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
            get
            {
                return values[index];
            }
        }


        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return new ArrayEnumerator(values);
        }

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
        
        //[FieldIgnored]
        //private string mHeader;

        #region "  ArrayEnumerator  "

        private sealed class ArrayEnumerator : IEnumerator<string>
        {
            private string[] mValues;
            private int i;
            public ArrayEnumerator(string[] values)
            {
                mValues = values;
                i = -1;
            }

            string IEnumerator<string>.Current
            {
                get { return mValues[i]; }
            }

            public void Dispose()
            {

            }

            public bool MoveNext()
            {
                i++;

                if (i >= mValues.Length)
                    return false;

                return true;
            }

            public void Reset()
            {
                i = -1;
            }

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
