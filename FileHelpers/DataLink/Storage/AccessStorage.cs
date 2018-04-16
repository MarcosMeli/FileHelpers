
using System;
using System.Data;

namespace FileHelpers.DataLink
{
#if !NETSTANDARD2_0
	using System.Data.OleDb;

	/// <summary>
	/// This is a base class that implements the <see cref="DataStorage"/> for
	/// Microsoft Access Files.
	/// </summary>
	public sealed class AccessStorage : DatabaseStorage
    {
	#region "  Constructors  "

        /// <summary>
        /// Creates a new AccessStorage.
        /// </summary>
        /// <param name="recordType">The Type of the Records</param>
        public AccessStorage(Type recordType)
            : this(recordType, string.Empty) {}

        /// <summary>
        /// Creates a new AccessStorage using the indicated file.
        /// </summary>
        /// <param name="recordType">The Type of the Records</param>
        /// <param name="accessFile">The MS Access file.</param>
        public AccessStorage(Type recordType, string accessFile)
            : base(recordType)
        {
            AccessFileName = accessFile;
            ConnectionString = DataBaseHelper.GetAccessConnection(AccessFileName, AccessFilePassword);
        }

	#endregion

	#region "  Create Connection and Command  "

        /// <summary>
        /// Create the connection object.
        /// </summary>
        /// <returns>An Abstract Connection Object.</returns>
        protected override sealed IDbConnection CreateConnection()
        {
            if (mAccessFile == null ||
                mAccessFile == string.Empty)
                throw new BadUsageException("The AccessFileName can't be null or empty.");

            return new OleDbConnection(ConnectionString);
        }

	#endregion

	#region "  AccessFileName  "

        private string mAccessFile = string.Empty;

        /// <summary>
        /// The file full path of the Microsoft Access File.
        /// </summary>
        public string AccessFileName
        {
            get { return mAccessFile; }
            set
            {
                mAccessFile = value;
                ConnectionString = DataBaseHelper.GetAccessConnection(AccessFileName, AccessFilePassword);
            }
        }

	#endregion

	#region "  AccessFilePassword  "

        private string mAccessPassword = string.Empty;

        /// <summary>
        /// The password for the access database.
        /// </summary>
        public string AccessFilePassword
        {
            get { return mAccessPassword; }
            set
            {
                mAccessPassword = value;
                ConnectionString = DataBaseHelper.GetAccessConnection(AccessFileName, AccessFilePassword);
            }
        }

	#endregion
    }
#endif
}

