using System;
using System.Text;

namespace FileHelpers.MasterDetail
{

    /// <summary>
    /// <para>This class contains information of a Master record an their Details records.</para>
    /// <para>This class is used for the Read and Write operations of the <see cref="MasterDetailEngine"/>.</para>
    /// </summary>
    public class MasterDetails
    {
        internal object mMaster;

        /// <summary>The Master record.</summary>
        public object Master
        {
            get { return mMaster; }
            set { mMaster = value; }
        }

        internal object[] mDetails;

        /// <summary>An Array with the Detail records.</summary>
        public object[] Details
        {
            get { return mDetails; }
            set { mDetails = value; }
        }
    }
}
