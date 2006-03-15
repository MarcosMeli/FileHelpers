namespace FileHelpers.MasterDetail
{
	/// <summary>
	/// <para>This class contains information of a Master record an their Details records.</para>
	/// <para>This class is used for the Read and Write operations of the <see cref="MasterDetailEngine"/>.</para>
	/// </summary>
	public class MasterDetails
	{

		private static MasterDetails mEmpty = new MasterDetails(null, new object[] {});

		/// <summary>Returns a canonical empty MasterDetail object.</summary>
		public static MasterDetails Empty
		{
			get { return mEmpty; }
		}

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
		/// <summary>Create an empty instance.</summary>
		public MasterDetails()
		{
			mDetails = mEmpty.mDetails;
		}

		/// <summary>Create a new instance with the specified values.</summary>
		/// <param name="master">The master record.</param>
		/// <param name="details">The details record.</param>
		public MasterDetails(object master, object[] details)
		{
			mMaster = master;
			mDetails = details;
		}
	}
}