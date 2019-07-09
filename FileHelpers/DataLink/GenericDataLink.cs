using System;

namespace FileHelpers.DataLink
{
    /// <summary>
    /// This class has the responsibility to enable the two directional
    /// transformation.
    /// <list type="bullet">
    /// <item> DataStorage &lt;-> DataStorage </item>
    /// </list>
    /// </summary>
    /// <remarks>
    /// <para>Uses two <see cref="DataStorage"/> types to accomplish this task.</para>
    /// </remarks>
    [Obsolete("Datalink feature is outdated and will be rewritten, see https://www.filehelpers.net/mustread/")]
    public sealed class GenericDataLink
    {
        #region "  Constructor  "

        /// <summary>Create a new instance of the class.</summary>
        /// <param name="provider1">The First <see cref="DataStorage"/>
        /// used to insert/extract records .</param>
        /// <param name="provider2">The Second <see cref="DataStorage"/>
        /// used to insert/extract records .</param>
        public GenericDataLink(DataStorage provider1, DataStorage provider2)
        {
            if (provider1 == null)
                throw new ArgumentException("provider1 can't be null", nameof(provider1));
            else
                mDataStorage1 = provider1;

            if (provider2 == null)
                throw new ArgumentException("provider2 can't be null", nameof(provider2));
            else
                mDataStorage2 = provider2;

            ValidateRecordTypes();
        }

        #endregion

        private DataStorage mDataStorage1;
        private DataStorage mDataStorage2;

        /// <summary>
        /// Extract the records from DataStorage1 and
        /// Insert them to the DataStorage2.
        /// </summary>
        /// <returns>The Copied records.</returns>
        public object[] CopyDataFrom1To2()
        {
            object[] res = DataStorage1.ExtractRecords();
            DataStorage2.InsertRecords(res);
            return res;
        }

        /// <summary>
        /// Extract the records from DataStorage2 and
        /// Insert them to the DataStorage1.
        /// </summary>
        /// <returns>The Copied records.</returns>
        public object[] CopyDataFrom2To1()
        {
            object[] res = DataStorage2.ExtractRecords();
            DataStorage1.InsertRecords(res);
            return res;
        }

        //private MethodInfo mConvert1to2 = null;
        //private MethodInfo mConvert2to1 = null;


        /// <summary>
        /// The first <see cref="DataStorage"/> of the
        /// <see cref="GenericDataLink"/>.
        /// </summary>
        public DataStorage DataStorage1
        {
            get { return mDataStorage1; }
        }

        /// <summary>
        /// The second <see cref="DataStorage"/> of the
        /// <see cref="GenericDataLink"/>.
        /// </summary>
        public DataStorage DataStorage2
        {
            get { return mDataStorage2; }
        }

        private void ValidateRecordTypes()
        {
            if (DataStorage1.RecordType == null)
                throw new BadUsageException("DataLink1 can't have a null RecordType.");

            if (DataStorage2.RecordType == null)
                throw new BadUsageException("DataLink2 can't have a null RecordType.");

            if (DataStorage1.RecordType != DataStorage2.RecordType) {
                    throw new BadUsageException("You can only use the same record type");
                }
       }

       
    }
}