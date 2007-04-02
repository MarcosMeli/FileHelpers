#undef GENERICS
//#define GENERICS
//#if NET_2_0

using System.ComponentModel;
using System.Diagnostics;
//using System.ComponentModel.TypeConverter;

namespace FileHelpers.MasterDetail
{
	/// <summary>
	/// <para>This class contains information of a Master record an their Details records.</para>
	/// <para>This class is used for the Read and Write operations of the <see cref="MasterDetailEngine"/>.</para>
	/// </summary>
#if ! GENERICS
#if NET_2_0
    [DebuggerDisplay("Master: {Master.ToString()} - Details: {Details.Length}")]
#endif
    [TypeConverter(typeof(ExpandableObjectConverter))]
	public class MasterDetails
	{

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
		
		#if NET_2_0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] 
		#endif
		private static MasterDetails mEmpty = new MasterDetails(null, new object[] {});

		/// <summary>Returns a canonical empty MasterDetail object.</summary>
		public static MasterDetails Empty
		{
			get { return mEmpty; }
		}

		#if NET_2_0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] 
		#endif
		internal object mMaster;

		/// <summary>The Master record.</summary>
		public object Master
		{
			get { return mMaster; }
			set { mMaster = value; }
		}

		#if NET_2_0
		[DebuggerBrowsable(DebuggerBrowsableState.Never)] 
		#endif
		internal object[] mDetails;

		/// <summary>An Array with the Detail records.</summary>
		[TypeConverter(typeof(ArrayConverter))]
		public object[] Details
		{
			get { return mDetails; }
			set { mDetails = value; }
		}

#else
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

		private static MasterDetails<M,D> mEmpty = new MasterDetails<M,D>(null, new D[] {});

		/// <summary>Returns a canonical empty MasterDetail object.</summary>
		public static MasterDetails<M,D> Empty
		{
			get { return mEmpty; }
		}

		internal M mMaster;

		/// <summary>The Master record.</summary>
		public M Master
		{
			get { return mMaster; }
			set { mMaster = value; }
		}

		internal D[] mDetails;

		/// <summary>An Array with the Detail records.</summary>
		public D[] Details
		{
			get { return mDetails; }
			set { mDetails = value; }
		}

#endif

	}
}

//#endif