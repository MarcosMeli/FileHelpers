using System.ComponentModel;
using System.Diagnostics;
//using System.ComponentModel.TypeConverter;


namespace FileHelpers.MasterDetail
{

    /// <summary>
    /// Records are read which one is the master and the following records
    /// are details, eg an order and the items ordered.
    /// </summary>
    [DebuggerDisplay("Master: {Master.ToString()} - Details: {Details.Length}")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class MasterDetails
        : MasterDetails<object, object>
    {
        /// <summary>
        /// records which have master and details
        /// </summary>
        /// <param name="master">Master record</param>
        /// <param name="details">Collection of detail records</param>
        public MasterDetails(object master, object[] details)
            :base(master, details)
        {
        }

        //public static implicit operator MasterDetails(MasterDetails<object, object> orig)
        //{
        //    MasterDetails res = new MasterDetails();
        //    res.mDetails = orig.Details;
        //    res.mMaster = orig.Master;

        //    return res;
        //}

    }

	/// <summary>
	/// <para>This class contains information of a Master record and its Details records.</para>
	/// <para>This class is used for the Read and Write operations of the <see cref="MasterDetailEngine"/>.</para>
	/// </summary>
	public class MasterDetails<M,D>
        where M : class
        where D : class
	{

		/// <summary>Create an empty instance.</summary>
		public MasterDetails()
		{
			mDetails = mEmpty.mDetails;
		}

		/// <summary>Create a new instance with the specified values.</summary>
		/// <param name="master">The master record.</param>
		/// <param name="details">The details record.</param>
		public MasterDetails(M master, D[] details)
		{
			mMaster = master;
			mDetails = details;
		}

        /// <summary>The canonical empty MasterDetail object.</summary>
		private static MasterDetails<M,D> mEmpty = new MasterDetails<M,D>(null, new D[] {});

		/// <summary>Returns a canonical empty MasterDetail object.</summary>
		public static MasterDetails<M,D> Empty
		{
			get { return mEmpty; }
		}

        /// <summary>
        /// Master record for this group
        /// </summary>
		protected M mMaster;

		/// <summary>The Master record.</summary>
		public M Master
		{
			get { return mMaster; }
			set { mMaster = value; }
		}

        /// <summary>
        /// An Array with the Detail records.
        /// </summary>
		protected D[] mDetails;

		/// <summary>An Array with the Detail records.</summary>
		public D[] Details
		{
			get { return mDetails; }
			set { mDetails = value; }
		}
	}
}

